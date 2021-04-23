using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mper_permission
{
    public interface Iper_permissionValidator : IServiceScoped
    {
        Task<bool>
    Create(per_permission per_permission);
    Task<bool>
        Update(per_permission per_permission);
        Task<bool>
            Delete(per_permission per_permission);
            Task<bool>
                BulkDelete(List<per_permission>
                    per_permissions);
                    Task<bool>
                        Import(List<per_permission>
                            per_permissions);
                            }

                            public class per_permissionValidator : Iper_permissionValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public per_permissionValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(per_permission per_permission)
                                {
                                per_permissionFilter per_permissionFilter = new per_permissionFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = per_permission.Id },
                                Selects = per_permissionSelect.Id
                                };

                                int count = await UOW.per_permissionRepository.Count(per_permissionFilter);
                                if (count == 0)
                                per_permission.AddError(nameof(per_permissionValidator), nameof(per_permission.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(per_permission per_permission)
                                    {
                                    return per_permission.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(per_permission per_permission)
                                        {
                                        if (await ValidateId(per_permission))
                                        {
                                        }
                                        return per_permission.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(per_permission per_permission)
                                            {
                                            if (await ValidateId(per_permission))
                                            {
                                            }
                                            return per_permission.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<per_permission>
                                                    per_permissions)
                                                    {
                                                    foreach (per_permission per_permission in per_permissions)
                                                    {
                                                    await Delete(per_permission);
                                                    }
                                                    return per_permissions.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<per_permission>
                                                            per_permissions)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
