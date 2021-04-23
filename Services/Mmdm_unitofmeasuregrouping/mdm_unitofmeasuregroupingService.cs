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

namespace BASE.Services.Mmdm_unitofmeasuregrouping
{
    public interface Imdm_unitofmeasuregroupingService :  IServiceScoped
    {
        Task<int>
    Count(mdm_unitofmeasuregroupingFilter mdm_unitofmeasuregroupingFilter);
    Task<List<mdm_unitofmeasuregrouping>> List(mdm_unitofmeasuregroupingFilter mdm_unitofmeasuregroupingFilter);
        Task<mdm_unitofmeasuregrouping> Get(long Id);
        Task<mdm_unitofmeasuregrouping> Create(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping);
        Task<mdm_unitofmeasuregrouping> Update(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping);
        Task<mdm_unitofmeasuregrouping> Delete(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping);
        Task<List<mdm_unitofmeasuregrouping>> BulkDelete(List<mdm_unitofmeasuregrouping> mdm_unitofmeasuregroupings);
        Task<List<mdm_unitofmeasuregrouping>> Import(List<mdm_unitofmeasuregrouping> mdm_unitofmeasuregroupings);
        mdm_unitofmeasuregroupingFilter ToFilter(mdm_unitofmeasuregroupingFilter mdm_unitofmeasuregroupingFilter);
    }

    public class mdm_unitofmeasuregroupingService : BaseService, Imdm_unitofmeasuregroupingService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Imdm_unitofmeasuregroupingValidator mdm_unitofmeasuregroupingValidator;

        public mdm_unitofmeasuregroupingService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Imdm_unitofmeasuregroupingValidator mdm_unitofmeasuregroupingValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.mdm_unitofmeasuregroupingValidator = mdm_unitofmeasuregroupingValidator;
        }
        public async Task<int> Count(mdm_unitofmeasuregroupingFilter mdm_unitofmeasuregroupingFilter)
        {
            try
            {
                int result = await UOW.mdm_unitofmeasuregroupingRepository.Count(mdm_unitofmeasuregroupingFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasuregroupingService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasuregroupingService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_unitofmeasuregrouping>> List(mdm_unitofmeasuregroupingFilter mdm_unitofmeasuregroupingFilter)
        {
            try
            {
                List<mdm_unitofmeasuregrouping> mdm_unitofmeasuregroupings = await UOW.mdm_unitofmeasuregroupingRepository.List(mdm_unitofmeasuregroupingFilter);
                return mdm_unitofmeasuregroupings;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasuregroupingService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasuregroupingService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<mdm_unitofmeasuregrouping> Get(long Id)
        {
            mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping = await UOW.mdm_unitofmeasuregroupingRepository.Get(Id);
            if (mdm_unitofmeasuregrouping == null)
                return null;
            return mdm_unitofmeasuregrouping;
        }

        public async Task<mdm_unitofmeasuregrouping> Create(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping)
        {
            if (!await mdm_unitofmeasuregroupingValidator.Create(mdm_unitofmeasuregrouping))
                return mdm_unitofmeasuregrouping;

            try
            {
                await UOW.Begin();
                await UOW.mdm_unitofmeasuregroupingRepository.Create(mdm_unitofmeasuregrouping);
                await UOW.Commit();
                mdm_unitofmeasuregrouping = await UOW.mdm_unitofmeasuregroupingRepository.Get(mdm_unitofmeasuregrouping.Id);
                await Logging.CreateAuditLog(mdm_unitofmeasuregrouping, new { }, nameof(mdm_unitofmeasuregroupingService));
                return mdm_unitofmeasuregrouping;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasuregroupingService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasuregroupingService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_unitofmeasuregrouping> Update(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping)
        {
            if (!await mdm_unitofmeasuregroupingValidator.Update(mdm_unitofmeasuregrouping))
                return mdm_unitofmeasuregrouping;
            try
            {
                var oldData = await UOW.mdm_unitofmeasuregroupingRepository.Get(mdm_unitofmeasuregrouping.Id);

                await UOW.Begin();
                await UOW.mdm_unitofmeasuregroupingRepository.Update(mdm_unitofmeasuregrouping);
                await UOW.Commit();

                mdm_unitofmeasuregrouping = await UOW.mdm_unitofmeasuregroupingRepository.Get(mdm_unitofmeasuregrouping.Id);
                await Logging.CreateAuditLog(mdm_unitofmeasuregrouping, oldData, nameof(mdm_unitofmeasuregroupingService));
                return mdm_unitofmeasuregrouping;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasuregroupingService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasuregroupingService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_unitofmeasuregrouping> Delete(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping)
        {
            if (!await mdm_unitofmeasuregroupingValidator.Delete(mdm_unitofmeasuregrouping))
                return mdm_unitofmeasuregrouping;

            try
            {
                await UOW.Begin();
                await UOW.mdm_unitofmeasuregroupingRepository.Delete(mdm_unitofmeasuregrouping);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_unitofmeasuregrouping, nameof(mdm_unitofmeasuregroupingService));
                return mdm_unitofmeasuregrouping;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasuregroupingService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasuregroupingService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_unitofmeasuregrouping>> BulkDelete(List<mdm_unitofmeasuregrouping> mdm_unitofmeasuregroupings)
        {
            if (!await mdm_unitofmeasuregroupingValidator.BulkDelete(mdm_unitofmeasuregroupings))
                return mdm_unitofmeasuregroupings;

            try
            {
                await UOW.Begin();
                await UOW.mdm_unitofmeasuregroupingRepository.BulkDelete(mdm_unitofmeasuregroupings);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_unitofmeasuregroupings, nameof(mdm_unitofmeasuregroupingService));
                return mdm_unitofmeasuregroupings;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasuregroupingService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasuregroupingService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_unitofmeasuregrouping>> Import(List<mdm_unitofmeasuregrouping> mdm_unitofmeasuregroupings)
        {
            if (!await mdm_unitofmeasuregroupingValidator.Import(mdm_unitofmeasuregroupings))
                return mdm_unitofmeasuregroupings;
            try
            {
                await UOW.Begin();
                await UOW.mdm_unitofmeasuregroupingRepository.BulkMerge(mdm_unitofmeasuregroupings);
                await UOW.Commit();

                await Logging.CreateAuditLog(mdm_unitofmeasuregroupings, new { }, nameof(mdm_unitofmeasuregroupingService));
                return mdm_unitofmeasuregroupings;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasuregroupingService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasuregroupingService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public mdm_unitofmeasuregroupingFilter ToFilter(mdm_unitofmeasuregroupingFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<mdm_unitofmeasuregroupingFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                mdm_unitofmeasuregroupingFilter subFilter = new mdm_unitofmeasuregroupingFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Description))
                        
                        
                        
                        
                        
                        
                        subFilter.Description = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.UnitOfMeasureId))
                        subFilter.UnitOfMeasureId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
