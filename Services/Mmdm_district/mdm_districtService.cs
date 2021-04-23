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

namespace BASE.Services.Mmdm_district
{
    public interface Imdm_districtService :  IServiceScoped
    {
        Task<int>
    Count(mdm_districtFilter mdm_districtFilter);
    Task<List<mdm_district>> List(mdm_districtFilter mdm_districtFilter);
        Task<mdm_district> Get(long Id);
        Task<mdm_district> Create(mdm_district mdm_district);
        Task<mdm_district> Update(mdm_district mdm_district);
        Task<mdm_district> Delete(mdm_district mdm_district);
        Task<List<mdm_district>> BulkDelete(List<mdm_district> mdm_districts);
        Task<List<mdm_district>> Import(List<mdm_district> mdm_districts);
        mdm_districtFilter ToFilter(mdm_districtFilter mdm_districtFilter);
    }

    public class mdm_districtService : BaseService, Imdm_districtService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Imdm_districtValidator mdm_districtValidator;

        public mdm_districtService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Imdm_districtValidator mdm_districtValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.mdm_districtValidator = mdm_districtValidator;
        }
        public async Task<int> Count(mdm_districtFilter mdm_districtFilter)
        {
            try
            {
                int result = await UOW.mdm_districtRepository.Count(mdm_districtFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_districtService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_districtService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_district>> List(mdm_districtFilter mdm_districtFilter)
        {
            try
            {
                List<mdm_district> mdm_districts = await UOW.mdm_districtRepository.List(mdm_districtFilter);
                return mdm_districts;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_districtService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_districtService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<mdm_district> Get(long Id)
        {
            mdm_district mdm_district = await UOW.mdm_districtRepository.Get(Id);
            if (mdm_district == null)
                return null;
            return mdm_district;
        }

        public async Task<mdm_district> Create(mdm_district mdm_district)
        {
            if (!await mdm_districtValidator.Create(mdm_district))
                return mdm_district;

            try
            {
                await UOW.Begin();
                await UOW.mdm_districtRepository.Create(mdm_district);
                await UOW.Commit();
                mdm_district = await UOW.mdm_districtRepository.Get(mdm_district.Id);
                await Logging.CreateAuditLog(mdm_district, new { }, nameof(mdm_districtService));
                return mdm_district;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_districtService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_districtService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_district> Update(mdm_district mdm_district)
        {
            if (!await mdm_districtValidator.Update(mdm_district))
                return mdm_district;
            try
            {
                var oldData = await UOW.mdm_districtRepository.Get(mdm_district.Id);

                await UOW.Begin();
                await UOW.mdm_districtRepository.Update(mdm_district);
                await UOW.Commit();

                mdm_district = await UOW.mdm_districtRepository.Get(mdm_district.Id);
                await Logging.CreateAuditLog(mdm_district, oldData, nameof(mdm_districtService));
                return mdm_district;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_districtService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_districtService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_district> Delete(mdm_district mdm_district)
        {
            if (!await mdm_districtValidator.Delete(mdm_district))
                return mdm_district;

            try
            {
                await UOW.Begin();
                await UOW.mdm_districtRepository.Delete(mdm_district);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_district, nameof(mdm_districtService));
                return mdm_district;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_districtService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_districtService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_district>> BulkDelete(List<mdm_district> mdm_districts)
        {
            if (!await mdm_districtValidator.BulkDelete(mdm_districts))
                return mdm_districts;

            try
            {
                await UOW.Begin();
                await UOW.mdm_districtRepository.BulkDelete(mdm_districts);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_districts, nameof(mdm_districtService));
                return mdm_districts;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_districtService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_districtService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_district>> Import(List<mdm_district> mdm_districts)
        {
            if (!await mdm_districtValidator.Import(mdm_districts))
                return mdm_districts;
            try
            {
                await UOW.Begin();
                await UOW.mdm_districtRepository.BulkMerge(mdm_districts);
                await UOW.Commit();

                await Logging.CreateAuditLog(mdm_districts, new { }, nameof(mdm_districtService));
                return mdm_districts;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_districtService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_districtService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public mdm_districtFilter ToFilter(mdm_districtFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<mdm_districtFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                mdm_districtFilter subFilter = new mdm_districtFilter();
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
                        
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ProvinceId))
                        subFilter.ProvinceId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
