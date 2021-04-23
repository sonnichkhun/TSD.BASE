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

namespace BASE.Services.Mmdm_taxtype
{
    public interface Imdm_taxtypeService :  IServiceScoped
    {
        Task<int>
    Count(mdm_taxtypeFilter mdm_taxtypeFilter);
    Task<List<mdm_taxtype>> List(mdm_taxtypeFilter mdm_taxtypeFilter);
        Task<mdm_taxtype> Get(long Id);
        Task<mdm_taxtype> Create(mdm_taxtype mdm_taxtype);
        Task<mdm_taxtype> Update(mdm_taxtype mdm_taxtype);
        Task<mdm_taxtype> Delete(mdm_taxtype mdm_taxtype);
        Task<List<mdm_taxtype>> BulkDelete(List<mdm_taxtype> mdm_taxtypes);
        Task<List<mdm_taxtype>> Import(List<mdm_taxtype> mdm_taxtypes);
        mdm_taxtypeFilter ToFilter(mdm_taxtypeFilter mdm_taxtypeFilter);
    }

    public class mdm_taxtypeService : BaseService, Imdm_taxtypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Imdm_taxtypeValidator mdm_taxtypeValidator;

        public mdm_taxtypeService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Imdm_taxtypeValidator mdm_taxtypeValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.mdm_taxtypeValidator = mdm_taxtypeValidator;
        }
        public async Task<int> Count(mdm_taxtypeFilter mdm_taxtypeFilter)
        {
            try
            {
                int result = await UOW.mdm_taxtypeRepository.Count(mdm_taxtypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_taxtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_taxtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_taxtype>> List(mdm_taxtypeFilter mdm_taxtypeFilter)
        {
            try
            {
                List<mdm_taxtype> mdm_taxtypes = await UOW.mdm_taxtypeRepository.List(mdm_taxtypeFilter);
                return mdm_taxtypes;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_taxtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_taxtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<mdm_taxtype> Get(long Id)
        {
            mdm_taxtype mdm_taxtype = await UOW.mdm_taxtypeRepository.Get(Id);
            if (mdm_taxtype == null)
                return null;
            return mdm_taxtype;
        }

        public async Task<mdm_taxtype> Create(mdm_taxtype mdm_taxtype)
        {
            if (!await mdm_taxtypeValidator.Create(mdm_taxtype))
                return mdm_taxtype;

            try
            {
                await UOW.Begin();
                await UOW.mdm_taxtypeRepository.Create(mdm_taxtype);
                await UOW.Commit();
                mdm_taxtype = await UOW.mdm_taxtypeRepository.Get(mdm_taxtype.Id);
                await Logging.CreateAuditLog(mdm_taxtype, new { }, nameof(mdm_taxtypeService));
                return mdm_taxtype;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_taxtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_taxtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_taxtype> Update(mdm_taxtype mdm_taxtype)
        {
            if (!await mdm_taxtypeValidator.Update(mdm_taxtype))
                return mdm_taxtype;
            try
            {
                var oldData = await UOW.mdm_taxtypeRepository.Get(mdm_taxtype.Id);

                await UOW.Begin();
                await UOW.mdm_taxtypeRepository.Update(mdm_taxtype);
                await UOW.Commit();

                mdm_taxtype = await UOW.mdm_taxtypeRepository.Get(mdm_taxtype.Id);
                await Logging.CreateAuditLog(mdm_taxtype, oldData, nameof(mdm_taxtypeService));
                return mdm_taxtype;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_taxtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_taxtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_taxtype> Delete(mdm_taxtype mdm_taxtype)
        {
            if (!await mdm_taxtypeValidator.Delete(mdm_taxtype))
                return mdm_taxtype;

            try
            {
                await UOW.Begin();
                await UOW.mdm_taxtypeRepository.Delete(mdm_taxtype);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_taxtype, nameof(mdm_taxtypeService));
                return mdm_taxtype;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_taxtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_taxtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_taxtype>> BulkDelete(List<mdm_taxtype> mdm_taxtypes)
        {
            if (!await mdm_taxtypeValidator.BulkDelete(mdm_taxtypes))
                return mdm_taxtypes;

            try
            {
                await UOW.Begin();
                await UOW.mdm_taxtypeRepository.BulkDelete(mdm_taxtypes);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_taxtypes, nameof(mdm_taxtypeService));
                return mdm_taxtypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_taxtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_taxtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_taxtype>> Import(List<mdm_taxtype> mdm_taxtypes)
        {
            if (!await mdm_taxtypeValidator.Import(mdm_taxtypes))
                return mdm_taxtypes;
            try
            {
                await UOW.Begin();
                await UOW.mdm_taxtypeRepository.BulkMerge(mdm_taxtypes);
                await UOW.Commit();

                await Logging.CreateAuditLog(mdm_taxtypes, new { }, nameof(mdm_taxtypeService));
                return mdm_taxtypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_taxtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_taxtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public mdm_taxtypeFilter ToFilter(mdm_taxtypeFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<mdm_taxtypeFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                mdm_taxtypeFilter subFilter = new mdm_taxtypeFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Percentage))
                        
                        
                        subFilter.Percentage = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
