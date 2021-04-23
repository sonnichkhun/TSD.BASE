using BASE.Common;
using BASE.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using BASE.Repositories;
using BASE.Entities;

namespace BASE.Services.Mmdm_image
{
    public interface Imdm_imageService :  IServiceScoped
    {
        Task<int>
    Count(mdm_imageFilter mdm_imageFilter);
    Task<List<mdm_image>> List(mdm_imageFilter mdm_imageFilter);
        Task<mdm_image> Get(long Id);
        Task<mdm_image> Create(mdm_image mdm_image);
        Task<mdm_image> Update(mdm_image mdm_image);
        Task<mdm_image> Delete(mdm_image mdm_image);
        Task<List<mdm_image>> BulkDelete(List<mdm_image> mdm_images);
        Task<List<mdm_image>> Import(List<mdm_image> mdm_images);
        mdm_imageFilter ToFilter(mdm_imageFilter mdm_imageFilter);
    }

    public class mdm_imageService : BaseService, Imdm_imageService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private Imdm_imageValidator mdm_imageValidator;

        public mdm_imageService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            Imdm_imageValidator mdm_imageValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.mdm_imageValidator = mdm_imageValidator;
        }
        public async Task<int> Count(mdm_imageFilter mdm_imageFilter)
        {
            try
            {
                int result = await UOW.mdm_imageRepository.Count(mdm_imageFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_imageService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_imageService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_image>> List(mdm_imageFilter mdm_imageFilter)
        {
            try
            {
                List<mdm_image> mdm_images = await UOW.mdm_imageRepository.List(mdm_imageFilter);
                return mdm_images;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_imageService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_imageService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<mdm_image> Get(long Id)
        {
            mdm_image mdm_image = await UOW.mdm_imageRepository.Get(Id);
            if (mdm_image == null)
                return null;
            return mdm_image;
        }

        public async Task<mdm_image> Create(mdm_image mdm_image)
        {
            if (!await mdm_imageValidator.Create(mdm_image))
                return mdm_image;

            try
            {
                await UOW.Begin();
                await UOW.mdm_imageRepository.Create(mdm_image);
                await UOW.Commit();
                mdm_image = await UOW.mdm_imageRepository.Get(mdm_image.Id);
                await Logging.CreateAuditLog(mdm_image, new { }, nameof(mdm_imageService));
                return mdm_image;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_imageService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_imageService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_image> Update(mdm_image mdm_image)
        {
            if (!await mdm_imageValidator.Update(mdm_image))
                return mdm_image;
            try
            {
                var oldData = await UOW.mdm_imageRepository.Get(mdm_image.Id);

                await UOW.Begin();
                await UOW.mdm_imageRepository.Update(mdm_image);
                await UOW.Commit();

                mdm_image = await UOW.mdm_imageRepository.Get(mdm_image.Id);
                await Logging.CreateAuditLog(mdm_image, oldData, nameof(mdm_imageService));
                return mdm_image;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_imageService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_imageService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<mdm_image> Delete(mdm_image mdm_image)
        {
            if (!await mdm_imageValidator.Delete(mdm_image))
                return mdm_image;

            try
            {
                await UOW.Begin();
                await UOW.mdm_imageRepository.Delete(mdm_image);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_image, nameof(mdm_imageService));
                return mdm_image;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_imageService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_imageService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_image>> BulkDelete(List<mdm_image> mdm_images)
        {
            if (!await mdm_imageValidator.BulkDelete(mdm_images))
                return mdm_images;

            try
            {
                await UOW.Begin();
                await UOW.mdm_imageRepository.BulkDelete(mdm_images);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, mdm_images, nameof(mdm_imageService));
                return mdm_images;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_imageService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_imageService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<mdm_image>> Import(List<mdm_image> mdm_images)
        {
            if (!await mdm_imageValidator.Import(mdm_images))
                return mdm_images;
            try
            {
                await UOW.Begin();
                await UOW.mdm_imageRepository.BulkMerge(mdm_images);
                await UOW.Commit();

                await Logging.CreateAuditLog(mdm_images, new { }, nameof(mdm_imageService));
                return mdm_images;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(mdm_imageService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(mdm_imageService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public mdm_imageFilter ToFilter(mdm_imageFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<mdm_imageFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                mdm_imageFilter subFilter = new mdm_imageFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Url))
                        
                        
                        
                        
                        
                        
                        subFilter.Url = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ThumbnailUrl))
                        
                        
                        
                        
                        
                        
                        subFilter.ThumbnailUrl = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
