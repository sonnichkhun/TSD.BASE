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

namespace BASE.Services.Menum_notificationstatus
{
    public interface Ienum_notificationstatusService :  IServiceScoped
    {
        Task<int>
    Count(enum_notificationstatusFilter enum_notificationstatusFilter);
    Task<List<enum_notificationstatus>> List(enum_notificationstatusFilter enum_notificationstatusFilter);
        Task<enum_notificationstatus> Get(long Id);
        Task<enum_notificationstatus> Create(enum_notificationstatus enum_notificationstatus);
        Task<enum_notificationstatus> Update(enum_notificationstatus enum_notificationstatus);
        Task<enum_notificationstatus> Delete(enum_notificationstatus enum_notificationstatus);
        Task<List<enum_notificationstatus>> BulkDelete(List<enum_notificationstatus> enum_notificationstatuses);
        Task<List<enum_notificationstatus>> Import(List<enum_notificationstatus> enum_notificationstatuses);
        enum_notificationstatusFilter ToFilter(enum_notificationstatusFilter enum_notificationstatusFilter);
    }

    public class enum_notificationstatusService : BaseService, Ienum_notificationstatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Ienum_notificationstatusValidator enum_notificationstatusValidator;

        public enum_notificationstatusService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Ienum_notificationstatusValidator enum_notificationstatusValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.enum_notificationstatusValidator = enum_notificationstatusValidator;
        }
        public async Task<int> Count(enum_notificationstatusFilter enum_notificationstatusFilter)
        {
            try
            {
                int result = await UOW.enum_notificationstatusRepository.Count(enum_notificationstatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_notificationstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_notificationstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_notificationstatus>> List(enum_notificationstatusFilter enum_notificationstatusFilter)
        {
            try
            {
                List<enum_notificationstatus> enum_notificationstatuss = await UOW.enum_notificationstatusRepository.List(enum_notificationstatusFilter);
                return enum_notificationstatuss;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_notificationstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_notificationstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<enum_notificationstatus> Get(long Id)
        {
            enum_notificationstatus enum_notificationstatus = await UOW.enum_notificationstatusRepository.Get(Id);
            if (enum_notificationstatus == null)
                return null;
            return enum_notificationstatus;
        }

        public async Task<enum_notificationstatus> Create(enum_notificationstatus enum_notificationstatus)
        {
            if (!await enum_notificationstatusValidator.Create(enum_notificationstatus))
                return enum_notificationstatus;

            try
            {
                await UOW.Begin();
                await UOW.enum_notificationstatusRepository.Create(enum_notificationstatus);
                await UOW.Commit();
                enum_notificationstatus = await UOW.enum_notificationstatusRepository.Get(enum_notificationstatus.Id);
                await Logging.CreateAuditLog(enum_notificationstatus, new { }, nameof(enum_notificationstatusService));
                return enum_notificationstatus;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_notificationstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_notificationstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_notificationstatus> Update(enum_notificationstatus enum_notificationstatus)
        {
            if (!await enum_notificationstatusValidator.Update(enum_notificationstatus))
                return enum_notificationstatus;
            try
            {
                var oldData = await UOW.enum_notificationstatusRepository.Get(enum_notificationstatus.Id);

                await UOW.Begin();
                await UOW.enum_notificationstatusRepository.Update(enum_notificationstatus);
                await UOW.Commit();

                enum_notificationstatus = await UOW.enum_notificationstatusRepository.Get(enum_notificationstatus.Id);
                await Logging.CreateAuditLog(enum_notificationstatus, oldData, nameof(enum_notificationstatusService));
                return enum_notificationstatus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_notificationstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_notificationstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_notificationstatus> Delete(enum_notificationstatus enum_notificationstatus)
        {
            if (!await enum_notificationstatusValidator.Delete(enum_notificationstatus))
                return enum_notificationstatus;

            try
            {
                await UOW.Begin();
                await UOW.enum_notificationstatusRepository.Delete(enum_notificationstatus);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_notificationstatus, nameof(enum_notificationstatusService));
                return enum_notificationstatus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_notificationstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_notificationstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_notificationstatus>> BulkDelete(List<enum_notificationstatus> enum_notificationstatuses)
        {
            if (!await enum_notificationstatusValidator.BulkDelete(enum_notificationstatuses))
                return enum_notificationstatuses;

            try
            {
                await UOW.Begin();
                await UOW.enum_notificationstatusRepository.BulkDelete(enum_notificationstatuses);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_notificationstatuses, nameof(enum_notificationstatusService));
                return enum_notificationstatuses;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_notificationstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_notificationstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_notificationstatus>> Import(List<enum_notificationstatus> enum_notificationstatuses)
        {
            if (!await enum_notificationstatusValidator.Import(enum_notificationstatuses))
                return enum_notificationstatuses;
            try
            {
                await UOW.Begin();
                await UOW.enum_notificationstatusRepository.BulkMerge(enum_notificationstatuses);
                await UOW.Commit();

                await Logging.CreateAuditLog(enum_notificationstatuses, new { }, nameof(enum_notificationstatusService));
                return enum_notificationstatuses;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_notificationstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_notificationstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public enum_notificationstatusFilter ToFilter(enum_notificationstatusFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<enum_notificationstatusFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                enum_notificationstatusFilter subFilter = new enum_notificationstatusFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
