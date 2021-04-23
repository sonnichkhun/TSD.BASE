using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Common;
using BASE.Entities;
using BASE;
using BASE.Repositories;

namespace BASE.Services.Mmdm_image
{
    public interface Imdm_imageValidator : IServiceScoped
    {
        Task<bool>
    Create(mdm_image mdm_image);
    Task<bool>
        Update(mdm_image mdm_image);
        Task<bool>
            Delete(mdm_image mdm_image);
            Task<bool>
                BulkDelete(List<mdm_image>
                    mdm_images);
                    Task<bool>
                        Import(List<mdm_image>
                            mdm_images);
                            }

                            public class mdm_imageValidator : Imdm_imageValidator
                            {
                            public enum ErrorCode
                            {
                            IdNotExisted,
                            }

                            private IUOW UOW;
                            private ICurrentContext CurrentContext;

                            public mdm_imageValidator(IUOW UOW, ICurrentContext CurrentContext)
                            {
                            this.UOW = UOW;
                            this.CurrentContext = CurrentContext;
                            }

                            public async Task<bool>
                                ValidateId(mdm_image mdm_image)
                                {
                                mdm_imageFilter mdm_imageFilter = new mdm_imageFilter
                                {
                                Skip = 0,
                                Take = 10,
                                Id = new IdFilter { Equal = mdm_image.Id },
                                Selects = mdm_imageSelect.Id
                                };

                                int count = await UOW.mdm_imageRepository.Count(mdm_imageFilter);
                                if (count == 0)
                                mdm_image.AddError(nameof(mdm_imageValidator), nameof(mdm_image.Id), ErrorCode.IdNotExisted);
                                return count == 1;
                                }

                                public async Task<bool>
                                    Create(mdm_image mdm_image)
                                    {
                                    return mdm_image.IsValidated;
                                    }

                                    public async Task<bool>
                                        Update(mdm_image mdm_image)
                                        {
                                        if (await ValidateId(mdm_image))
                                        {
                                        }
                                        return mdm_image.IsValidated;
                                        }

                                        public async Task<bool>
                                            Delete(mdm_image mdm_image)
                                            {
                                            if (await ValidateId(mdm_image))
                                            {
                                            }
                                            return mdm_image.IsValidated;
                                            }

                                            public async Task<bool>
                                                BulkDelete(List<mdm_image>
                                                    mdm_images)
                                                    {
                                                    foreach (mdm_image mdm_image in mdm_images)
                                                    {
                                                    await Delete(mdm_image);
                                                    }
                                                    return mdm_images.All(x => x.IsValidated);
                                                    }

                                                    public async Task<bool>
                                                        Import(List<mdm_image>
                                                            mdm_images)
                                                            {
                                                            return true;
                                                            }
                                                            }
                                                            }
