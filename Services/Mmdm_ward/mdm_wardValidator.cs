using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mmdm_ward
{
    public interface Imdm_wardValidator : IServiceScoped
    {
        Task<bool>
    Create(mdm_ward mdm_ward);
    Task<bool>
        Update(mdm_ward mdm_ward);
        Task<bool>
            Delete(mdm_ward mdm_ward);
            Task<bool>
                BulkDelete(List<mdm_ward>
                    mdm_wards);
                    Task<bool>
                        Import(List<mdm_ward>
                            mdm_wards);
                            }

                            public class mdm_wardValidator : Imdm_wardValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public mdm_wardValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(mdm_ward mdm_ward)
                                {
                                mdm_wardFilter mdm_wardFilter = new mdm_wardFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = mdm_ward.Id },
                                Selects = mdm_wardSelect.Id
                                };

                                int count = await UOW.mdm_wardRepository.Count(mdm_wardFilter);
                                if (count == 0)
                                mdm_ward.AddError(nameof(mdm_wardValidator), nameof(mdm_ward.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(mdm_ward mdm_ward)
                                    {
                                    return mdm_ward.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(mdm_ward mdm_ward)
                                        {
                                        if (await ValidateId(mdm_ward))
                                        {
                                        }
                                        return mdm_ward.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(mdm_ward mdm_ward)
                                            {
                                            if (await ValidateId(mdm_ward))
                                            {
                                            }
                                            return mdm_ward.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<mdm_ward>
                                                    mdm_wards)
                                                    {
                                                    foreach (mdm_ward mdm_ward in mdm_wards)
                                                    {
                                                    await Delete(mdm_ward);
                                                    }
                                                    return mdm_wards.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<mdm_ward>
                                                            mdm_wards)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
