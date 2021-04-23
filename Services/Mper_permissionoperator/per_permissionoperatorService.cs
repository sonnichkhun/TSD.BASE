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

namespace BASE.Services.Mper_permissionoperator
{
    public interface Iper_permissionoperatorService :  IServiceScoped
    {
        Task<int>
    Count(per_permissionoperatorFilter per_permissionoperatorFilter);
    Task<List<per_permissionoperator>> List(per_permissionoperatorFilter per_permissionoperatorFilter);
        Task<per_permissionoperator> Get(long Id);
        Task<per_permissionoperator> Create(per_permissionoperator per_permissionoperator);
        Task<per_permissionoperator> Update(per_permissionoperator per_permissionoperator);
        Task<per_permissionoperator> Delete(per_permissionoperator per_permissionoperator);
        Task<List<per_permissionoperator>> BulkDelete(List<per_permissionoperator> per_permissionoperators);
        Task<List<per_permissionoperator>> Import(List<per_permissionoperator> per_permissionoperators);
        per_permissionoperatorFilter ToFilter(per_permissionoperatorFilter per_permissionoperatorFilter);
    }

    public class per_permissionoperatorService : BaseService, Iper_permissionoperatorService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Iper_permissionoperatorValidator per_permissionoperatorValidator;

        public per_permissionoperatorService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Iper_permissionoperatorValidator per_permissionoperatorValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.per_permissionoperatorValidator = per_permissionoperatorValidator;
        }
        public async Task<int> Count(per_permissionoperatorFilter per_permissionoperatorFilter)
        {
            try
            {
                int result = await UOW.per_permissionoperatorRepository.Count(per_permissionoperatorFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissionoperatorService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissionoperatorService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_permissionoperator>> List(per_permissionoperatorFilter per_permissionoperatorFilter)
        {
            try
            {
                List<per_permissionoperator> per_permissionoperators = await UOW.per_permissionoperatorRepository.List(per_permissionoperatorFilter);
                return per_permissionoperators;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissionoperatorService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissionoperatorService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<per_permissionoperator> Get(long Id)
        {
            per_permissionoperator per_permissionoperator = await UOW.per_permissionoperatorRepository.Get(Id);
            if (per_permissionoperator == null)
                return null;
            return per_permissionoperator;
        }

        public async Task<per_permissionoperator> Create(per_permissionoperator per_permissionoperator)
        {
            if (!await per_permissionoperatorValidator.Create(per_permissionoperator))
                return per_permissionoperator;

            try
            {
                await UOW.Begin();
                await UOW.per_permissionoperatorRepository.Create(per_permissionoperator);
                await UOW.Commit();
                per_permissionoperator = await UOW.per_permissionoperatorRepository.Get(per_permissionoperator.Id);
                await Logging.CreateAuditLog(per_permissionoperator, new { }, nameof(per_permissionoperatorService));
                return per_permissionoperator;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissionoperatorService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissionoperatorService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_permissionoperator> Update(per_permissionoperator per_permissionoperator)
        {
            if (!await per_permissionoperatorValidator.Update(per_permissionoperator))
                return per_permissionoperator;
            try
            {
                var oldData = await UOW.per_permissionoperatorRepository.Get(per_permissionoperator.Id);

                await UOW.Begin();
                await UOW.per_permissionoperatorRepository.Update(per_permissionoperator);
                await UOW.Commit();

                per_permissionoperator = await UOW.per_permissionoperatorRepository.Get(per_permissionoperator.Id);
                await Logging.CreateAuditLog(per_permissionoperator, oldData, nameof(per_permissionoperatorService));
                return per_permissionoperator;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissionoperatorService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissionoperatorService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<per_permissionoperator> Delete(per_permissionoperator per_permissionoperator)
        {
            if (!await per_permissionoperatorValidator.Delete(per_permissionoperator))
                return per_permissionoperator;

            try
            {
                await UOW.Begin();
                await UOW.per_permissionoperatorRepository.Delete(per_permissionoperator);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_permissionoperator, nameof(per_permissionoperatorService));
                return per_permissionoperator;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissionoperatorService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissionoperatorService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_permissionoperator>> BulkDelete(List<per_permissionoperator> per_permissionoperators)
        {
            if (!await per_permissionoperatorValidator.BulkDelete(per_permissionoperators))
                return per_permissionoperators;

            try
            {
                await UOW.Begin();
                await UOW.per_permissionoperatorRepository.BulkDelete(per_permissionoperators);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, per_permissionoperators, nameof(per_permissionoperatorService));
                return per_permissionoperators;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissionoperatorService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissionoperatorService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<per_permissionoperator>> Import(List<per_permissionoperator> per_permissionoperators)
        {
            if (!await per_permissionoperatorValidator.Import(per_permissionoperators))
                return per_permissionoperators;
            try
            {
                await UOW.Begin();
                await UOW.per_permissionoperatorRepository.BulkMerge(per_permissionoperators);
                await UOW.Commit();

                await Logging.CreateAuditLog(per_permissionoperators, new { }, nameof(per_permissionoperatorService));
                return per_permissionoperators;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(per_permissionoperatorService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(per_permissionoperatorService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public per_permissionoperatorFilter ToFilter(per_permissionoperatorFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<per_permissionoperatorFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                per_permissionoperatorFilter subFilter = new per_permissionoperatorFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.FieldTypeId))
                        subFilter.FieldTypeId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
