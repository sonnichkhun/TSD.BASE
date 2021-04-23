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

namespace BASE.Services.Menum_paymentstatus
{
    public interface Ienum_paymentstatusService :  IServiceScoped
    {
        Task<int>
    Count(enum_paymentstatusFilter enum_paymentstatusFilter);
    Task<List<enum_paymentstatus>> List(enum_paymentstatusFilter enum_paymentstatusFilter);
        Task<enum_paymentstatus> Get(long Id);
        Task<enum_paymentstatus> Create(enum_paymentstatus enum_paymentstatus);
        Task<enum_paymentstatus> Update(enum_paymentstatus enum_paymentstatus);
        Task<enum_paymentstatus> Delete(enum_paymentstatus enum_paymentstatus);
        Task<List<enum_paymentstatus>> BulkDelete(List<enum_paymentstatus> enum_paymentstatuses);
        Task<List<enum_paymentstatus>> Import(List<enum_paymentstatus> enum_paymentstatuses);
        enum_paymentstatusFilter ToFilter(enum_paymentstatusFilter enum_paymentstatusFilter);
    }

    public class enum_paymentstatusService : BaseService, Ienum_paymentstatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Ienum_paymentstatusValidator enum_paymentstatusValidator;

        public enum_paymentstatusService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Ienum_paymentstatusValidator enum_paymentstatusValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.enum_paymentstatusValidator = enum_paymentstatusValidator;
        }
        public async Task<int> Count(enum_paymentstatusFilter enum_paymentstatusFilter)
        {
            try
            {
                int result = await UOW.enum_paymentstatusRepository.Count(enum_paymentstatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_paymentstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_paymentstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_paymentstatus>> List(enum_paymentstatusFilter enum_paymentstatusFilter)
        {
            try
            {
                List<enum_paymentstatus> enum_paymentstatuss = await UOW.enum_paymentstatusRepository.List(enum_paymentstatusFilter);
                return enum_paymentstatuss;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_paymentstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_paymentstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<enum_paymentstatus> Get(long Id)
        {
            enum_paymentstatus enum_paymentstatus = await UOW.enum_paymentstatusRepository.Get(Id);
            if (enum_paymentstatus == null)
                return null;
            return enum_paymentstatus;
        }

        public async Task<enum_paymentstatus> Create(enum_paymentstatus enum_paymentstatus)
        {
            if (!await enum_paymentstatusValidator.Create(enum_paymentstatus))
                return enum_paymentstatus;

            try
            {
                await UOW.Begin();
                await UOW.enum_paymentstatusRepository.Create(enum_paymentstatus);
                await UOW.Commit();
                enum_paymentstatus = await UOW.enum_paymentstatusRepository.Get(enum_paymentstatus.Id);
                await Logging.CreateAuditLog(enum_paymentstatus, new { }, nameof(enum_paymentstatusService));
                return enum_paymentstatus;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_paymentstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_paymentstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_paymentstatus> Update(enum_paymentstatus enum_paymentstatus)
        {
            if (!await enum_paymentstatusValidator.Update(enum_paymentstatus))
                return enum_paymentstatus;
            try
            {
                var oldData = await UOW.enum_paymentstatusRepository.Get(enum_paymentstatus.Id);

                await UOW.Begin();
                await UOW.enum_paymentstatusRepository.Update(enum_paymentstatus);
                await UOW.Commit();

                enum_paymentstatus = await UOW.enum_paymentstatusRepository.Get(enum_paymentstatus.Id);
                await Logging.CreateAuditLog(enum_paymentstatus, oldData, nameof(enum_paymentstatusService));
                return enum_paymentstatus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_paymentstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_paymentstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_paymentstatus> Delete(enum_paymentstatus enum_paymentstatus)
        {
            if (!await enum_paymentstatusValidator.Delete(enum_paymentstatus))
                return enum_paymentstatus;

            try
            {
                await UOW.Begin();
                await UOW.enum_paymentstatusRepository.Delete(enum_paymentstatus);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_paymentstatus, nameof(enum_paymentstatusService));
                return enum_paymentstatus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_paymentstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_paymentstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_paymentstatus>> BulkDelete(List<enum_paymentstatus> enum_paymentstatuses)
        {
            if (!await enum_paymentstatusValidator.BulkDelete(enum_paymentstatuses))
                return enum_paymentstatuses;

            try
            {
                await UOW.Begin();
                await UOW.enum_paymentstatusRepository.BulkDelete(enum_paymentstatuses);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_paymentstatuses, nameof(enum_paymentstatusService));
                return enum_paymentstatuses;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_paymentstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_paymentstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_paymentstatus>> Import(List<enum_paymentstatus> enum_paymentstatuses)
        {
            if (!await enum_paymentstatusValidator.Import(enum_paymentstatuses))
                return enum_paymentstatuses;
            try
            {
                await UOW.Begin();
                await UOW.enum_paymentstatusRepository.BulkMerge(enum_paymentstatuses);
                await UOW.Commit();

                await Logging.CreateAuditLog(enum_paymentstatuses, new { }, nameof(enum_paymentstatusService));
                return enum_paymentstatuses;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_paymentstatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_paymentstatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public enum_paymentstatusFilter ToFilter(enum_paymentstatusFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<enum_paymentstatusFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                enum_paymentstatusFilter subFilter = new enum_paymentstatusFilter();
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
