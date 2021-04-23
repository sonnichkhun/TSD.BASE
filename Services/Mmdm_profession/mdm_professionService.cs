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

namespace BASE.Services.Mmdm_profession
{
    public interface Imdm_professionService :  IServiceScoped
    {
        Task<int>
    Count(mdm_professionFilter mdm_professionFilter);
    Task<List<mdm_profession>> List(mdm_professionFilter mdm_professionFilter);
        Task<mdm_profession> Get(long Id);
        Task<mdm_profession> Create(mdm_profession mdm_profession);
        Task<mdm_profession> Update(mdm_profession mdm_profession);
        Task<mdm_profession> Delete(mdm_profession mdm_profession);
        Task<List<mdm_profession>> BulkDelete(List<mdm_profession> mdm_professions);
        Task<List<mdm_profession>> Import(List<mdm_profession> mdm_professions);
        mdm_professionFilter ToFilter(mdm_professionFilter mdm_professionFilter);
    }

    public class mdm_professionService : BaseService, Imdm_professionService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Imdm_professionValidator mdm_professionValidator;

        public mdm_professionService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Imdm_professionValidator mdm_professionValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.mdm_professionValidator = mdm_professionValidator;
        }
        public async Task<int> Count(mdm_professionFilter mdm_professionFilter)
        {
            try
            {
                int result = await UOW.mdm_professionRepository.Count(mdm_professionFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_professionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_professionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_profession>> List(mdm_professionFilter mdm_professionFilter)
        {
            try
            {
                List<mdm_profession> mdm_professions = await UOW.mdm_professionRepository.List(mdm_professionFilter);
                return mdm_professions;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_professionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_professionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<mdm_profession> Get(long Id)
        {
            mdm_profession mdm_profession = await UOW.mdm_professionRepository.Get(Id);
            if (mdm_profession == null)
                return null;
            return mdm_profession;
        }

        public async Task<mdm_profession> Create(mdm_profession mdm_profession)
        {
            if (!await mdm_professionValidator.Create(mdm_profession))
                return mdm_profession;

            try
            {
                await UOW.Begin();
                await UOW.mdm_professionRepository.Create(mdm_profession);
                await UOW.Commit();
                mdm_profession = await UOW.mdm_professionRepository.Get(mdm_profession.Id);
                await Logging.CreateAuditLog(mdm_profession, new { }, nameof(mdm_professionService));
                return mdm_profession;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_professionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_professionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_profession> Update(mdm_profession mdm_profession)
        {
            if (!await mdm_professionValidator.Update(mdm_profession))
                return mdm_profession;
            try
            {
                var oldData = await UOW.mdm_professionRepository.Get(mdm_profession.Id);

                await UOW.Begin();
                await UOW.mdm_professionRepository.Update(mdm_profession);
                await UOW.Commit();

                mdm_profession = await UOW.mdm_professionRepository.Get(mdm_profession.Id);
                await Logging.CreateAuditLog(mdm_profession, oldData, nameof(mdm_professionService));
                return mdm_profession;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_professionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_professionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_profession> Delete(mdm_profession mdm_profession)
        {
            if (!await mdm_professionValidator.Delete(mdm_profession))
                return mdm_profession;

            try
            {
                await UOW.Begin();
                await UOW.mdm_professionRepository.Delete(mdm_profession);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_profession, nameof(mdm_professionService));
                return mdm_profession;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_professionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_professionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_profession>> BulkDelete(List<mdm_profession> mdm_professions)
        {
            if (!await mdm_professionValidator.BulkDelete(mdm_professions))
                return mdm_professions;

            try
            {
                await UOW.Begin();
                await UOW.mdm_professionRepository.BulkDelete(mdm_professions);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_professions, nameof(mdm_professionService));
                return mdm_professions;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_professionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_professionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_profession>> Import(List<mdm_profession> mdm_professions)
        {
            if (!await mdm_professionValidator.Import(mdm_professions))
                return mdm_professions;
            try
            {
                await UOW.Begin();
                await UOW.mdm_professionRepository.BulkMerge(mdm_professions);
                await UOW.Commit();

                await Logging.CreateAuditLog(mdm_professions, new { }, nameof(mdm_professionService));
                return mdm_professions;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_professionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_professionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public mdm_professionFilter ToFilter(mdm_professionFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<mdm_professionFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                mdm_professionFilter subFilter = new mdm_professionFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
