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

namespace BASE.Services.Mper_menu
{
    public interface Iper_menuService :  IServiceScoped
    {
        Task<int>
    Count(per_menuFilter per_menuFilter);
    Task<List<per_menu>> List(per_menuFilter per_menuFilter);
        Task<per_menu> Get(long Id);
        Task<per_menu> Create(per_menu per_menu);
        Task<per_menu> Update(per_menu per_menu);
        Task<per_menu> Delete(per_menu per_menu);
        Task<List<per_menu>> BulkDelete(List<per_menu> per_menus);
        Task<List<per_menu>> Import(List<per_menu> per_menus);
        per_menuFilter ToFilter(per_menuFilter per_menuFilter);
    }

    public class per_menuService : BaseService, Iper_menuService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Iper_menuValidator per_menuValidator;

        public per_menuService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Iper_menuValidator per_menuValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.per_menuValidator = per_menuValidator;
        }
        public async Task<int> Count(per_menuFilter per_menuFilter)
        {
            try
            {
                int result = await UOW.per_menuRepository.Count(per_menuFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_menuService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_menuService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_menu>> List(per_menuFilter per_menuFilter)
        {
            try
            {
                List<per_menu> per_menus = await UOW.per_menuRepository.List(per_menuFilter);
                return per_menus;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_menuService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_menuService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<per_menu> Get(long Id)
        {
            per_menu per_menu = await UOW.per_menuRepository.Get(Id);
            if (per_menu == null)
                return null;
            return per_menu;
        }

        public async Task<per_menu> Create(per_menu per_menu)
        {
            if (!await per_menuValidator.Create(per_menu))
                return per_menu;

            try
            {
                await UOW.Begin();
                await UOW.per_menuRepository.Create(per_menu);
                await UOW.Commit();
                per_menu = await UOW.per_menuRepository.Get(per_menu.Id);
                await Logging.CreateAuditLog(per_menu, new { }, nameof(per_menuService));
                return per_menu;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_menuService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_menuService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_menu> Update(per_menu per_menu)
        {
            if (!await per_menuValidator.Update(per_menu))
                return per_menu;
            try
            {
                var oldData = await UOW.per_menuRepository.Get(per_menu.Id);

                await UOW.Begin();
                await UOW.per_menuRepository.Update(per_menu);
                await UOW.Commit();

                per_menu = await UOW.per_menuRepository.Get(per_menu.Id);
                await Logging.CreateAuditLog(per_menu, oldData, nameof(per_menuService));
                return per_menu;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_menuService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_menuService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_menu> Delete(per_menu per_menu)
        {
            if (!await per_menuValidator.Delete(per_menu))
                return per_menu;

            try
            {
                await UOW.Begin();
                await UOW.per_menuRepository.Delete(per_menu);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_menu, nameof(per_menuService));
                return per_menu;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_menuService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_menuService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_menu>> BulkDelete(List<per_menu> per_menus)
        {
            if (!await per_menuValidator.BulkDelete(per_menus))
                return per_menus;

            try
            {
                await UOW.Begin();
                await UOW.per_menuRepository.BulkDelete(per_menus);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_menus, nameof(per_menuService));
                return per_menus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_menuService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_menuService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_menu>> Import(List<per_menu> per_menus)
        {
            if (!await per_menuValidator.Import(per_menus))
                return per_menus;
            try
            {
                await UOW.Begin();
                await UOW.per_menuRepository.BulkMerge(per_menus);
                await UOW.Commit();

                await Logging.CreateAuditLog(per_menus, new { }, nameof(per_menuService));
                return per_menus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_menuService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_menuService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public per_menuFilter ToFilter(per_menuFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<per_menuFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                per_menuFilter subFilter = new per_menuFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Path))
                        
                        
                        
                        
                        
                        
                        subFilter.Path = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
