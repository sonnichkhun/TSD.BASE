using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mdbo_mailtemplate
{
    public interface Idbo_mailtemplateValidator : IServiceScoped
    {
        Task<bool>
    Create(dbo_mailtemplate dbo_mailtemplate);
    Task<bool>
        Update(dbo_mailtemplate dbo_mailtemplate);
        Task<bool>
            Delete(dbo_mailtemplate dbo_mailtemplate);
            Task<bool>
                BulkDelete(List<dbo_mailtemplate>
                    dbo_mailtemplates);
                    Task<bool>
                        Import(List<dbo_mailtemplate>
                            dbo_mailtemplates);
                            }

                            public class dbo_mailtemplateValidator : Idbo_mailtemplateValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public dbo_mailtemplateValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(dbo_mailtemplate dbo_mailtemplate)
                                {
                                dbo_mailtemplateFilter dbo_mailtemplateFilter = new dbo_mailtemplateFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = dbo_mailtemplate.Id },
                                Selects = dbo_mailtemplateSelect.Id
                                };

                                int count = await UOW.dbo_mailtemplateRepository.Count(dbo_mailtemplateFilter);
                                if (count == 0)
                                dbo_mailtemplate.AddError(nameof(dbo_mailtemplateValidator), nameof(dbo_mailtemplate.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(dbo_mailtemplate dbo_mailtemplate)
                                    {
                                    return dbo_mailtemplate.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(dbo_mailtemplate dbo_mailtemplate)
                                        {
                                        if (await ValidateId(dbo_mailtemplate))
                                        {
                                        }
                                        return dbo_mailtemplate.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(dbo_mailtemplate dbo_mailtemplate)
                                            {
                                            if (await ValidateId(dbo_mailtemplate))
                                            {
                                            }
                                            return dbo_mailtemplate.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<dbo_mailtemplate>
                                                    dbo_mailtemplates)
                                                    {
                                                    foreach (dbo_mailtemplate dbo_mailtemplate in dbo_mailtemplates)
                                                    {
                                                    await Delete(dbo_mailtemplate);
                                                    }
                                                    return dbo_mailtemplates.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<dbo_mailtemplate>
                                                            dbo_mailtemplates)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
