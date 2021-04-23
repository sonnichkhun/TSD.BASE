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
    public interface Ienum_sexRepository
    {
        Task<int>
    Count(enum_sexFilter enum_sexFilter);
    Task<List<BASE.Entities.enum_sex>> List(enum_sexFilter enum_sexFilter);
        Task<BASE.Entities.enum_sex> Get(long Id);
        Task<bool> Create(BASE.Entities.enum_sex enum_sex);
        Task<bool> Update(BASE.Entities.enum_sex enum_sex);
        Task<bool> Delete(BASE.Entities.enum_sex enum_sex);
        Task<bool> BulkMerge(List<BASE.Entities.enum_sex> enum_sexes);
        Task<bool> BulkDelete(List<BASE.Entities.enum_sex> enum_sexes);
                    }
                    public class enum_sexRepository : Ienum_sexRepository
                    {
                    private DataContext DataContext;
                    public enum_sexRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.enum_sex>
                        DynamicFilter(IQueryable<BASE.Models.enum_sex>
                            query, enum_sexFilter filter)
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

                            private IQueryable<BASE.Models.enum_sex>
                                OrFilter(IQueryable<BASE.Models.enum_sex>
                                    query, enum_sexFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.enum_sex>
                                        initQuery = query.Where(q => false);
                                        foreach (enum_sexFilter enum_sexFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.enum_sex>
                                            queryable = query;
                                            if (enum_sexFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, enum_sexFilter.Id);
                                            if (enum_sexFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, enum_sexFilter.Code);
                                            if (enum_sexFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, enum_sexFilter.Name);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.enum_sex>
                                                DynamicOrder(IQueryable<BASE.Models.enum_sex>
                                                    query, enum_sexFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_sexOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case enum_sexOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case enum_sexOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_sexOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case enum_sexOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case enum_sexOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.enum_sex>> DynamicSelect(IQueryable<BASE.Models.enum_sex> query, enum_sexFilter filter)
        {
            List<enum_sex> enum_sexes = await query.Select(q => new enum_sex()
            {
                Id = filter.Selects.Contains(enum_sexSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(enum_sexSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(enum_sexSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return enum_sexes;
        }

        public async Task<int> Count(enum_sexFilter filter)
        {
            IQueryable<BASE.Models.enum_sex> enum_sexes = DataContext.enum_sex.AsNoTracking();
            enum_sexes = DynamicFilter(enum_sexes, filter);
            return await enum_sexes.CountAsync();
        }

        public async Task<List<enum_sex>> List(enum_sexFilter filter)
        {
            if (filter == null) return new List<enum_sex>();
            IQueryable<BASE.Models.enum_sex> enum_sexs = DataContext.enum_sex.AsNoTracking();
            enum_sexs = DynamicFilter(enum_sexs, filter);
            enum_sexs = DynamicOrder(enum_sexs, filter);
            List<enum_sex> enum_sexes = await DynamicSelect(enum_sexs, filter);
            return enum_sexes;
        }

        public async Task<enum_sex> Get(long Id)
        {
            enum_sex enum_sex = await DataContext.enum_sex.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new enum_sex()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (enum_sex == null)
                return null;

            return enum_sex;
        }
        public async Task<bool> Create(enum_sex enum_sex)
        {
            enum_sex enum_sex = new enum_sex();
            enum_sex.Id = enum_sex.Id;
            enum_sex.Code = enum_sex.Code;
            enum_sex.Name = enum_sex.Name;
            DataContext.enum_sex.Add(enum_sex);
            await DataContext.SaveChangesAsync();
            enum_sex.Id = enum_sex.Id;
            await SaveReference(enum_sex);
            return true;
        }

        public async Task<bool> Update(enum_sex enum_sex)
        {
            enum_sex enum_sex = DataContext.enum_sex.Where(x => x.Id == enum_sex.Id).FirstOrDefault();
            if (enum_sex == null)
                return false;
            enum_sex.Id = enum_sex.Id;
            enum_sex.Code = enum_sex.Code;
            enum_sex.Name = enum_sex.Name;
            await DataContext.SaveChangesAsync();
            await SaveReference(enum_sex);
            return true;
        }

        public async Task<bool> Delete(enum_sex enum_sex)
        {
            await DataContext.enum_sex.Where(x => x.Id == enum_sex.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<enum_sex> enum_sexes)
        {
            List<enum_sex> enum_sexs = new List<enum_sex>();
            foreach (enum_sex enum_sex in enum_sexes)
            {
                enum_sex enum_sex = new enum_sex();
                enum_sex.Id = enum_sex.Id;
                enum_sex.Code = enum_sex.Code;
                enum_sex.Name = enum_sex.Name;
                enum_sexs.Add(enum_sex);
            }
            await DataContext.BulkMergeAsync(enum_sexs);
            return true;
        }

        public async Task<bool> BulkDelete(List<enum_sex> enum_sexes)
        {
            List<long> Ids = enum_sexes.Select(x => x.Id).ToList();
            await DataContext.enum_sex
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(enum_sex enum_sex)
        {
        }

    }
}
