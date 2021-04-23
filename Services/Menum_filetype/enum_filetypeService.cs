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

namespace BASE.Services.Menum_filetype
{
    public interface Ienum_filetypeService :  IServiceScoped
    {
        Task<int>
    Count(enum_filetypeFilter enum_filetypeFilter);
    Task<List<enum_filetype>> List(enum_filetypeFilter enum_filetypeFilter);
        Task<enum_filetype> Get(long Id);
        Task<enum_filetype> Create(enum_filetype enum_filetype);
        Task<enum_filetype> Update(enum_filetype enum_filetype);
        Task<enum_filetype> Delete(enum_filetype enum_filetype);
        Task<List<enum_filetype>> BulkDelete(List<enum_filetype> enum_filetypes);
        Task<List<enum_filetype>> Import(List<enum_filetype> enum_filetypes);
        enum_filetypeFilter ToFilter(enum_filetypeFilter enum_filetypeFilter);
    }

    public class enum_filetypeService : BaseService, Ienum_filetypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Ienum_filetypeValidator enum_filetypeValidator;

        public enum_filetypeService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Ienum_filetypeValidator enum_filetypeValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.enum_filetypeValidator = enum_filetypeValidator;
        }
        public async Task<int> Count(enum_filetypeFilter enum_filetypeFilter)
        {
            try
            {
                int result = await UOW.enum_filetypeRepository.Count(enum_filetypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_filetypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_filetypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_filetype>> List(enum_filetypeFilter enum_filetypeFilter)
        {
            try
            {
                List<enum_filetype> enum_filetypes = await UOW.enum_filetypeRepository.List(enum_filetypeFilter);
                return enum_filetypes;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_filetypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_filetypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<enum_filetype> Get(long Id)
        {
            enum_filetype enum_filetype = await UOW.enum_filetypeRepository.Get(Id);
            if (enum_filetype == null)
                return null;
            return enum_filetype;
        }

        public async Task<enum_filetype> Create(enum_filetype enum_filetype)
        {
            if (!await enum_filetypeValidator.Create(enum_filetype))
                return enum_filetype;

            try
            {
                await UOW.Begin();
                await UOW.enum_filetypeRepository.Create(enum_filetype);
                await UOW.Commit();
                enum_filetype = await UOW.enum_filetypeRepository.Get(enum_filetype.Id);
                await Logging.CreateAuditLog(enum_filetype, new { }, nameof(enum_filetypeService));
                return enum_filetype;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_filetypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_filetypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_filetype> Update(enum_filetype enum_filetype)
        {
            if (!await enum_filetypeValidator.Update(enum_filetype))
                return enum_filetype;
            try
            {
                var oldData = await UOW.enum_filetypeRepository.Get(enum_filetype.Id);

                await UOW.Begin();
                await UOW.enum_filetypeRepository.Update(enum_filetype);
                await UOW.Commit();

                enum_filetype = await UOW.enum_filetypeRepository.Get(enum_filetype.Id);
                await Logging.CreateAuditLog(enum_filetype, oldData, nameof(enum_filetypeService));
                return enum_filetype;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_filetypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_filetypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_filetype> Delete(enum_filetype enum_filetype)
        {
            if (!await enum_filetypeValidator.Delete(enum_filetype))
                return enum_filetype;

            try
            {
                await UOW.Begin();
                await UOW.enum_filetypeRepository.Delete(enum_filetype);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_filetype, nameof(enum_filetypeService));
                return enum_filetype;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_filetypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_filetypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_filetype>> BulkDelete(List<enum_filetype> enum_filetypes)
        {
            if (!await enum_filetypeValidator.BulkDelete(enum_filetypes))
                return enum_filetypes;

            try
            {
                await UOW.Begin();
                await UOW.enum_filetypeRepository.BulkDelete(enum_filetypes);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_filetypes, nameof(enum_filetypeService));
                return enum_filetypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_filetypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_filetypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_filetype>> Import(List<enum_filetype> enum_filetypes)
        {
            if (!await enum_filetypeValidator.Import(enum_filetypes))
                return enum_filetypes;
            try
            {
                await UOW.Begin();
                await UOW.enum_filetypeRepository.BulkMerge(enum_filetypes);
                await UOW.Commit();

                await Logging.CreateAuditLog(enum_filetypes, new { }, nameof(enum_filetypeService));
                return enum_filetypes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_filetypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_filetypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public enum_filetypeFilter ToFilter(enum_filetypeFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<enum_filetypeFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                enum_filetypeFilter subFilter = new enum_filetypeFilter();
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
