using BASE.Common;
using BASE.Entities;
using BASE.Models;
using BASE.Helpers;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BASE.Handlers
{
    public class WardHandler : Handler
    {
        private string SyncKey => Name + ".Sync";

        public override string Name => nameof(Ward);

        public override void QueueBind(IModel channel, string queue, string exchange)
        {
            channel.QueueBind(queue, exchange, $"{Name}.*", null);
        }
        public override async Task Handle(DataContext context, string routingKey, string content)
        {
            if (routingKey == SyncKey)
                await Sync(context, content);
        }
        public async Task Sync(DataContext context, string json)
        {
            List<EventMessage<Ward>> EventMessageReceived = JsonConvert.DeserializeObject<List<EventMessage<Ward>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReceived);
            List<Guid> RowIds = EventMessageReceived.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<Ward>> WardEventMessages = await ListEventMessage<Ward>(context, SyncKey, RowIds);
            List<Ward> Wards = WardEventMessages.Select(x => x.Content).ToList();
            try
            {
                List<WardDAO> WardDAOs = Wards.Select(x => new WardDAO
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    DeletedAt = x.DeletedAt,
                    Used = x.Used,
                    DistrictId = x.DistrictId,
                    Priority = x.Priority,
                    RowId = x.RowId,
                    StatusId = x.StatusId,
                }).ToList();
                await context.BulkMergeAsync(WardDAOs);
            }
            catch (Exception ex)
            {
                Log(ex, nameof(WardHandler));
            }
        }
    }
}
