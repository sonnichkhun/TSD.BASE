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
    public interface Ienum_paymentstatusRepository
    {
        Task<int>
    Count(enum_paymentstatusFilter enum_paymentstatusFilter);
    Task<List<BASE.Entities.enum_paymentstatus>> List(enum_paymentstatusFilter enum_paymentstatusFilter);
        Task<BASE.Entities.enum_paymentstatus> Get(long Id);
        Task<bool> Create(BASE.Entities.enum_paymentstatus enum_paymentstatus);
        Task<bool> Update(BASE.Entities.enum_paymentstatus enum_paymentstatus);
        Task<bool> Delete(BASE.Entities.enum_paymentstatus enum_paymentstatus);
        Task<bool> BulkMerge(List<BASE.Entities.enum_paymentstatus> enum_paymentstatuses);
        Task<bool> BulkDelete(List<BASE.Entities.enum_paymentstatus> enum_paymentstatuses);
                    }
                    public class enum_paymentstatusRepository : Ienum_paymentstatusRepository
                    {
                    private DataContext DataContext;
                    public enum_paymentstatusRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.enum_paymentstatus>
                        DynamicFilter(IQueryable<BASE.Models.enum_paymentstatus>
                            query, enum_paymentstatusFilter filter)
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

                            private IQueryable<BASE.Models.enum_paymentstatus>
                                OrFilter(IQueryable<BASE.Models.enum_paymentstatus>
                                    query, enum_paymentstatusFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.enum_paymentstatus>
                                        initQuery = query.Where(q => false);
                                        foreach (enum_paymentstatusFilter enum_paymentstatusFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.enum_paymentstatus>
                                            queryable = query;
                                            if (enum_paymentstatusFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, enum_paymentstatusFilter.Id);
                                            if (enum_paymentstatusFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, enum_paymentstatusFilter.Code);
                                            if (enum_paymentstatusFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, enum_paymentstatusFilter.Name);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.enum_paymentstatus>
                                                DynamicOrder(IQueryable<BASE.Models.enum_paymentstatus>
                                                    query, enum_paymentstatusFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_paymentstatusOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case enum_paymentstatusOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case enum_paymentstatusOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_paymentstatusOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case enum_paymentstatusOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case enum_paymentstatusOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.enum_paymentstatus>> DynamicSelect(IQueryable<BASE.Models.enum_paymentstatus> query, enum_paymentstatusFilter filter)
        {
            List<enum_paymentstatus> enum_paymentstatuses = await query.Select(q => new enum_paymentstatus()
            {
                Id = filter.Selects.Contains(enum_paymentstatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(enum_paymentstatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(enum_paymentstatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return enum_paymentstatuses;
        }

        public async Task<int> Count(enum_paymentstatusFilter filter)
        {
            IQueryable<BASE.Models.enum_paymentstatus> enum_paymentstatuses = DataContext.enum_paymentstatus.AsNoTracking();
            enum_paymentstatuses = DynamicFilter(enum_paymentstatuses, filter);
            return await enum_paymentstatuses.CountAsync();
        }

        public async Task<List<enum_paymentstatus>> List(enum_paymentstatusFilter filter)
        {
            if (filter == null) return new List<enum_paymentstatus>();
            IQueryable<BASE.Models.enum_paymentstatus> enum_paymentstatuss = DataContext.enum_paymentstatus.AsNoTracking();
            enum_paymentstatuss = DynamicFilter(enum_paymentstatuss, filter);
            enum_paymentstatuss = DynamicOrder(enum_paymentstatuss, filter);
            List<enum_paymentstatus> enum_paymentstatuses = await DynamicSelect(enum_paymentstatuss, filter);
            return enum_paymentstatuses;
        }

        public async Task<enum_paymentstatus> Get(long Id)
        {
            enum_paymentstatus enum_paymentstatus = await DataContext.enum_paymentstatus.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new enum_paymentstatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (enum_paymentstatus == null)
                return null;

            return enum_paymentstatus;
        }
        public async Task<bool> Create(enum_paymentstatus enum_paymentstatus)
        {
            enum_paymentstatus enum_paymentstatus = new enum_paymentstatus();
            enum_paymentstatus.Id = enum_paymentstatus.Id;
            enum_paymentstatus.Code = enum_paymentstatus.Code;
            enum_paymentstatus.Name = enum_paymentstatus.Name;
            DataContext.enum_paymentstatus.Add(enum_paymentstatus);
            await DataContext.SaveChangesAsync();
            enum_paymentstatus.Id = enum_paymentstatus.Id;
            await SaveReference(enum_paymentstatus);
            return true;
        }

        public async Task<bool> Update(enum_paymentstatus enum_paymentstatus)
        {
            enum_paymentstatus enum_paymentstatus = DataContext.enum_paymentstatus.Where(x => x.Id == enum_paymentstatus.Id).FirstOrDefault();
            if (enum_paymentstatus == null)
                return false;
            enum_paymentstatus.Id = enum_paymentstatus.Id;
            enum_paymentstatus.Code = enum_paymentstatus.Code;
            enum_paymentstatus.Name = enum_paymentstatus.Name;
            await DataContext.SaveChangesAsync();
            await SaveReference(enum_paymentstatus);
            return true;
        }

        public async Task<bool> Delete(enum_paymentstatus enum_paymentstatus)
        {
            await DataContext.enum_paymentstatus.Where(x => x.Id == enum_paymentstatus.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<enum_paymentstatus> enum_paymentstatuses)
        {
            List<enum_paymentstatus> enum_paymentstatuss = new List<enum_paymentstatus>();
            foreach (enum_paymentstatus enum_paymentstatus in enum_paymentstatuses)
            {
                enum_paymentstatus enum_paymentstatus = new enum_paymentstatus();
                enum_paymentstatus.Id = enum_paymentstatus.Id;
                enum_paymentstatus.Code = enum_paymentstatus.Code;
                enum_paymentstatus.Name = enum_paymentstatus.Name;
                enum_paymentstatuss.Add(enum_paymentstatus);
            }
            await DataContext.BulkMergeAsync(enum_paymentstatuss);
            return true;
        }

        public async Task<bool> BulkDelete(List<enum_paymentstatus> enum_paymentstatuses)
        {
            List<long> Ids = enum_paymentstatuses.Select(x => x.Id).ToList();
            await DataContext.enum_paymentstatus
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(enum_paymentstatus enum_paymentstatus)
        {
        }

    }
}
