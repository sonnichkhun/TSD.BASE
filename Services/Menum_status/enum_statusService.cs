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

namespace BASE.Services.Menum_status
{
    public interface Ienum_statusService :  IServiceScoped
    {
        Task<int>
    Count(enum_statusFilter enum_statusFilter);
    Task<List<enum_status>> List(enum_statusFilter enum_statusFilter);
        Task<enum_status> Get(long Id);
        Task<enum_status> Create(enum_status enum_status);
        Task<enum_status> Update(enum_status enum_status);
        Task<enum_status> Delete(enum_status enum_status);
        Task<List<enum_status>> BulkDelete(List<enum_status> enum_statuses);
        Task<List<enum_status>> Import(List<enum_status> enum_statuses);
        enum_statusFilter ToFilter(enum_statusFilter enum_statusFilter);
    }

    public class enum_statusService : BaseService, Ienum_statusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Ienum_statusValidator enum_statusValidator;

        public enum_statusService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Ienum_statusValidator enum_statusValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.enum_statusValidator = enum_statusValidator;
        }
        public async Task<int> Count(enum_statusFilter enum_statusFilter)
        {
            try
            {
                int result = await UOW.enum_statusRepository.Count(enum_statusFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_statusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_statusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_status>> List(enum_statusFilter enum_statusFilter)
        {
            try
            {
                List<enum_status> enum_statuss = await UOW.enum_statusRepository.List(enum_statusFilter);
                return enum_statuss;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_statusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_statusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<enum_status> Get(long Id)
        {
            enum_status enum_status = await UOW.enum_statusRepository.Get(Id);
            if (enum_status == null)
                return null;
            return enum_status;
        }

        public async Task<enum_status> Create(enum_status enum_status)
        {
            if (!await enum_statusValidator.Create(enum_status))
                return enum_status;

            try
            {
                await UOW.Begin();
                await UOW.enum_statusRepository.Create(enum_status);
                await UOW.Commit();
                enum_status = await UOW.enum_statusRepository.Get(enum_status.Id);
                await Logging.CreateAuditLog(enum_status, new { }, nameof(enum_statusService));
                return enum_status;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_statusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_statusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_status> Update(enum_status enum_status)
        {
            if (!await enum_statusValidator.Update(enum_status))
                return enum_status;
            try
            {
                var oldData = await UOW.enum_statusRepository.Get(enum_status.Id);

                await UOW.Begin();
                await UOW.enum_statusRepository.Update(enum_status);
                await UOW.Commit();

                enum_status = await UOW.enum_statusRepository.Get(enum_status.Id);
                await Logging.CreateAuditLog(enum_status, oldData, nameof(enum_statusService));
                return enum_status;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_statusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_statusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_status> Delete(enum_status enum_status)
        {
            if (!await enum_statusValidator.Delete(enum_status))
                return enum_status;

            try
            {
                await UOW.Begin();
                await UOW.enum_statusRepository.Delete(enum_status);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_status, nameof(enum_statusService));
                return enum_status;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_statusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_statusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_status>> BulkDelete(List<enum_status> enum_statuses)
        {
            if (!await enum_statusValidator.BulkDelete(enum_statuses))
                return enum_statuses;

            try
            {
                await UOW.Begin();
                await UOW.enum_statusRepository.BulkDelete(enum_statuses);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_statuses, nameof(enum_statusService));
                return enum_statuses;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_statusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_statusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_status>> Import(List<enum_status> enum_statuses)
        {
            if (!await enum_statusValidator.Import(enum_statuses))
                return enum_statuses;
            try
            {
                await UOW.Begin();
                await UOW.enum_statusRepository.BulkMerge(enum_statuses);
                await UOW.Commit();

                await Logging.CreateAuditLog(enum_statuses, new { }, nameof(enum_statusService));
                return enum_statuses;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_statusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_statusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public enum_statusFilter ToFilter(enum_statusFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<enum_statusFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                enum_statusFilter subFilter = new enum_statusFilter();
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
