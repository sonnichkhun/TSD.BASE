using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mmdm_unitofmeasure
{
    public interface Imdm_unitofmeasureValidator : IServiceScoped
    {
        Task<bool>
    Create(mdm_unitofmeasure mdm_unitofmeasure);
    Task<bool>
        Update(mdm_unitofmeasure mdm_unitofmeasure);
        Task<bool>
            Delete(mdm_unitofmeasure mdm_unitofmeasure);
            Task<bool>
                BulkDelete(List<mdm_unitofmeasure>
                    mdm_unitofmeasures);
                    Task<bool>
                        Import(List<mdm_unitofmeasure>
                            mdm_unitofmeasures);
                            }

                            public class mdm_unitofmeasureValidator : Imdm_unitofmeasureValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public mdm_unitofmeasureValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(mdm_unitofmeasure mdm_unitofmeasure)
                                {
                                mdm_unitofmeasureFilter mdm_unitofmeasureFilter = new mdm_unitofmeasureFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = mdm_unitofmeasure.Id },
                                Selects = mdm_unitofmeasureSelect.Id
                                };

                                int count = await UOW.mdm_unitofmeasureRepository.Count(mdm_unitofmeasureFilter);
                                if (count == 0)
                                mdm_unitofmeasure.AddError(nameof(mdm_unitofmeasureValidator), nameof(mdm_unitofmeasure.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(mdm_unitofmeasure mdm_unitofmeasure)
                                    {
                                    return mdm_unitofmeasure.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(mdm_unitofmeasure mdm_unitofmeasure)
                                        {
                                        if (await ValidateId(mdm_unitofmeasure))
                                        {
                                        }
                                        return mdm_unitofmeasure.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(mdm_unitofmeasure mdm_unitofmeasure)
                                            {
                                            if (await ValidateId(mdm_unitofmeasure))
                                            {
                                            }
                                            return mdm_unitofmeasure.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<mdm_unitofmeasure>
                                                    mdm_unitofmeasures)
                                                    {
                                                    foreach (mdm_unitofmeasure mdm_unitofmeasure in mdm_unitofmeasures)
                                                    {
                                                    await Delete(mdm_unitofmeasure);
                                                    }
                                                    return mdm_unitofmeasures.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<mdm_unitofmeasure>
                                                            mdm_unitofmeasures)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
