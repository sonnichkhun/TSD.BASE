using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mmdm_profession
{
    public interface Imdm_professionValidator : IServiceScoped
    {
        Task<bool>
    Create(mdm_profession mdm_profession);
    Task<bool>
        Update(mdm_profession mdm_profession);
        Task<bool>
            Delete(mdm_profession mdm_profession);
            Task<bool>
                BulkDelete(List<mdm_profession>
                    mdm_professions);
                    Task<bool>
                        Import(List<mdm_profession>
                            mdm_professions);
                            }

                            public class mdm_professionValidator : Imdm_professionValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public mdm_professionValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(mdm_profession mdm_profession)
                                {
                                mdm_professionFilter mdm_professionFilter = new mdm_professionFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = mdm_profession.Id },
                                Selects = mdm_professionSelect.Id
                                };

                                int count = await UOW.mdm_professionRepository.Count(mdm_professionFilter);
                                if (count == 0)
                                mdm_profession.AddError(nameof(mdm_professionValidator), nameof(mdm_profession.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(mdm_profession mdm_profession)
                                    {
                                    return mdm_profession.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(mdm_profession mdm_profession)
                                        {
                                        if (await ValidateId(mdm_profession))
                                        {
                                        }
                                        return mdm_profession.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(mdm_profession mdm_profession)
                                            {
                                            if (await ValidateId(mdm_profession))
                                            {
                                            }
                                            return mdm_profession.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<mdm_profession>
                                                    mdm_professions)
                                                    {
                                                    foreach (mdm_profession mdm_profession in mdm_professions)
                                                    {
                                                    await Delete(mdm_profession);
                                                    }
                                                    return mdm_professions.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<mdm_profession>
                                                            mdm_professions)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
