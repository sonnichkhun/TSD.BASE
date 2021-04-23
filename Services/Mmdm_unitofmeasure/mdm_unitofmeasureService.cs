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

namespace BASE.Services.Mmdm_unitofmeasure
{
    public interface Imdm_unitofmeasureService :  IServiceScoped
    {
        Task<int>
    Count(mdm_unitofmeasureFilter mdm_unitofmeasureFilter);
    Task<List<mdm_unitofmeasure>> List(mdm_unitofmeasureFilter mdm_unitofmeasureFilter);
        Task<mdm_unitofmeasure> Get(long Id);
        Task<mdm_unitofmeasure> Create(mdm_unitofmeasure mdm_unitofmeasure);
        Task<mdm_unitofmeasure> Update(mdm_unitofmeasure mdm_unitofmeasure);
        Task<mdm_unitofmeasure> Delete(mdm_unitofmeasure mdm_unitofmeasure);
        Task<List<mdm_unitofmeasure>> BulkDelete(List<mdm_unitofmeasure> mdm_unitofmeasures);
        Task<List<mdm_unitofmeasure>> Import(List<mdm_unitofmeasure> mdm_unitofmeasures);
        mdm_unitofmeasureFilter ToFilter(mdm_unitofmeasureFilter mdm_unitofmeasureFilter);
    }

    public class mdm_unitofmeasureService : BaseService, Imdm_unitofmeasureService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Imdm_unitofmeasureValidator mdm_unitofmeasureValidator;

        public mdm_unitofmeasureService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Imdm_unitofmeasureValidator mdm_unitofmeasureValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.mdm_unitofmeasureValidator = mdm_unitofmeasureValidator;
        }
        public async Task<int> Count(mdm_unitofmeasureFilter mdm_unitofmeasureFilter)
        {
            try
            {
                int result = await UOW.mdm_unitofmeasureRepository.Count(mdm_unitofmeasureFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasureService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasureService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_unitofmeasure>> List(mdm_unitofmeasureFilter mdm_unitofmeasureFilter)
        {
            try
            {
                List<mdm_unitofmeasure> mdm_unitofmeasures = await UOW.mdm_unitofmeasureRepository.List(mdm_unitofmeasureFilter);
                return mdm_unitofmeasures;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasureService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasureService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<mdm_unitofmeasure> Get(long Id)
        {
            mdm_unitofmeasure mdm_unitofmeasure = await UOW.mdm_unitofmeasureRepository.Get(Id);
            if (mdm_unitofmeasure == null)
                return null;
            return mdm_unitofmeasure;
        }

        public async Task<mdm_unitofmeasure> Create(mdm_unitofmeasure mdm_unitofmeasure)
        {
            if (!await mdm_unitofmeasureValidator.Create(mdm_unitofmeasure))
                return mdm_unitofmeasure;

            try
            {
                await UOW.Begin();
                await UOW.mdm_unitofmeasureRepository.Create(mdm_unitofmeasure);
                await UOW.Commit();
                mdm_unitofmeasure = await UOW.mdm_unitofmeasureRepository.Get(mdm_unitofmeasure.Id);
                await Logging.CreateAuditLog(mdm_unitofmeasure, new { }, nameof(mdm_unitofmeasureService));
                return mdm_unitofmeasure;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasureService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasureService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_unitofmeasure> Update(mdm_unitofmeasure mdm_unitofmeasure)
        {
            if (!await mdm_unitofmeasureValidator.Update(mdm_unitofmeasure))
                return mdm_unitofmeasure;
            try
            {
                var oldData = await UOW.mdm_unitofmeasureRepository.Get(mdm_unitofmeasure.Id);

                await UOW.Begin();
                await UOW.mdm_unitofmeasureRepository.Update(mdm_unitofmeasure);
                await UOW.Commit();

                mdm_unitofmeasure = await UOW.mdm_unitofmeasureRepository.Get(mdm_unitofmeasure.Id);
                await Logging.CreateAuditLog(mdm_unitofmeasure, oldData, nameof(mdm_unitofmeasureService));
                return mdm_unitofmeasure;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasureService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasureService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_unitofmeasure> Delete(mdm_unitofmeasure mdm_unitofmeasure)
        {
            if (!await mdm_unitofmeasureValidator.Delete(mdm_unitofmeasure))
                return mdm_unitofmeasure;

            try
            {
                await UOW.Begin();
                await UOW.mdm_unitofmeasureRepository.Delete(mdm_unitofmeasure);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_unitofmeasure, nameof(mdm_unitofmeasureService));
                return mdm_unitofmeasure;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasureService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasureService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_unitofmeasure>> BulkDelete(List<mdm_unitofmeasure> mdm_unitofmeasures)
        {
            if (!await mdm_unitofmeasureValidator.BulkDelete(mdm_unitofmeasures))
                return mdm_unitofmeasures;

            try
            {
                await UOW.Begin();
                await UOW.mdm_unitofmeasureRepository.BulkDelete(mdm_unitofmeasures);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_unitofmeasures, nameof(mdm_unitofmeasureService));
                return mdm_unitofmeasures;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasureService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasureService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_unitofmeasure>> Import(List<mdm_unitofmeasure> mdm_unitofmeasures)
        {
            if (!await mdm_unitofmeasureValidator.Import(mdm_unitofmeasures))
                return mdm_unitofmeasures;
            try
            {
                await UOW.Begin();
                await UOW.mdm_unitofmeasureRepository.BulkMerge(mdm_unitofmeasures);
                await UOW.Commit();

                await Logging.CreateAuditLog(mdm_unitofmeasures, new { }, nameof(mdm_unitofmeasureService));
                return mdm_unitofmeasures;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasureService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasureService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public mdm_unitofmeasureFilter ToFilter(mdm_unitofmeasureFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<mdm_unitofmeasureFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                mdm_unitofmeasureFilter subFilter = new mdm_unitofmeasureFilter();
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
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
