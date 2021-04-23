﻿using BASE.Common;
using BASE.Entities;
using BASE.Models;
using BASE.Helpers;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BASE.Enums;

namespace BASE.Handlers
{
    public class UnitOfMeasureHandler : Handler
    {
        private string SyncKey => Name + ".Sync";

        public override string Name => nameof(UnitOfMeasure);

        public override void QueueBind(IModel channel, string queue, string exchange)
        {
            channel.QueueBind(queue, exchange, $"{Name}.*", null);
        }
        public override async Task Handle(DataContext context, string routingKey, string content)
        {
            if (routingKey == SyncKey)
                await Sync(context, content);
        }

        private async Task Sync(DataContext context, string json)
        {
            List<EventMessage<UnitOfMeasure>> EventMessageReceived = JsonConvert.DeserializeObject<List<EventMessage<UnitOfMeasure>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReceived);
            List<Guid> RowIds = EventMessageReceived.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<UnitOfMeasure>> UnitOfMeasureEventMessages = await ListEventMessage<UnitOfMeasure>(context, SyncKey, RowIds);
            List<UnitOfMeasure> UnitOfMeasures = UnitOfMeasureEventMessages.Select(x => x.Content).ToList();
            try
            {
                List<UnitOfMeasureDAO> UnitOfMeasureDAOs = UnitOfMeasures
                    .Select(x => new UnitOfMeasureDAO
                    {
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt,
                        DeletedAt = x.DeletedAt,                     
                        Id = x.Id,
                        Code = x.Code,
                        Name = x.Name,
                        Description = x.Description,
                        Used = x.Used,
                        RowId = x.RowId,
                        StatusId = x.StatusId                      
                    }).ToList();
                await context.BulkMergeAsync(UnitOfMeasureDAOs);
            }
            catch (Exception ex)
            {
                Log(ex, nameof(UnitOfMeasureHandler));
            }
        }
    }
}
