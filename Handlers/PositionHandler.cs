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
    public class PositionHandler : Handler
    {
        private string SyncKey => Name + ".Sync";

        public override string Name => nameof(Position);

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
            List<EventMessage<Position>> EventMessageReceived = JsonConvert.DeserializeObject<List<EventMessage<Position>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReceived);
            List<Guid> RowIds = EventMessageReceived.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<Position>> PositionEventMessages = await ListEventMessage<Position>(context, SyncKey, RowIds);
            List<Position> Positions = PositionEventMessages.Select(x => x.Content).ToList();

            try
            {
                List<PositionDAO> PositionDAOs = Positions.Select(x => new PositionDAO
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    DeletedAt = x.DeletedAt,
                    Used = x.Used,
                    RowId = x.RowId,
                    StatusId = x.StatusId,
                }).ToList();
                await context.BulkMergeAsync(PositionDAOs);
            }
            catch (Exception ex)
            {
                Log(ex, nameof(PositionHandler));
            }
        }
    }
}
