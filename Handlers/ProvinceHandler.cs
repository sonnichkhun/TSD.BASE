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
    public class ProvinceHandler : Handler
    {
        private string SyncKey => $"{Name}.Sync";
        private string ImportKey => $"AdministrativeUnit.Import";
        public override string Name => nameof(Province);

        public override void QueueBind(IModel channel, string queue, string exchange)
        {
            channel.QueueBind(queue, exchange, $"{Name}.*", null);
            channel.QueueBind(queue, exchange, $"{ImportKey}", null);
        }
        public override async Task Handle(DataContext context, string routingKey, string content)
        {
            if (routingKey == SyncKey)
                await Sync(context, content);
            if (routingKey == ImportKey)
                await Import(context, content);
        }
        public async Task Sync(DataContext context, string json)
        {
            List<EventMessage<Province>> EventMessageReceived = JsonConvert.DeserializeObject<List<EventMessage<Province>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReceived);
            List<Guid> RowIds = EventMessageReceived.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<Province>> ProvinceEventMessages = await ListEventMessage<Province>(context, SyncKey, RowIds);
            List<Province> Provinces = ProvinceEventMessages.Select(x => x.Content).ToList();
            try
            {
                List<ProvinceDAO> ProvinceDAOs = Provinces.Select(x => new ProvinceDAO
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    DeletedAt = x.DeletedAt,
                    Used = x.Used,
                    Priority = x.Priority,
                    RowId = x.RowId,
                    StatusId = x.StatusId,
                }).ToList();
                await context.BulkMergeAsync(ProvinceDAOs);
            }
            catch (Exception ex)
            {
                Log(ex, nameof(ProvinceHandler));
            }
        }

        private async Task Import(DataContext context, string json)
        {
            List<EventMessage<Province>> EventMessageReveiced = JsonConvert.DeserializeObject<List<EventMessage<Province>>>(json);
            List<Province> Provinces = EventMessageReveiced.Select(x => x.Content).ToList();
            List<District> Districts = Provinces.SelectMany(x => x.Districts).ToList();
            List<Ward> Wards = Districts.SelectMany(x => x.Wards).ToList();

            try
            {
                List<ProvinceDAO> ProvinceDAOs = Provinces.Select(x => new ProvinceDAO
                {
                    Code = x.Code,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    DeletedAt = x.DeletedAt,
                    Used = x.Used,
                    Id = x.Id,
                    Name = x.Name,
                    Priority = x.Priority,
                    RowId = x.RowId,
                    StatusId = x.StatusId,
                }).ToList();
                await context.BulkMergeAsync(ProvinceDAOs);

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
                Log(ex, nameof(ProvinceHandler));
            }
        }
    }
}
