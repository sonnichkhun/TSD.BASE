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

namespace BASE.Services.Mdbo_file
{
    public interface Idbo_fileService :  IServiceScoped
    {
        Task<int>
    Count(dbo_fileFilter dbo_fileFilter);
    Task<List<dbo_file>> List(dbo_fileFilter dbo_fileFilter);
        Task<dbo_file> Get(long Id);
        Task<dbo_file> Create(dbo_file dbo_file);
        Task<dbo_file> Update(dbo_file dbo_file);
        Task<dbo_file> Delete(dbo_file dbo_file);
        Task<List<dbo_file>> BulkDelete(List<dbo_file> dbo_files);
        Task<List<dbo_file>> Import(List<dbo_file> dbo_files);
        dbo_fileFilter ToFilter(dbo_fileFilter dbo_fileFilter);
    }

    public class dbo_fileService : BaseService, Idbo_fileService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Idbo_fileValidator dbo_fileValidator;

        public dbo_fileService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Idbo_fileValidator dbo_fileValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.dbo_fileValidator = dbo_fileValidator;
        }
        public async Task<int> Count(dbo_fileFilter dbo_fileFilter)
        {
            try
            {
                int result = await UOW.dbo_fileRepository.Count(dbo_fileFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_fileService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_fileService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<dbo_file>> List(dbo_fileFilter dbo_fileFilter)
        {
            try
            {
                List<dbo_file> dbo_files = await UOW.dbo_fileRepository.List(dbo_fileFilter);
                return dbo_files;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_fileService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_fileService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<dbo_file> Get(long Id)
        {
            dbo_file dbo_file = await UOW.dbo_fileRepository.Get(Id);
            if (dbo_file == null)
                return null;
            return dbo_file;
        }

        public async Task<dbo_file> Create(dbo_file dbo_file)
        {
            if (!await dbo_fileValidator.Create(dbo_file))
                return dbo_file;

            try
            {
                await UOW.Begin();
                await UOW.dbo_fileRepository.Create(dbo_file);
                await UOW.Commit();
                dbo_file = await UOW.dbo_fileRepository.Get(dbo_file.Id);
                await Logging.CreateAuditLog(dbo_file, new { }, nameof(dbo_fileService));
                return dbo_file;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_fileService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_fileService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<dbo_file> Update(dbo_file dbo_file)
        {
            if (!await dbo_fileValidator.Update(dbo_file))
                return dbo_file;
            try
            {
                var oldData = await UOW.dbo_fileRepository.Get(dbo_file.Id);

                await UOW.Begin();
                await UOW.dbo_fileRepository.Update(dbo_file);
                await UOW.Commit();

                dbo_file = await UOW.dbo_fileRepository.Get(dbo_file.Id);
                await Logging.CreateAuditLog(dbo_file, oldData, nameof(dbo_fileService));
                return dbo_file;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_fileService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_fileService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<dbo_file> Delete(dbo_file dbo_file)
        {
            if (!await dbo_fileValidator.Delete(dbo_file))
                return dbo_file;

            try
            {
                await UOW.Begin();
                await UOW.dbo_fileRepository.Delete(dbo_file);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, dbo_file, nameof(dbo_fileService));
                return dbo_file;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_fileService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_fileService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<dbo_file>> BulkDelete(List<dbo_file> dbo_files)
        {
            if (!await dbo_fileValidator.BulkDelete(dbo_files))
                return dbo_files;

            try
            {
                await UOW.Begin();
                await UOW.dbo_fileRepository.BulkDelete(dbo_files);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, dbo_files, nameof(dbo_fileService));
                return dbo_files;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_fileService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_fileService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<dbo_file>> Import(List<dbo_file> dbo_files)
        {
            if (!await dbo_fileValidator.Import(dbo_files))
                return dbo_files;
            try
            {
                await UOW.Begin();
                await UOW.dbo_fileRepository.BulkMerge(dbo_files);
                await UOW.Commit();

                await Logging.CreateAuditLog(dbo_files, new { }, nameof(dbo_fileService));
                return dbo_files;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_fileService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_fileService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public dbo_fileFilter ToFilter(dbo_fileFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<dbo_fileFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                dbo_fileFilter subFilter = new dbo_fileFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Url))
                        
                        
                        
                        
                        
                        
                        subFilter.Url = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
