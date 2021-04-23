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
    public interface Ienum_notificationstatusRepository
    {
        Task<int>
    Count(enum_notificationstatusFilter enum_notificationstatusFilter);
    Task<List<BASE.Entities.enum_notificationstatus>> List(enum_notificationstatusFilter enum_notificationstatusFilter);
        Task<BASE.Entities.enum_notificationstatus> Get(long Id);
        Task<bool> Create(BASE.Entities.enum_notificationstatus enum_notificationstatus);
        Task<bool> Update(BASE.Entities.enum_notificationstatus enum_notificationstatus);
        Task<bool> Delete(BASE.Entities.enum_notificationstatus enum_notificationstatus);
        Task<bool> BulkMerge(List<BASE.Entities.enum_notificationstatus> enum_notificationstatuses);
        Task<bool> BulkDelete(List<BASE.Entities.enum_notificationstatus> enum_notificationstatuses);
                    }
                    public class enum_notificationstatusRepository : Ienum_notificationstatusRepository
                    {
                    private DataContext DataContext;
                    public enum_notificationstatusRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.enum_notificationstatus>
                        DynamicFilter(IQueryable<BASE.Models.enum_notificationstatus>
                            query, enum_notificationstatusFilter filter)
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

                            private IQueryable<BASE.Models.enum_notificationstatus>
                                OrFilter(IQueryable<BASE.Models.enum_notificationstatus>
                                    query, enum_notificationstatusFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.enum_notificationstatus>
                                        initQuery = query.Where(q => false);
                                        foreach (enum_notificationstatusFilter enum_notificationstatusFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.enum_notificationstatus>
                                            queryable = query;
                                            if (enum_notificationstatusFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, enum_notificationstatusFilter.Id);
                                            if (enum_notificationstatusFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, enum_notificationstatusFilter.Code);
                                            if (enum_notificationstatusFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, enum_notificationstatusFilter.Name);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.enum_notificationstatus>
                                                DynamicOrder(IQueryable<BASE.Models.enum_notificationstatus>
                                                    query, enum_notificationstatusFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_notificationstatusOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case enum_notificationstatusOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case enum_notificationstatusOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_notificationstatusOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case enum_notificationstatusOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case enum_notificationstatusOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.enum_notificationstatus>> DynamicSelect(IQueryable<BASE.Models.enum_notificationstatus> query, enum_notificationstatusFilter filter)
        {
            List<enum_notificationstatus> enum_notificationstatuses = await query.Select(q => new enum_notificationstatus()
            {
                Id = filter.Selects.Contains(enum_notificationstatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(enum_notificationstatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(enum_notificationstatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return enum_notificationstatuses;
        }

        public async Task<int> Count(enum_notificationstatusFilter filter)
        {
            IQueryable<BASE.Models.enum_notificationstatus> enum_notificationstatuses = DataContext.enum_notificationstatus.AsNoTracking();
            enum_notificationstatuses = DynamicFilter(enum_notificationstatuses, filter);
            return await enum_notificationstatuses.CountAsync();
        }

        public async Task<List<enum_notificationstatus>> List(enum_notificationstatusFilter filter)
        {
            if (filter == null) return new List<enum_notificationstatus>();
            IQueryable<BASE.Models.enum_notificationstatus> enum_notificationstatuss = DataContext.enum_notificationstatus.AsNoTracking();
            enum_notificationstatuss = DynamicFilter(enum_notificationstatuss, filter);
            enum_notificationstatuss = DynamicOrder(enum_notificationstatuss, filter);
            List<enum_notificationstatus> enum_notificationstatuses = await DynamicSelect(enum_notificationstatuss, filter);
            return enum_notificationstatuses;
        }

        public async Task<enum_notificationstatus> Get(long Id)
        {
            enum_notificationstatus enum_notificationstatus = await DataContext.enum_notificationstatus.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new enum_notificationstatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (enum_notificationstatus == null)
                return null;

            return enum_notificationstatus;
        }
        public async Task<bool> Create(enum_notificationstatus enum_notificationstatus)
        {
            enum_notificationstatus enum_notificationstatus = new enum_notificationstatus();
            enum_notificationstatus.Id = enum_notificationstatus.Id;
            enum_notificationstatus.Code = enum_notificationstatus.Code;
            enum_notificationstatus.Name = enum_notificationstatus.Name;
            DataContext.enum_notificationstatus.Add(enum_notificationstatus);
            await DataContext.SaveChangesAsync();
            enum_notificationstatus.Id = enum_notificationstatus.Id;
            await SaveReference(enum_notificationstatus);
            return true;
        }

        public async Task<bool> Update(enum_notificationstatus enum_notificationstatus)
        {
            enum_notificationstatus enum_notificationstatus = DataContext.enum_notificationstatus.Where(x => x.Id == enum_notificationstatus.Id).FirstOrDefault();
            if (enum_notificationstatus == null)
                return false;
            enum_notificationstatus.Id = enum_notificationstatus.Id;
            enum_notificationstatus.Code = enum_notificationstatus.Code;
            enum_notificationstatus.Name = enum_notificationstatus.Name;
            await DataContext.SaveChangesAsync();
            await SaveReference(enum_notificationstatus);
            return true;
        }

        public async Task<bool> Delete(enum_notificationstatus enum_notificationstatus)
        {
            await DataContext.enum_notificationstatus.Where(x => x.Id == enum_notificationstatus.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<enum_notificationstatus> enum_notificationstatuses)
        {
            List<enum_notificationstatus> enum_notificationstatuss = new List<enum_notificationstatus>();
            foreach (enum_notificationstatus enum_notificationstatus in enum_notificationstatuses)
            {
                enum_notificationstatus enum_notificationstatus = new enum_notificationstatus();
                enum_notificationstatus.Id = enum_notificationstatus.Id;
                enum_notificationstatus.Code = enum_notificationstatus.Code;
                enum_notificationstatus.Name = enum_notificationstatus.Name;
                enum_notificationstatuss.Add(enum_notificationstatus);
            }
            await DataContext.BulkMergeAsync(enum_notificationstatuss);
            return true;
        }

        public async Task<bool> BulkDelete(List<enum_notificationstatus> enum_notificationstatuses)
        {
            List<long> Ids = enum_notificationstatuses.Select(x => x.Id).ToList();
            await DataContext.enum_notificationstatus
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(enum_notificationstatus enum_notificationstatus)
        {
        }

    }
}
