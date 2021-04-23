using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mper_permissioncontent
{
    public interface Iper_permissioncontentValidator : IServiceScoped
    {
        Task<bool>
    Create(per_permissioncontent per_permissioncontent);
    Task<bool>
        Update(per_permissioncontent per_permissioncontent);
        Task<bool>
            Delete(per_permissioncontent per_permissioncontent);
            Task<bool>
                BulkDelete(List<per_permissioncontent>
                    per_permissioncontents);
                    Task<bool>
                        Import(List<per_permissioncontent>
                            per_permissioncontents);
                            }

                            public class per_permissioncontentValidator : Iper_permissioncontentValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public per_permissioncontentValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(per_permissioncontent per_permissioncontent)
                                {
                                per_permissioncontentFilter per_permissioncontentFilter = new per_permissioncontentFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = per_permissioncontent.Id },
                                Selects = per_permissioncontentSelect.Id
                                };

                                int count = await UOW.per_permissioncontentRepository.Count(per_permissioncontentFilter);
                                if (count == 0)
                                per_permissioncontent.AddError(nameof(per_permissioncontentValidator), nameof(per_permissioncontent.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(per_permissioncontent per_permissioncontent)
                                    {
                                    return per_permissioncontent.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(per_permissioncontent per_permissioncontent)
                                        {
                                        if (await ValidateId(per_permissioncontent))
                                        {
                                        }
                                        return per_permissioncontent.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(per_permissioncontent per_permissioncontent)
                                            {
                                            if (await ValidateId(per_permissioncontent))
                                            {
                                            }
                                            return per_permissioncontent.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<per_permissioncontent>
                                                    per_permissioncontents)
                                                    {
                                                    foreach (per_permissioncontent per_permissioncontent in per_permissioncontents)
                                                    {
                                                    await Delete(per_permissioncontent);
                                                    }
                                                    return per_permissioncontents.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<per_permissioncontent>
                                                            per_permissioncontents)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
