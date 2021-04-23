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

namespace BASE.Services.Mmdm_nation
{
    public interface Imdm_nationService :  IServiceScoped
    {
        Task<int>
    Count(mdm_nationFilter mdm_nationFilter);
    Task<List<mdm_nation>> List(mdm_nationFilter mdm_nationFilter);
        Task<mdm_nation> Get(long Id);
        Task<mdm_nation> Create(mdm_nation mdm_nation);
        Task<mdm_nation> Update(mdm_nation mdm_nation);
        Task<mdm_nation> Delete(mdm_nation mdm_nation);
        Task<List<mdm_nation>> BulkDelete(List<mdm_nation> mdm_nations);
        Task<List<mdm_nation>> Import(List<mdm_nation> mdm_nations);
        mdm_nationFilter ToFilter(mdm_nationFilter mdm_nationFilter);
    }

    public class mdm_nationService : BaseService, Imdm_nationService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Imdm_nationValidator mdm_nationValidator;

        public mdm_nationService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Imdm_nationValidator mdm_nationValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.mdm_nationValidator = mdm_nationValidator;
        }
        public async Task<int> Count(mdm_nationFilter mdm_nationFilter)
        {
            try
            {
                int result = await UOW.mdm_nationRepository.Count(mdm_nationFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_nationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_nationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_nation>> List(mdm_nationFilter mdm_nationFilter)
        {
            try
            {
                List<mdm_nation> mdm_nations = await UOW.mdm_nationRepository.List(mdm_nationFilter);
                return mdm_nations;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_nationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_nationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<mdm_nation> Get(long Id)
        {
            mdm_nation mdm_nation = await UOW.mdm_nationRepository.Get(Id);
            if (mdm_nation == null)
                return null;
            return mdm_nation;
        }

        public async Task<mdm_nation> Create(mdm_nation mdm_nation)
        {
            if (!await mdm_nationValidator.Create(mdm_nation))
                return mdm_nation;

            try
            {
                await UOW.Begin();
                await UOW.mdm_nationRepository.Create(mdm_nation);
                await UOW.Commit();
                mdm_nation = await UOW.mdm_nationRepository.Get(mdm_nation.Id);
                await Logging.CreateAuditLog(mdm_nation, new { }, nameof(mdm_nationService));
                return mdm_nation;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_nationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_nationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_nation> Update(mdm_nation mdm_nation)
        {
            if (!await mdm_nationValidator.Update(mdm_nation))
                return mdm_nation;
            try
            {
                var oldData = await UOW.mdm_nationRepository.Get(mdm_nation.Id);

                await UOW.Begin();
                await UOW.mdm_nationRepository.Update(mdm_nation);
                await UOW.Commit();

                mdm_nation = await UOW.mdm_nationRepository.Get(mdm_nation.Id);
                await Logging.CreateAuditLog(mdm_nation, oldData, nameof(mdm_nationService));
                return mdm_nation;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_nationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_nationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_nation> Delete(mdm_nation mdm_nation)
        {
            if (!await mdm_nationValidator.Delete(mdm_nation))
                return mdm_nation;

            try
            {
                await UOW.Begin();
                await UOW.mdm_nationRepository.Delete(mdm_nation);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_nation, nameof(mdm_nationService));
                return mdm_nation;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_nationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_nationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_nation>> BulkDelete(List<mdm_nation> mdm_nations)
        {
            if (!await mdm_nationValidator.BulkDelete(mdm_nations))
                return mdm_nations;

            try
            {
                await UOW.Begin();
                await UOW.mdm_nationRepository.BulkDelete(mdm_nations);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_nations, nameof(mdm_nationService));
                return mdm_nations;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_nationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_nationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_nation>> Import(List<mdm_nation> mdm_nations)
        {
            if (!await mdm_nationValidator.Import(mdm_nations))
                return mdm_nations;
            try
            {
                await UOW.Begin();
                await UOW.mdm_nationRepository.BulkMerge(mdm_nations);
                await UOW.Commit();

                await Logging.CreateAuditLog(mdm_nations, new { }, nameof(mdm_nationService));
                return mdm_nations;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_nationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_nationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public mdm_nationFilter ToFilter(mdm_nationFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<mdm_nationFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                mdm_nationFilter subFilter = new mdm_nationFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Priority))
                        
                        subFilter.Priority = FilterPermissionDefinition.LongFilter;
                        
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
