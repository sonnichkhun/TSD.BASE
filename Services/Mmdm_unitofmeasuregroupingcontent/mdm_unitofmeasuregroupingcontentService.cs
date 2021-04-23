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

namespace BASE.Services.Mmdm_unitofmeasuregroupingcontent
{
    public interface Imdm_unitofmeasuregroupingcontentService :  IServiceScoped
    {
        Task<int>
    Count(mdm_unitofmeasuregroupingcontentFilter mdm_unitofmeasuregroupingcontentFilter);
    Task<List<mdm_unitofmeasuregroupingcontent>> List(mdm_unitofmeasuregroupingcontentFilter mdm_unitofmeasuregroupingcontentFilter);
        Task<mdm_unitofmeasuregroupingcontent> Get(long Id);
        Task<mdm_unitofmeasuregroupingcontent> Create(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent);
        Task<mdm_unitofmeasuregroupingcontent> Update(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent);
        Task<mdm_unitofmeasuregroupingcontent> Delete(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent);
        Task<List<mdm_unitofmeasuregroupingcontent>> BulkDelete(List<mdm_unitofmeasuregroupingcontent> mdm_unitofmeasuregroupingcontents);
        Task<List<mdm_unitofmeasuregroupingcontent>> Import(List<mdm_unitofmeasuregroupingcontent> mdm_unitofmeasuregroupingcontents);
        mdm_unitofmeasuregroupingcontentFilter ToFilter(mdm_unitofmeasuregroupingcontentFilter mdm_unitofmeasuregroupingcontentFilter);
    }

    public class mdm_unitofmeasuregroupingcontentService : BaseService, Imdm_unitofmeasuregroupingcontentService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Imdm_unitofmeasuregroupingcontentValidator mdm_unitofmeasuregroupingcontentValidator;

        public mdm_unitofmeasuregroupingcontentService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Imdm_unitofmeasuregroupingcontentValidator mdm_unitofmeasuregroupingcontentValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.mdm_unitofmeasuregroupingcontentValidator = mdm_unitofmeasuregroupingcontentValidator;
        }
        public async Task<int> Count(mdm_unitofmeasuregroupingcontentFilter mdm_unitofmeasuregroupingcontentFilter)
        {
            try
            {
                int result = await UOW.mdm_unitofmeasuregroupingcontentRepository.Count(mdm_unitofmeasuregroupingcontentFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasuregroupingcontentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasuregroupingcontentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_unitofmeasuregroupingcontent>> List(mdm_unitofmeasuregroupingcontentFilter mdm_unitofmeasuregroupingcontentFilter)
        {
            try
            {
                List<mdm_unitofmeasuregroupingcontent> mdm_unitofmeasuregroupingcontents = await UOW.mdm_unitofmeasuregroupingcontentRepository.List(mdm_unitofmeasuregroupingcontentFilter);
                return mdm_unitofmeasuregroupingcontents;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasuregroupingcontentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasuregroupingcontentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<mdm_unitofmeasuregroupingcontent> Get(long Id)
        {
            mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent = await UOW.mdm_unitofmeasuregroupingcontentRepository.Get(Id);
            if (mdm_unitofmeasuregroupingcontent == null)
                return null;
            return mdm_unitofmeasuregroupingcontent;
        }

        public async Task<mdm_unitofmeasuregroupingcontent> Create(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent)
        {
            if (!await mdm_unitofmeasuregroupingcontentValidator.Create(mdm_unitofmeasuregroupingcontent))
                return mdm_unitofmeasuregroupingcontent;

            try
            {
                await UOW.Begin();
                await UOW.mdm_unitofmeasuregroupingcontentRepository.Create(mdm_unitofmeasuregroupingcontent);
                await UOW.Commit();
                mdm_unitofmeasuregroupingcontent = await UOW.mdm_unitofmeasuregroupingcontentRepository.Get(mdm_unitofmeasuregroupingcontent.Id);
                await Logging.CreateAuditLog(mdm_unitofmeasuregroupingcontent, new { }, nameof(mdm_unitofmeasuregroupingcontentService));
                return mdm_unitofmeasuregroupingcontent;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasuregroupingcontentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasuregroupingcontentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_unitofmeasuregroupingcontent> Update(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent)
        {
            if (!await mdm_unitofmeasuregroupingcontentValidator.Update(mdm_unitofmeasuregroupingcontent))
                return mdm_unitofmeasuregroupingcontent;
            try
            {
                var oldData = await UOW.mdm_unitofmeasuregroupingcontentRepository.Get(mdm_unitofmeasuregroupingcontent.Id);

                await UOW.Begin();
                await UOW.mdm_unitofmeasuregroupingcontentRepository.Update(mdm_unitofmeasuregroupingcontent);
                await UOW.Commit();

                mdm_unitofmeasuregroupingcontent = await UOW.mdm_unitofmeasuregroupingcontentRepository.Get(mdm_unitofmeasuregroupingcontent.Id);
                await Logging.CreateAuditLog(mdm_unitofmeasuregroupingcontent, oldData, nameof(mdm_unitofmeasuregroupingcontentService));
                return mdm_unitofmeasuregroupingcontent;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasuregroupingcontentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasuregroupingcontentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_unitofmeasuregroupingcontent> Delete(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent)
        {
            if (!await mdm_unitofmeasuregroupingcontentValidator.Delete(mdm_unitofmeasuregroupingcontent))
                return mdm_unitofmeasuregroupingcontent;

            try
            {
                await UOW.Begin();
                await UOW.mdm_unitofmeasuregroupingcontentRepository.Delete(mdm_unitofmeasuregroupingcontent);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_unitofmeasuregroupingcontent, nameof(mdm_unitofmeasuregroupingcontentService));
                return mdm_unitofmeasuregroupingcontent;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasuregroupingcontentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasuregroupingcontentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_unitofmeasuregroupingcontent>> BulkDelete(List<mdm_unitofmeasuregroupingcontent> mdm_unitofmeasuregroupingcontents)
        {
            if (!await mdm_unitofmeasuregroupingcontentValidator.BulkDelete(mdm_unitofmeasuregroupingcontents))
                return mdm_unitofmeasuregroupingcontents;

            try
            {
                await UOW.Begin();
                await UOW.mdm_unitofmeasuregroupingcontentRepository.BulkDelete(mdm_unitofmeasuregroupingcontents);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_unitofmeasuregroupingcontents, nameof(mdm_unitofmeasuregroupingcontentService));
                return mdm_unitofmeasuregroupingcontents;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasuregroupingcontentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasuregroupingcontentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_unitofmeasuregroupingcontent>> Import(List<mdm_unitofmeasuregroupingcontent> mdm_unitofmeasuregroupingcontents)
        {
            if (!await mdm_unitofmeasuregroupingcontentValidator.Import(mdm_unitofmeasuregroupingcontents))
                return mdm_unitofmeasuregroupingcontents;
            try
            {
                await UOW.Begin();
                await UOW.mdm_unitofmeasuregroupingcontentRepository.BulkMerge(mdm_unitofmeasuregroupingcontents);
                await UOW.Commit();

                await Logging.CreateAuditLog(mdm_unitofmeasuregroupingcontents, new { }, nameof(mdm_unitofmeasuregroupingcontentService));
                return mdm_unitofmeasuregroupingcontents;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_unitofmeasuregroupingcontentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_unitofmeasuregroupingcontentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public mdm_unitofmeasuregroupingcontentFilter ToFilter(mdm_unitofmeasuregroupingcontentFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<mdm_unitofmeasuregroupingcontentFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                mdm_unitofmeasuregroupingcontentFilter subFilter = new mdm_unitofmeasuregroupingcontentFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.UnitOfMeasureGroupingId))
                        subFilter.UnitOfMeasureGroupingId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.UnitOfMeasureId))
                        subFilter.UnitOfMeasureId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Factor))
                        
                        subFilter.Factor = FilterPermissionDefinition.LongFilter;
                        
                        
                        
                        
                        
                        
                }
            }
            return filter;
        }
    }
}
