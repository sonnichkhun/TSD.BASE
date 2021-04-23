﻿using BASE.Entities;
using BASE.Enums;
using BASE.Helpers;
using BASE.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace BASE.Handlers
{
    public interface IHandler
    {
        string Name { get; }
        IRabbitManager RabbitManager { get; set; }
        void QueueBind(IModel channel, string queue, string exchange);
        Task Handle(DataContext context, string routingKey, string content);
    }

    public abstract class Handler : IHandler
    {
        public abstract string Name { get; }
        public IRabbitManager RabbitManager { get; set; }

        public abstract Task Handle(DataContext context, string routingKey, string content);

        public abstract void QueueBind(IModel channel, string queue, string exchange);

        protected async Task<bool> SaveEventMessage<T>(DataContext DataContext, string RoutingKey, List<EventMessage<T>> EventMessages)
        {
            List<EventMessageDAO> EventMessageDAOs = new List<EventMessageDAO>();
            foreach (var EventMessage in EventMessages)
            {
                EventMessageDAO EventMessageDAO = new EventMessageDAO
                {
                    Id = EventMessage.Id,
                    Time = EventMessage.Time,
                    RoutingKey = RoutingKey,
                    RowId = EventMessage.RowId,
                    EntityName = typeof(T).Name,
                    Content = JsonConvert.SerializeObject(EventMessage.Content),
                };
                EventMessageDAOs.Add(EventMessageDAO);
            }
            await DataContext.EventMessage.BulkMergeAsync(EventMessageDAOs);
            return true;
        }
        protected async Task<List<EventMessage<T>>> ListEventMessage<T>(DataContext DataContext, string RoutingKey, List<Guid> RowIds)
        {
            string typeName = typeof(T).Name;
            List<LastestEventMessageDAO> LastestEventMessageDAOs = await DataContext.LastestEventMessage
                .Where(e => RowIds.Contains(e.RowId) && e.RoutingKey == RoutingKey && e.EntityName == typeName)
                .OrderBy(e => e.Id)
                .ToListAsync();
            List<EventMessage<T>> EventMessages = LastestEventMessageDAOs.Select(q => new EventMessage<T>()
            {
                Id = q.Id,
                Time = q.Time,
                RowId = q.RowId,
                EntityName = q.EntityName,
                Content = JsonConvert.DeserializeObject<T>(q.Content)
            }).ToList();
            return EventMessages;
        }

        protected void Log(Exception ex, string className, [CallerMemberName] string methodName = "")
        {
            SystemLog SystemLog = new SystemLog
            {
                AppUserId = null,
                AppUser = "RABBITMQ",
                ClassName = className,
                MethodName = methodName,
                ModuleName = StaticParams.ModuleName,
                Exception = ex.ToString(),
                Time = StaticParams.DateTimeNow,
            };
            RabbitManager.PublishSingle(new EventMessage<SystemLog>(SystemLog, SystemLog.RowId), RoutingKeyEnum.SystemLogSend);
        }
    }
}
