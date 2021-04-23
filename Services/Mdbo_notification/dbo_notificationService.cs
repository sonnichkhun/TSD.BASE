using BASE.Common;
using BASE.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using BASE.Repositories;
using BASE.Entities;

namespace BASE.Services.Mdbo_notification
{
    public interface Idbo_notificationService :  IServiceScoped
    {
        Task<int>
    Count(dbo_notificationFilter dbo_notificationFilter);
    Task<List<dbo_notification>> List(dbo_notificationFilter dbo_notificationFilter);
        Task<dbo_notification> Get(long Id);
        Task<dbo_notification> Create(dbo_notification dbo_notification);
        Task<dbo_notification> Update(dbo_notification dbo_notification);
        Task<dbo_notification> Delete(dbo_notification dbo_notification);
        Task<List<dbo_notification>> BulkDelete(List<dbo_notification> dbo_notifications);
        Task<List<dbo_notification>> Import(List<dbo_notification> dbo_notifications);
        dbo_notificationFilter ToFilter(dbo_notificationFilter dbo_notificationFilter);
    }

    public class dbo_notificationService : BaseService, Idbo_notificationService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Idbo_notificationValidator dbo_notificationValidator;

        public dbo_notificationService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Idbo_notificationValidator dbo_notificationValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.dbo_notificationValidator = dbo_notificationValidator;
        }
        public async Task<int> Count(dbo_notificationFilter dbo_notificationFilter)
        {
            try
            {
                int result = await UOW.dbo_notificationRepository.Count(dbo_notificationFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_notificationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_notificationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<dbo_notification>> List(dbo_notificationFilter dbo_notificationFilter)
        {
            try
            {
                List<dbo_notification> dbo_notifications = await UOW.dbo_notificationRepository.List(dbo_notificationFilter);
                return dbo_notifications;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_notificationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_notificationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<dbo_notification> Get(long Id)
        {
            dbo_notification dbo_notification = await UOW.dbo_notificationRepository.Get(Id);
            if (dbo_notification == null)
                return null;
            return dbo_notification;
        }

        public async Task<dbo_notification> Create(dbo_notification dbo_notification)
        {
            if (!await dbo_notificationValidator.Create(dbo_notification))
                return dbo_notification;

            try
            {
                await UOW.Begin();
                await UOW.dbo_notificationRepository.Create(dbo_notification);
                await UOW.Commit();
                dbo_notification = await UOW.dbo_notificationRepository.Get(dbo_notification.Id);
                await Logging.CreateAuditLog(dbo_notification, new { }, nameof(dbo_notificationService));
                return dbo_notification;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_notificationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_notificationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<dbo_notification> Update(dbo_notification dbo_notification)
        {
            if (!await dbo_notificationValidator.Update(dbo_notification))
                return dbo_notification;
            try
            {
                var oldData = await UOW.dbo_notificationRepository.Get(dbo_notification.Id);

                await UOW.Begin();
                await UOW.dbo_notificationRepository.Update(dbo_notification);
                await UOW.Commit();

                dbo_notification = await UOW.dbo_notificationRepository.Get(dbo_notification.Id);
                await Logging.CreateAuditLog(dbo_notification, oldData, nameof(dbo_notificationService));
                return dbo_notification;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_notificationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_notificationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<dbo_notification> Delete(dbo_notification dbo_notification)
        {
            if (!await dbo_notificationValidator.Delete(dbo_notification))
                return dbo_notification;

            try
            {
                await UOW.Begin();
                await UOW.dbo_notificationRepository.Delete(dbo_notification);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, dbo_notification, nameof(dbo_notificationService));
                return dbo_notification;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_notificationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_notificationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<dbo_notification>> BulkDelete(List<dbo_notification> dbo_notifications)
        {
            if (!await dbo_notificationValidator.BulkDelete(dbo_notifications))
                return dbo_notifications;

            try
            {
                await UOW.Begin();
                await UOW.dbo_notificationRepository.BulkDelete(dbo_notifications);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, dbo_notifications, nameof(dbo_notificationService));
                return dbo_notifications;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_notificationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_notificationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<dbo_notification>> Import(List<dbo_notification> dbo_notifications)
        {
            if (!await dbo_notificationValidator.Import(dbo_notifications))
                return dbo_notifications;
            try
            {
                await UOW.Begin();
                await UOW.dbo_notificationRepository.BulkMerge(dbo_notifications);
                await UOW.Commit();

                await Logging.CreateAuditLog(dbo_notifications, new { }, nameof(dbo_notificationService));
                return dbo_notifications;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_notificationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_notificationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public dbo_notificationFilter ToFilter(dbo_notificationFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<dbo_notificationFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                dbo_notificationFilter subFilter = new dbo_notificationFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Title))
                        
                        
                        
                        
                        
                        
                        subFilter.Title = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Content))
                        
                        
                        
                        
                        
                        
                        subFilter.Content = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrganizationId))
                        subFilter.OrganizationId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.NotificationStatusId))
                        subFilter.NotificationStatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
