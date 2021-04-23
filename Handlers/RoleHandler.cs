using BASE.Common;
using BASE.Entities;
using BASE.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BASE.Handlers
{
    public class RoleHandler : Handler
    {
        private string UsedKey => $"BASE.{Name}.Used";
        public override string Name => nameof(Role);

        public override void QueueBind(IModel channel, string queue, string exchange)
        {
            channel.QueueBind(queue, exchange, $"BASE.{Name}.*", null);
        }
        public override async Task Handle(DataContext context, string routingKey, string content)
        {
            if (routingKey == UsedKey)
                await Used(context, content);
        }

        private async Task Used(DataContext context, string json)
        {
            List<EventMessage<Role>> EventMessageReviced = JsonConvert.DeserializeObject<List<EventMessage<Role>>>(json);
            List<long> RoleIds = EventMessageReviced.Select(em => em.Content.Id).ToList();
            await context.Role.Where(a => RoleIds.Contains(a.Id)).UpdateFromQueryAsync(a => new RoleDAO { Used = true });
        }
    }
}
