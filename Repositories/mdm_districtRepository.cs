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
    public interface Imdm_districtRepository
    {
        Task<int>
    Count(mdm_districtFilter mdm_districtFilter);
    Task<List<BASE.Entities.mdm_district>> List(mdm_districtFilter mdm_districtFilter);
        Task<BASE.Entities.mdm_district> Get(long Id);
        Task<bool> Create(BASE.Entities.mdm_district mdm_district);
        Task<bool> Update(BASE.Entities.mdm_district mdm_district);
        Task<bool> Delete(BASE.Entities.mdm_district mdm_district);
        Task<bool> BulkMerge(List<BASE.Entities.mdm_district> mdm_districts);
        Task<bool> BulkDelete(List<BASE.Entities.mdm_district> mdm_districts);
                    }
                    public class mdm_districtRepository : Imdm_districtRepository
                    {
                    private DataContext DataContext;
                    public mdm_districtRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.mdm_district>
                        DynamicFilter(IQueryable<BASE.Models.mdm_district>
                            query, mdm_districtFilter filter)
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
                            if (filter.Code != null)
                            query = query.Where(q => q.Code, filter.Code);
                            if (filter.Name != null)
                            query = query.Where(q => q.Name, filter.Name);
                            if (filter.Priority != null)
                            query = query.Where(q => q.Priority.HasValue).Where(q => q.Priority, filter.Priority);
                            if (filter.ProvinceId != null)
                            query = query.Where(q => q.ProvinceId, filter.ProvinceId);
                            if (filter.StatusId != null)
                            query = query.Where(q => q.StatusId, filter.StatusId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.mdm_district>
                                OrFilter(IQueryable<BASE.Models.mdm_district>
                                    query, mdm_districtFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.mdm_district>
                                        initQuery = query.Where(q => false);
                                        foreach (mdm_districtFilter mdm_districtFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.mdm_district>
                                            queryable = query;
                                            if (mdm_districtFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, mdm_districtFilter.Id);
                                            if (mdm_districtFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, mdm_districtFilter.Code);
                                            if (mdm_districtFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, mdm_districtFilter.Name);
                                            if (mdm_districtFilter.Priority != null)
                                            queryable = queryable.Where(q => q.Priority.HasValue).Where(q => q.Priority, mdm_districtFilter.Priority);
                                            if (mdm_districtFilter.ProvinceId != null)
                                            queryable = queryable.Where(q => q.ProvinceId, mdm_districtFilter.ProvinceId);
                                            if (mdm_districtFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId, mdm_districtFilter.StatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.mdm_district>
                                                DynamicOrder(IQueryable<BASE.Models.mdm_district>
                                                    query, mdm_districtFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_districtOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case mdm_districtOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case mdm_districtOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case mdm_districtOrder.Priority:
                                                    query = query.OrderBy(q => q.Priority);
                                                    break;
                                                    case mdm_districtOrder.Province:
                                                    query = query.OrderBy(q => q.ProvinceId);
                                                    break;
                                                    case mdm_districtOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_districtOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case mdm_districtOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case mdm_districtOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case mdm_districtOrder.Priority:
                                                    query = query.OrderByDescending(q => q.Priority);
                                                    break;
                                                    case mdm_districtOrder.Province:
                                                    query = query.OrderByDescending(q => q.ProvinceId);
                                                    break;
                                                    case mdm_districtOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.mdm_district>> DynamicSelect(IQueryable<BASE.Models.mdm_district> query, mdm_districtFilter filter)
        {
            List<mdm_district> mdm_districts = await query.Select(q => new mdm_district()
            {
                Id = filter.Selects.Contains(mdm_districtSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(mdm_districtSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(mdm_districtSelect.Name) ? q.Name : default(string),
                Priority = filter.Selects.Contains(mdm_districtSelect.Priority) ? q.Priority : default(long?),
                ProvinceId = filter.Selects.Contains(mdm_districtSelect.Province) ? q.ProvinceId : default(long),
                StatusId = filter.Selects.Contains(mdm_districtSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(mdm_districtSelect.Used) && q.Used != null ? new UInt64
                {
                } : null,
                Province = filter.Selects.Contains(mdm_districtSelect.Province) && q.Province != null ? new mdm_province
                {
                    Id = q.Province.Id,
                    Code = q.Province.Code,
                    Name = q.Province.Name,
                    Priority = q.Province.Priority,
                    StatusId = q.Province.StatusId,
                    Used = q.Province.Used,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return mdm_districts;
        }

        public async Task<int> Count(mdm_districtFilter filter)
        {
            IQueryable<BASE.Models.mdm_district> mdm_districts = DataContext.mdm_district.AsNoTracking();
            mdm_districts = DynamicFilter(mdm_districts, filter);
            return await mdm_districts.CountAsync();
        }

        public async Task<List<mdm_district>> List(mdm_districtFilter filter)
        {
            if (filter == null) return new List<mdm_district>();
            IQueryable<BASE.Models.mdm_district> mdm_districts = DataContext.mdm_district.AsNoTracking();
            mdm_districts = DynamicFilter(mdm_districts, filter);
            mdm_districts = DynamicOrder(mdm_districts, filter);
            List<mdm_district> mdm_districts = await DynamicSelect(mdm_districts, filter);
            return mdm_districts;
        }

        public async Task<mdm_district> Get(long Id)
        {
            mdm_district mdm_district = await DataContext.mdm_district.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new mdm_district()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Priority = x.Priority,
                ProvinceId = x.ProvinceId,
                StatusId = x.StatusId,
                Used = x.Used == null ? null : new UInt64
                {
                },
                Province = x.Province == null ? null : new mdm_province
                {
                    Id = x.Province.Id,
                    Code = x.Province.Code,
                    Name = x.Province.Name,
                    Priority = x.Province.Priority,
                    StatusId = x.Province.StatusId,
                    Used = x.Province.Used,
                },
            }).FirstOrDefaultAsync();

            if (mdm_district == null)
                return null;

            return mdm_district;
        }
        public async Task<bool> Create(mdm_district mdm_district)
        {
            mdm_district mdm_district = new mdm_district();
            mdm_district.Id = mdm_district.Id;
            mdm_district.Code = mdm_district.Code;
            mdm_district.Name = mdm_district.Name;
            mdm_district.Priority = mdm_district.Priority;
            mdm_district.ProvinceId = mdm_district.ProvinceId;
            mdm_district.StatusId = mdm_district.StatusId;
            mdm_district.CreatedAt = StaticParams.DateTimeNow;
            mdm_district.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.mdm_district.Add(mdm_district);
            await DataContext.SaveChangesAsync();
            mdm_district.Id = mdm_district.Id;
            await SaveReference(mdm_district);
            return true;
        }

        public async Task<bool> Update(mdm_district mdm_district)
        {
            mdm_district mdm_district = DataContext.mdm_district.Where(x => x.Id == mdm_district.Id).FirstOrDefault();
            if (mdm_district == null)
                return false;
            mdm_district.Id = mdm_district.Id;
            mdm_district.Code = mdm_district.Code;
            mdm_district.Name = mdm_district.Name;
            mdm_district.Priority = mdm_district.Priority;
            mdm_district.ProvinceId = mdm_district.ProvinceId;
            mdm_district.StatusId = mdm_district.StatusId;
            mdm_district.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(mdm_district);
            return true;
        }

        public async Task<bool> Delete(mdm_district mdm_district)
        {
            await DataContext.mdm_district.Where(x => x.Id == mdm_district.Id).UpdateFromQueryAsync(x => new mdm_district { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<mdm_district> mdm_districts)
        {
            List<mdm_district> mdm_districts = new List<mdm_district>();
            foreach (mdm_district mdm_district in mdm_districts)
            {
                mdm_district mdm_district = new mdm_district();
                mdm_district.Id = mdm_district.Id;
                mdm_district.Code = mdm_district.Code;
                mdm_district.Name = mdm_district.Name;
                mdm_district.Priority = mdm_district.Priority;
                mdm_district.ProvinceId = mdm_district.ProvinceId;
                mdm_district.StatusId = mdm_district.StatusId;
                mdm_district.CreatedAt = StaticParams.DateTimeNow;
                mdm_district.UpdatedAt = StaticParams.DateTimeNow;
                mdm_districts.Add(mdm_district);
            }
            await DataContext.BulkMergeAsync(mdm_districts);
            return true;
        }

        public async Task<bool> BulkDelete(List<mdm_district> mdm_districts)
        {
            List<long> Ids = mdm_districts.Select(x => x.Id).ToList();
            await DataContext.mdm_district
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new mdm_district { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(mdm_district mdm_district)
        {
        }

    }
}
