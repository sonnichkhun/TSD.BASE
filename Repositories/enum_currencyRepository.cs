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
    public interface Ienum_currencyRepository
    {
        Task<int>
    Count(enum_currencyFilter enum_currencyFilter);
    Task<List<BASE.Entities.enum_currency>> List(enum_currencyFilter enum_currencyFilter);
        Task<BASE.Entities.enum_currency> Get(long Id);
        Task<bool> Create(BASE.Entities.enum_currency enum_currency);
        Task<bool> Update(BASE.Entities.enum_currency enum_currency);
        Task<bool> Delete(BASE.Entities.enum_currency enum_currency);
        Task<bool> BulkMerge(List<BASE.Entities.enum_currency> enum_currencies);
        Task<bool> BulkDelete(List<BASE.Entities.enum_currency> enum_currencies);
                    }
                    public class enum_currencyRepository : Ienum_currencyRepository
                    {
                    private DataContext DataContext;
                    public enum_currencyRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.enum_currency>
                        DynamicFilter(IQueryable<BASE.Models.enum_currency>
                            query, enum_currencyFilter filter)
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

                            private IQueryable<BASE.Models.enum_currency>
                                OrFilter(IQueryable<BASE.Models.enum_currency>
                                    query, enum_currencyFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.enum_currency>
                                        initQuery = query.Where(q => false);
                                        foreach (enum_currencyFilter enum_currencyFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.enum_currency>
                                            queryable = query;
                                            if (enum_currencyFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, enum_currencyFilter.Id);
                                            if (enum_currencyFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, enum_currencyFilter.Code);
                                            if (enum_currencyFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, enum_currencyFilter.Name);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.enum_currency>
                                                DynamicOrder(IQueryable<BASE.Models.enum_currency>
                                                    query, enum_currencyFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_currencyOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case enum_currencyOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case enum_currencyOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_currencyOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case enum_currencyOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case enum_currencyOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.enum_currency>> DynamicSelect(IQueryable<BASE.Models.enum_currency> query, enum_currencyFilter filter)
        {
            List<enum_currency> enum_currencies = await query.Select(q => new enum_currency()
            {
                Id = filter.Selects.Contains(enum_currencySelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(enum_currencySelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(enum_currencySelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return enum_currencies;
        }

        public async Task<int> Count(enum_currencyFilter filter)
        {
            IQueryable<BASE.Models.enum_currency> enum_currencies = DataContext.enum_currency.AsNoTracking();
            enum_currencies = DynamicFilter(enum_currencies, filter);
            return await enum_currencies.CountAsync();
        }

        public async Task<List<enum_currency>> List(enum_currencyFilter filter)
        {
            if (filter == null) return new List<enum_currency>();
            IQueryable<BASE.Models.enum_currency> enum_currencys = DataContext.enum_currency.AsNoTracking();
            enum_currencys = DynamicFilter(enum_currencys, filter);
            enum_currencys = DynamicOrder(enum_currencys, filter);
            List<enum_currency> enum_currencies = await DynamicSelect(enum_currencys, filter);
            return enum_currencies;
        }

        public async Task<enum_currency> Get(long Id)
        {
            enum_currency enum_currency = await DataContext.enum_currency.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new enum_currency()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (enum_currency == null)
                return null;

            return enum_currency;
        }
        public async Task<bool> Create(enum_currency enum_currency)
        {
            enum_currency enum_currency = new enum_currency();
            enum_currency.Id = enum_currency.Id;
            enum_currency.Code = enum_currency.Code;
            enum_currency.Name = enum_currency.Name;
            DataContext.enum_currency.Add(enum_currency);
            await DataContext.SaveChangesAsync();
            enum_currency.Id = enum_currency.Id;
            await SaveReference(enum_currency);
            return true;
        }

        public async Task<bool> Update(enum_currency enum_currency)
        {
            enum_currency enum_currency = DataContext.enum_currency.Where(x => x.Id == enum_currency.Id).FirstOrDefault();
            if (enum_currency == null)
                return false;
            enum_currency.Id = enum_currency.Id;
            enum_currency.Code = enum_currency.Code;
            enum_currency.Name = enum_currency.Name;
            await DataContext.SaveChangesAsync();
            await SaveReference(enum_currency);
            return true;
        }

        public async Task<bool> Delete(enum_currency enum_currency)
        {
            await DataContext.enum_currency.Where(x => x.Id == enum_currency.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<enum_currency> enum_currencies)
        {
            List<enum_currency> enum_currencys = new List<enum_currency>();
            foreach (enum_currency enum_currency in enum_currencies)
            {
                enum_currency enum_currency = new enum_currency();
                enum_currency.Id = enum_currency.Id;
                enum_currency.Code = enum_currency.Code;
                enum_currency.Name = enum_currency.Name;
                enum_currencys.Add(enum_currency);
            }
            await DataContext.BulkMergeAsync(enum_currencys);
            return true;
        }

        public async Task<bool> BulkDelete(List<enum_currency> enum_currencies)
        {
            List<long> Ids = enum_currencies.Select(x => x.Id).ToList();
            await DataContext.enum_currency
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(enum_currency enum_currency)
        {
        }

    }
}
