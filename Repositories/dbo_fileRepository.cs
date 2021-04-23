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
    public interface Idbo_fileRepository
    {
        Task<int>
    Count(dbo_fileFilter dbo_fileFilter);
        Task<List<BASE.Entities.dbo_file>> List(dbo_fileFilter dbo_fileFilter);
        Task<BASE.Entities.dbo_file> Get(long Id);
        Task<bool> Create(BASE.Entities.dbo_file dbo_file);
        Task<bool> Update(BASE.Entities.dbo_file dbo_file);
        Task<bool> Delete(BASE.Entities.dbo_file dbo_file);
        Task<bool> BulkMerge(List<BASE.Entities.dbo_file> dbo_files);
        Task<bool> BulkDelete(List<BASE.Entities.dbo_file> dbo_files);
    }
    public class dbo_fileRepository : Idbo_fileRepository
    {
        private DataContext DataContext;
        public dbo_fileRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<BASE.Models.dbo_file>
            DynamicFilter(IQueryable<BASE.Models.dbo_file>
                query, dbo_fileFilter filter)
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
            if (filter.AppUserId != null)
                query = query.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, filter.AppUserId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<BASE.Models.dbo_file>
            OrFilter(IQueryable<BASE.Models.dbo_file>
                query, dbo_fileFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<BASE.Models.dbo_file>
                initQuery = query.Where(q => false);
            foreach (dbo_fileFilter dbo_fileFilter in filter.OrFilter)
            {
                IQueryable<BASE.Models.dbo_file>
                    queryable = query;
                if (dbo_fileFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, dbo_fileFilter.Id);
                if (dbo_fileFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, dbo_fileFilter.Name);
                if (dbo_fileFilter.Url != null)
                    queryable = queryable.Where(q => q.Url, dbo_fileFilter.Url);
                if (dbo_fileFilter.AppUserId != null)
                    queryable = queryable.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, dbo_fileFilter.AppUserId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<BASE.Models.dbo_file>
            DynamicOrder(IQueryable<BASE.Models.dbo_file>
                query, dbo_fileFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case dbo_fileOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case dbo_fileOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case dbo_fileOrder.Url:
                            query = query.OrderBy(q => q.Url);
                            break;
                        case dbo_fileOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case dbo_fileOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case dbo_fileOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case dbo_fileOrder.Url:
                            query = query.OrderByDescending(q => q.Url);
                            break;
                        case dbo_fileOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<BASE.Models.dbo_file>> DynamicSelect(IQueryable<BASE.Models.dbo_file> query, dbo_fileFilter filter)
        {
            List<dbo_file> dbo_files = await query.Select(q => new dbo_file()
            {
                Id = filter.Selects.Contains(dbo_fileSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(dbo_fileSelect.Name) ? q.Name : default(string),
                Url = filter.Selects.Contains(dbo_fileSelect.Url) ? q.Url : default(string),
                AppUserId = filter.Selects.Contains(dbo_fileSelect.AppUser) ? q.AppUserId : default(long?),
                AppUser = filter.Selects.Contains(dbo_fileSelect.AppUser) && q.AppUser != null ? new mdm_appuser
                {
                    Id = q.AppUser.Id,
                    Username = q.AppUser.Username,
                    DisplayName = q.AppUser.DisplayName,
                    Address = q.AppUser.Address,
                    Email = q.AppUser.Email,
                    Phone = q.AppUser.Phone,
                    SexId = q.AppUser.SexId,
                    Birthday = q.AppUser.Birthday,
                    Avatar = q.AppUser.Avatar,
                    Department = q.AppUser.Department,
                    OrganizationId = q.AppUser.OrganizationId,
                    Longitude = q.AppUser.Longitude,
                    Latitude = q.AppUser.Latitude,
                    StatusId = q.AppUser.StatusId,
                    Used = q.AppUser.Used,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return dbo_files;
        }

        public async Task<int> Count(dbo_fileFilter filter)
        {
            IQueryable<BASE.Models.dbo_file> dbo_files = DataContext.dbo_file.AsNoTracking();
            dbo_files = DynamicFilter(dbo_files, filter);
            return await dbo_files.CountAsync();
        }

        public async Task<List<dbo_file>> List(dbo_fileFilter filter)
        {
            if (filter == null) return new List<dbo_file>();
            IQueryable<BASE.Models.dbo_file> dbo_files = DataContext.dbo_file.AsNoTracking();
            dbo_files = DynamicFilter(dbo_files, filter);
            dbo_files = DynamicOrder(dbo_files, filter);
            List<dbo_file> dbo_files = await DynamicSelect(dbo_files, filter);
            return dbo_files;
        }

        public async Task<dbo_file> Get(long Id)
        {
            dbo_file dbo_file = await DataContext.dbo_file.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new dbo_file()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Name = x.Name,
                Url = x.Url,
                AppUserId = x.AppUserId,
                AppUser = x.AppUser == null ? null : new mdm_appuser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Address = x.AppUser.Address,
                    Email = x.AppUser.Email,
                    Phone = x.AppUser.Phone,
                    SexId = x.AppUser.SexId,
                    Birthday = x.AppUser.Birthday,
                    Avatar = x.AppUser.Avatar,
                    Department = x.AppUser.Department,
                    OrganizationId = x.AppUser.OrganizationId,
                    Longitude = x.AppUser.Longitude,
                    Latitude = x.AppUser.Latitude,
                    StatusId = x.AppUser.StatusId,
                    Used = x.AppUser.Used,
                },
            }).FirstOrDefaultAsync();

            if (dbo_file == null)
                return null;

            return dbo_file;
        }
        public async Task<bool> Create(dbo_file dbo_file)
        {
            dbo_file dbo_file = new dbo_file();
            dbo_file.Id = dbo_file.Id;
            dbo_file.Name = dbo_file.Name;
            dbo_file.Url = dbo_file.Url;
            dbo_file.AppUserId = dbo_file.AppUserId;
            dbo_file.CreatedAt = StaticParams.DateTimeNow;
            dbo_file.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.dbo_file.Add(dbo_file);
            await DataContext.SaveChangesAsync();
            dbo_file.Id = dbo_file.Id;
            await SaveReference(dbo_file);
            return true;
        }

        public async Task<bool> Update(dbo_file dbo_file)
        {
            dbo_file dbo_file = DataContext.dbo_file.Where(x => x.Id == dbo_file.Id).FirstOrDefault();
            if (dbo_file == null)
                return false;
            dbo_file.Id = dbo_file.Id;
            dbo_file.Name = dbo_file.Name;
            dbo_file.Url = dbo_file.Url;
            dbo_file.AppUserId = dbo_file.AppUserId;
            dbo_file.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(dbo_file);
            return true;
        }

        public async Task<bool> Delete(dbo_file dbo_file)
        {
            await DataContext.dbo_file.Where(x => x.Id == dbo_file.Id).UpdateFromQueryAsync(x => new dbo_file { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<dbo_file> dbo_files)
        {
            List<dbo_file> dbo_files = new List<dbo_file>();
            foreach (dbo_file dbo_file in dbo_files)
            {
                dbo_file dbo_file = new dbo_file();
                dbo_file.Id = dbo_file.Id;
                dbo_file.Name = dbo_file.Name;
                dbo_file.Url = dbo_file.Url;
                dbo_file.AppUserId = dbo_file.AppUserId;
                dbo_file.CreatedAt = StaticParams.DateTimeNow;
                dbo_file.UpdatedAt = StaticParams.DateTimeNow;
                dbo_files.Add(dbo_file);
            }
            await DataContext.BulkMergeAsync(dbo_files);
            return true;
        }

        public async Task<bool> BulkDelete(List<dbo_file> dbo_files)
        {
            List<long> Ids = dbo_files.Select(x => x.Id).ToList();
            await DataContext.dbo_file
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new dbo_file { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(dbo_file dbo_file)
        {
        }

    }
}
