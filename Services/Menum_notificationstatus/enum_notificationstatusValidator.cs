using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Menum_notificationstatus
{
    public interface Ienum_notificationstatusValidator : IServiceScoped
    {
        Task<bool>
    Create(enum_notificationstatus enum_notificationstatus);
    Task<bool>
        Update(enum_notificationstatus enum_notificationstatus);
        Task<bool>
            Delete(enum_notificationstatus enum_notificationstatus);
            Task<bool>
                BulkDelete(List<enum_notificationstatus>
                    enum_notificationstatuses);
                    Task<bool>
                        Import(List<enum_notificationstatus>
                            enum_notificationstatuses);
                            }

                            public class enum_notificationstatusValidator : Ienum_notificationstatusValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public enum_notificationstatusValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(enum_notificationstatus enum_notificationstatus)
                                {
                                enum_notificationstatusFilter enum_notificationstatusFilter = new enum_notificationstatusFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = enum_notificationstatus.Id },
                                Selects = enum_notificationstatusSelect.Id
                                };

                                int count = await UOW.enum_notificationstatusRepository.Count(enum_notificationstatusFilter);
                                if (count == 0)
                                enum_notificationstatus.AddError(nameof(enum_notificationstatusValidator), nameof(enum_notificationstatus.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(enum_notificationstatus enum_notificationstatus)
                                    {
                                    return enum_notificationstatus.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(enum_notificationstatus enum_notificationstatus)
                                        {
                                        if (await ValidateId(enum_notificationstatus))
                                        {
                                        }
                                        return enum_notificationstatus.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(enum_notificationstatus enum_notificationstatus)
                                            {
                                            if (await ValidateId(enum_notificationstatus))
                                            {
                                            }
                                            return enum_notificationstatus.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<enum_notificationstatus>
                                                    enum_notificationstatuses)
                                                    {
                                                    foreach (enum_notificationstatus enum_notificationstatus in enum_notificationstatuses)
                                                    {
                                                    await Delete(enum_notificationstatus);
                                                    }
                                                    return enum_notificationstatuses.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<enum_notificationstatus>
                                                            enum_notificationstatuses)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
