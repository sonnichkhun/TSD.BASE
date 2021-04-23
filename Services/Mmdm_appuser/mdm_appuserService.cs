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

namespace BASE.Services.Mmdm_appuser
{
    public interface Imdm_appuserService :  IServiceScoped
    {
        Task<int>
    Count(mdm_appuserFilter mdm_appuserFilter);
    Task<List<mdm_appuser>> List(mdm_appuserFilter mdm_appuserFilter);
        Task<mdm_appuser> Get(long Id);
        Task<mdm_appuser> Create(mdm_appuser mdm_appuser);
        Task<mdm_appuser> Update(mdm_appuser mdm_appuser);
        Task<mdm_appuser> Delete(mdm_appuser mdm_appuser);
        Task<List<mdm_appuser>> BulkDelete(List<mdm_appuser> mdm_appusers);
        Task<List<mdm_appuser>> Import(List<mdm_appuser> mdm_appusers);
        mdm_appuserFilter ToFilter(mdm_appuserFilter mdm_appuserFilter);
    }

    public class mdm_appuserService : BaseService, Imdm_appuserService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Imdm_appuserValidator mdm_appuserValidator;

        public mdm_appuserService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Imdm_appuserValidator mdm_appuserValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.mdm_appuserValidator = mdm_appuserValidator;
        }
        public async Task<int> Count(mdm_appuserFilter mdm_appuserFilter)
        {
            try
            {
                int result = await UOW.mdm_appuserRepository.Count(mdm_appuserFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_appuserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_appuserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_appuser>> List(mdm_appuserFilter mdm_appuserFilter)
        {
            try
            {
                List<mdm_appuser> mdm_appusers = await UOW.mdm_appuserRepository.List(mdm_appuserFilter);
                return mdm_appusers;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_appuserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_appuserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<mdm_appuser> Get(long Id)
        {
            mdm_appuser mdm_appuser = await UOW.mdm_appuserRepository.Get(Id);
            if (mdm_appuser == null)
                return null;
            return mdm_appuser;
        }

        public async Task<mdm_appuser> Create(mdm_appuser mdm_appuser)
        {
            if (!await mdm_appuserValidator.Create(mdm_appuser))
                return mdm_appuser;

            try
            {
                await UOW.Begin();
                await UOW.mdm_appuserRepository.Create(mdm_appuser);
                await UOW.Commit();
                mdm_appuser = await UOW.mdm_appuserRepository.Get(mdm_appuser.Id);
                await Logging.CreateAuditLog(mdm_appuser, new { }, nameof(mdm_appuserService));
                return mdm_appuser;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_appuserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_appuserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_appuser> Update(mdm_appuser mdm_appuser)
        {
            if (!await mdm_appuserValidator.Update(mdm_appuser))
                return mdm_appuser;
            try
            {
                var oldData = await UOW.mdm_appuserRepository.Get(mdm_appuser.Id);

                await UOW.Begin();
                await UOW.mdm_appuserRepository.Update(mdm_appuser);
                await UOW.Commit();

                mdm_appuser = await UOW.mdm_appuserRepository.Get(mdm_appuser.Id);
                await Logging.CreateAuditLog(mdm_appuser, oldData, nameof(mdm_appuserService));
                return mdm_appuser;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_appuserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_appuserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_appuser> Delete(mdm_appuser mdm_appuser)
        {
            if (!await mdm_appuserValidator.Delete(mdm_appuser))
                return mdm_appuser;

            try
            {
                await UOW.Begin();
                await UOW.mdm_appuserRepository.Delete(mdm_appuser);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_appuser, nameof(mdm_appuserService));
                return mdm_appuser;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_appuserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_appuserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_appuser>> BulkDelete(List<mdm_appuser> mdm_appusers)
        {
            if (!await mdm_appuserValidator.BulkDelete(mdm_appusers))
                return mdm_appusers;

            try
            {
                await UOW.Begin();
                await UOW.mdm_appuserRepository.BulkDelete(mdm_appusers);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_appusers, nameof(mdm_appuserService));
                return mdm_appusers;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_appuserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_appuserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_appuser>> Import(List<mdm_appuser> mdm_appusers)
        {
            if (!await mdm_appuserValidator.Import(mdm_appusers))
                return mdm_appusers;
            try
            {
                await UOW.Begin();
                await UOW.mdm_appuserRepository.BulkMerge(mdm_appusers);
                await UOW.Commit();

                await Logging.CreateAuditLog(mdm_appusers, new { }, nameof(mdm_appuserService));
                return mdm_appusers;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_appuserService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_appuserService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public mdm_appuserFilter ToFilter(mdm_appuserFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<mdm_appuserFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                mdm_appuserFilter subFilter = new mdm_appuserFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Username))
                        
                        
                        
                        
                        
                        
                        subFilter.Username = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.DisplayName))
                        
                        
                        
                        
                        
                        
                        subFilter.DisplayName = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Address))
                        
                        
                        
                        
                        
                        
                        subFilter.Address = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Email))
                        
                        
                        
                        
                        
                        
                        subFilter.Email = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Phone))
                        
                        
                        
                        
                        
                        
                        subFilter.Phone = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.SexId))
                        subFilter.SexId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Birthday))
                        
                        
                        
                        
                        
                        subFilter.Birthday = FilterPermissionDefinition.DateFilter;
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Avatar))
                        
                        
                        
                        
                        
                        
                        subFilter.Avatar = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Department))
                        
                        
                        
                        
                        
                        
                        subFilter.Department = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrganizationId))
                        subFilter.OrganizationId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Longitude))
                        
                        
                        subFilter.Longitude = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Latitude))
                        
                        
                        subFilter.Latitude = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
