using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mmdm_nation
{
    public interface Imdm_nationValidator : IServiceScoped
    {
        Task<bool>
    Create(mdm_nation mdm_nation);
    Task<bool>
        Update(mdm_nation mdm_nation);
        Task<bool>
            Delete(mdm_nation mdm_nation);
            Task<bool>
                BulkDelete(List<mdm_nation>
                    mdm_nations);
                    Task<bool>
                        Import(List<mdm_nation>
                            mdm_nations);
                            }

                            public class mdm_nationValidator : Imdm_nationValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public mdm_nationValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(mdm_nation mdm_nation)
                                {
                                mdm_nationFilter mdm_nationFilter = new mdm_nationFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = mdm_nation.Id },
                                Selects = mdm_nationSelect.Id
                                };

                                int count = await UOW.mdm_nationRepository.Count(mdm_nationFilter);
                                if (count == 0)
                                mdm_nation.AddError(nameof(mdm_nationValidator), nameof(mdm_nation.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(mdm_nation mdm_nation)
                                    {
                                    return mdm_nation.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(mdm_nation mdm_nation)
                                        {
                                        if (await ValidateId(mdm_nation))
                                        {
                                        }
                                        return mdm_nation.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(mdm_nation mdm_nation)
                                            {
                                            if (await ValidateId(mdm_nation))
                                            {
                                            }
                                            return mdm_nation.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<mdm_nation>
                                                    mdm_nations)
                                                    {
                                                    foreach (mdm_nation mdm_nation in mdm_nations)
                                                    {
                                                    await Delete(mdm_nation);
                                                    }
                                                    return mdm_nations.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<mdm_nation>
                                                            mdm_nations)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
