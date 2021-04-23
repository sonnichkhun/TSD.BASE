using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mper_fieldtype
{
    public interface Iper_fieldtypeValidator : IServiceScoped
    {
        Task<bool>
    Create(per_fieldtype per_fieldtype);
    Task<bool>
        Update(per_fieldtype per_fieldtype);
        Task<bool>
            Delete(per_fieldtype per_fieldtype);
            Task<bool>
                BulkDelete(List<per_fieldtype>
                    per_fieldtypes);
                    Task<bool>
                        Import(List<per_fieldtype>
                            per_fieldtypes);
                            }

                            public class per_fieldtypeValidator : Iper_fieldtypeValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public per_fieldtypeValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(per_fieldtype per_fieldtype)
                                {
                                per_fieldtypeFilter per_fieldtypeFilter = new per_fieldtypeFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = per_fieldtype.Id },
                                Selects = per_fieldtypeSelect.Id
                                };

                                int count = await UOW.per_fieldtypeRepository.Count(per_fieldtypeFilter);
                                if (count == 0)
                                per_fieldtype.AddError(nameof(per_fieldtypeValidator), nameof(per_fieldtype.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(per_fieldtype per_fieldtype)
                                    {
                                    return per_fieldtype.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(per_fieldtype per_fieldtype)
                                        {
                                        if (await ValidateId(per_fieldtype))
                                        {
                                        }
                                        return per_fieldtype.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(per_fieldtype per_fieldtype)
                                            {
                                            if (await ValidateId(per_fieldtype))
                                            {
                                            }
                                            return per_fieldtype.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<per_fieldtype>
                                                    per_fieldtypes)
                                                    {
                                                    foreach (per_fieldtype per_fieldtype in per_fieldtypes)
                                                    {
                                                    await Delete(per_fieldtype);
                                                    }
                                                    return per_fieldtypes.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<per_fieldtype>
                                                            per_fieldtypes)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
