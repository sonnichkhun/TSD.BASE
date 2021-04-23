using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mmdm_district
{
    public interface Imdm_districtValidator : IServiceScoped
    {
        Task<bool>
    Create(mdm_district mdm_district);
    Task<bool>
        Update(mdm_district mdm_district);
        Task<bool>
            Delete(mdm_district mdm_district);
            Task<bool>
                BulkDelete(List<mdm_district>
                    mdm_districts);
                    Task<bool>
                        Import(List<mdm_district>
                            mdm_districts);
                            }

                            public class mdm_districtValidator : Imdm_districtValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public mdm_districtValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(mdm_district mdm_district)
                                {
                                mdm_districtFilter mdm_districtFilter = new mdm_districtFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = mdm_district.Id },
                                Selects = mdm_districtSelect.Id
                                };

                                int count = await UOW.mdm_districtRepository.Count(mdm_districtFilter);
                                if (count == 0)
                                mdm_district.AddError(nameof(mdm_districtValidator), nameof(mdm_district.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(mdm_district mdm_district)
                                    {
                                    return mdm_district.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(mdm_district mdm_district)
                                        {
                                        if (await ValidateId(mdm_district))
                                        {
                                        }
                                        return mdm_district.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(mdm_district mdm_district)
                                            {
                                            if (await ValidateId(mdm_district))
                                            {
                                            }
                                            return mdm_district.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<mdm_district>
                                                    mdm_districts)
                                                    {
                                                    foreach (mdm_district mdm_district in mdm_districts)
                                                    {
                                                    await Delete(mdm_district);
                                                    }
                                                    return mdm_districts.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<mdm_district>
                                                            mdm_districts)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
