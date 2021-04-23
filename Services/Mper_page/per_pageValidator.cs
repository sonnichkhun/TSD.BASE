using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mper_page
{
    public interface Iper_pageValidator : IServiceScoped
    {
        Task<bool>
    Create(per_page per_page);
    Task<bool>
        Update(per_page per_page);
        Task<bool>
            Delete(per_page per_page);
            Task<bool>
                BulkDelete(List<per_page>
                    per_pages);
                    Task<bool>
                        Import(List<per_page>
                            per_pages);
                            }

                            public class per_pageValidator : Iper_pageValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public per_pageValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(per_page per_page)
                                {
                                per_pageFilter per_pageFilter = new per_pageFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = per_page.Id },
                                Selects = per_pageSelect.Id
                                };

                                int count = await UOW.per_pageRepository.Count(per_pageFilter);
                                if (count == 0)
                                per_page.AddError(nameof(per_pageValidator), nameof(per_page.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(per_page per_page)
                                    {
                                    return per_page.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(per_page per_page)
                                        {
                                        if (await ValidateId(per_page))
                                        {
                                        }
                                        return per_page.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(per_page per_page)
                                            {
                                            if (await ValidateId(per_page))
                                            {
                                            }
                                            return per_page.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<per_page>
                                                    per_pages)
                                                    {
                                                    foreach (per_page per_page in per_pages)
                                                    {
                                                    await Delete(per_page);
                                                    }
                                                    return per_pages.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<per_page>
                                                            per_pages)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
