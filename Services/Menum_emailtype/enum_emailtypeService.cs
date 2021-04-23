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

namespace BASE.Services.Menum_emailtype
{
    public interface Ienum_emailtypeService :  IServiceScoped
    {
        Task<int>
    Count(enum_emailtypeFilter enum_emailtypeFilter);
    Task<List<enum_emailtype>> List(enum_emailtypeFilter enum_emailtypeFilter);
        Task<enum_emailtype> Get(long Id);
        Task<enum_emailtype> Create(enum_emailtype enum_emailtype);
        Task<enum_emailtype> Update(enum_emailtype enum_emailtype);
        Task<enum_emailtype> Delete(enum_emailtype enum_emailtype);
        Task<List<enum_emailtype>> BulkDelete(List<enum_emailtype> enum_emailtypes);
        Task<List<enum_emailtype>> Import(List<enum_emailtype> enum_emailtypes);
        enum_emailtypeFilter ToFilter(enum_emailtypeFilter enum_emailtypeFilter);
    }

    public class enum_emailtypeService : BaseService, Ienum_emailtypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Ienum_emailtypeValidator enum_emailtypeValidator;

        public enum_emailtypeService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Ienum_emailtypeValidator enum_emailtypeValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.enum_emailtypeValidator = enum_emailtypeValidator;
        }
        public async Task<int> Count(enum_emailtypeFilter enum_emailtypeFilter)
        {
            try
            {
                int result = await UOW.enum_emailtypeRepository.Count(enum_emailtypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_emailtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_emailtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_emailtype>> List(enum_emailtypeFilter enum_emailtypeFilter)
        {
            try
            {
                List<enum_emailtype> enum_emailtypes = await UOW.enum_emailtypeRepository.List(enum_emailtypeFilter);
                return enum_emailtypes;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_emailtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_emailtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<enum_emailtype> Get(long Id)
        {
            enum_emailtype enum_emailtype = await UOW.enum_emailtypeRepository.Get(Id);
            if (enum_emailtype == null)
                return null;
            return enum_emailtype;
        }

        public async Task<enum_emailtype> Create(enum_emailtype enum_emailtype)
        {
            if (!await enum_emailtypeValidator.Create(enum_emailtype))
                return enum_emailtype;

            try
            {
                await UOW.Begin();
                await UOW.enum_emailtypeRepository.Create(enum_emailtype);
                await UOW.Commit();
                enum_emailtype = await UOW.enum_emailtypeRepository.Get(enum_emailtype.Id);
                await Logging.CreateAuditLog(enum_emailtype, new { }, nameof(enum_emailtypeService));
                return enum_emailtype;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_emailtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_emailtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_emailtype> Update(enum_emailtype enum_emailtype)
        {
            if (!await enum_emailtypeValidator.Update(enum_emailtype))
                return enum_emailtype;
            try
            {
                var oldData = await UOW.enum_emailtypeRepository.Get(enum_emailtype.Id);

                await UOW.Begin();
                await UOW.enum_emailtypeRepository.Update(enum_emailtype);
                await UOW.Commit();

                enum_emailtype = await UOW.enum_emailtypeRepository.Get(enum_emailtype.Id);
                await Logging.CreateAuditLog(enum_emailtype, oldData, nameof(enum_emailtypeService));
                return enum_emailtype;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_emailtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_emailtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_emailtype> Delete(enum_emailtype enum_emailtype)
        {
            if (!await enum_emailtypeValidator.Delete(enum_emailtype))
                return enum_emailtype;

            try
            {
                await UOW.Begin();
                await UOW.enum_emailtypeRepository.Delete(enum_emailtype);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_emailtype, nameof(enum_emailtypeService));
                return enum_emailtype;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_emailtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_emailtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_emailtype>> BulkDelete(List<enum_emailtype> enum_emailtypes)
        {
            if (!await enum_emailtypeValidator.BulkDelete(enum_emailtypes))
                return enum_emailtypes;

            try
            {
                await UOW.Begin();
                await UOW.enum_emailtypeRepository.BulkDelete(enum_emailtypes);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_emailtypes, nameof(enum_emailtypeService));
                return enum_emailtypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_emailtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_emailtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_emailtype>> Import(List<enum_emailtype> enum_emailtypes)
        {
            if (!await enum_emailtypeValidator.Import(enum_emailtypes))
                return enum_emailtypes;
            try
            {
                await UOW.Begin();
                await UOW.enum_emailtypeRepository.BulkMerge(enum_emailtypes);
                await UOW.Commit();

                await Logging.CreateAuditLog(enum_emailtypes, new { }, nameof(enum_emailtypeService));
                return enum_emailtypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_emailtypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_emailtypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public enum_emailtypeFilter ToFilter(enum_emailtypeFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<enum_emailtypeFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                enum_emailtypeFilter subFilter = new enum_emailtypeFilter();
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
