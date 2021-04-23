using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Menum_emailstatus
{
    public interface Ienum_emailstatusValidator : IServiceScoped
    {
        Task<bool>
    Create(enum_emailstatus enum_emailstatus);
    Task<bool>
        Update(enum_emailstatus enum_emailstatus);
        Task<bool>
            Delete(enum_emailstatus enum_emailstatus);
            Task<bool>
                BulkDelete(List<enum_emailstatus>
                    enum_emailstatuses);
                    Task<bool>
                        Import(List<enum_emailstatus>
                            enum_emailstatuses);
                            }

                            public class enum_emailstatusValidator : Ienum_emailstatusValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public enum_emailstatusValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(enum_emailstatus enum_emailstatus)
                                {
                                enum_emailstatusFilter enum_emailstatusFilter = new enum_emailstatusFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = enum_emailstatus.Id },
                                Selects = enum_emailstatusSelect.Id
                                };

                                int count = await UOW.enum_emailstatusRepository.Count(enum_emailstatusFilter);
                                if (count == 0)
                                enum_emailstatus.AddError(nameof(enum_emailstatusValidator), nameof(enum_emailstatus.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(enum_emailstatus enum_emailstatus)
                                    {
                                    return enum_emailstatus.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(enum_emailstatus enum_emailstatus)
                                        {
                                        if (await ValidateId(enum_emailstatus))
                                        {
                                        }
                                        return enum_emailstatus.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(enum_emailstatus enum_emailstatus)
                                            {
                                            if (await ValidateId(enum_emailstatus))
                                            {
                                            }
                                            return enum_emailstatus.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<enum_emailstatus>
                                                    enum_emailstatuses)
                                                    {
                                                    foreach (enum_emailstatus enum_emailstatus in enum_emailstatuses)
                                                    {
                                                    await Delete(enum_emailstatus);
                                                    }
                                                    return enum_emailstatuses.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<enum_emailstatus>
                                                            enum_emailstatuses)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
