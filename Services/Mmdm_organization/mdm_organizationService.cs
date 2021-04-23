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

namespace BASE.Services.Mmdm_organization
{
    public interface Imdm_organizationService :  IServiceScoped
    {
        Task<int>
    Count(mdm_organizationFilter mdm_organizationFilter);
    Task<List<mdm_organization>> List(mdm_organizationFilter mdm_organizationFilter);
        Task<mdm_organization> Get(long Id);
        Task<mdm_organization> Create(mdm_organization mdm_organization);
        Task<mdm_organization> Update(mdm_organization mdm_organization);
        Task<mdm_organization> Delete(mdm_organization mdm_organization);
        Task<List<mdm_organization>> BulkDelete(List<mdm_organization> mdm_organizations);
        Task<List<mdm_organization>> Import(List<mdm_organization> mdm_organizations);
        mdm_organizationFilter ToFilter(mdm_organizationFilter mdm_organizationFilter);
    }

    public class mdm_organizationService : BaseService, Imdm_organizationService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Imdm_organizationValidator mdm_organizationValidator;

        public mdm_organizationService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Imdm_organizationValidator mdm_organizationValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.mdm_organizationValidator = mdm_organizationValidator;
        }
        public async Task<int> Count(mdm_organizationFilter mdm_organizationFilter)
        {
            try
            {
                int result = await UOW.mdm_organizationRepository.Count(mdm_organizationFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_organizationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_organizationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_organization>> List(mdm_organizationFilter mdm_organizationFilter)
        {
            try
            {
                List<mdm_organization> mdm_organizations = await UOW.mdm_organizationRepository.List(mdm_organizationFilter);
                return mdm_organizations;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_organizationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_organizationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<mdm_organization> Get(long Id)
        {
            mdm_organization mdm_organization = await UOW.mdm_organizationRepository.Get(Id);
            if (mdm_organization == null)
                return null;
            return mdm_organization;
        }

        public async Task<mdm_organization> Create(mdm_organization mdm_organization)
        {
            if (!await mdm_organizationValidator.Create(mdm_organization))
                return mdm_organization;

            try
            {
                await UOW.Begin();
                await UOW.mdm_organizationRepository.Create(mdm_organization);
                await UOW.Commit();
                mdm_organization = await UOW.mdm_organizationRepository.Get(mdm_organization.Id);
                await Logging.CreateAuditLog(mdm_organization, new { }, nameof(mdm_organizationService));
                return mdm_organization;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_organizationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_organizationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_organization> Update(mdm_organization mdm_organization)
        {
            if (!await mdm_organizationValidator.Update(mdm_organization))
                return mdm_organization;
            try
            {
                var oldData = await UOW.mdm_organizationRepository.Get(mdm_organization.Id);

                await UOW.Begin();
                await UOW.mdm_organizationRepository.Update(mdm_organization);
                await UOW.Commit();

                mdm_organization = await UOW.mdm_organizationRepository.Get(mdm_organization.Id);
                await Logging.CreateAuditLog(mdm_organization, oldData, nameof(mdm_organizationService));
                return mdm_organization;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_organizationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_organizationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_organization> Delete(mdm_organization mdm_organization)
        {
            if (!await mdm_organizationValidator.Delete(mdm_organization))
                return mdm_organization;

            try
            {
                await UOW.Begin();
                await UOW.mdm_organizationRepository.Delete(mdm_organization);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_organization, nameof(mdm_organizationService));
                return mdm_organization;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_organizationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_organizationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_organization>> BulkDelete(List<mdm_organization> mdm_organizations)
        {
            if (!await mdm_organizationValidator.BulkDelete(mdm_organizations))
                return mdm_organizations;

            try
            {
                await UOW.Begin();
                await UOW.mdm_organizationRepository.BulkDelete(mdm_organizations);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_organizations, nameof(mdm_organizationService));
                return mdm_organizations;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_organizationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_organizationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_organization>> Import(List<mdm_organization> mdm_organizations)
        {
            if (!await mdm_organizationValidator.Import(mdm_organizations))
                return mdm_organizations;
            try
            {
                await UOW.Begin();
                await UOW.mdm_organizationRepository.BulkMerge(mdm_organizations);
                await UOW.Commit();

                await Logging.CreateAuditLog(mdm_organizations, new { }, nameof(mdm_organizationService));
                return mdm_organizations;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_organizationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_organizationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public mdm_organizationFilter ToFilter(mdm_organizationFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<mdm_organizationFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                mdm_organizationFilter subFilter = new mdm_organizationFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ParentId))
                        subFilter.ParentId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Path))
                        
                        
                        
                        
                        
                        
                        subFilter.Path = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Level))
                        
                        subFilter.Level = FilterPermissionDefinition.LongFilter;
                        
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Phone))
                        
                        
                        
                        
                        
                        
                        subFilter.Phone = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Email))
                        
                        
                        
                        
                        
                        
                        subFilter.Email = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Address))
                        
                        
                        
                        
                        
                        
                        subFilter.Address = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
