using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mper_permissionoperator
{
    public interface Iper_permissionoperatorValidator : IServiceScoped
    {
        Task<bool>
    Create(per_permissionoperator per_permissionoperator);
    Task<bool>
        Update(per_permissionoperator per_permissionoperator);
        Task<bool>
            Delete(per_permissionoperator per_permissionoperator);
            Task<bool>
                BulkDelete(List<per_permissionoperator>
                    per_permissionoperators);
                    Task<bool>
                        Import(List<per_permissionoperator>
                            per_permissionoperators);
                            }

                            public class per_permissionoperatorValidator : Iper_permissionoperatorValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public per_permissionoperatorValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(per_permissionoperator per_permissionoperator)
                                {
                                per_permissionoperatorFilter per_permissionoperatorFilter = new per_permissionoperatorFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = per_permissionoperator.Id },
                                Selects = per_permissionoperatorSelect.Id
                                };

                                int count = await UOW.per_permissionoperatorRepository.Count(per_permissionoperatorFilter);
                                if (count == 0)
                                per_permissionoperator.AddError(nameof(per_permissionoperatorValidator), nameof(per_permissionoperator.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(per_permissionoperator per_permissionoperator)
                                    {
                                    return per_permissionoperator.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(per_permissionoperator per_permissionoperator)
                                        {
                                        if (await ValidateId(per_permissionoperator))
                                        {
                                        }
                                        return per_permissionoperator.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(per_permissionoperator per_permissionoperator)
                                            {
                                            if (await ValidateId(per_permissionoperator))
                                            {
                                            }
                                            return per_permissionoperator.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<per_permissionoperator>
                                                    per_permissionoperators)
                                                    {
                                                    foreach (per_permissionoperator per_permissionoperator in per_permissionoperators)
                                                    {
                                                    await Delete(per_permissionoperator);
                                                    }
                                                    return per_permissionoperators.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<per_permissionoperator>
                                                            per_permissionoperators)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
