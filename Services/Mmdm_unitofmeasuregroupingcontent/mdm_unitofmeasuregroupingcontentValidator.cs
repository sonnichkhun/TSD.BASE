using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mmdm_unitofmeasuregroupingcontent
{
    public interface Imdm_unitofmeasuregroupingcontentValidator : IServiceScoped
    {
        Task<bool>
    Create(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent);
    Task<bool>
        Update(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent);
        Task<bool>
            Delete(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent);
            Task<bool>
                BulkDelete(List<mdm_unitofmeasuregroupingcontent>
                    mdm_unitofmeasuregroupingcontents);
                    Task<bool>
                        Import(List<mdm_unitofmeasuregroupingcontent>
                            mdm_unitofmeasuregroupingcontents);
                            }

                            public class mdm_unitofmeasuregroupingcontentValidator : Imdm_unitofmeasuregroupingcontentValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public mdm_unitofmeasuregroupingcontentValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent)
                                {
                                mdm_unitofmeasuregroupingcontentFilter mdm_unitofmeasuregroupingcontentFilter = new mdm_unitofmeasuregroupingcontentFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = mdm_unitofmeasuregroupingcontent.Id },
                                Selects = mdm_unitofmeasuregroupingcontentSelect.Id
                                };

                                int count = await UOW.mdm_unitofmeasuregroupingcontentRepository.Count(mdm_unitofmeasuregroupingcontentFilter);
                                if (count == 0)
                                mdm_unitofmeasuregroupingcontent.AddError(nameof(mdm_unitofmeasuregroupingcontentValidator), nameof(mdm_unitofmeasuregroupingcontent.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent)
                                    {
                                    return mdm_unitofmeasuregroupingcontent.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent)
                                        {
                                        if (await ValidateId(mdm_unitofmeasuregroupingcontent))
                                        {
                                        }
                                        return mdm_unitofmeasuregroupingcontent.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent)
                                            {
                                            if (await ValidateId(mdm_unitofmeasuregroupingcontent))
                                            {
                                            }
                                            return mdm_unitofmeasuregroupingcontent.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<mdm_unitofmeasuregroupingcontent>
                                                    mdm_unitofmeasuregroupingcontents)
                                                    {
                                                    foreach (mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent in mdm_unitofmeasuregroupingcontents)
                                                    {
                                                    await Delete(mdm_unitofmeasuregroupingcontent);
                                                    }
                                                    return mdm_unitofmeasuregroupingcontents.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<mdm_unitofmeasuregroupingcontent>
                                                            mdm_unitofmeasuregroupingcontents)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
