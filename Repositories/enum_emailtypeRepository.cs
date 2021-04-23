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
    public interface Ienum_emailtypeRepository
    {
        Task<int>
    Count(enum_emailtypeFilter enum_emailtypeFilter);
    Task<List<BASE.Entities.enum_emailtype>> List(enum_emailtypeFilter enum_emailtypeFilter);
        Task<BASE.Entities.enum_emailtype> Get(long Id);
        Task<bool> Create(BASE.Entities.enum_emailtype enum_emailtype);
        Task<bool> Update(BASE.Entities.enum_emailtype enum_emailtype);
        Task<bool> Delete(BASE.Entities.enum_emailtype enum_emailtype);
        Task<bool> BulkMerge(List<BASE.Entities.enum_emailtype> enum_emailtypes);
        Task<bool> BulkDelete(List<BASE.Entities.enum_emailtype> enum_emailtypes);
                    }
                    public class enum_emailtypeRepository : Ienum_emailtypeRepository
                    {
                    private DataContext DataContext;
                    public enum_emailtypeRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.enum_emailtype>
                        DynamicFilter(IQueryable<BASE.Models.enum_emailtype>
                            query, enum_emailtypeFilter filter)
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

                            private IQueryable<BASE.Models.enum_emailtype>
                                OrFilter(IQueryable<BASE.Models.enum_emailtype>
                                    query, enum_emailtypeFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.enum_emailtype>
                                        initQuery = query.Where(q => false);
                                        foreach (enum_emailtypeFilter enum_emailtypeFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.enum_emailtype>
                                            queryable = query;
                                            if (enum_emailtypeFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, enum_emailtypeFilter.Id);
                                            if (enum_emailtypeFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, enum_emailtypeFilter.Code);
                                            if (enum_emailtypeFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, enum_emailtypeFilter.Name);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.enum_emailtype>
                                                DynamicOrder(IQueryable<BASE.Models.enum_emailtype>
                                                    query, enum_emailtypeFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_emailtypeOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case enum_emailtypeOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case enum_emailtypeOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_emailtypeOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case enum_emailtypeOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case enum_emailtypeOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.enum_emailtype>> DynamicSelect(IQueryable<BASE.Models.enum_emailtype> query, enum_emailtypeFilter filter)
        {
            List<enum_emailtype> enum_emailtypes = await query.Select(q => new enum_emailtype()
            {
                Id = filter.Selects.Contains(enum_emailtypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(enum_emailtypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(enum_emailtypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return enum_emailtypes;
        }

        public async Task<int> Count(enum_emailtypeFilter filter)
        {
            IQueryable<BASE.Models.enum_emailtype> enum_emailtypes = DataContext.enum_emailtype.AsNoTracking();
            enum_emailtypes = DynamicFilter(enum_emailtypes, filter);
            return await enum_emailtypes.CountAsync();
        }

        public async Task<List<enum_emailtype>> List(enum_emailtypeFilter filter)
        {
            if (filter == null) return new List<enum_emailtype>();
            IQueryable<BASE.Models.enum_emailtype> enum_emailtypes = DataContext.enum_emailtype.AsNoTracking();
            enum_emailtypes = DynamicFilter(enum_emailtypes, filter);
            enum_emailtypes = DynamicOrder(enum_emailtypes, filter);
            List<enum_emailtype> enum_emailtypes = await DynamicSelect(enum_emailtypes, filter);
            return enum_emailtypes;
        }

        public async Task<enum_emailtype> Get(long Id)
        {
            enum_emailtype enum_emailtype = await DataContext.enum_emailtype.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new enum_emailtype()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (enum_emailtype == null)
                return null;

            return enum_emailtype;
        }
        public async Task<bool> Create(enum_emailtype enum_emailtype)
        {
            enum_emailtype enum_emailtype = new enum_emailtype();
            enum_emailtype.Id = enum_emailtype.Id;
            enum_emailtype.Code = enum_emailtype.Code;
            enum_emailtype.Name = enum_emailtype.Name;
            DataContext.enum_emailtype.Add(enum_emailtype);
            await DataContext.SaveChangesAsync();
            enum_emailtype.Id = enum_emailtype.Id;
            await SaveReference(enum_emailtype);
            return true;
        }

        public async Task<bool> Update(enum_emailtype enum_emailtype)
        {
            enum_emailtype enum_emailtype = DataContext.enum_emailtype.Where(x => x.Id == enum_emailtype.Id).FirstOrDefault();
            if (enum_emailtype == null)
                return false;
            enum_emailtype.Id = enum_emailtype.Id;
            enum_emailtype.Code = enum_emailtype.Code;
            enum_emailtype.Name = enum_emailtype.Name;
            await DataContext.SaveChangesAsync();
            await SaveReference(enum_emailtype);
            return true;
        }

        public async Task<bool> Delete(enum_emailtype enum_emailtype)
        {
            await DataContext.enum_emailtype.Where(x => x.Id == enum_emailtype.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<enum_emailtype> enum_emailtypes)
        {
            List<enum_emailtype> enum_emailtypes = new List<enum_emailtype>();
            foreach (enum_emailtype enum_emailtype in enum_emailtypes)
            {
                enum_emailtype enum_emailtype = new enum_emailtype();
                enum_emailtype.Id = enum_emailtype.Id;
                enum_emailtype.Code = enum_emailtype.Code;
                enum_emailtype.Name = enum_emailtype.Name;
                enum_emailtypes.Add(enum_emailtype);
            }
            await DataContext.BulkMergeAsync(enum_emailtypes);
            return true;
        }

        public async Task<bool> BulkDelete(List<enum_emailtype> enum_emailtypes)
        {
            List<long> Ids = enum_emailtypes.Select(x => x.Id).ToList();
            await DataContext.enum_emailtype
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(enum_emailtype enum_emailtype)
        {
        }

    }
}
