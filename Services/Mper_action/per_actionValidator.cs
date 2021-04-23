using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mper_action
{
    public interface Iper_actionValidator : IServiceScoped
    {
        Task<bool>
    Create(per_action per_action);
    Task<bool>
        Update(per_action per_action);
        Task<bool>
            Delete(per_action per_action);
            Task<bool>
                BulkDelete(List<per_action>
                    per_actions);
                    Task<bool>
                        Import(List<per_action>
                            per_actions);
                            }

                            public class per_actionValidator : Iper_actionValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public per_actionValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(per_action per_action)
                                {
                                per_actionFilter per_actionFilter = new per_actionFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = per_action.Id },
                                Selects = per_actionSelect.Id
                                };

                                int count = await UOW.per_actionRepository.Count(per_actionFilter);
                                if (count == 0)
                                per_action.AddError(nameof(per_actionValidator), nameof(per_action.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(per_action per_action)
                                    {
                                    return per_action.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(per_action per_action)
                                        {
                                        if (await ValidateId(per_action))
                                        {
                                        }
                                        return per_action.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(per_action per_action)
                                            {
                                            if (await ValidateId(per_action))
                                            {
                                            }
                                            return per_action.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<per_action>
                                                    per_actions)
                                                    {
                                                    foreach (per_action per_action in per_actions)
                                                    {
                                                    await Delete(per_action);
                                                    }
                                                    return per_actions.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<per_action>
                                                            per_actions)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
