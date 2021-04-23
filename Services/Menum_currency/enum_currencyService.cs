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

namespace BASE.Services.Menum_currency
{
    public interface Ienum_currencyService :  IServiceScoped
    {
        Task<int>
    Count(enum_currencyFilter enum_currencyFilter);
    Task<List<enum_currency>> List(enum_currencyFilter enum_currencyFilter);
        Task<enum_currency> Get(long Id);
        Task<enum_currency> Create(enum_currency enum_currency);
        Task<enum_currency> Update(enum_currency enum_currency);
        Task<enum_currency> Delete(enum_currency enum_currency);
        Task<List<enum_currency>> BulkDelete(List<enum_currency> enum_currencies);
        Task<List<enum_currency>> Import(List<enum_currency> enum_currencies);
        enum_currencyFilter ToFilter(enum_currencyFilter enum_currencyFilter);
    }

    public class enum_currencyService : BaseService, Ienum_currencyService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Ienum_currencyValidator enum_currencyValidator;

        public enum_currencyService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Ienum_currencyValidator enum_currencyValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.enum_currencyValidator = enum_currencyValidator;
        }
        public async Task<int> Count(enum_currencyFilter enum_currencyFilter)
        {
            try
            {
                int result = await UOW.enum_currencyRepository.Count(enum_currencyFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_currencyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_currencyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_currency>> List(enum_currencyFilter enum_currencyFilter)
        {
            try
            {
                List<enum_currency> enum_currencys = await UOW.enum_currencyRepository.List(enum_currencyFilter);
                return enum_currencys;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_currencyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_currencyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<enum_currency> Get(long Id)
        {
            enum_currency enum_currency = await UOW.enum_currencyRepository.Get(Id);
            if (enum_currency == null)
                return null;
            return enum_currency;
        }

        public async Task<enum_currency> Create(enum_currency enum_currency)
        {
            if (!await enum_currencyValidator.Create(enum_currency))
                return enum_currency;

            try
            {
                await UOW.Begin();
                await UOW.enum_currencyRepository.Create(enum_currency);
                await UOW.Commit();
                enum_currency = await UOW.enum_currencyRepository.Get(enum_currency.Id);
                await Logging.CreateAuditLog(enum_currency, new { }, nameof(enum_currencyService));
                return enum_currency;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_currencyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_currencyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_currency> Update(enum_currency enum_currency)
        {
            if (!await enum_currencyValidator.Update(enum_currency))
                return enum_currency;
            try
            {
                var oldData = await UOW.enum_currencyRepository.Get(enum_currency.Id);

                await UOW.Begin();
                await UOW.enum_currencyRepository.Update(enum_currency);
                await UOW.Commit();

                enum_currency = await UOW.enum_currencyRepository.Get(enum_currency.Id);
                await Logging.CreateAuditLog(enum_currency, oldData, nameof(enum_currencyService));
                return enum_currency;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_currencyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_currencyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<enum_currency> Delete(enum_currency enum_currency)
        {
            if (!await enum_currencyValidator.Delete(enum_currency))
                return enum_currency;

            try
            {
                await UOW.Begin();
                await UOW.enum_currencyRepository.Delete(enum_currency);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_currency, nameof(enum_currencyService));
                return enum_currency;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_currencyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_currencyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_currency>> BulkDelete(List<enum_currency> enum_currencies)
        {
            if (!await enum_currencyValidator.BulkDelete(enum_currencies))
                return enum_currencies;

            try
            {
                await UOW.Begin();
                await UOW.enum_currencyRepository.BulkDelete(enum_currencies);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, enum_currencies, nameof(enum_currencyService));
                return enum_currencies;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_currencyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_currencyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<enum_currency>> Import(List<enum_currency> enum_currencies)
        {
            if (!await enum_currencyValidator.Import(enum_currencies))
                return enum_currencies;
            try
            {
                await UOW.Begin();
                await UOW.enum_currencyRepository.BulkMerge(enum_currencies);
                await UOW.Commit();

                await Logging.CreateAuditLog(enum_currencies, new { }, nameof(enum_currencyService));
                return enum_currencies;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(enum_currencyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(enum_currencyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public enum_currencyFilter ToFilter(enum_currencyFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<enum_currencyFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                enum_currencyFilter subFilter = new enum_currencyFilter();
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
