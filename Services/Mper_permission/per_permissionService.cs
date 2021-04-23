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

namespace BASE.Services.Mper_permission
{
    public interface Iper_permissionService :  IServiceScoped
    {
        Task<int>
    Count(per_permissionFilter per_permissionFilter);
    Task<List<per_permission>> List(per_permissionFilter per_permissionFilter);
        Task<per_permission> Get(long Id);
        Task<per_permission> Create(per_permission per_permission);
        Task<per_permission> Update(per_permission per_permission);
        Task<per_permission> Delete(per_permission per_permission);
        Task<List<per_permission>> BulkDelete(List<per_permission> per_permissions);
        Task<List<per_permission>> Import(List<per_permission> per_permissions);
        per_permissionFilter ToFilter(per_permissionFilter per_permissionFilter);
    }

    public class per_permissionService : BaseService, Iper_permissionService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Iper_permissionValidator per_permissionValidator;

        public per_permissionService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Iper_permissionValidator per_permissionValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.per_permissionValidator = per_permissionValidator;
        }
        public async Task<int> Count(per_permissionFilter per_permissionFilter)
        {
            try
            {
                int result = await UOW.per_permissionRepository.Count(per_permissionFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_permission>> List(per_permissionFilter per_permissionFilter)
        {
            try
            {
                List<per_permission> per_permissions = await UOW.per_permissionRepository.List(per_permissionFilter);
                return per_permissions;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<per_permission> Get(long Id)
        {
            per_permission per_permission = await UOW.per_permissionRepository.Get(Id);
            if (per_permission == null)
                return null;
            return per_permission;
        }

        public async Task<per_permission> Create(per_permission per_permission)
        {
            if (!await per_permissionValidator.Create(per_permission))
                return per_permission;

            try
            {
                await UOW.Begin();
                await UOW.per_permissionRepository.Create(per_permission);
                await UOW.Commit();
                per_permission = await UOW.per_permissionRepository.Get(per_permission.Id);
                await Logging.CreateAuditLog(per_permission, new { }, nameof(per_permissionService));
                return per_permission;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_permission> Update(per_permission per_permission)
        {
            if (!await per_permissionValidator.Update(per_permission))
                return per_permission;
            try
            {
                var oldData = await UOW.per_permissionRepository.Get(per_permission.Id);

                await UOW.Begin();
                await UOW.per_permissionRepository.Update(per_permission);
                await UOW.Commit();

                per_permission = await UOW.per_permissionRepository.Get(per_permission.Id);
                await Logging.CreateAuditLog(per_permission, oldData, nameof(per_permissionService));
                return per_permission;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_permission> Delete(per_permission per_permission)
        {
            if (!await per_permissionValidator.Delete(per_permission))
                return per_permission;

            try
            {
                await UOW.Begin();
                await UOW.per_permissionRepository.Delete(per_permission);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_permission, nameof(per_permissionService));
                return per_permission;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_permission>> BulkDelete(List<per_permission> per_permissions)
        {
            if (!await per_permissionValidator.BulkDelete(per_permissions))
                return per_permissions;

            try
            {
                await UOW.Begin();
                await UOW.per_permissionRepository.BulkDelete(per_permissions);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_permissions, nameof(per_permissionService));
                return per_permissions;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_permission>> Import(List<per_permission> per_permissions)
        {
            if (!await per_permissionValidator.Import(per_permissions))
                return per_permissions;
            try
            {
                await UOW.Begin();
                await UOW.per_permissionRepository.BulkMerge(per_permissions);
                await UOW.Commit();

                await Logging.CreateAuditLog(per_permissions, new { }, nameof(per_permissionService));
                return per_permissions;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public per_permissionFilter ToFilter(per_permissionFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<per_permissionFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                per_permissionFilter subFilter = new per_permissionFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.RoleId))
                        subFilter.RoleId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.MenuId))
                        subFilter.MenuId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
