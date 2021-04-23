using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Menum_currency
{
    public interface Ienum_currencyValidator : IServiceScoped
    {
        Task<bool>
    Create(enum_currency enum_currency);
    Task<bool>
        Update(enum_currency enum_currency);
        Task<bool>
            Delete(enum_currency enum_currency);
            Task<bool>
                BulkDelete(List<enum_currency>
                    enum_currencies);
                    Task<bool>
                        Import(List<enum_currency>
                            enum_currencies);
                            }

                            public class enum_currencyValidator : Ienum_currencyValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public enum_currencyValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(enum_currency enum_currency)
                                {
                                enum_currencyFilter enum_currencyFilter = new enum_currencyFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = enum_currency.Id },
                                Selects = enum_currencySelect.Id
                                };

                                int count = await UOW.enum_currencyRepository.Count(enum_currencyFilter);
                                if (count == 0)
                                enum_currency.AddError(nameof(enum_currencyValidator), nameof(enum_currency.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(enum_currency enum_currency)
                                    {
                                    return enum_currency.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(enum_currency enum_currency)
                                        {
                                        if (await ValidateId(enum_currency))
                                        {
                                        }
                                        return enum_currency.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(enum_currency enum_currency)
                                            {
                                            if (await ValidateId(enum_currency))
                                            {
                                            }
                                            return enum_currency.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<enum_currency>
                                                    enum_currencies)
                                                    {
                                                    foreach (enum_currency enum_currency in enum_currencies)
                                                    {
                                                    await Delete(enum_currency);
                                                    }
                                                    return enum_currencies.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<enum_currency>
                                                            enum_currencies)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
