using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mmdm_taxtype
{
    public interface Imdm_taxtypeValidator : IServiceScoped
    {
        Task<bool>
    Create(mdm_taxtype mdm_taxtype);
    Task<bool>
        Update(mdm_taxtype mdm_taxtype);
        Task<bool>
            Delete(mdm_taxtype mdm_taxtype);
            Task<bool>
                BulkDelete(List<mdm_taxtype>
                    mdm_taxtypes);
                    Task<bool>
                        Import(List<mdm_taxtype>
                            mdm_taxtypes);
                            }

                            public class mdm_taxtypeValidator : Imdm_taxtypeValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public mdm_taxtypeValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(mdm_taxtype mdm_taxtype)
                                {
                                mdm_taxtypeFilter mdm_taxtypeFilter = new mdm_taxtypeFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = mdm_taxtype.Id },
                                Selects = mdm_taxtypeSelect.Id
                                };

                                int count = await UOW.mdm_taxtypeRepository.Count(mdm_taxtypeFilter);
                                if (count == 0)
                                mdm_taxtype.AddError(nameof(mdm_taxtypeValidator), nameof(mdm_taxtype.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(mdm_taxtype mdm_taxtype)
                                    {
                                    return mdm_taxtype.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(mdm_taxtype mdm_taxtype)
                                        {
                                        if (await ValidateId(mdm_taxtype))
                                        {
                                        }
                                        return mdm_taxtype.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(mdm_taxtype mdm_taxtype)
                                            {
                                            if (await ValidateId(mdm_taxtype))
                                            {
                                            }
                                            return mdm_taxtype.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<mdm_taxtype>
                                                    mdm_taxtypes)
                                                    {
                                                    foreach (mdm_taxtype mdm_taxtype in mdm_taxtypes)
                                                    {
                                                    await Delete(mdm_taxtype);
                                                    }
                                                    return mdm_taxtypes.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<mdm_taxtype>
                                                            mdm_taxtypes)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
