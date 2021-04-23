using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mper_field
{
    public interface Iper_fieldValidator : IServiceScoped
    {
        Task<bool>
    Create(per_field per_field);
    Task<bool>
        Update(per_field per_field);
        Task<bool>
            Delete(per_field per_field);
            Task<bool>
                BulkDelete(List<per_field>
                    per_fields);
                    Task<bool>
                        Import(List<per_field>
                            per_fields);
                            }

                            public class per_fieldValidator : Iper_fieldValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public per_fieldValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(per_field per_field)
                                {
                                per_fieldFilter per_fieldFilter = new per_fieldFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = per_field.Id },
                                Selects = per_fieldSelect.Id
                                };

                                int count = await UOW.per_fieldRepository.Count(per_fieldFilter);
                                if (count == 0)
                                per_field.AddError(nameof(per_fieldValidator), nameof(per_field.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(per_field per_field)
                                    {
                                    return per_field.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(per_field per_field)
                                        {
                                        if (await ValidateId(per_field))
                                        {
                                        }
                                        return per_field.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(per_field per_field)
                                            {
                                            if (await ValidateId(per_field))
                                            {
                                            }
                                            return per_field.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<per_field>
                                                    per_fields)
                                                    {
                                                    foreach (per_field per_field in per_fields)
                                                    {
                                                    await Delete(per_field);
                                                    }
                                                    return per_fields.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<per_field>
                                                            per_fields)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
