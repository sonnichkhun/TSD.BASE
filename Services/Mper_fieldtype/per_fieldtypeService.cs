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

namespace BASE.Services.Mper_fieldtype
{
    public interface Iper_fieldtypeService :  IServiceScoped
    {
        Task<int>
    Count(per_fieldtypeFilter per_fieldtypeFilter);
    Task<List<per_fieldtype>> List(per_fieldtypeFilter per_fieldtypeFilter);
        Task<per_fieldtype> Get(long Id);
        Task<per_fieldtype> Create(per_fieldtype per_fieldtype);
        Task<per_fieldtype> Update(per_fieldtype per_fieldtype);
        Task<per_fieldtype> Delete(per_fieldtype per_fieldtype);
        Task<List<per_fieldtype>> BulkDelete(List<per_fieldtype> per_fieldtypes);
        Task<List<per_fieldtype>> Import(List<per_fieldtype> per_fieldtypes);
        per_fieldtypeFilter ToFilter(per_fieldtypeFilter per_fieldtypeFilter);
    }

    public class per_fieldtypeService : BaseService, Iper_fieldtypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Iper_fieldtypeValidator per_fieldtypeValidator;

        public per_fieldtypeService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Iper_fieldtypeValidator per_fieldtypeValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.per_fieldtypeValidator = per_fieldtypeValidator;
        }
        public async Task<int> Count(per_fieldtypeFilter per_fieldtypeFilter)
        {
            try
            {
                int result = await UOW.per_fieldtypeRepository.Count(per_fieldtypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_fieldtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_fieldtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_fieldtype>> List(per_fieldtypeFilter per_fieldtypeFilter)
        {
            try
            {
                List<per_fieldtype> per_fieldtypes = await UOW.per_fieldtypeRepository.List(per_fieldtypeFilter);
                return per_fieldtypes;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_fieldtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_fieldtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<per_fieldtype> Get(long Id)
        {
            per_fieldtype per_fieldtype = await UOW.per_fieldtypeRepository.Get(Id);
            if (per_fieldtype == null)
                return null;
            return per_fieldtype;
        }

        public async Task<per_fieldtype> Create(per_fieldtype per_fieldtype)
        {
            if (!await per_fieldtypeValidator.Create(per_fieldtype))
                return per_fieldtype;

            try
            {
                await UOW.Begin();
                await UOW.per_fieldtypeRepository.Create(per_fieldtype);
                await UOW.Commit();
                per_fieldtype = await UOW.per_fieldtypeRepository.Get(per_fieldtype.Id);
                await Logging.CreateAuditLog(per_fieldtype, new { }, nameof(per_fieldtypeService));
                return per_fieldtype;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_fieldtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_fieldtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_fieldtype> Update(per_fieldtype per_fieldtype)
        {
            if (!await per_fieldtypeValidator.Update(per_fieldtype))
                return per_fieldtype;
            try
            {
                var oldData = await UOW.per_fieldtypeRepository.Get(per_fieldtype.Id);

                await UOW.Begin();
                await UOW.per_fieldtypeRepository.Update(per_fieldtype);
                await UOW.Commit();

                per_fieldtype = await UOW.per_fieldtypeRepository.Get(per_fieldtype.Id);
                await Logging.CreateAuditLog(per_fieldtype, oldData, nameof(per_fieldtypeService));
                return per_fieldtype;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_fieldtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_fieldtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_fieldtype> Delete(per_fieldtype per_fieldtype)
        {
            if (!await per_fieldtypeValidator.Delete(per_fieldtype))
                return per_fieldtype;

            try
            {
                await UOW.Begin();
                await UOW.per_fieldtypeRepository.Delete(per_fieldtype);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_fieldtype, nameof(per_fieldtypeService));
                return per_fieldtype;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_fieldtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_fieldtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_fieldtype>> BulkDelete(List<per_fieldtype> per_fieldtypes)
        {
            if (!await per_fieldtypeValidator.BulkDelete(per_fieldtypes))
                return per_fieldtypes;

            try
            {
                await UOW.Begin();
                await UOW.per_fieldtypeRepository.BulkDelete(per_fieldtypes);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_fieldtypes, nameof(per_fieldtypeService));
                return per_fieldtypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_fieldtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_fieldtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_fieldtype>> Import(List<per_fieldtype> per_fieldtypes)
        {
            if (!await per_fieldtypeValidator.Import(per_fieldtypes))
                return per_fieldtypes;
            try
            {
                await UOW.Begin();
                await UOW.per_fieldtypeRepository.BulkMerge(per_fieldtypes);
                await UOW.Commit();

                await Logging.CreateAuditLog(per_fieldtypes, new { }, nameof(per_fieldtypeService));
                return per_fieldtypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_fieldtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_fieldtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public per_fieldtypeFilter ToFilter(per_fieldtypeFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<per_fieldtypeFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                per_fieldtypeFilter subFilter = new per_fieldtypeFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
