using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Menum_status
{
    public interface Ienum_statusValidator : IServiceScoped
    {
        Task<bool>
    Create(enum_status enum_status);
    Task<bool>
        Update(enum_status enum_status);
        Task<bool>
            Delete(enum_status enum_status);
            Task<bool>
                BulkDelete(List<enum_status>
                    enum_statuses);
                    Task<bool>
                        Import(List<enum_status>
                            enum_statuses);
                            }

                            public class enum_statusValidator : Ienum_statusValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public enum_statusValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(enum_status enum_status)
                                {
                                enum_statusFilter enum_statusFilter = new enum_statusFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = enum_status.Id },
                                Selects = enum_statusSelect.Id
                                };

                                int count = await UOW.enum_statusRepository.Count(enum_statusFilter);
                                if (count == 0)
                                enum_status.AddError(nameof(enum_statusValidator), nameof(enum_status.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(enum_status enum_status)
                                    {
                                    return enum_status.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(enum_status enum_status)
                                        {
                                        if (await ValidateId(enum_status))
                                        {
                                        }
                                        return enum_status.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(enum_status enum_status)
                                            {
                                            if (await ValidateId(enum_status))
                                            {
                                            }
                                            return enum_status.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<enum_status>
                                                    enum_statuses)
                                                    {
                                                    foreach (enum_status enum_status in enum_statuses)
                                                    {
                                                    await Delete(enum_status);
                                                    }
                                                    return enum_statuses.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<enum_status>
                                                            enum_statuses)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
