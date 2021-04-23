using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mmdm_position
{
    public interface Imdm_positionValidator : IServiceScoped
    {
        Task<bool>
    Create(mdm_position mdm_position);
    Task<bool>
        Update(mdm_position mdm_position);
        Task<bool>
            Delete(mdm_position mdm_position);
            Task<bool>
                BulkDelete(List<mdm_position>
                    mdm_positions);
                    Task<bool>
                        Import(List<mdm_position>
                            mdm_positions);
                            }

                            public class mdm_positionValidator : Imdm_positionValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public mdm_positionValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(mdm_position mdm_position)
                                {
                                mdm_positionFilter mdm_positionFilter = new mdm_positionFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = mdm_position.Id },
                                Selects = mdm_positionSelect.Id
                                };

                                int count = await UOW.mdm_positionRepository.Count(mdm_positionFilter);
                                if (count == 0)
                                mdm_position.AddError(nameof(mdm_positionValidator), nameof(mdm_position.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(mdm_position mdm_position)
                                    {
                                    return mdm_position.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(mdm_position mdm_position)
                                        {
                                        if (await ValidateId(mdm_position))
                                        {
                                        }
                                        return mdm_position.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(mdm_position mdm_position)
                                            {
                                            if (await ValidateId(mdm_position))
                                            {
                                            }
                                            return mdm_position.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<mdm_position>
                                                    mdm_positions)
                                                    {
                                                    foreach (mdm_position mdm_position in mdm_positions)
                                                    {
                                                    await Delete(mdm_position);
                                                    }
                                                    return mdm_positions.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<mdm_position>
                                                            mdm_positions)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
