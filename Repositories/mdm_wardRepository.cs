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
    public interface Imdm_wardRepository
    {
        Task<int>
    Count(mdm_wardFilter mdm_wardFilter);
    Task<List<BASE.Entities.mdm_ward>> List(mdm_wardFilter mdm_wardFilter);
        Task<BASE.Entities.mdm_ward> Get(long Id);
        Task<bool> Create(BASE.Entities.mdm_ward mdm_ward);
        Task<bool> Update(BASE.Entities.mdm_ward mdm_ward);
        Task<bool> Delete(BASE.Entities.mdm_ward mdm_ward);
        Task<bool> BulkMerge(List<BASE.Entities.mdm_ward> mdm_wards);
        Task<bool> BulkDelete(List<BASE.Entities.mdm_ward> mdm_wards);
                    }
                    public class mdm_wardRepository : Imdm_wardRepository
                    {
                    private DataContext DataContext;
                    public mdm_wardRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.mdm_ward>
                        DynamicFilter(IQueryable<BASE.Models.mdm_ward>
                            query, mdm_wardFilter filter)
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
                            if (filter.DistrictId != null)
                            query = query.Where(q => q.DistrictId, filter.DistrictId);
                            if (filter.StatusId != null)
                            query = query.Where(q => q.StatusId, filter.StatusId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.mdm_ward>
                                OrFilter(IQueryable<BASE.Models.mdm_ward>
                                    query, mdm_wardFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.mdm_ward>
                                        initQuery = query.Where(q => false);
                                        foreach (mdm_wardFilter mdm_wardFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.mdm_ward>
                                            queryable = query;
                                            if (mdm_wardFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, mdm_wardFilter.Id);
                                            if (mdm_wardFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, mdm_wardFilter.Code);
                                            if (mdm_wardFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, mdm_wardFilter.Name);
                                            if (mdm_wardFilter.Priority != null)
                                            queryable = queryable.Where(q => q.Priority.HasValue).Where(q => q.Priority, mdm_wardFilter.Priority);
                                            if (mdm_wardFilter.DistrictId != null)
                                            queryable = queryable.Where(q => q.DistrictId, mdm_wardFilter.DistrictId);
                                            if (mdm_wardFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId, mdm_wardFilter.StatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.mdm_ward>
                                                DynamicOrder(IQueryable<BASE.Models.mdm_ward>
                                                    query, mdm_wardFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_wardOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case mdm_wardOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case mdm_wardOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case mdm_wardOrder.Priority:
                                                    query = query.OrderBy(q => q.Priority);
                                                    break;
                                                    case mdm_wardOrder.District:
                                                    query = query.OrderBy(q => q.DistrictId);
                                                    break;
                                                    case mdm_wardOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    case mdm_wardOrder.Used:
                                                    query = query.OrderBy(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_wardOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case mdm_wardOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case mdm_wardOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case mdm_wardOrder.Priority:
                                                    query = query.OrderByDescending(q => q.Priority);
                                                    break;
                                                    case mdm_wardOrder.District:
                                                    query = query.OrderByDescending(q => q.DistrictId);
                                                    break;
                                                    case mdm_wardOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    case mdm_wardOrder.Used:
                                                    query = query.OrderByDescending(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.mdm_ward>> DynamicSelect(IQueryable<BASE.Models.mdm_ward> query, mdm_wardFilter filter)
        {
            List<mdm_ward> mdm_wards = await query.Select(q => new mdm_ward()
            {
                Id = filter.Selects.Contains(mdm_wardSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(mdm_wardSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(mdm_wardSelect.Name) ? q.Name : default(string),
                Priority = filter.Selects.Contains(mdm_wardSelect.Priority) ? q.Priority : default(long?),
                DistrictId = filter.Selects.Contains(mdm_wardSelect.District) ? q.DistrictId : default(long),
                StatusId = filter.Selects.Contains(mdm_wardSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(mdm_wardSelect.Used) ? q.Used : default(bool),
                District = filter.Selects.Contains(mdm_wardSelect.District) && q.District != null ? new mdm_district
                {
                    Id = q.District.Id,
                    Code = q.District.Code,
                    Name = q.District.Name,
                    Priority = q.District.Priority,
                    ProvinceId = q.District.ProvinceId,
                    StatusId = q.District.StatusId,
                } : null,
                Status = filter.Selects.Contains(mdm_wardSelect.Status) && q.Status != null ? new enum_status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return mdm_wards;
        }

        public async Task<int> Count(mdm_wardFilter filter)
        {
            IQueryable<BASE.Models.mdm_ward> mdm_wards = DataContext.mdm_ward.AsNoTracking();
            mdm_wards = DynamicFilter(mdm_wards, filter);
            return await mdm_wards.CountAsync();
        }

        public async Task<List<mdm_ward>> List(mdm_wardFilter filter)
        {
            if (filter == null) return new List<mdm_ward>();
            IQueryable<BASE.Models.mdm_ward> mdm_wards = DataContext.mdm_ward.AsNoTracking();
            mdm_wards = DynamicFilter(mdm_wards, filter);
            mdm_wards = DynamicOrder(mdm_wards, filter);
            List<mdm_ward> mdm_wards = await DynamicSelect(mdm_wards, filter);
            return mdm_wards;
        }

        public async Task<mdm_ward> Get(long Id)
        {
            mdm_ward mdm_ward = await DataContext.mdm_ward.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new mdm_ward()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Priority = x.Priority,
                DistrictId = x.DistrictId,
                StatusId = x.StatusId,
                Used = x.Used,
                District = x.District == null ? null : new mdm_district
                {
                    Id = x.District.Id,
                    Code = x.District.Code,
                    Name = x.District.Name,
                    Priority = x.District.Priority,
                    ProvinceId = x.District.ProvinceId,
                    StatusId = x.District.StatusId,
                },
                Status = x.Status == null ? null : new enum_status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (mdm_ward == null)
                return null;

            return mdm_ward;
        }
        public async Task<bool> Create(mdm_ward mdm_ward)
        {
            mdm_ward mdm_ward = new mdm_ward();
            mdm_ward.Id = mdm_ward.Id;
            mdm_ward.Code = mdm_ward.Code;
            mdm_ward.Name = mdm_ward.Name;
            mdm_ward.Priority = mdm_ward.Priority;
            mdm_ward.DistrictId = mdm_ward.DistrictId;
            mdm_ward.StatusId = mdm_ward.StatusId;
            mdm_ward.Used = mdm_ward.Used;
            mdm_ward.CreatedAt = StaticParams.DateTimeNow;
            mdm_ward.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.mdm_ward.Add(mdm_ward);
            await DataContext.SaveChangesAsync();
            mdm_ward.Id = mdm_ward.Id;
            await SaveReference(mdm_ward);
            return true;
        }

        public async Task<bool> Update(mdm_ward mdm_ward)
        {
            mdm_ward mdm_ward = DataContext.mdm_ward.Where(x => x.Id == mdm_ward.Id).FirstOrDefault();
            if (mdm_ward == null)
                return false;
            mdm_ward.Id = mdm_ward.Id;
            mdm_ward.Code = mdm_ward.Code;
            mdm_ward.Name = mdm_ward.Name;
            mdm_ward.Priority = mdm_ward.Priority;
            mdm_ward.DistrictId = mdm_ward.DistrictId;
            mdm_ward.StatusId = mdm_ward.StatusId;
            mdm_ward.Used = mdm_ward.Used;
            mdm_ward.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(mdm_ward);
            return true;
        }

        public async Task<bool> Delete(mdm_ward mdm_ward)
        {
            await DataContext.mdm_ward.Where(x => x.Id == mdm_ward.Id).UpdateFromQueryAsync(x => new mdm_ward { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<mdm_ward> mdm_wards)
        {
            List<mdm_ward> mdm_wards = new List<mdm_ward>();
            foreach (mdm_ward mdm_ward in mdm_wards)
            {
                mdm_ward mdm_ward = new mdm_ward();
                mdm_ward.Id = mdm_ward.Id;
                mdm_ward.Code = mdm_ward.Code;
                mdm_ward.Name = mdm_ward.Name;
                mdm_ward.Priority = mdm_ward.Priority;
                mdm_ward.DistrictId = mdm_ward.DistrictId;
                mdm_ward.StatusId = mdm_ward.StatusId;
                mdm_ward.Used = mdm_ward.Used;
                mdm_ward.CreatedAt = StaticParams.DateTimeNow;
                mdm_ward.UpdatedAt = StaticParams.DateTimeNow;
                mdm_wards.Add(mdm_ward);
            }
            await DataContext.BulkMergeAsync(mdm_wards);
            return true;
        }

        public async Task<bool> BulkDelete(List<mdm_ward> mdm_wards)
        {
            List<long> Ids = mdm_wards.Select(x => x.Id).ToList();
            await DataContext.mdm_ward
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new mdm_ward { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(mdm_ward mdm_ward)
        {
        }

    }
}
