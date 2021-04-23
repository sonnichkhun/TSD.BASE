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
    public interface Imdm_provinceRepository
    {
        Task<int>
    Count(mdm_provinceFilter mdm_provinceFilter);
    Task<List<BASE.Entities.mdm_province>> List(mdm_provinceFilter mdm_provinceFilter);
        Task<BASE.Entities.mdm_province> Get(long Id);
        Task<bool> Create(BASE.Entities.mdm_province mdm_province);
        Task<bool> Update(BASE.Entities.mdm_province mdm_province);
        Task<bool> Delete(BASE.Entities.mdm_province mdm_province);
        Task<bool> BulkMerge(List<BASE.Entities.mdm_province> mdm_provinces);
        Task<bool> BulkDelete(List<BASE.Entities.mdm_province> mdm_provinces);
                    }
                    public class mdm_provinceRepository : Imdm_provinceRepository
                    {
                    private DataContext DataContext;
                    public mdm_provinceRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.mdm_province>
                        DynamicFilter(IQueryable<BASE.Models.mdm_province>
                            query, mdm_provinceFilter filter)
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
                            if (filter.StatusId != null)
                            query = query.Where(q => q.StatusId, filter.StatusId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.mdm_province>
                                OrFilter(IQueryable<BASE.Models.mdm_province>
                                    query, mdm_provinceFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.mdm_province>
                                        initQuery = query.Where(q => false);
                                        foreach (mdm_provinceFilter mdm_provinceFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.mdm_province>
                                            queryable = query;
                                            if (mdm_provinceFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, mdm_provinceFilter.Id);
                                            if (mdm_provinceFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, mdm_provinceFilter.Code);
                                            if (mdm_provinceFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, mdm_provinceFilter.Name);
                                            if (mdm_provinceFilter.Priority != null)
                                            queryable = queryable.Where(q => q.Priority.HasValue).Where(q => q.Priority, mdm_provinceFilter.Priority);
                                            if (mdm_provinceFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId, mdm_provinceFilter.StatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.mdm_province>
                                                DynamicOrder(IQueryable<BASE.Models.mdm_province>
                                                    query, mdm_provinceFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_provinceOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case mdm_provinceOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case mdm_provinceOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case mdm_provinceOrder.Priority:
                                                    query = query.OrderBy(q => q.Priority);
                                                    break;
                                                    case mdm_provinceOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    case mdm_provinceOrder.Used:
                                                    query = query.OrderBy(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_provinceOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case mdm_provinceOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case mdm_provinceOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case mdm_provinceOrder.Priority:
                                                    query = query.OrderByDescending(q => q.Priority);
                                                    break;
                                                    case mdm_provinceOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    case mdm_provinceOrder.Used:
                                                    query = query.OrderByDescending(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.mdm_province>> DynamicSelect(IQueryable<BASE.Models.mdm_province> query, mdm_provinceFilter filter)
        {
            List<mdm_province> mdm_provinces = await query.Select(q => new mdm_province()
            {
                Id = filter.Selects.Contains(mdm_provinceSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(mdm_provinceSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(mdm_provinceSelect.Name) ? q.Name : default(string),
                Priority = filter.Selects.Contains(mdm_provinceSelect.Priority) ? q.Priority : default(long?),
                StatusId = filter.Selects.Contains(mdm_provinceSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(mdm_provinceSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(mdm_provinceSelect.Status) && q.Status != null ? new enum_status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return mdm_provinces;
        }

        public async Task<int> Count(mdm_provinceFilter filter)
        {
            IQueryable<BASE.Models.mdm_province> mdm_provinces = DataContext.mdm_province.AsNoTracking();
            mdm_provinces = DynamicFilter(mdm_provinces, filter);
            return await mdm_provinces.CountAsync();
        }

        public async Task<List<mdm_province>> List(mdm_provinceFilter filter)
        {
            if (filter == null) return new List<mdm_province>();
            IQueryable<BASE.Models.mdm_province> mdm_provinces = DataContext.mdm_province.AsNoTracking();
            mdm_provinces = DynamicFilter(mdm_provinces, filter);
            mdm_provinces = DynamicOrder(mdm_provinces, filter);
            List<mdm_province> mdm_provinces = await DynamicSelect(mdm_provinces, filter);
            return mdm_provinces;
        }

        public async Task<mdm_province> Get(long Id)
        {
            mdm_province mdm_province = await DataContext.mdm_province.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new mdm_province()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Priority = x.Priority,
                StatusId = x.StatusId,
                Used = x.Used,
                Status = x.Status == null ? null : new enum_status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (mdm_province == null)
                return null;

            return mdm_province;
        }
        public async Task<bool> Create(mdm_province mdm_province)
        {
            mdm_province mdm_province = new mdm_province();
            mdm_province.Id = mdm_province.Id;
            mdm_province.Code = mdm_province.Code;
            mdm_province.Name = mdm_province.Name;
            mdm_province.Priority = mdm_province.Priority;
            mdm_province.StatusId = mdm_province.StatusId;
            mdm_province.Used = mdm_province.Used;
            mdm_province.CreatedAt = StaticParams.DateTimeNow;
            mdm_province.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.mdm_province.Add(mdm_province);
            await DataContext.SaveChangesAsync();
            mdm_province.Id = mdm_province.Id;
            await SaveReference(mdm_province);
            return true;
        }

        public async Task<bool> Update(mdm_province mdm_province)
        {
            mdm_province mdm_province = DataContext.mdm_province.Where(x => x.Id == mdm_province.Id).FirstOrDefault();
            if (mdm_province == null)
                return false;
            mdm_province.Id = mdm_province.Id;
            mdm_province.Code = mdm_province.Code;
            mdm_province.Name = mdm_province.Name;
            mdm_province.Priority = mdm_province.Priority;
            mdm_province.StatusId = mdm_province.StatusId;
            mdm_province.Used = mdm_province.Used;
            mdm_province.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(mdm_province);
            return true;
        }

        public async Task<bool> Delete(mdm_province mdm_province)
        {
            await DataContext.mdm_province.Where(x => x.Id == mdm_province.Id).UpdateFromQueryAsync(x => new mdm_province { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<mdm_province> mdm_provinces)
        {
            List<mdm_province> mdm_provinces = new List<mdm_province>();
            foreach (mdm_province mdm_province in mdm_provinces)
            {
                mdm_province mdm_province = new mdm_province();
                mdm_province.Id = mdm_province.Id;
                mdm_province.Code = mdm_province.Code;
                mdm_province.Name = mdm_province.Name;
                mdm_province.Priority = mdm_province.Priority;
                mdm_province.StatusId = mdm_province.StatusId;
                mdm_province.Used = mdm_province.Used;
                mdm_province.CreatedAt = StaticParams.DateTimeNow;
                mdm_province.UpdatedAt = StaticParams.DateTimeNow;
                mdm_provinces.Add(mdm_province);
            }
            await DataContext.BulkMergeAsync(mdm_provinces);
            return true;
        }

        public async Task<bool> BulkDelete(List<mdm_province> mdm_provinces)
        {
            List<long> Ids = mdm_provinces.Select(x => x.Id).ToList();
            await DataContext.mdm_province
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new mdm_province { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(mdm_province mdm_province)
        {
        }

    }
}
