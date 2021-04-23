using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mmdm_organization
{
    public interface Imdm_organizationValidator : IServiceScoped
    {
        Task<bool>
    Create(mdm_organization mdm_organization);
    Task<bool>
        Update(mdm_organization mdm_organization);
        Task<bool>
            Delete(mdm_organization mdm_organization);
            Task<bool>
                BulkDelete(List<mdm_organization>
                    mdm_organizations);
                    Task<bool>
                        Import(List<mdm_organization>
                            mdm_organizations);
                            }

                            public class mdm_organizationValidator : Imdm_organizationValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public mdm_organizationValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(mdm_organization mdm_organization)
                                {
                                mdm_organizationFilter mdm_organizationFilter = new mdm_organizationFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = mdm_organization.Id },
                                Selects = mdm_organizationSelect.Id
                                };

                                int count = await UOW.mdm_organizationRepository.Count(mdm_organizationFilter);
                                if (count == 0)
                                mdm_organization.AddError(nameof(mdm_organizationValidator), nameof(mdm_organization.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(mdm_organization mdm_organization)
                                    {
                                    return mdm_organization.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(mdm_organization mdm_organization)
                                        {
                                        if (await ValidateId(mdm_organization))
                                        {
                                        }
                                        return mdm_organization.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(mdm_organization mdm_organization)
                                            {
                                            if (await ValidateId(mdm_organization))
                                            {
                                            }
                                            return mdm_organization.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<mdm_organization>
                                                    mdm_organizations)
                                                    {
                                                    foreach (mdm_organization mdm_organization in mdm_organizations)
                                                    {
                                                    await Delete(mdm_organization);
                                                    }
                                                    return mdm_organizations.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<mdm_organization>
                                                            mdm_organizations)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
