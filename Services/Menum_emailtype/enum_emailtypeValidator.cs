using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Menum_emailtype
{
    public interface Ienum_emailtypeValidator : IServiceScoped
    {
        Task<bool>
    Create(enum_emailtype enum_emailtype);
    Task<bool>
        Update(enum_emailtype enum_emailtype);
        Task<bool>
            Delete(enum_emailtype enum_emailtype);
            Task<bool>
                BulkDelete(List<enum_emailtype>
                    enum_emailtypes);
                    Task<bool>
                        Import(List<enum_emailtype>
                            enum_emailtypes);
                            }

                            public class enum_emailtypeValidator : Ienum_emailtypeValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public enum_emailtypeValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(enum_emailtype enum_emailtype)
                                {
                                enum_emailtypeFilter enum_emailtypeFilter = new enum_emailtypeFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = enum_emailtype.Id },
                                Selects = enum_emailtypeSelect.Id
                                };

                                int count = await UOW.enum_emailtypeRepository.Count(enum_emailtypeFilter);
                                if (count == 0)
                                enum_emailtype.AddError(nameof(enum_emailtypeValidator), nameof(enum_emailtype.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(enum_emailtype enum_emailtype)
                                    {
                                    return enum_emailtype.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(enum_emailtype enum_emailtype)
                                        {
                                        if (await ValidateId(enum_emailtype))
                                        {
                                        }
                                        return enum_emailtype.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(enum_emailtype enum_emailtype)
                                            {
                                            if (await ValidateId(enum_emailtype))
                                            {
                                            }
                                            return enum_emailtype.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<enum_emailtype>
                                                    enum_emailtypes)
                                                    {
                                                    foreach (enum_emailtype enum_emailtype in enum_emailtypes)
                                                    {
                                                    await Delete(enum_emailtype);
                                                    }
                                                    return enum_emailtypes.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<enum_emailtype>
                                                            enum_emailtypes)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
