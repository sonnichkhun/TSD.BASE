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
    public interface Imdm_nationRepository
    {
        Task<int>
    Count(mdm_nationFilter mdm_nationFilter);
    Task<List<BASE.Entities.mdm_nation>> List(mdm_nationFilter mdm_nationFilter);
        Task<BASE.Entities.mdm_nation> Get(long Id);
        Task<bool> Create(BASE.Entities.mdm_nation mdm_nation);
        Task<bool> Update(BASE.Entities.mdm_nation mdm_nation);
        Task<bool> Delete(BASE.Entities.mdm_nation mdm_nation);
        Task<bool> BulkMerge(List<BASE.Entities.mdm_nation> mdm_nations);
        Task<bool> BulkDelete(List<BASE.Entities.mdm_nation> mdm_nations);
                    }
                    public class mdm_nationRepository : Imdm_nationRepository
                    {
                    private DataContext DataContext;
                    public mdm_nationRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.mdm_nation>
                        DynamicFilter(IQueryable<BASE.Models.mdm_nation>
                            query, mdm_nationFilter filter)
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

                            private IQueryable<BASE.Models.mdm_nation>
                                OrFilter(IQueryable<BASE.Models.mdm_nation>
                                    query, mdm_nationFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.mdm_nation>
                                        initQuery = query.Where(q => false);
                                        foreach (mdm_nationFilter mdm_nationFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.mdm_nation>
                                            queryable = query;
                                            if (mdm_nationFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, mdm_nationFilter.Id);
                                            if (mdm_nationFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, mdm_nationFilter.Code);
                                            if (mdm_nationFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, mdm_nationFilter.Name);
                                            if (mdm_nationFilter.Priority != null)
                                            queryable = queryable.Where(q => q.Priority.HasValue).Where(q => q.Priority, mdm_nationFilter.Priority);
                                            if (mdm_nationFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId, mdm_nationFilter.StatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.mdm_nation>
                                                DynamicOrder(IQueryable<BASE.Models.mdm_nation>
                                                    query, mdm_nationFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_nationOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case mdm_nationOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case mdm_nationOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case mdm_nationOrder.Priority:
                                                    query = query.OrderBy(q => q.Priority);
                                                    break;
                                                    case mdm_nationOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    case mdm_nationOrder.Used:
                                                    query = query.OrderBy(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_nationOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case mdm_nationOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case mdm_nationOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case mdm_nationOrder.Priority:
                                                    query = query.OrderByDescending(q => q.Priority);
                                                    break;
                                                    case mdm_nationOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    case mdm_nationOrder.Used:
                                                    query = query.OrderByDescending(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.mdm_nation>> DynamicSelect(IQueryable<BASE.Models.mdm_nation> query, mdm_nationFilter filter)
        {
            List<mdm_nation> mdm_nations = await query.Select(q => new mdm_nation()
            {
                Id = filter.Selects.Contains(mdm_nationSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(mdm_nationSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(mdm_nationSelect.Name) ? q.Name : default(string),
                Priority = filter.Selects.Contains(mdm_nationSelect.Priority) ? q.Priority : default(long?),
                StatusId = filter.Selects.Contains(mdm_nationSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(mdm_nationSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(mdm_nationSelect.Status) && q.Status != null ? new enum_status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return mdm_nations;
        }

        public async Task<int> Count(mdm_nationFilter filter)
        {
            IQueryable<BASE.Models.mdm_nation> mdm_nations = DataContext.mdm_nation.AsNoTracking();
            mdm_nations = DynamicFilter(mdm_nations, filter);
            return await mdm_nations.CountAsync();
        }

        public async Task<List<mdm_nation>> List(mdm_nationFilter filter)
        {
            if (filter == null) return new List<mdm_nation>();
            IQueryable<BASE.Models.mdm_nation> mdm_nations = DataContext.mdm_nation.AsNoTracking();
            mdm_nations = DynamicFilter(mdm_nations, filter);
            mdm_nations = DynamicOrder(mdm_nations, filter);
            List<mdm_nation> mdm_nations = await DynamicSelect(mdm_nations, filter);
            return mdm_nations;
        }

        public async Task<mdm_nation> Get(long Id)
        {
            mdm_nation mdm_nation = await DataContext.mdm_nation.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new mdm_nation()
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

            if (mdm_nation == null)
                return null;

            return mdm_nation;
        }
        public async Task<bool> Create(mdm_nation mdm_nation)
        {
            mdm_nation mdm_nation = new mdm_nation();
            mdm_nation.Id = mdm_nation.Id;
            mdm_nation.Code = mdm_nation.Code;
            mdm_nation.Name = mdm_nation.Name;
            mdm_nation.Priority = mdm_nation.Priority;
            mdm_nation.StatusId = mdm_nation.StatusId;
            mdm_nation.Used = mdm_nation.Used;
            mdm_nation.CreatedAt = StaticParams.DateTimeNow;
            mdm_nation.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.mdm_nation.Add(mdm_nation);
            await DataContext.SaveChangesAsync();
            mdm_nation.Id = mdm_nation.Id;
            await SaveReference(mdm_nation);
            return true;
        }

        public async Task<bool> Update(mdm_nation mdm_nation)
        {
            mdm_nation mdm_nation = DataContext.mdm_nation.Where(x => x.Id == mdm_nation.Id).FirstOrDefault();
            if (mdm_nation == null)
                return false;
            mdm_nation.Id = mdm_nation.Id;
            mdm_nation.Code = mdm_nation.Code;
            mdm_nation.Name = mdm_nation.Name;
            mdm_nation.Priority = mdm_nation.Priority;
            mdm_nation.StatusId = mdm_nation.StatusId;
            mdm_nation.Used = mdm_nation.Used;
            mdm_nation.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(mdm_nation);
            return true;
        }

        public async Task<bool> Delete(mdm_nation mdm_nation)
        {
            await DataContext.mdm_nation.Where(x => x.Id == mdm_nation.Id).UpdateFromQueryAsync(x => new mdm_nation { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<mdm_nation> mdm_nations)
        {
            List<mdm_nation> mdm_nations = new List<mdm_nation>();
            foreach (mdm_nation mdm_nation in mdm_nations)
            {
                mdm_nation mdm_nation = new mdm_nation();
                mdm_nation.Id = mdm_nation.Id;
                mdm_nation.Code = mdm_nation.Code;
                mdm_nation.Name = mdm_nation.Name;
                mdm_nation.Priority = mdm_nation.Priority;
                mdm_nation.StatusId = mdm_nation.StatusId;
                mdm_nation.Used = mdm_nation.Used;
                mdm_nation.CreatedAt = StaticParams.DateTimeNow;
                mdm_nation.UpdatedAt = StaticParams.DateTimeNow;
                mdm_nations.Add(mdm_nation);
            }
            await DataContext.BulkMergeAsync(mdm_nations);
            return true;
        }

        public async Task<bool> BulkDelete(List<mdm_nation> mdm_nations)
        {
            List<long> Ids = mdm_nations.Select(x => x.Id).ToList();
            await DataContext.mdm_nation
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new mdm_nation { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(mdm_nation mdm_nation)
        {
        }

    }
}
