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

namespace BASE.Services.Menum_emailstatus
{
    public interface Ienum_emailstatusService :  IServiceScoped
    {
        Task<int>
    Count(enum_emailstatusFilter enum_emailstatusFilter);
    Task<List<enum_emailstatus>> List(enum_emailstatusFilter enum_emailstatusFilter);
        Task<enum_emailstatus> Get(long Id);
        Task<enum_emailstatus> Create(enum_emailstatus enum_emailstatus);
        Task<enum_emailstatus> Update(enum_emailstatus enum_emailstatus);
        Task<enum_emailstatus> Delete(enum_emailstatus enum_emailstatus);
        Task<List<enum_emailstatus>> BulkDelete(List<enum_emailstatus> enum_emailstatuses);
        Task<List<enum_emailstatus>> Import(List<enum_emailstatus> enum_emailstatuses);
        enum_emailstatusFilter ToFilter(enum_emailstatusFilter enum_emailstatusFilter);
    }

    public class enum_emailstatusService : BaseService, Ienum_emailstatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Ienum_emailstatusValidator enum_emailstatusValidator;

        public enum_emailstatusService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Ienum_emailstatusValidator enum_emailstatusValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.enum_emailstatusValidator = enum_emailstatusValidator;
        }
        public async Task<int> Count(enum_emailstatusFilter enum_emailstatusFilter)
        {
            try
            {
                int result = await UOW.enum_emailstatusRepository.Count(enum_emailstatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_emailstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_emailstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_emailstatus>> List(enum_emailstatusFilter enum_emailstatusFilter)
        {
            try
            {
                List<enum_emailstatus> enum_emailstatuss = await UOW.enum_emailstatusRepository.List(enum_emailstatusFilter);
                return enum_emailstatuss;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_emailstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_emailstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<enum_emailstatus> Get(long Id)
        {
            enum_emailstatus enum_emailstatus = await UOW.enum_emailstatusRepository.Get(Id);
            if (enum_emailstatus == null)
                return null;
            return enum_emailstatus;
        }

        public async Task<enum_emailstatus> Create(enum_emailstatus enum_emailstatus)
        {
            if (!await enum_emailstatusValidator.Create(enum_emailstatus))
                return enum_emailstatus;

            try
            {
                await UOW.Begin();
                await UOW.enum_emailstatusRepository.Create(enum_emailstatus);
                await UOW.Commit();
                enum_emailstatus = await UOW.enum_emailstatusRepository.Get(enum_emailstatus.Id);
                await Logging.CreateAuditLog(enum_emailstatus, new { }, nameof(enum_emailstatusService));
                return enum_emailstatus;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_emailstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_emailstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_emailstatus> Update(enum_emailstatus enum_emailstatus)
        {
            if (!await enum_emailstatusValidator.Update(enum_emailstatus))
                return enum_emailstatus;
            try
            {
                var oldData = await UOW.enum_emailstatusRepository.Get(enum_emailstatus.Id);

                await UOW.Begin();
                await UOW.enum_emailstatusRepository.Update(enum_emailstatus);
                await UOW.Commit();

                enum_emailstatus = await UOW.enum_emailstatusRepository.Get(enum_emailstatus.Id);
                await Logging.CreateAuditLog(enum_emailstatus, oldData, nameof(enum_emailstatusService));
                return enum_emailstatus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_emailstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_emailstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_emailstatus> Delete(enum_emailstatus enum_emailstatus)
        {
            if (!await enum_emailstatusValidator.Delete(enum_emailstatus))
                return enum_emailstatus;

            try
            {
                await UOW.Begin();
                await UOW.enum_emailstatusRepository.Delete(enum_emailstatus);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_emailstatus, nameof(enum_emailstatusService));
                return enum_emailstatus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_emailstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_emailstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_emailstatus>> BulkDelete(List<enum_emailstatus> enum_emailstatuses)
        {
            if (!await enum_emailstatusValidator.BulkDelete(enum_emailstatuses))
                return enum_emailstatuses;

            try
            {
                await UOW.Begin();
                await UOW.enum_emailstatusRepository.BulkDelete(enum_emailstatuses);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_emailstatuses, nameof(enum_emailstatusService));
                return enum_emailstatuses;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_emailstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_emailstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_emailstatus>> Import(List<enum_emailstatus> enum_emailstatuses)
        {
            if (!await enum_emailstatusValidator.Import(enum_emailstatuses))
                return enum_emailstatuses;
            try
            {
                await UOW.Begin();
                await UOW.enum_emailstatusRepository.BulkMerge(enum_emailstatuses);
                await UOW.Commit();

                await Logging.CreateAuditLog(enum_emailstatuses, new { }, nameof(enum_emailstatusService));
                return enum_emailstatuses;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_emailstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_emailstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public enum_emailstatusFilter ToFilter(enum_emailstatusFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<enum_emailstatusFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                enum_emailstatusFilter subFilter = new enum_emailstatusFilter();
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
