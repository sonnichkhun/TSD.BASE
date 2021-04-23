using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mper_role
{
    public interface Iper_roleValidator : IServiceScoped
    {
        Task<bool>
    Create(per_role per_role);
    Task<bool>
        Update(per_role per_role);
        Task<bool>
            Delete(per_role per_role);
            Task<bool>
                BulkDelete(List<per_role>
                    per_roles);
                    Task<bool>
                        Import(List<per_role>
                            per_roles);
                            }

                            public class per_roleValidator : Iper_roleValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public per_roleValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(per_role per_role)
                                {
                                per_roleFilter per_roleFilter = new per_roleFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = per_role.Id },
                                Selects = per_roleSelect.Id
                                };

                                int count = await UOW.per_roleRepository.Count(per_roleFilter);
                                if (count == 0)
                                per_role.AddError(nameof(per_roleValidator), nameof(per_role.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(per_role per_role)
                                    {
                                    return per_role.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(per_role per_role)
                                        {
                                        if (await ValidateId(per_role))
                                        {
                                        }
                                        return per_role.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(per_role per_role)
                                            {
                                            if (await ValidateId(per_role))
                                            {
                                            }
                                            return per_role.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<per_role>
                                                    per_roles)
                                                    {
                                                    foreach (per_role per_role in per_roles)
                                                    {
                                                    await Delete(per_role);
                                                    }
                                                    return per_roles.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<per_role>
                                                            per_roles)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
