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

namespace BASE.Services.Mmdm_phonetype
{
    public interface Imdm_phonetypeService :  IServiceScoped
    {
        Task<int>
    Count(mdm_phonetypeFilter mdm_phonetypeFilter);
    Task<List<mdm_phonetype>> List(mdm_phonetypeFilter mdm_phonetypeFilter);
        Task<mdm_phonetype> Get(long Id);
        Task<mdm_phonetype> Create(mdm_phonetype mdm_phonetype);
        Task<mdm_phonetype> Update(mdm_phonetype mdm_phonetype);
        Task<mdm_phonetype> Delete(mdm_phonetype mdm_phonetype);
        Task<List<mdm_phonetype>> BulkDelete(List<mdm_phonetype> mdm_phonetypes);
        Task<List<mdm_phonetype>> Import(List<mdm_phonetype> mdm_phonetypes);
        mdm_phonetypeFilter ToFilter(mdm_phonetypeFilter mdm_phonetypeFilter);
    }

    public class mdm_phonetypeService : BaseService, Imdm_phonetypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Imdm_phonetypeValidator mdm_phonetypeValidator;

        public mdm_phonetypeService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Imdm_phonetypeValidator mdm_phonetypeValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.mdm_phonetypeValidator = mdm_phonetypeValidator;
        }
        public async Task<int> Count(mdm_phonetypeFilter mdm_phonetypeFilter)
        {
            try
            {
                int result = await UOW.mdm_phonetypeRepository.Count(mdm_phonetypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_phonetypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_phonetypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_phonetype>> List(mdm_phonetypeFilter mdm_phonetypeFilter)
        {
            try
            {
                List<mdm_phonetype> mdm_phonetypes = await UOW.mdm_phonetypeRepository.List(mdm_phonetypeFilter);
                return mdm_phonetypes;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_phonetypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_phonetypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<mdm_phonetype> Get(long Id)
        {
            mdm_phonetype mdm_phonetype = await UOW.mdm_phonetypeRepository.Get(Id);
            if (mdm_phonetype == null)
                return null;
            return mdm_phonetype;
        }

        public async Task<mdm_phonetype> Create(mdm_phonetype mdm_phonetype)
        {
            if (!await mdm_phonetypeValidator.Create(mdm_phonetype))
                return mdm_phonetype;

            try
            {
                await UOW.Begin();
                await UOW.mdm_phonetypeRepository.Create(mdm_phonetype);
                await UOW.Commit();
                mdm_phonetype = await UOW.mdm_phonetypeRepository.Get(mdm_phonetype.Id);
                await Logging.CreateAuditLog(mdm_phonetype, new { }, nameof(mdm_phonetypeService));
                return mdm_phonetype;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_phonetypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_phonetypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_phonetype> Update(mdm_phonetype mdm_phonetype)
        {
            if (!await mdm_phonetypeValidator.Update(mdm_phonetype))
                return mdm_phonetype;
            try
            {
                var oldData = await UOW.mdm_phonetypeRepository.Get(mdm_phonetype.Id);

                await UOW.Begin();
                await UOW.mdm_phonetypeRepository.Update(mdm_phonetype);
                await UOW.Commit();

                mdm_phonetype = await UOW.mdm_phonetypeRepository.Get(mdm_phonetype.Id);
                await Logging.CreateAuditLog(mdm_phonetype, oldData, nameof(mdm_phonetypeService));
                return mdm_phonetype;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_phonetypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_phonetypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_phonetype> Delete(mdm_phonetype mdm_phonetype)
        {
            if (!await mdm_phonetypeValidator.Delete(mdm_phonetype))
                return mdm_phonetype;

            try
            {
                await UOW.Begin();
                await UOW.mdm_phonetypeRepository.Delete(mdm_phonetype);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_phonetype, nameof(mdm_phonetypeService));
                return mdm_phonetype;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_phonetypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_phonetypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_phonetype>> BulkDelete(List<mdm_phonetype> mdm_phonetypes)
        {
            if (!await mdm_phonetypeValidator.BulkDelete(mdm_phonetypes))
                return mdm_phonetypes;

            try
            {
                await UOW.Begin();
                await UOW.mdm_phonetypeRepository.BulkDelete(mdm_phonetypes);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_phonetypes, nameof(mdm_phonetypeService));
                return mdm_phonetypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_phonetypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_phonetypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_phonetype>> Import(List<mdm_phonetype> mdm_phonetypes)
        {
            if (!await mdm_phonetypeValidator.Import(mdm_phonetypes))
                return mdm_phonetypes;
            try
            {
                await UOW.Begin();
                await UOW.mdm_phonetypeRepository.BulkMerge(mdm_phonetypes);
                await UOW.Commit();

                await Logging.CreateAuditLog(mdm_phonetypes, new { }, nameof(mdm_phonetypeService));
                return mdm_phonetypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_phonetypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_phonetypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public mdm_phonetypeFilter ToFilter(mdm_phonetypeFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<mdm_phonetypeFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                mdm_phonetypeFilter subFilter = new mdm_phonetypeFilter();
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
