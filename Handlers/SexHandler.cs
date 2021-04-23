﻿using BASE.Common;
using BASE.Entities;
using BASE.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BASE.Handlers
{
    public class SexHandler : Handler
    {
        private string SyncKey => Name + ".Sync";

        public override string Name => nameof(Sex);

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
            List<EventMessage<Sex>> EventMessageReceived = JsonConvert.DeserializeObject<List<EventMessage<Sex>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReceived);
            List<Guid> RowIds = EventMessageReceived.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<Sex>> SexEventMessages = await ListEventMessage<Sex>(context, SyncKey, RowIds);
            List<Sex> Sexs = SexEventMessages.Select(x => x.Content).ToList();
            try
            {
                List<SexDAO> SexDAOs = Sexs.Select(x => new SexDAO
                {
                    Code = x.Code,
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
                await context.BulkMergeAsync(SexDAOs);
            }
            catch (Exception ex)
            {
                Log(ex, nameof(SexHandler));
            }
        }
    }
}
