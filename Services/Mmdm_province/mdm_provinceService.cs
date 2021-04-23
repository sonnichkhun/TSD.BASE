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

namespace BASE.Services.Mmdm_province
{
    public interface Imdm_provinceService :  IServiceScoped
    {
        Task<int>
    Count(mdm_provinceFilter mdm_provinceFilter);
    Task<List<mdm_province>> List(mdm_provinceFilter mdm_provinceFilter);
        Task<mdm_province> Get(long Id);
        Task<mdm_province> Create(mdm_province mdm_province);
        Task<mdm_province> Update(mdm_province mdm_province);
        Task<mdm_province> Delete(mdm_province mdm_province);
        Task<List<mdm_province>> BulkDelete(List<mdm_province> mdm_provinces);
        Task<List<mdm_province>> Import(List<mdm_province> mdm_provinces);
        mdm_provinceFilter ToFilter(mdm_provinceFilter mdm_provinceFilter);
    }

    public class mdm_provinceService : BaseService, Imdm_provinceService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Imdm_provinceValidator mdm_provinceValidator;

        public mdm_provinceService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Imdm_provinceValidator mdm_provinceValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.mdm_provinceValidator = mdm_provinceValidator;
        }
        public async Task<int> Count(mdm_provinceFilter mdm_provinceFilter)
        {
            try
            {
                int result = await UOW.mdm_provinceRepository.Count(mdm_provinceFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_provinceService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_provinceService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_province>> List(mdm_provinceFilter mdm_provinceFilter)
        {
            try
            {
                List<mdm_province> mdm_provinces = await UOW.mdm_provinceRepository.List(mdm_provinceFilter);
                return mdm_provinces;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_provinceService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_provinceService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<mdm_province> Get(long Id)
        {
            mdm_province mdm_province = await UOW.mdm_provinceRepository.Get(Id);
            if (mdm_province == null)
                return null;
            return mdm_province;
        }

        public async Task<mdm_province> Create(mdm_province mdm_province)
        {
            if (!await mdm_provinceValidator.Create(mdm_province))
                return mdm_province;

            try
            {
                await UOW.Begin();
                await UOW.mdm_provinceRepository.Create(mdm_province);
                await UOW.Commit();
                mdm_province = await UOW.mdm_provinceRepository.Get(mdm_province.Id);
                await Logging.CreateAuditLog(mdm_province, new { }, nameof(mdm_provinceService));
                return mdm_province;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_provinceService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_provinceService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_province> Update(mdm_province mdm_province)
        {
            if (!await mdm_provinceValidator.Update(mdm_province))
                return mdm_province;
            try
            {
                var oldData = await UOW.mdm_provinceRepository.Get(mdm_province.Id);

                await UOW.Begin();
                await UOW.mdm_provinceRepository.Update(mdm_province);
                await UOW.Commit();

                mdm_province = await UOW.mdm_provinceRepository.Get(mdm_province.Id);
                await Logging.CreateAuditLog(mdm_province, oldData, nameof(mdm_provinceService));
                return mdm_province;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_provinceService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_provinceService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_province> Delete(mdm_province mdm_province)
        {
            if (!await mdm_provinceValidator.Delete(mdm_province))
                return mdm_province;

            try
            {
                await UOW.Begin();
                await UOW.mdm_provinceRepository.Delete(mdm_province);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_province, nameof(mdm_provinceService));
                return mdm_province;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_provinceService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_provinceService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_province>> BulkDelete(List<mdm_province> mdm_provinces)
        {
            if (!await mdm_provinceValidator.BulkDelete(mdm_provinces))
                return mdm_provinces;

            try
            {
                await UOW.Begin();
                await UOW.mdm_provinceRepository.BulkDelete(mdm_provinces);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_provinces, nameof(mdm_provinceService));
                return mdm_provinces;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_provinceService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_provinceService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_province>> Import(List<mdm_province> mdm_provinces)
        {
            if (!await mdm_provinceValidator.Import(mdm_provinces))
                return mdm_provinces;
            try
            {
                await UOW.Begin();
                await UOW.mdm_provinceRepository.BulkMerge(mdm_provinces);
                await UOW.Commit();

                await Logging.CreateAuditLog(mdm_provinces, new { }, nameof(mdm_provinceService));
                return mdm_provinces;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_provinceService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_provinceService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public mdm_provinceFilter ToFilter(mdm_provinceFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<mdm_provinceFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                mdm_provinceFilter subFilter = new mdm_provinceFilter();
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
