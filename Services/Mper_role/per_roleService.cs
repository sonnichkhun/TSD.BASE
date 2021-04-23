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

namespace BASE.Services.Mper_role
{
    public interface Iper_roleService :  IServiceScoped
    {
        Task<int>
    Count(per_roleFilter per_roleFilter);
    Task<List<per_role>> List(per_roleFilter per_roleFilter);
        Task<per_role> Get(long Id);
        Task<per_role> Create(per_role per_role);
        Task<per_role> Update(per_role per_role);
        Task<per_role> Delete(per_role per_role);
        Task<List<per_role>> BulkDelete(List<per_role> per_roles);
        Task<List<per_role>> Import(List<per_role> per_roles);
        per_roleFilter ToFilter(per_roleFilter per_roleFilter);
    }

    public class per_roleService : BaseService, Iper_roleService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Iper_roleValidator per_roleValidator;

        public per_roleService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Iper_roleValidator per_roleValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.per_roleValidator = per_roleValidator;
        }
        public async Task<int> Count(per_roleFilter per_roleFilter)
        {
            try
            {
                int result = await UOW.per_roleRepository.Count(per_roleFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_roleService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_roleService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_role>> List(per_roleFilter per_roleFilter)
        {
            try
            {
                List<per_role> per_roles = await UOW.per_roleRepository.List(per_roleFilter);
                return per_roles;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_roleService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_roleService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<per_role> Get(long Id)
        {
            per_role per_role = await UOW.per_roleRepository.Get(Id);
            if (per_role == null)
                return null;
            return per_role;
        }

        public async Task<per_role> Create(per_role per_role)
        {
            if (!await per_roleValidator.Create(per_role))
                return per_role;

            try
            {
                await UOW.Begin();
                await UOW.per_roleRepository.Create(per_role);
                await UOW.Commit();
                per_role = await UOW.per_roleRepository.Get(per_role.Id);
                await Logging.CreateAuditLog(per_role, new { }, nameof(per_roleService));
                return per_role;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_roleService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_roleService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_role> Update(per_role per_role)
        {
            if (!await per_roleValidator.Update(per_role))
                return per_role;
            try
            {
                var oldData = await UOW.per_roleRepository.Get(per_role.Id);

                await UOW.Begin();
                await UOW.per_roleRepository.Update(per_role);
                await UOW.Commit();

                per_role = await UOW.per_roleRepository.Get(per_role.Id);
                await Logging.CreateAuditLog(per_role, oldData, nameof(per_roleService));
                return per_role;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_roleService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_roleService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_role> Delete(per_role per_role)
        {
            if (!await per_roleValidator.Delete(per_role))
                return per_role;

            try
            {
                await UOW.Begin();
                await UOW.per_roleRepository.Delete(per_role);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_role, nameof(per_roleService));
                return per_role;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_roleService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_roleService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_role>> BulkDelete(List<per_role> per_roles)
        {
            if (!await per_roleValidator.BulkDelete(per_roles))
                return per_roles;

            try
            {
                await UOW.Begin();
                await UOW.per_roleRepository.BulkDelete(per_roles);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_roles, nameof(per_roleService));
                return per_roles;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_roleService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_roleService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_role>> Import(List<per_role> per_roles)
        {
            if (!await per_roleValidator.Import(per_roles))
                return per_roles;
            try
            {
                await UOW.Begin();
                await UOW.per_roleRepository.BulkMerge(per_roles);
                await UOW.Commit();

                await Logging.CreateAuditLog(per_roles, new { }, nameof(per_roleService));
                return per_roles;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_roleService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_roleService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public per_roleFilter ToFilter(per_roleFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<per_roleFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                per_roleFilter subFilter = new per_roleFilter();
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
