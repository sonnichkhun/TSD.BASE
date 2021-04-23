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
    public interface Iper_fieldtypeRepository
    {
        Task<int>
    Count(per_fieldtypeFilter per_fieldtypeFilter);
    Task<List<BASE.Entities.per_fieldtype>> List(per_fieldtypeFilter per_fieldtypeFilter);
        Task<BASE.Entities.per_fieldtype> Get(long Id);
        Task<bool> Create(BASE.Entities.per_fieldtype per_fieldtype);
        Task<bool> Update(BASE.Entities.per_fieldtype per_fieldtype);
        Task<bool> Delete(BASE.Entities.per_fieldtype per_fieldtype);
        Task<bool> BulkMerge(List<BASE.Entities.per_fieldtype> per_fieldtypes);
        Task<bool> BulkDelete(List<BASE.Entities.per_fieldtype> per_fieldtypes);
                    }
                    public class per_fieldtypeRepository : Iper_fieldtypeRepository
                    {
                    private DataContext DataContext;
                    public per_fieldtypeRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.per_fieldtype>
                        DynamicFilter(IQueryable<BASE.Models.per_fieldtype>
                            query, per_fieldtypeFilter filter)
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

                            private IQueryable<BASE.Models.per_fieldtype>
                                OrFilter(IQueryable<BASE.Models.per_fieldtype>
                                    query, per_fieldtypeFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.per_fieldtype>
                                        initQuery = query.Where(q => false);
                                        foreach (per_fieldtypeFilter per_fieldtypeFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.per_fieldtype>
                                            queryable = query;
                                            if (per_fieldtypeFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, per_fieldtypeFilter.Id);
                                            if (per_fieldtypeFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, per_fieldtypeFilter.Code);
                                            if (per_fieldtypeFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, per_fieldtypeFilter.Name);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.per_fieldtype>
                                                DynamicOrder(IQueryable<BASE.Models.per_fieldtype>
                                                    query, per_fieldtypeFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_fieldtypeOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case per_fieldtypeOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case per_fieldtypeOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_fieldtypeOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case per_fieldtypeOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case per_fieldtypeOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.per_fieldtype>> DynamicSelect(IQueryable<BASE.Models.per_fieldtype> query, per_fieldtypeFilter filter)
        {
            List<per_fieldtype> per_fieldtypes = await query.Select(q => new per_fieldtype()
            {
                Id = filter.Selects.Contains(per_fieldtypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(per_fieldtypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(per_fieldtypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return per_fieldtypes;
        }

        public async Task<int> Count(per_fieldtypeFilter filter)
        {
            IQueryable<BASE.Models.per_fieldtype> per_fieldtypes = DataContext.per_fieldtype.AsNoTracking();
            per_fieldtypes = DynamicFilter(per_fieldtypes, filter);
            return await per_fieldtypes.CountAsync();
        }

        public async Task<List<per_fieldtype>> List(per_fieldtypeFilter filter)
        {
            if (filter == null) return new List<per_fieldtype>();
            IQueryable<BASE.Models.per_fieldtype> per_fieldtypes = DataContext.per_fieldtype.AsNoTracking();
            per_fieldtypes = DynamicFilter(per_fieldtypes, filter);
            per_fieldtypes = DynamicOrder(per_fieldtypes, filter);
            List<per_fieldtype> per_fieldtypes = await DynamicSelect(per_fieldtypes, filter);
            return per_fieldtypes;
        }

        public async Task<per_fieldtype> Get(long Id)
        {
            per_fieldtype per_fieldtype = await DataContext.per_fieldtype.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new per_fieldtype()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (per_fieldtype == null)
                return null;

            return per_fieldtype;
        }
        public async Task<bool> Create(per_fieldtype per_fieldtype)
        {
            per_fieldtype per_fieldtype = new per_fieldtype();
            per_fieldtype.Id = per_fieldtype.Id;
            per_fieldtype.Code = per_fieldtype.Code;
            per_fieldtype.Name = per_fieldtype.Name;
            DataContext.per_fieldtype.Add(per_fieldtype);
            await DataContext.SaveChangesAsync();
            per_fieldtype.Id = per_fieldtype.Id;
            await SaveReference(per_fieldtype);
            return true;
        }

        public async Task<bool> Update(per_fieldtype per_fieldtype)
        {
            per_fieldtype per_fieldtype = DataContext.per_fieldtype.Where(x => x.Id == per_fieldtype.Id).FirstOrDefault();
            if (per_fieldtype == null)
                return false;
            per_fieldtype.Id = per_fieldtype.Id;
            per_fieldtype.Code = per_fieldtype.Code;
            per_fieldtype.Name = per_fieldtype.Name;
            await DataContext.SaveChangesAsync();
            await SaveReference(per_fieldtype);
            return true;
        }

        public async Task<bool> Delete(per_fieldtype per_fieldtype)
        {
            await DataContext.per_fieldtype.Where(x => x.Id == per_fieldtype.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<per_fieldtype> per_fieldtypes)
        {
            List<per_fieldtype> per_fieldtypes = new List<per_fieldtype>();
            foreach (per_fieldtype per_fieldtype in per_fieldtypes)
            {
                per_fieldtype per_fieldtype = new per_fieldtype();
                per_fieldtype.Id = per_fieldtype.Id;
                per_fieldtype.Code = per_fieldtype.Code;
                per_fieldtype.Name = per_fieldtype.Name;
                per_fieldtypes.Add(per_fieldtype);
            }
            await DataContext.BulkMergeAsync(per_fieldtypes);
            return true;
        }

        public async Task<bool> BulkDelete(List<per_fieldtype> per_fieldtypes)
        {
            List<long> Ids = per_fieldtypes.Select(x => x.Id).ToList();
            await DataContext.per_fieldtype
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(per_fieldtype per_fieldtype)
        {
        }

    }
}
