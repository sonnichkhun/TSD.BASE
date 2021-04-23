using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mmdm_phonetype
{
    public interface Imdm_phonetypeValidator : IServiceScoped
    {
        Task<bool>
    Create(mdm_phonetype mdm_phonetype);
    Task<bool>
        Update(mdm_phonetype mdm_phonetype);
        Task<bool>
            Delete(mdm_phonetype mdm_phonetype);
            Task<bool>
                BulkDelete(List<mdm_phonetype>
                    mdm_phonetypes);
                    Task<bool>
                        Import(List<mdm_phonetype>
                            mdm_phonetypes);
                            }

                            public class mdm_phonetypeValidator : Imdm_phonetypeValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public mdm_phonetypeValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(mdm_phonetype mdm_phonetype)
                                {
                                mdm_phonetypeFilter mdm_phonetypeFilter = new mdm_phonetypeFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = mdm_phonetype.Id },
                                Selects = mdm_phonetypeSelect.Id
                                };

                                int count = await UOW.mdm_phonetypeRepository.Count(mdm_phonetypeFilter);
                                if (count == 0)
                                mdm_phonetype.AddError(nameof(mdm_phonetypeValidator), nameof(mdm_phonetype.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(mdm_phonetype mdm_phonetype)
                                    {
                                    return mdm_phonetype.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(mdm_phonetype mdm_phonetype)
                                        {
                                        if (await ValidateId(mdm_phonetype))
                                        {
                                        }
                                        return mdm_phonetype.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(mdm_phonetype mdm_phonetype)
                                            {
                                            if (await ValidateId(mdm_phonetype))
                                            {
                                            }
                                            return mdm_phonetype.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<mdm_phonetype>
                                                    mdm_phonetypes)
                                                    {
                                                    foreach (mdm_phonetype mdm_phonetype in mdm_phonetypes)
                                                    {
                                                    await Delete(mdm_phonetype);
                                                    }
                                                    return mdm_phonetypes.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<mdm_phonetype>
                                                            mdm_phonetypes)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
