using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Menum_sex
{
    public interface Ienum_sexValidator : IServiceScoped
    {
        Task<bool>
    Create(enum_sex enum_sex);
    Task<bool>
        Update(enum_sex enum_sex);
        Task<bool>
            Delete(enum_sex enum_sex);
            Task<bool>
                BulkDelete(List<enum_sex>
                    enum_sexes);
                    Task<bool>
                        Import(List<enum_sex>
                            enum_sexes);
                            }

                            public class enum_sexValidator : Ienum_sexValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public enum_sexValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(enum_sex enum_sex)
                                {
                                enum_sexFilter enum_sexFilter = new enum_sexFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = enum_sex.Id },
                                Selects = enum_sexSelect.Id
                                };

                                int count = await UOW.enum_sexRepository.Count(enum_sexFilter);
                                if (count == 0)
                                enum_sex.AddError(nameof(enum_sexValidator), nameof(enum_sex.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(enum_sex enum_sex)
                                    {
                                    return enum_sex.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(enum_sex enum_sex)
                                        {
                                        if (await ValidateId(enum_sex))
                                        {
                                        }
                                        return enum_sex.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(enum_sex enum_sex)
                                            {
                                            if (await ValidateId(enum_sex))
                                            {
                                            }
                                            return enum_sex.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<enum_sex>
                                                    enum_sexes)
                                                    {
                                                    foreach (enum_sex enum_sex in enum_sexes)
                                                    {
                                                    await Delete(enum_sex);
                                                    }
                                                    return enum_sexes.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<enum_sex>
                                                            enum_sexes)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
