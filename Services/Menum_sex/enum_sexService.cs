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

namespace BASE.Services.Menum_sex
{
    public interface Ienum_sexService :  IServiceScoped
    {
        Task<int>
    Count(enum_sexFilter enum_sexFilter);
    Task<List<enum_sex>> List(enum_sexFilter enum_sexFilter);
        Task<enum_sex> Get(long Id);
        Task<enum_sex> Create(enum_sex enum_sex);
        Task<enum_sex> Update(enum_sex enum_sex);
        Task<enum_sex> Delete(enum_sex enum_sex);
        Task<List<enum_sex>> BulkDelete(List<enum_sex> enum_sexes);
        Task<List<enum_sex>> Import(List<enum_sex> enum_sexes);
        enum_sexFilter ToFilter(enum_sexFilter enum_sexFilter);
    }

    public class enum_sexService : BaseService, Ienum_sexService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Ienum_sexValidator enum_sexValidator;

        public enum_sexService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Ienum_sexValidator enum_sexValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.enum_sexValidator = enum_sexValidator;
        }
        public async Task<int> Count(enum_sexFilter enum_sexFilter)
        {
            try
            {
                int result = await UOW.enum_sexRepository.Count(enum_sexFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_sexService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_sexService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_sex>> List(enum_sexFilter enum_sexFilter)
        {
            try
            {
                List<enum_sex> enum_sexs = await UOW.enum_sexRepository.List(enum_sexFilter);
                return enum_sexs;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_sexService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_sexService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<enum_sex> Get(long Id)
        {
            enum_sex enum_sex = await UOW.enum_sexRepository.Get(Id);
            if (enum_sex == null)
                return null;
            return enum_sex;
        }

        public async Task<enum_sex> Create(enum_sex enum_sex)
        {
            if (!await enum_sexValidator.Create(enum_sex))
                return enum_sex;

            try
            {
                await UOW.Begin();
                await UOW.enum_sexRepository.Create(enum_sex);
                await UOW.Commit();
                enum_sex = await UOW.enum_sexRepository.Get(enum_sex.Id);
                await Logging.CreateAuditLog(enum_sex, new { }, nameof(enum_sexService));
                return enum_sex;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_sexService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_sexService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_sex> Update(enum_sex enum_sex)
        {
            if (!await enum_sexValidator.Update(enum_sex))
                return enum_sex;
            try
            {
                var oldData = await UOW.enum_sexRepository.Get(enum_sex.Id);

                await UOW.Begin();
                await UOW.enum_sexRepository.Update(enum_sex);
                await UOW.Commit();

                enum_sex = await UOW.enum_sexRepository.Get(enum_sex.Id);
                await Logging.CreateAuditLog(enum_sex, oldData, nameof(enum_sexService));
                return enum_sex;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_sexService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_sexService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_sex> Delete(enum_sex enum_sex)
        {
            if (!await enum_sexValidator.Delete(enum_sex))
                return enum_sex;

            try
            {
                await UOW.Begin();
                await UOW.enum_sexRepository.Delete(enum_sex);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_sex, nameof(enum_sexService));
                return enum_sex;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_sexService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_sexService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_sex>> BulkDelete(List<enum_sex> enum_sexes)
        {
            if (!await enum_sexValidator.BulkDelete(enum_sexes))
                return enum_sexes;

            try
            {
                await UOW.Begin();
                await UOW.enum_sexRepository.BulkDelete(enum_sexes);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_sexes, nameof(enum_sexService));
                return enum_sexes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_sexService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_sexService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_sex>> Import(List<enum_sex> enum_sexes)
        {
            if (!await enum_sexValidator.Import(enum_sexes))
                return enum_sexes;
            try
            {
                await UOW.Begin();
                await UOW.enum_sexRepository.BulkMerge(enum_sexes);
                await UOW.Commit();

                await Logging.CreateAuditLog(enum_sexes, new { }, nameof(enum_sexService));
                return enum_sexes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_sexService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_sexService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public enum_sexFilter ToFilter(enum_sexFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<enum_sexFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                enum_sexFilter subFilter = new enum_sexFilter();
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
