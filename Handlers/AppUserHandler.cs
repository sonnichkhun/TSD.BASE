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
    public class AppUserHandler : Handler
    {
        private string SyncKey => Name + ".Sync";

        public override string Name => nameof(AppUser);

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
            List<EventMessage<AppUser>> EventMessageReceived = JsonConvert.DeserializeObject<List<EventMessage<AppUser>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReceived);
            List<Guid> RowIds = EventMessageReceived.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<AppUser>> AppUserEventMessages = await ListEventMessage<AppUser>(context, SyncKey, RowIds);
            List<AppUser> AppUsers = AppUserEventMessages.Select(x => x.Content).ToList();
            try
            {
                List<AppUserDAO> AppUserDAOs = AppUsers.Select(au => new AppUserDAO
                {
                    Address = au.Address,
                    Avatar = au.Avatar,
                    CreatedAt = au.CreatedAt,
                    UpdatedAt = au.UpdatedAt,
                    DeletedAt = au.DeletedAt,
                    Used = au.Used,
                    Department = au.Department,
                    DisplayName = au.DisplayName,
                    Email = au.Email,
                    Id = au.Id,
                    OrganizationId = au.OrganizationId,
                    Phone = au.Phone,
                    RowId = au.RowId,
                    StatusId = au.StatusId,
                    Username = au.Username,
                    SexId = au.SexId,
                    Birthday = au.Birthday,
                }).ToList();
                await context.BulkMergeAsync(AppUserDAOs);
            }
            catch (Exception ex)
            {
                Log(ex, nameof(AppUserHandler));
            }
        }
    }
}
