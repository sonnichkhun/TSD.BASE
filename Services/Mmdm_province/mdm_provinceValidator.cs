using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mmdm_province
{
    public interface Imdm_provinceValidator : IServiceScoped
    {
        Task<bool>
    Create(mdm_province mdm_province);
    Task<bool>
        Update(mdm_province mdm_province);
        Task<bool>
            Delete(mdm_province mdm_province);
            Task<bool>
                BulkDelete(List<mdm_province>
                    mdm_provinces);
                    Task<bool>
                        Import(List<mdm_province>
                            mdm_provinces);
                            }

                            public class mdm_provinceValidator : Imdm_provinceValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public mdm_provinceValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(mdm_province mdm_province)
                                {
                                mdm_provinceFilter mdm_provinceFilter = new mdm_provinceFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = mdm_province.Id },
                                Selects = mdm_provinceSelect.Id
                                };

                                int count = await UOW.mdm_provinceRepository.Count(mdm_provinceFilter);
                                if (count == 0)
                                mdm_province.AddError(nameof(mdm_provinceValidator), nameof(mdm_province.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(mdm_province mdm_province)
                                    {
                                    return mdm_province.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(mdm_province mdm_province)
                                        {
                                        if (await ValidateId(mdm_province))
                                        {
                                        }
                                        return mdm_province.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(mdm_province mdm_province)
                                            {
                                            if (await ValidateId(mdm_province))
                                            {
                                            }
                                            return mdm_province.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<mdm_province>
                                                    mdm_provinces)
                                                    {
                                                    foreach (mdm_province mdm_province in mdm_provinces)
                                                    {
                                                    await Delete(mdm_province);
                                                    }
                                                    return mdm_provinces.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<mdm_province>
                                                            mdm_provinces)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
