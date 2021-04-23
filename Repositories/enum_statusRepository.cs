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
    public interface Ienum_statusRepository
    {
        Task<int>
    Count(enum_statusFilter enum_statusFilter);
    Task<List<BASE.Entities.enum_status>> List(enum_statusFilter enum_statusFilter);
        Task<BASE.Entities.enum_status> Get(long Id);
        Task<bool> Create(BASE.Entities.enum_status enum_status);
        Task<bool> Update(BASE.Entities.enum_status enum_status);
        Task<bool> Delete(BASE.Entities.enum_status enum_status);
        Task<bool> BulkMerge(List<BASE.Entities.enum_status> enum_statuses);
        Task<bool> BulkDelete(List<BASE.Entities.enum_status> enum_statuses);
                    }
                    public class enum_statusRepository : Ienum_statusRepository
                    {
                    private DataContext DataContext;
                    public enum_statusRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.enum_status>
                        DynamicFilter(IQueryable<BASE.Models.enum_status>
                            query, enum_statusFilter filter)
                            {
                            if (filter == null)
                            return query.Where(q => false);
                            if (filter.Id != null)
                            query = query.Where(q => q.Id, filter.Id);
                            if (filter.Code != null)
                            query = query.Where(q => q.Code, filter.Code);
                            if (filter.Name != null)
                            query = query.Where(q => q.Name, filter.Name);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.enum_status>
                                OrFilter(IQueryable<BASE.Models.enum_status>
                                    query, enum_statusFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.enum_status>
                                        initQuery = query.Where(q => false);
                                        foreach (enum_statusFilter enum_statusFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.enum_status>
                                            queryable = query;
                                            if (enum_statusFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, enum_statusFilter.Id);
                                            if (enum_statusFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, enum_statusFilter.Code);
                                            if (enum_statusFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, enum_statusFilter.Name);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.enum_status>
                                                DynamicOrder(IQueryable<BASE.Models.enum_status>
                                                    query, enum_statusFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_statusOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case enum_statusOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case enum_statusOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_statusOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case enum_statusOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case enum_statusOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.enum_status>> DynamicSelect(IQueryable<BASE.Models.enum_status> query, enum_statusFilter filter)
        {
            List<enum_status> enum_statuses = await query.Select(q => new enum_status()
            {
                Id = filter.Selects.Contains(enum_statusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(enum_statusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(enum_statusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return enum_statuses;
        }

        public async Task<int> Count(enum_statusFilter filter)
        {
            IQueryable<BASE.Models.enum_status> enum_statuses = DataContext.enum_status.AsNoTracking();
            enum_statuses = DynamicFilter(enum_statuses, filter);
            return await enum_statuses.CountAsync();
        }

        public async Task<List<enum_status>> List(enum_statusFilter filter)
        {
            if (filter == null) return new List<enum_status>();
            IQueryable<BASE.Models.enum_status> enum_statuss = DataContext.enum_status.AsNoTracking();
            enum_statuss = DynamicFilter(enum_statuss, filter);
            enum_statuss = DynamicOrder(enum_statuss, filter);
            List<enum_status> enum_statuses = await DynamicSelect(enum_statuss, filter);
            return enum_statuses;
        }

        public async Task<enum_status> Get(long Id)
        {
            enum_status enum_status = await DataContext.enum_status.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new enum_status()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (enum_status == null)
                return null;

            return enum_status;
        }
        public async Task<bool> Create(enum_status enum_status)
        {
            enum_status enum_status = new enum_status();
            enum_status.Id = enum_status.Id;
            enum_status.Code = enum_status.Code;
            enum_status.Name = enum_status.Name;
            DataContext.enum_status.Add(enum_status);
            await DataContext.SaveChangesAsync();
            enum_status.Id = enum_status.Id;
            await SaveReference(enum_status);
            return true;
        }

        public async Task<bool> Update(enum_status enum_status)
        {
            enum_status enum_status = DataContext.enum_status.Where(x => x.Id == enum_status.Id).FirstOrDefault();
            if (enum_status == null)
                return false;
            enum_status.Id = enum_status.Id;
            enum_status.Code = enum_status.Code;
            enum_status.Name = enum_status.Name;
            await DataContext.SaveChangesAsync();
            await SaveReference(enum_status);
            return true;
        }

        public async Task<bool> Delete(enum_status enum_status)
        {
            await DataContext.enum_status.Where(x => x.Id == enum_status.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<enum_status> enum_statuses)
        {
            List<enum_status> enum_statuss = new List<enum_status>();
            foreach (enum_status enum_status in enum_statuses)
            {
                enum_status enum_status = new enum_status();
                enum_status.Id = enum_status.Id;
                enum_status.Code = enum_status.Code;
                enum_status.Name = enum_status.Name;
                enum_statuss.Add(enum_status);
            }
            await DataContext.BulkMergeAsync(enum_statuss);
            return true;
        }

        public async Task<bool> BulkDelete(List<enum_status> enum_statuses)
        {
            List<long> Ids = enum_statuses.Select(x => x.Id).ToList();
            await DataContext.enum_status
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(enum_status enum_status)
        {
        }

    }
}
