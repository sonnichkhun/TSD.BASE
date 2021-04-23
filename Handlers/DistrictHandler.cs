using BASE.Common;
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

namespace BASE.Handlers
{
    public class DistrictHandler : Handler
    {
        private string SyncKey => Name + ".Sync";

        public override string Name => nameof(District);

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
            List<EventMessage<District>> EventMessageReceived = JsonConvert.DeserializeObject<List<EventMessage<District>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReceived);
            List<Guid> RowIds = EventMessageReceived.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<District>> DistrictEventMessages = await ListEventMessage<District>(context, SyncKey, RowIds);
            List<District> Districts = DistrictEventMessages.Select(x => x.Content).ToList();
            try
            {
                List<DistrictDAO> DistrictDAOs = Districts.Select(x => new DistrictDAO
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    DeletedAt = x.DeletedAt,
                    Used = x.Used,
                    ProvinceId = x.ProvinceId,
                    Priority = x.Priority,
                    RowId = x.RowId,
                    StatusId = x.StatusId,
                }).ToList();
                await context.BulkMergeAsync(DistrictDAOs);
            }
            catch (Exception ex)
            {
                Log(ex, nameof(DistrictHandler));
            }
        }
    }
}
