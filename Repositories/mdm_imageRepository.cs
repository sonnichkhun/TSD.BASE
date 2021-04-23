using BASE.Common;
using BASE.Entities;
using BASE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BASE.Helpers;

namespace BASE.Repositories
{
    public interface Imdm_imageRepository
    {
        Task<int>
    Count(mdm_imageFilter mdm_imageFilter);
    Task<List<BASE.Entities.mdm_image>> List(mdm_imageFilter mdm_imageFilter);
        Task<BASE.Entities.mdm_image> Get(long Id);
        Task<bool> Create(BASE.Entities.mdm_image mdm_image);
        Task<bool> Update(BASE.Entities.mdm_image mdm_image);
        Task<bool> Delete(BASE.Entities.mdm_image mdm_image);
        Task<bool> BulkMerge(List<BASE.Entities.mdm_image> mdm_images);
        Task<bool> BulkDelete(List<BASE.Entities.mdm_image> mdm_images);
                    }
                    public class mdm_imageRepository : Imdm_imageRepository
                    {
                    private DataContext DataContext;
                    public mdm_imageRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.mdm_image>
                        DynamicFilter(IQueryable<BASE.Models.mdm_image>
                            query, mdm_imageFilter filter)
                            {
                            if (filter == null)
                            return query.Where(q => false);
                            query = query.Where(q => !q.DeletedAt.HasValue);
                            if (filter.CreatedAt != null)
                            query = query.Where(q => q.CreatedAt, filter.CreatedAt);
                            if (filter.UpdatedAt != null)
                            query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
                            if (filter.Id != null)
                            query = query.Where(q => q.Id, filter.Id);
                            if (filter.Name != null)
                            query = query.Where(q => q.Name, filter.Name);
                            if (filter.Url != null)
                            query = query.Where(q => q.Url, filter.Url);
                            if (filter.ThumbnailUrl != null)
                            query = query.Where(q => q.ThumbnailUrl, filter.ThumbnailUrl);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.mdm_image>
                                OrFilter(IQueryable<BASE.Models.mdm_image>
                                    query, mdm_imageFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.mdm_image>
                                        initQuery = query.Where(q => false);
                                        foreach (mdm_imageFilter mdm_imageFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.mdm_image>
                                            queryable = query;
                                            if (mdm_imageFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, mdm_imageFilter.Id);
                                            if (mdm_imageFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, mdm_imageFilter.Name);
                                            if (mdm_imageFilter.Url != null)
                                            queryable = queryable.Where(q => q.Url, mdm_imageFilter.Url);
                                            if (mdm_imageFilter.ThumbnailUrl != null)
                                            queryable = queryable.Where(q => q.ThumbnailUrl, mdm_imageFilter.ThumbnailUrl);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.mdm_image>
                                                DynamicOrder(IQueryable<BASE.Models.mdm_image>
                                                    query, mdm_imageFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_imageOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case mdm_imageOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case mdm_imageOrder.Url:
                                                    query = query.OrderBy(q => q.Url);
                                                    break;
                                                    case mdm_imageOrder.ThumbnailUrl:
                                                    query = query.OrderBy(q => q.ThumbnailUrl);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_imageOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case mdm_imageOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case mdm_imageOrder.Url:
                                                    query = query.OrderByDescending(q => q.Url);
                                                    break;
                                                    case mdm_imageOrder.ThumbnailUrl:
                                                    query = query.OrderByDescending(q => q.ThumbnailUrl);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.mdm_image>> DynamicSelect(IQueryable<BASE.Models.mdm_image> query, mdm_imageFilter filter)
        {
            List<mdm_image> mdm_images = await query.Select(q => new mdm_image()
            {
                Id = filter.Selects.Contains(mdm_imageSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(mdm_imageSelect.Name) ? q.Name : default(string),
                Url = filter.Selects.Contains(mdm_imageSelect.Url) ? q.Url : default(string),
                ThumbnailUrl = filter.Selects.Contains(mdm_imageSelect.ThumbnailUrl) ? q.ThumbnailUrl : default(string),
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return mdm_images;
        }

        public async Task<int> Count(mdm_imageFilter filter)
        {
            IQueryable<BASE.Models.mdm_image> mdm_images = DataContext.mdm_image.AsNoTracking();
            mdm_images = DynamicFilter(mdm_images, filter);
            return await mdm_images.CountAsync();
        }

        public async Task<List<mdm_image>> List(mdm_imageFilter filter)
        {
            if (filter == null) return new List<mdm_image>();
            IQueryable<BASE.Models.mdm_image> mdm_images = DataContext.mdm_image.AsNoTracking();
            mdm_images = DynamicFilter(mdm_images, filter);
            mdm_images = DynamicOrder(mdm_images, filter);
            List<mdm_image> mdm_images = await DynamicSelect(mdm_images, filter);
            return mdm_images;
        }

        public async Task<mdm_image> Get(long Id)
        {
            mdm_image mdm_image = await DataContext.mdm_image.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new mdm_image()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Name = x.Name,
                Url = x.Url,
                ThumbnailUrl = x.ThumbnailUrl,
            }).FirstOrDefaultAsync();

            if (mdm_image == null)
                return null;

            return mdm_image;
        }
        public async Task<bool> Create(mdm_image mdm_image)
        {
            mdm_image mdm_image = new mdm_image();
            mdm_image.Id = mdm_image.Id;
            mdm_image.Name = mdm_image.Name;
            mdm_image.Url = mdm_image.Url;
            mdm_image.ThumbnailUrl = mdm_image.ThumbnailUrl;
            mdm_image.CreatedAt = StaticParams.DateTimeNow;
            mdm_image.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.mdm_image.Add(mdm_image);
            await DataContext.SaveChangesAsync();
            mdm_image.Id = mdm_image.Id;
            await SaveReference(mdm_image);
            return true;
        }

        public async Task<bool> Update(mdm_image mdm_image)
        {
            mdm_image mdm_image = DataContext.mdm_image.Where(x => x.Id == mdm_image.Id).FirstOrDefault();
            if (mdm_image == null)
                return false;
            mdm_image.Id = mdm_image.Id;
            mdm_image.Name = mdm_image.Name;
            mdm_image.Url = mdm_image.Url;
            mdm_image.ThumbnailUrl = mdm_image.ThumbnailUrl;
            mdm_image.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(mdm_image);
            return true;
        }

        public async Task<bool> Delete(mdm_image mdm_image)
        {
            await DataContext.mdm_image.Where(x => x.Id == mdm_image.Id).UpdateFromQueryAsync(x => new mdm_image { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<mdm_image> mdm_images)
        {
            List<mdm_image> mdm_images = new List<mdm_image>();
            foreach (mdm_image mdm_image in mdm_images)
            {
                mdm_image mdm_image = new mdm_image();
                mdm_image.Id = mdm_image.Id;
                mdm_image.Name = mdm_image.Name;
                mdm_image.Url = mdm_image.Url;
                mdm_image.ThumbnailUrl = mdm_image.ThumbnailUrl;
                mdm_image.CreatedAt = StaticParams.DateTimeNow;
                mdm_image.UpdatedAt = StaticParams.DateTimeNow;
                mdm_images.Add(mdm_image);
            }
            await DataContext.BulkMergeAsync(mdm_images);
            return true;
        }

        public async Task<bool> BulkDelete(List<mdm_image> mdm_images)
        {
            List<long> Ids = mdm_images.Select(x => x.Id).ToList();
            await DataContext.mdm_image
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new mdm_image { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(mdm_image mdm_image)
        {
        }

    }
}
