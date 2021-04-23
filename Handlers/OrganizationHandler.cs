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
using Microsoft.EntityFrameworkCore;

namespace BASE.Handlers
{
    public class OrganizationHandler : Handler
    {
        private string SyncKey => Name + ".Sync";

        public override string Name => nameof(Organization);

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
            List<EventMessage<Organization>> EventMessageReceived = JsonConvert.DeserializeObject<List<EventMessage<Organization>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReceived);
            List<Guid> RowIds = EventMessageReceived.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<Organization>> OrganizationEventMessages = await ListEventMessage<Organization>(context, SyncKey, RowIds);
            List<Organization> Organizations = new List<Organization>();
            foreach (var RowId in RowIds)
            {
                EventMessage<Organization> EventMessage = OrganizationEventMessages.Where(e => e.RowId == RowId).OrderByDescending(e => e.Time).FirstOrDefault();
                if (EventMessage != null)
                    Organizations.Add(EventMessage.Content);
            }

            var AppUsers = Organizations.Where(x => x.AppUsers != null).SelectMany(x => x.AppUsers).ToList();
            var AppUserIds = AppUsers.Select(x => x.Id).ToList();
            var AppUserDAOs = await context.AppUser.Where(x => AppUserIds.Contains(x.Id)).ToListAsync();
            try
            {
                List<OrganizationDAO> OrganizationDAOs = Organizations.Select(x => new OrganizationDAO
                {
                    Address = x.Address,
                    Code = x.Code,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    DeletedAt = x.DeletedAt,
                    Used = x.Used,
                    Email = x.Email,
                    Id = x.Id,
                    Level = x.Level,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    Path = x.Path,
                    Phone = x.Phone,
                    RowId = x.RowId,
                    StatusId = x.StatusId,
                }).ToList();
                await context.BulkMergeAsync(OrganizationDAOs);

                foreach (var AppUserDAO in AppUserDAOs)
                {
                    AppUserDAO.OrganizationId = AppUsers.Where(x => x.Id == AppUserDAO.Id).Select(x => x.OrganizationId).FirstOrDefault();
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log(ex, nameof(OrganizationHandler));
            }
        }
    }
}
