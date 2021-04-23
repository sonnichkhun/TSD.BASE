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

namespace BASE.Services.Mper_permissioncontent
{
    public interface Iper_permissioncontentService :  IServiceScoped
    {
        Task<int>
    Count(per_permissioncontentFilter per_permissioncontentFilter);
    Task<List<per_permissioncontent>> List(per_permissioncontentFilter per_permissioncontentFilter);
        Task<per_permissioncontent> Get(long Id);
        Task<per_permissioncontent> Create(per_permissioncontent per_permissioncontent);
        Task<per_permissioncontent> Update(per_permissioncontent per_permissioncontent);
        Task<per_permissioncontent> Delete(per_permissioncontent per_permissioncontent);
        Task<List<per_permissioncontent>> BulkDelete(List<per_permissioncontent> per_permissioncontents);
        Task<List<per_permissioncontent>> Import(List<per_permissioncontent> per_permissioncontents);
        per_permissioncontentFilter ToFilter(per_permissioncontentFilter per_permissioncontentFilter);
    }

    public class per_permissioncontentService : BaseService, Iper_permissioncontentService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Iper_permissioncontentValidator per_permissioncontentValidator;

        public per_permissioncontentService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Iper_permissioncontentValidator per_permissioncontentValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.per_permissioncontentValidator = per_permissioncontentValidator;
        }
        public async Task<int> Count(per_permissioncontentFilter per_permissioncontentFilter)
        {
            try
            {
                int result = await UOW.per_permissioncontentRepository.Count(per_permissioncontentFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissioncontentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissioncontentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_permissioncontent>> List(per_permissioncontentFilter per_permissioncontentFilter)
        {
            try
            {
                List<per_permissioncontent> per_permissioncontents = await UOW.per_permissioncontentRepository.List(per_permissioncontentFilter);
                return per_permissioncontents;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissioncontentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissioncontentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<per_permissioncontent> Get(long Id)
        {
            per_permissioncontent per_permissioncontent = await UOW.per_permissioncontentRepository.Get(Id);
            if (per_permissioncontent == null)
                return null;
            return per_permissioncontent;
        }

        public async Task<per_permissioncontent> Create(per_permissioncontent per_permissioncontent)
        {
            if (!await per_permissioncontentValidator.Create(per_permissioncontent))
                return per_permissioncontent;

            try
            {
                await UOW.Begin();
                await UOW.per_permissioncontentRepository.Create(per_permissioncontent);
                await UOW.Commit();
                per_permissioncontent = await UOW.per_permissioncontentRepository.Get(per_permissioncontent.Id);
                await Logging.CreateAuditLog(per_permissioncontent, new { }, nameof(per_permissioncontentService));
                return per_permissioncontent;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissioncontentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissioncontentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_permissioncontent> Update(per_permissioncontent per_permissioncontent)
        {
            if (!await per_permissioncontentValidator.Update(per_permissioncontent))
                return per_permissioncontent;
            try
            {
                var oldData = await UOW.per_permissioncontentRepository.Get(per_permissioncontent.Id);

                await UOW.Begin();
                await UOW.per_permissioncontentRepository.Update(per_permissioncontent);
                await UOW.Commit();

                per_permissioncontent = await UOW.per_permissioncontentRepository.Get(per_permissioncontent.Id);
                await Logging.CreateAuditLog(per_permissioncontent, oldData, nameof(per_permissioncontentService));
                return per_permissioncontent;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissioncontentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissioncontentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_permissioncontent> Delete(per_permissioncontent per_permissioncontent)
        {
            if (!await per_permissioncontentValidator.Delete(per_permissioncontent))
                return per_permissioncontent;

            try
            {
                await UOW.Begin();
                await UOW.per_permissioncontentRepository.Delete(per_permissioncontent);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_permissioncontent, nameof(per_permissioncontentService));
                return per_permissioncontent;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissioncontentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissioncontentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_permissioncontent>> BulkDelete(List<per_permissioncontent> per_permissioncontents)
        {
            if (!await per_permissioncontentValidator.BulkDelete(per_permissioncontents))
                return per_permissioncontents;

            try
            {
                await UOW.Begin();
                await UOW.per_permissioncontentRepository.BulkDelete(per_permissioncontents);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_permissioncontents, nameof(per_permissioncontentService));
                return per_permissioncontents;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissioncontentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissioncontentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_permissioncontent>> Import(List<per_permissioncontent> per_permissioncontents)
        {
            if (!await per_permissioncontentValidator.Import(per_permissioncontents))
                return per_permissioncontents;
            try
            {
                await UOW.Begin();
                await UOW.per_permissioncontentRepository.BulkMerge(per_permissioncontents);
                await UOW.Commit();

                await Logging.CreateAuditLog(per_permissioncontents, new { }, nameof(per_permissioncontentService));
                return per_permissioncontents;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissioncontentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissioncontentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public per_permissioncontentFilter ToFilter(per_permissioncontentFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<per_permissioncontentFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                per_permissioncontentFilter subFilter = new per_permissioncontentFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.PermissionId))
                        subFilter.PermissionId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.FieldId))
                        subFilter.FieldId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.PermissionOperatorId))
                        subFilter.PermissionOperatorId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Value))
                        
                        
                        
                        
                        
                        
                        subFilter.Value = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
