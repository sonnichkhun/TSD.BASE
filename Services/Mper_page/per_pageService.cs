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

namespace BASE.Services.Mper_page
{
    public interface Iper_pageService :  IServiceScoped
    {
        Task<int>
    Count(per_pageFilter per_pageFilter);
    Task<List<per_page>> List(per_pageFilter per_pageFilter);
        Task<per_page> Get(long Id);
        Task<per_page> Create(per_page per_page);
        Task<per_page> Update(per_page per_page);
        Task<per_page> Delete(per_page per_page);
        Task<List<per_page>> BulkDelete(List<per_page> per_pages);
        Task<List<per_page>> Import(List<per_page> per_pages);
        per_pageFilter ToFilter(per_pageFilter per_pageFilter);
    }

    public class per_pageService : BaseService, Iper_pageService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Iper_pageValidator per_pageValidator;

        public per_pageService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Iper_pageValidator per_pageValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.per_pageValidator = per_pageValidator;
        }
        public async Task<int> Count(per_pageFilter per_pageFilter)
        {
            try
            {
                int result = await UOW.per_pageRepository.Count(per_pageFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_pageService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_pageService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_page>> List(per_pageFilter per_pageFilter)
        {
            try
            {
                List<per_page> per_pages = await UOW.per_pageRepository.List(per_pageFilter);
                return per_pages;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_pageService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_pageService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<per_page> Get(long Id)
        {
            per_page per_page = await UOW.per_pageRepository.Get(Id);
            if (per_page == null)
                return null;
            return per_page;
        }

        public async Task<per_page> Create(per_page per_page)
        {
            if (!await per_pageValidator.Create(per_page))
                return per_page;

            try
            {
                await UOW.Begin();
                await UOW.per_pageRepository.Create(per_page);
                await UOW.Commit();
                per_page = await UOW.per_pageRepository.Get(per_page.Id);
                await Logging.CreateAuditLog(per_page, new { }, nameof(per_pageService));
                return per_page;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_pageService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_pageService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_page> Update(per_page per_page)
        {
            if (!await per_pageValidator.Update(per_page))
                return per_page;
            try
            {
                var oldData = await UOW.per_pageRepository.Get(per_page.Id);

                await UOW.Begin();
                await UOW.per_pageRepository.Update(per_page);
                await UOW.Commit();

                per_page = await UOW.per_pageRepository.Get(per_page.Id);
                await Logging.CreateAuditLog(per_page, oldData, nameof(per_pageService));
                return per_page;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_pageService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_pageService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_page> Delete(per_page per_page)
        {
            if (!await per_pageValidator.Delete(per_page))
                return per_page;

            try
            {
                await UOW.Begin();
                await UOW.per_pageRepository.Delete(per_page);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_page, nameof(per_pageService));
                return per_page;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_pageService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_pageService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_page>> BulkDelete(List<per_page> per_pages)
        {
            if (!await per_pageValidator.BulkDelete(per_pages))
                return per_pages;

            try
            {
                await UOW.Begin();
                await UOW.per_pageRepository.BulkDelete(per_pages);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_pages, nameof(per_pageService));
                return per_pages;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_pageService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_pageService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_page>> Import(List<per_page> per_pages)
        {
            if (!await per_pageValidator.Import(per_pages))
                return per_pages;
            try
            {
                await UOW.Begin();
                await UOW.per_pageRepository.BulkMerge(per_pages);
                await UOW.Commit();

                await Logging.CreateAuditLog(per_pages, new { }, nameof(per_pageService));
                return per_pages;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_pageService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_pageService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public per_pageFilter ToFilter(per_pageFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<per_pageFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                per_pageFilter subFilter = new per_pageFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Path))
                        
                        
                        
                        
                        
                        
                        subFilter.Path = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
