using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mmdm_unitofmeasuregrouping
{
    public interface Imdm_unitofmeasuregroupingValidator : IServiceScoped
    {
        Task<bool>
    Create(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping);
    Task<bool>
        Update(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping);
        Task<bool>
            Delete(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping);
            Task<bool>
                BulkDelete(List<mdm_unitofmeasuregrouping>
                    mdm_unitofmeasuregroupings);
                    Task<bool>
                        Import(List<mdm_unitofmeasuregrouping>
                            mdm_unitofmeasuregroupings);
                            }

                            public class mdm_unitofmeasuregroupingValidator : Imdm_unitofmeasuregroupingValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public mdm_unitofmeasuregroupingValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping)
                                {
                                mdm_unitofmeasuregroupingFilter mdm_unitofmeasuregroupingFilter = new mdm_unitofmeasuregroupingFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = mdm_unitofmeasuregrouping.Id },
                                Selects = mdm_unitofmeasuregroupingSelect.Id
                                };

                                int count = await UOW.mdm_unitofmeasuregroupingRepository.Count(mdm_unitofmeasuregroupingFilter);
                                if (count == 0)
                                mdm_unitofmeasuregrouping.AddError(nameof(mdm_unitofmeasuregroupingValidator), nameof(mdm_unitofmeasuregrouping.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping)
                                    {
                                    return mdm_unitofmeasuregrouping.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping)
                                        {
                                        if (await ValidateId(mdm_unitofmeasuregrouping))
                                        {
                                        }
                                        return mdm_unitofmeasuregrouping.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping)
                                            {
                                            if (await ValidateId(mdm_unitofmeasuregrouping))
                                            {
                                            }
                                            return mdm_unitofmeasuregrouping.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<mdm_unitofmeasuregrouping>
                                                    mdm_unitofmeasuregroupings)
                                                    {
                                                    foreach (mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping in mdm_unitofmeasuregroupings)
                                                    {
                                                    await Delete(mdm_unitofmeasuregrouping);
                                                    }
                                                    return mdm_unitofmeasuregroupings.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<mdm_unitofmeasuregrouping>
                                                            mdm_unitofmeasuregroupings)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
