using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mmdm_appuser
{
    public interface Imdm_appuserValidator : IServiceScoped
    {
        Task<bool>
    Create(mdm_appuser mdm_appuser);
    Task<bool>
        Update(mdm_appuser mdm_appuser);
        Task<bool>
            Delete(mdm_appuser mdm_appuser);
            Task<bool>
                BulkDelete(List<mdm_appuser>
                    mdm_appusers);
                    Task<bool>
                        Import(List<mdm_appuser>
                            mdm_appusers);
                            }

                            public class mdm_appuserValidator : Imdm_appuserValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public mdm_appuserValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(mdm_appuser mdm_appuser)
                                {
                                mdm_appuserFilter mdm_appuserFilter = new mdm_appuserFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = mdm_appuser.Id },
                                Selects = mdm_appuserSelect.Id
                                };

                                int count = await UOW.mdm_appuserRepository.Count(mdm_appuserFilter);
                                if (count == 0)
                                mdm_appuser.AddError(nameof(mdm_appuserValidator), nameof(mdm_appuser.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(mdm_appuser mdm_appuser)
                                    {
                                    return mdm_appuser.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(mdm_appuser mdm_appuser)
                                        {
                                        if (await ValidateId(mdm_appuser))
                                        {
                                        }
                                        return mdm_appuser.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(mdm_appuser mdm_appuser)
                                            {
                                            if (await ValidateId(mdm_appuser))
                                            {
                                            }
                                            return mdm_appuser.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<mdm_appuser>
                                                    mdm_appusers)
                                                    {
                                                    foreach (mdm_appuser mdm_appuser in mdm_appusers)
                                                    {
                                                    await Delete(mdm_appuser);
                                                    }
                                                    return mdm_appusers.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<mdm_appuser>
                                                            mdm_appusers)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
