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

namespace BASE.Services.Mmdm_ward
{
    public interface Imdm_wardService :  IServiceScoped
    {
        Task<int>
    Count(mdm_wardFilter mdm_wardFilter);
    Task<List<mdm_ward>> List(mdm_wardFilter mdm_wardFilter);
        Task<mdm_ward> Get(long Id);
        Task<mdm_ward> Create(mdm_ward mdm_ward);
        Task<mdm_ward> Update(mdm_ward mdm_ward);
        Task<mdm_ward> Delete(mdm_ward mdm_ward);
        Task<List<mdm_ward>> BulkDelete(List<mdm_ward> mdm_wards);
        Task<List<mdm_ward>> Import(List<mdm_ward> mdm_wards);
        mdm_wardFilter ToFilter(mdm_wardFilter mdm_wardFilter);
    }

    public class mdm_wardService : BaseService, Imdm_wardService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Imdm_wardValidator mdm_wardValidator;

        public mdm_wardService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Imdm_wardValidator mdm_wardValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.mdm_wardValidator = mdm_wardValidator;
        }
        public async Task<int> Count(mdm_wardFilter mdm_wardFilter)
        {
            try
            {
                int result = await UOW.mdm_wardRepository.Count(mdm_wardFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_wardService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_wardService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_ward>> List(mdm_wardFilter mdm_wardFilter)
        {
            try
            {
                List<mdm_ward> mdm_wards = await UOW.mdm_wardRepository.List(mdm_wardFilter);
                return mdm_wards;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_wardService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_wardService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<mdm_ward> Get(long Id)
        {
            mdm_ward mdm_ward = await UOW.mdm_wardRepository.Get(Id);
            if (mdm_ward == null)
                return null;
            return mdm_ward;
        }

        public async Task<mdm_ward> Create(mdm_ward mdm_ward)
        {
            if (!await mdm_wardValidator.Create(mdm_ward))
                return mdm_ward;

            try
            {
                await UOW.Begin();
                await UOW.mdm_wardRepository.Create(mdm_ward);
                await UOW.Commit();
                mdm_ward = await UOW.mdm_wardRepository.Get(mdm_ward.Id);
                await Logging.CreateAuditLog(mdm_ward, new { }, nameof(mdm_wardService));
                return mdm_ward;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_wardService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_wardService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_ward> Update(mdm_ward mdm_ward)
        {
            if (!await mdm_wardValidator.Update(mdm_ward))
                return mdm_ward;
            try
            {
                var oldData = await UOW.mdm_wardRepository.Get(mdm_ward.Id);

                await UOW.Begin();
                await UOW.mdm_wardRepository.Update(mdm_ward);
                await UOW.Commit();

                mdm_ward = await UOW.mdm_wardRepository.Get(mdm_ward.Id);
                await Logging.CreateAuditLog(mdm_ward, oldData, nameof(mdm_wardService));
                return mdm_ward;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_wardService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_wardService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_ward> Delete(mdm_ward mdm_ward)
        {
            if (!await mdm_wardValidator.Delete(mdm_ward))
                return mdm_ward;

            try
            {
                await UOW.Begin();
                await UOW.mdm_wardRepository.Delete(mdm_ward);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_ward, nameof(mdm_wardService));
                return mdm_ward;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_wardService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_wardService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_ward>> BulkDelete(List<mdm_ward> mdm_wards)
        {
            if (!await mdm_wardValidator.BulkDelete(mdm_wards))
                return mdm_wards;

            try
            {
                await UOW.Begin();
                await UOW.mdm_wardRepository.BulkDelete(mdm_wards);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_wards, nameof(mdm_wardService));
                return mdm_wards;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_wardService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_wardService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_ward>> Import(List<mdm_ward> mdm_wards)
        {
            if (!await mdm_wardValidator.Import(mdm_wards))
                return mdm_wards;
            try
            {
                await UOW.Begin();
                await UOW.mdm_wardRepository.BulkMerge(mdm_wards);
                await UOW.Commit();

                await Logging.CreateAuditLog(mdm_wards, new { }, nameof(mdm_wardService));
                return mdm_wards;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_wardService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_wardService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public mdm_wardFilter ToFilter(mdm_wardFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<mdm_wardFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                mdm_wardFilter subFilter = new mdm_wardFilter();
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
                        
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.DistrictId))
                        subFilter.DistrictId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
