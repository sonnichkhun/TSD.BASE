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

namespace BASE.Services.Mdbo_mailtemplate
{
    public interface Idbo_mailtemplateService :  IServiceScoped
    {
        Task<int>
    Count(dbo_mailtemplateFilter dbo_mailtemplateFilter);
    Task<List<dbo_mailtemplate>> List(dbo_mailtemplateFilter dbo_mailtemplateFilter);
        Task<dbo_mailtemplate> Get(long Id);
        Task<dbo_mailtemplate> Create(dbo_mailtemplate dbo_mailtemplate);
        Task<dbo_mailtemplate> Update(dbo_mailtemplate dbo_mailtemplate);
        Task<dbo_mailtemplate> Delete(dbo_mailtemplate dbo_mailtemplate);
        Task<List<dbo_mailtemplate>> BulkDelete(List<dbo_mailtemplate> dbo_mailtemplates);
        Task<List<dbo_mailtemplate>> Import(List<dbo_mailtemplate> dbo_mailtemplates);
        dbo_mailtemplateFilter ToFilter(dbo_mailtemplateFilter dbo_mailtemplateFilter);
    }

    public class dbo_mailtemplateService : BaseService, Idbo_mailtemplateService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Idbo_mailtemplateValidator dbo_mailtemplateValidator;

        public dbo_mailtemplateService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Idbo_mailtemplateValidator dbo_mailtemplateValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.dbo_mailtemplateValidator = dbo_mailtemplateValidator;
        }
        public async Task<int> Count(dbo_mailtemplateFilter dbo_mailtemplateFilter)
        {
            try
            {
                int result = await UOW.dbo_mailtemplateRepository.Count(dbo_mailtemplateFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_mailtemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_mailtemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<dbo_mailtemplate>> List(dbo_mailtemplateFilter dbo_mailtemplateFilter)
        {
            try
            {
                List<dbo_mailtemplate> dbo_mailtemplates = await UOW.dbo_mailtemplateRepository.List(dbo_mailtemplateFilter);
                return dbo_mailtemplates;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_mailtemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_mailtemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<dbo_mailtemplate> Get(long Id)
        {
            dbo_mailtemplate dbo_mailtemplate = await UOW.dbo_mailtemplateRepository.Get(Id);
            if (dbo_mailtemplate == null)
                return null;
            return dbo_mailtemplate;
        }

        public async Task<dbo_mailtemplate> Create(dbo_mailtemplate dbo_mailtemplate)
        {
            if (!await dbo_mailtemplateValidator.Create(dbo_mailtemplate))
                return dbo_mailtemplate;

            try
            {
                await UOW.Begin();
                await UOW.dbo_mailtemplateRepository.Create(dbo_mailtemplate);
                await UOW.Commit();
                dbo_mailtemplate = await UOW.dbo_mailtemplateRepository.Get(dbo_mailtemplate.Id);
                await Logging.CreateAuditLog(dbo_mailtemplate, new { }, nameof(dbo_mailtemplateService));
                return dbo_mailtemplate;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_mailtemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_mailtemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<dbo_mailtemplate> Update(dbo_mailtemplate dbo_mailtemplate)
        {
            if (!await dbo_mailtemplateValidator.Update(dbo_mailtemplate))
                return dbo_mailtemplate;
            try
            {
                var oldData = await UOW.dbo_mailtemplateRepository.Get(dbo_mailtemplate.Id);

                await UOW.Begin();
                await UOW.dbo_mailtemplateRepository.Update(dbo_mailtemplate);
                await UOW.Commit();

                dbo_mailtemplate = await UOW.dbo_mailtemplateRepository.Get(dbo_mailtemplate.Id);
                await Logging.CreateAuditLog(dbo_mailtemplate, oldData, nameof(dbo_mailtemplateService));
                return dbo_mailtemplate;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_mailtemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_mailtemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<dbo_mailtemplate> Delete(dbo_mailtemplate dbo_mailtemplate)
        {
            if (!await dbo_mailtemplateValidator.Delete(dbo_mailtemplate))
                return dbo_mailtemplate;

            try
            {
                await UOW.Begin();
                await UOW.dbo_mailtemplateRepository.Delete(dbo_mailtemplate);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, dbo_mailtemplate, nameof(dbo_mailtemplateService));
                return dbo_mailtemplate;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_mailtemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_mailtemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<dbo_mailtemplate>> BulkDelete(List<dbo_mailtemplate> dbo_mailtemplates)
        {
            if (!await dbo_mailtemplateValidator.BulkDelete(dbo_mailtemplates))
                return dbo_mailtemplates;

            try
            {
                await UOW.Begin();
                await UOW.dbo_mailtemplateRepository.BulkDelete(dbo_mailtemplates);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, dbo_mailtemplates, nameof(dbo_mailtemplateService));
                return dbo_mailtemplates;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_mailtemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_mailtemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<dbo_mailtemplate>> Import(List<dbo_mailtemplate> dbo_mailtemplates)
        {
            if (!await dbo_mailtemplateValidator.Import(dbo_mailtemplates))
                return dbo_mailtemplates;
            try
            {
                await UOW.Begin();
                await UOW.dbo_mailtemplateRepository.BulkMerge(dbo_mailtemplates);
                await UOW.Commit();

                await Logging.CreateAuditLog(dbo_mailtemplates, new { }, nameof(dbo_mailtemplateService));
                return dbo_mailtemplates;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(dbo_mailtemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(dbo_mailtemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public dbo_mailtemplateFilter ToFilter(dbo_mailtemplateFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<dbo_mailtemplateFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                dbo_mailtemplateFilter subFilter = new dbo_mailtemplateFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Content))
                        
                        
                        
                        
                        
                        
                        subFilter.Content = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
