using BASE.Common;
using BASE.Entities;
using BASE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Helpers;

namespace BASE.Repositories
{
    public interface Idbo_notificationRepository
    {
        Task<int>
    Count(dbo_notificationFilter dbo_notificationFilter);
    Task<List<BASE.Entities.dbo_notification>> List(dbo_notificationFilter dbo_notificationFilter);
        Task<BASE.Entities.dbo_notification> Get(long Id);
        Task<bool> Create(BASE.Entities.dbo_notification dbo_notification);
        Task<bool> Update(BASE.Entities.dbo_notification dbo_notification);
        Task<bool> Delete(BASE.Entities.dbo_notification dbo_notification);
        Task<bool> BulkMerge(List<BASE.Entities.dbo_notification> dbo_notifications);
        Task<bool> BulkDelete(List<BASE.Entities.dbo_notification> dbo_notifications);
                    }
                    public class dbo_notificationRepository : Idbo_notificationRepository
                    {
                    private DataContext DataContext;
                    public dbo_notificationRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.dbo_notification>
                        DynamicFilter(IQueryable<BASE.Models.dbo_notification>
                            query, dbo_notificationFilter filter)
                            {
                            if (filter == null)
                            return query.Where(q => false);
                            if (filter.Id != null)
                            query = query.Where(q => q.Id, filter.Id);
                            if (filter.Title != null)
                            query = query.Where(q => q.Title, filter.Title);
                            if (filter.Content != null)
                            query = query.Where(q => q.Content, filter.Content);
                            if (filter.OrganizationId != null)
                            query = query.Where(q => q.OrganizationId.HasValue).Where(q => q.OrganizationId, filter.OrganizationId);
                            if (filter.NotificationStatusId != null)
                            query = query.Where(q => q.NotificationStatusId, filter.NotificationStatusId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.dbo_notification>
                                OrFilter(IQueryable<BASE.Models.dbo_notification>
                                    query, dbo_notificationFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.dbo_notification>
                                        initQuery = query.Where(q => false);
                                        foreach (dbo_notificationFilter dbo_notificationFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.dbo_notification>
                                            queryable = query;
                                            if (dbo_notificationFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, dbo_notificationFilter.Id);
                                            if (dbo_notificationFilter.Title != null)
                                            queryable = queryable.Where(q => q.Title, dbo_notificationFilter.Title);
                                            if (dbo_notificationFilter.Content != null)
                                            queryable = queryable.Where(q => q.Content, dbo_notificationFilter.Content);
                                            if (dbo_notificationFilter.OrganizationId != null)
                                            queryable = queryable.Where(q => q.OrganizationId.HasValue).Where(q => q.OrganizationId, dbo_notificationFilter.OrganizationId);
                                            if (dbo_notificationFilter.NotificationStatusId != null)
                                            queryable = queryable.Where(q => q.NotificationStatusId, dbo_notificationFilter.NotificationStatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.dbo_notification>
                                                DynamicOrder(IQueryable<BASE.Models.dbo_notification>
                                                    query, dbo_notificationFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case dbo_notificationOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case dbo_notificationOrder.Title:
                                                    query = query.OrderBy(q => q.Title);
                                                    break;
                                                    case dbo_notificationOrder.Content:
                                                    query = query.OrderBy(q => q.Content);
                                                    break;
                                                    case dbo_notificationOrder.Organization:
                                                    query = query.OrderBy(q => q.OrganizationId);
                                                    break;
                                                    case dbo_notificationOrder.NotificationStatus:
                                                    query = query.OrderBy(q => q.NotificationStatusId);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case dbo_notificationOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case dbo_notificationOrder.Title:
                                                    query = query.OrderByDescending(q => q.Title);
                                                    break;
                                                    case dbo_notificationOrder.Content:
                                                    query = query.OrderByDescending(q => q.Content);
                                                    break;
                                                    case dbo_notificationOrder.Organization:
                                                    query = query.OrderByDescending(q => q.OrganizationId);
                                                    break;
                                                    case dbo_notificationOrder.NotificationStatus:
                                                    query = query.OrderByDescending(q => q.NotificationStatusId);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.dbo_notification>> DynamicSelect(IQueryable<BASE.Models.dbo_notification> query, dbo_notificationFilter filter)
        {
            List<dbo_notification> dbo_notifications = await query.Select(q => new dbo_notification()
            {
                Id = filter.Selects.Contains(dbo_notificationSelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(dbo_notificationSelect.Title) ? q.Title : default(string),
                Content = filter.Selects.Contains(dbo_notificationSelect.Content) ? q.Content : default(string),
                OrganizationId = filter.Selects.Contains(dbo_notificationSelect.Organization) ? q.OrganizationId : default(long?),
                NotificationStatusId = filter.Selects.Contains(dbo_notificationSelect.NotificationStatus) ? q.NotificationStatusId : default(long),
                NotificationStatus = filter.Selects.Contains(dbo_notificationSelect.NotificationStatus) && q.NotificationStatus != null ? new enum_notificationstatus
                {
                    Id = q.NotificationStatus.Id,
                    Code = q.NotificationStatus.Code,
                    Name = q.NotificationStatus.Name,
                } : null,
                Organization = filter.Selects.Contains(dbo_notificationSelect.Organization) && q.Organization != null ? new mdm_organization
                {
                    Id = q.Organization.Id,
                    Code = q.Organization.Code,
                    Name = q.Organization.Name,
                    ParentId = q.Organization.ParentId,
                    Path = q.Organization.Path,
                    Level = q.Organization.Level,
                    StatusId = q.Organization.StatusId,
                    Phone = q.Organization.Phone,
                    Email = q.Organization.Email,
                    Address = q.Organization.Address,
                    Used = q.Organization.Used,
                } : null,
            }).ToListAsync();
            return dbo_notifications;
        }

        public async Task<int> Count(dbo_notificationFilter filter)
        {
            IQueryable<BASE.Models.dbo_notification> dbo_notifications = DataContext.dbo_notification.AsNoTracking();
            dbo_notifications = DynamicFilter(dbo_notifications, filter);
            return await dbo_notifications.CountAsync();
        }

        public async Task<List<dbo_notification>> List(dbo_notificationFilter filter)
        {
            if (filter == null) return new List<dbo_notification>();
            IQueryable<BASE.Models.dbo_notification> dbo_notifications = DataContext.dbo_notification.AsNoTracking();
            dbo_notifications = DynamicFilter(dbo_notifications, filter);
            dbo_notifications = DynamicOrder(dbo_notifications, filter);
            List<dbo_notification> dbo_notifications = await DynamicSelect(dbo_notifications, filter);
            return dbo_notifications;
        }

        public async Task<dbo_notification> Get(long Id)
        {
            dbo_notification dbo_notification = await DataContext.dbo_notification.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new dbo_notification()
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                OrganizationId = x.OrganizationId,
                NotificationStatusId = x.NotificationStatusId,
                NotificationStatus = x.NotificationStatus == null ? null : new enum_notificationstatus
                {
                    Id = x.NotificationStatus.Id,
                    Code = x.NotificationStatus.Code,
                    Name = x.NotificationStatus.Name,
                },
                Organization = x.Organization == null ? null : new mdm_organization
                {
                    Id = x.Organization.Id,
                    Code = x.Organization.Code,
                    Name = x.Organization.Name,
                    ParentId = x.Organization.ParentId,
                    Path = x.Organization.Path,
                    Level = x.Organization.Level,
                    StatusId = x.Organization.StatusId,
                    Phone = x.Organization.Phone,
                    Email = x.Organization.Email,
                    Address = x.Organization.Address,
                    Used = x.Organization.Used,
                },
            }).FirstOrDefaultAsync();

            if (dbo_notification == null)
                return null;

            return dbo_notification;
        }
        public async Task<bool> Create(dbo_notification dbo_notification)
        {
            dbo_notification dbo_notification = new dbo_notification();
            dbo_notification.Id = dbo_notification.Id;
            dbo_notification.Title = dbo_notification.Title;
            dbo_notification.Content = dbo_notification.Content;
            dbo_notification.OrganizationId = dbo_notification.OrganizationId;
            dbo_notification.NotificationStatusId = dbo_notification.NotificationStatusId;
            DataContext.dbo_notification.Add(dbo_notification);
            await DataContext.SaveChangesAsync();
            dbo_notification.Id = dbo_notification.Id;
            await SaveReference(dbo_notification);
            return true;
        }

        public async Task<bool> Update(dbo_notification dbo_notification)
        {
            dbo_notification dbo_notification = DataContext.dbo_notification.Where(x => x.Id == dbo_notification.Id).FirstOrDefault();
            if (dbo_notification == null)
                return false;
            dbo_notification.Id = dbo_notification.Id;
            dbo_notification.Title = dbo_notification.Title;
            dbo_notification.Content = dbo_notification.Content;
            dbo_notification.OrganizationId = dbo_notification.OrganizationId;
            dbo_notification.NotificationStatusId = dbo_notification.NotificationStatusId;
            await DataContext.SaveChangesAsync();
            await SaveReference(dbo_notification);
            return true;
        }

        public async Task<bool> Delete(dbo_notification dbo_notification)
        {
            await DataContext.dbo_notification.Where(x => x.Id == dbo_notification.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<dbo_notification> dbo_notifications)
        {
            List<dbo_notification> dbo_notifications = new List<dbo_notification>();
            foreach (dbo_notification dbo_notification in dbo_notifications)
            {
                dbo_notification dbo_notification = new dbo_notification();
                dbo_notification.Id = dbo_notification.Id;
                dbo_notification.Title = dbo_notification.Title;
                dbo_notification.Content = dbo_notification.Content;
                dbo_notification.OrganizationId = dbo_notification.OrganizationId;
                dbo_notification.NotificationStatusId = dbo_notification.NotificationStatusId;
                dbo_notifications.Add(dbo_notification);
            }
            await DataContext.BulkMergeAsync(dbo_notifications);
            return true;
        }

        public async Task<bool> BulkDelete(List<dbo_notification> dbo_notifications)
        {
            List<long> Ids = dbo_notifications.Select(x => x.Id).ToList();
            await DataContext.dbo_notification
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(dbo_notification dbo_notification)
        {
        }

    }
}
