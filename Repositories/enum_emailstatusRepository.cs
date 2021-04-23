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
    public interface Ienum_emailstatusRepository
    {
        Task<int>
    Count(enum_emailstatusFilter enum_emailstatusFilter);
    Task<List<BASE.Entities.enum_emailstatus>> List(enum_emailstatusFilter enum_emailstatusFilter);
        Task<BASE.Entities.enum_emailstatus> Get(long Id);
        Task<bool> Create(BASE.Entities.enum_emailstatus enum_emailstatus);
        Task<bool> Update(BASE.Entities.enum_emailstatus enum_emailstatus);
        Task<bool> Delete(BASE.Entities.enum_emailstatus enum_emailstatus);
        Task<bool> BulkMerge(List<BASE.Entities.enum_emailstatus> enum_emailstatuses);
        Task<bool> BulkDelete(List<BASE.Entities.enum_emailstatus> enum_emailstatuses);
                    }
                    public class enum_emailstatusRepository : Ienum_emailstatusRepository
                    {
                    private DataContext DataContext;
                    public enum_emailstatusRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.enum_emailstatus>
                        DynamicFilter(IQueryable<BASE.Models.enum_emailstatus>
                            query, enum_emailstatusFilter filter)
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

                            private IQueryable<BASE.Models.enum_emailstatus>
                                OrFilter(IQueryable<BASE.Models.enum_emailstatus>
                                    query, enum_emailstatusFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.enum_emailstatus>
                                        initQuery = query.Where(q => false);
                                        foreach (enum_emailstatusFilter enum_emailstatusFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.enum_emailstatus>
                                            queryable = query;
                                            if (enum_emailstatusFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, enum_emailstatusFilter.Id);
                                            if (enum_emailstatusFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, enum_emailstatusFilter.Code);
                                            if (enum_emailstatusFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, enum_emailstatusFilter.Name);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.enum_emailstatus>
                                                DynamicOrder(IQueryable<BASE.Models.enum_emailstatus>
                                                    query, enum_emailstatusFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_emailstatusOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case enum_emailstatusOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case enum_emailstatusOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_emailstatusOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case enum_emailstatusOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case enum_emailstatusOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.enum_emailstatus>> DynamicSelect(IQueryable<BASE.Models.enum_emailstatus> query, enum_emailstatusFilter filter)
        {
            List<enum_emailstatus> enum_emailstatuses = await query.Select(q => new enum_emailstatus()
            {
                Id = filter.Selects.Contains(enum_emailstatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(enum_emailstatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(enum_emailstatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return enum_emailstatuses;
        }

        public async Task<int> Count(enum_emailstatusFilter filter)
        {
            IQueryable<BASE.Models.enum_emailstatus> enum_emailstatuses = DataContext.enum_emailstatus.AsNoTracking();
            enum_emailstatuses = DynamicFilter(enum_emailstatuses, filter);
            return await enum_emailstatuses.CountAsync();
        }

        public async Task<List<enum_emailstatus>> List(enum_emailstatusFilter filter)
        {
            if (filter == null) return new List<enum_emailstatus>();
            IQueryable<BASE.Models.enum_emailstatus> enum_emailstatuss = DataContext.enum_emailstatus.AsNoTracking();
            enum_emailstatuss = DynamicFilter(enum_emailstatuss, filter);
            enum_emailstatuss = DynamicOrder(enum_emailstatuss, filter);
            List<enum_emailstatus> enum_emailstatuses = await DynamicSelect(enum_emailstatuss, filter);
            return enum_emailstatuses;
        }

        public async Task<enum_emailstatus> Get(long Id)
        {
            enum_emailstatus enum_emailstatus = await DataContext.enum_emailstatus.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new enum_emailstatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (enum_emailstatus == null)
                return null;

            return enum_emailstatus;
        }
        public async Task<bool> Create(enum_emailstatus enum_emailstatus)
        {
            enum_emailstatus enum_emailstatus = new enum_emailstatus();
            enum_emailstatus.Id = enum_emailstatus.Id;
            enum_emailstatus.Code = enum_emailstatus.Code;
            enum_emailstatus.Name = enum_emailstatus.Name;
            DataContext.enum_emailstatus.Add(enum_emailstatus);
            await DataContext.SaveChangesAsync();
            enum_emailstatus.Id = enum_emailstatus.Id;
            await SaveReference(enum_emailstatus);
            return true;
        }

        public async Task<bool> Update(enum_emailstatus enum_emailstatus)
        {
            enum_emailstatus enum_emailstatus = DataContext.enum_emailstatus.Where(x => x.Id == enum_emailstatus.Id).FirstOrDefault();
            if (enum_emailstatus == null)
                return false;
            enum_emailstatus.Id = enum_emailstatus.Id;
            enum_emailstatus.Code = enum_emailstatus.Code;
            enum_emailstatus.Name = enum_emailstatus.Name;
            await DataContext.SaveChangesAsync();
            await SaveReference(enum_emailstatus);
            return true;
        }

        public async Task<bool> Delete(enum_emailstatus enum_emailstatus)
        {
            await DataContext.enum_emailstatus.Where(x => x.Id == enum_emailstatus.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<enum_emailstatus> enum_emailstatuses)
        {
            List<enum_emailstatus> enum_emailstatuss = new List<enum_emailstatus>();
            foreach (enum_emailstatus enum_emailstatus in enum_emailstatuses)
            {
                enum_emailstatus enum_emailstatus = new enum_emailstatus();
                enum_emailstatus.Id = enum_emailstatus.Id;
                enum_emailstatus.Code = enum_emailstatus.Code;
                enum_emailstatus.Name = enum_emailstatus.Name;
                enum_emailstatuss.Add(enum_emailstatus);
            }
            await DataContext.BulkMergeAsync(enum_emailstatuss);
            return true;
        }

        public async Task<bool> BulkDelete(List<enum_emailstatus> enum_emailstatuses)
        {
            List<long> Ids = enum_emailstatuses.Select(x => x.Id).ToList();
            await DataContext.enum_emailstatus
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(enum_emailstatus enum_emailstatus)
        {
        }

    }
}
