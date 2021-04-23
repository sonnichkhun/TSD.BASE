using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mper_menu
{
    public interface Iper_menuValidator : IServiceScoped
    {
        Task<bool>
    Create(per_menu per_menu);
    Task<bool>
        Update(per_menu per_menu);
        Task<bool>
            Delete(per_menu per_menu);
            Task<bool>
                BulkDelete(List<per_menu>
                    per_menus);
                    Task<bool>
                        Import(List<per_menu>
                            per_menus);
                            }

                            public class per_menuValidator : Iper_menuValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public per_menuValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(per_menu per_menu)
                                {
                                per_menuFilter per_menuFilter = new per_menuFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = per_menu.Id },
                                Selects = per_menuSelect.Id
                                };

                                int count = await UOW.per_menuRepository.Count(per_menuFilter);
                                if (count == 0)
                                per_menu.AddError(nameof(per_menuValidator), nameof(per_menu.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(per_menu per_menu)
                                    {
                                    return per_menu.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(per_menu per_menu)
                                        {
                                        if (await ValidateId(per_menu))
                                        {
                                        }
                                        return per_menu.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(per_menu per_menu)
                                            {
                                            if (await ValidateId(per_menu))
                                            {
                                            }
                                            return per_menu.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<per_menu>
                                                    per_menus)
                                                    {
                                                    foreach (per_menu per_menu in per_menus)
                                                    {
                                                    await Delete(per_menu);
                                                    }
                                                    return per_menus.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<per_menu>
                                                            per_menus)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
