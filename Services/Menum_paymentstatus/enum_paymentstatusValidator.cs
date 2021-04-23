using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Menum_paymentstatus
{
    public interface Ienum_paymentstatusValidator : IServiceScoped
    {
        Task<bool>
    Create(enum_paymentstatus enum_paymentstatus);
    Task<bool>
        Update(enum_paymentstatus enum_paymentstatus);
        Task<bool>
            Delete(enum_paymentstatus enum_paymentstatus);
            Task<bool>
                BulkDelete(List<enum_paymentstatus>
                    enum_paymentstatuses);
                    Task<bool>
                        Import(List<enum_paymentstatus>
                            enum_paymentstatuses);
                            }

                            public class enum_paymentstatusValidator : Ienum_paymentstatusValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public enum_paymentstatusValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(enum_paymentstatus enum_paymentstatus)
                                {
                                enum_paymentstatusFilter enum_paymentstatusFilter = new enum_paymentstatusFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = enum_paymentstatus.Id },
                                Selects = enum_paymentstatusSelect.Id
                                };

                                int count = await UOW.enum_paymentstatusRepository.Count(enum_paymentstatusFilter);
                                if (count == 0)
                                enum_paymentstatus.AddError(nameof(enum_paymentstatusValidator), nameof(enum_paymentstatus.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(enum_paymentstatus enum_paymentstatus)
                                    {
                                    return enum_paymentstatus.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(enum_paymentstatus enum_paymentstatus)
                                        {
                                        if (await ValidateId(enum_paymentstatus))
                                        {
                                        }
                                        return enum_paymentstatus.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(enum_paymentstatus enum_paymentstatus)
                                            {
                                            if (await ValidateId(enum_paymentstatus))
                                            {
                                            }
                                            return enum_paymentstatus.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<enum_paymentstatus>
                                                    enum_paymentstatuses)
                                                    {
                                                    foreach (enum_paymentstatus enum_paymentstatus in enum_paymentstatuses)
                                                    {
                                                    await Delete(enum_paymentstatus);
                                                    }
                                                    return enum_paymentstatuses.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<enum_paymentstatus>
                                                            enum_paymentstatuses)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
