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

namespace BASE.Services.Mper_field
{
    public interface Iper_fieldService :  IServiceScoped
    {
        Task<int>
    Count(per_fieldFilter per_fieldFilter);
    Task<List<per_field>> List(per_fieldFilter per_fieldFilter);
        Task<per_field> Get(long Id);
        Task<per_field> Create(per_field per_field);
        Task<per_field> Update(per_field per_field);
        Task<per_field> Delete(per_field per_field);
        Task<List<per_field>> BulkDelete(List<per_field> per_fields);
        Task<List<per_field>> Import(List<per_field> per_fields);
        per_fieldFilter ToFilter(per_fieldFilter per_fieldFilter);
    }

    public class per_fieldService : BaseService, Iper_fieldService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Iper_fieldValidator per_fieldValidator;

        public per_fieldService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Iper_fieldValidator per_fieldValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.per_fieldValidator = per_fieldValidator;
        }
        public async Task<int> Count(per_fieldFilter per_fieldFilter)
        {
            try
            {
                int result = await UOW.per_fieldRepository.Count(per_fieldFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_fieldService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_fieldService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_field>> List(per_fieldFilter per_fieldFilter)
        {
            try
            {
                List<per_field> per_fields = await UOW.per_fieldRepository.List(per_fieldFilter);
                return per_fields;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_fieldService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_fieldService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<per_field> Get(long Id)
        {
            per_field per_field = await UOW.per_fieldRepository.Get(Id);
            if (per_field == null)
                return null;
            return per_field;
        }

        public async Task<per_field> Create(per_field per_field)
        {
            if (!await per_fieldValidator.Create(per_field))
                return per_field;

            try
            {
                await UOW.Begin();
                await UOW.per_fieldRepository.Create(per_field);
                await UOW.Commit();
                per_field = await UOW.per_fieldRepository.Get(per_field.Id);
                await Logging.CreateAuditLog(per_field, new { }, nameof(per_fieldService));
                return per_field;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_fieldService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_fieldService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_field> Update(per_field per_field)
        {
            if (!await per_fieldValidator.Update(per_field))
                return per_field;
            try
            {
                var oldData = await UOW.per_fieldRepository.Get(per_field.Id);

                await UOW.Begin();
                await UOW.per_fieldRepository.Update(per_field);
                await UOW.Commit();

                per_field = await UOW.per_fieldRepository.Get(per_field.Id);
                await Logging.CreateAuditLog(per_field, oldData, nameof(per_fieldService));
                return per_field;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_fieldService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_fieldService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_field> Delete(per_field per_field)
        {
            if (!await per_fieldValidator.Delete(per_field))
                return per_field;

            try
            {
                await UOW.Begin();
                await UOW.per_fieldRepository.Delete(per_field);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_field, nameof(per_fieldService));
                return per_field;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_fieldService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_fieldService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_field>> BulkDelete(List<per_field> per_fields)
        {
            if (!await per_fieldValidator.BulkDelete(per_fields))
                return per_fields;

            try
            {
                await UOW.Begin();
                await UOW.per_fieldRepository.BulkDelete(per_fields);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_fields, nameof(per_fieldService));
                return per_fields;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_fieldService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_fieldService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_field>> Import(List<per_field> per_fields)
        {
            if (!await per_fieldValidator.Import(per_fields))
                return per_fields;
            try
            {
                await UOW.Begin();
                await UOW.per_fieldRepository.BulkMerge(per_fields);
                await UOW.Commit();

                await Logging.CreateAuditLog(per_fields, new { }, nameof(per_fieldService));
                return per_fields;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_fieldService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_fieldService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public per_fieldFilter ToFilter(per_fieldFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<per_fieldFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                per_fieldFilter subFilter = new per_fieldFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.FieldTypeId))
                        subFilter.FieldTypeId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.MenuId))
                        subFilter.MenuId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
