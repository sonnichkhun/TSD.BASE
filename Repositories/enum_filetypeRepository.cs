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
    public interface Ienum_filetypeRepository
    {
        Task<int>
    Count(enum_filetypeFilter enum_filetypeFilter);
    Task<List<BASE.Entities.enum_filetype>> List(enum_filetypeFilter enum_filetypeFilter);
        Task<BASE.Entities.enum_filetype> Get(long Id);
        Task<bool> Create(BASE.Entities.enum_filetype enum_filetype);
        Task<bool> Update(BASE.Entities.enum_filetype enum_filetype);
        Task<bool> Delete(BASE.Entities.enum_filetype enum_filetype);
        Task<bool> BulkMerge(List<BASE.Entities.enum_filetype> enum_filetypes);
        Task<bool> BulkDelete(List<BASE.Entities.enum_filetype> enum_filetypes);
                    }
                    public class enum_filetypeRepository : Ienum_filetypeRepository
                    {
                    private DataContext DataContext;
                    public enum_filetypeRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.enum_filetype>
                        DynamicFilter(IQueryable<BASE.Models.enum_filetype>
                            query, enum_filetypeFilter filter)
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

                            private IQueryable<BASE.Models.enum_filetype>
                                OrFilter(IQueryable<BASE.Models.enum_filetype>
                                    query, enum_filetypeFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.enum_filetype>
                                        initQuery = query.Where(q => false);
                                        foreach (enum_filetypeFilter enum_filetypeFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.enum_filetype>
                                            queryable = query;
                                            if (enum_filetypeFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, enum_filetypeFilter.Id);
                                            if (enum_filetypeFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, enum_filetypeFilter.Code);
                                            if (enum_filetypeFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, enum_filetypeFilter.Name);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.enum_filetype>
                                                DynamicOrder(IQueryable<BASE.Models.enum_filetype>
                                                    query, enum_filetypeFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_filetypeOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case enum_filetypeOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case enum_filetypeOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case enum_filetypeOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case enum_filetypeOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case enum_filetypeOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.enum_filetype>> DynamicSelect(IQueryable<BASE.Models.enum_filetype> query, enum_filetypeFilter filter)
        {
            List<enum_filetype> enum_filetypes = await query.Select(q => new enum_filetype()
            {
                Id = filter.Selects.Contains(enum_filetypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(enum_filetypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(enum_filetypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return enum_filetypes;
        }

        public async Task<int> Count(enum_filetypeFilter filter)
        {
            IQueryable<BASE.Models.enum_filetype> enum_filetypes = DataContext.enum_filetype.AsNoTracking();
            enum_filetypes = DynamicFilter(enum_filetypes, filter);
            return await enum_filetypes.CountAsync();
        }

        public async Task<List<enum_filetype>> List(enum_filetypeFilter filter)
        {
            if (filter == null) return new List<enum_filetype>();
            IQueryable<BASE.Models.enum_filetype> enum_filetypes = DataContext.enum_filetype.AsNoTracking();
            enum_filetypes = DynamicFilter(enum_filetypes, filter);
            enum_filetypes = DynamicOrder(enum_filetypes, filter);
            List<enum_filetype> enum_filetypes = await DynamicSelect(enum_filetypes, filter);
            return enum_filetypes;
        }

        public async Task<enum_filetype> Get(long Id)
        {
            enum_filetype enum_filetype = await DataContext.enum_filetype.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new enum_filetype()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (enum_filetype == null)
                return null;

            return enum_filetype;
        }
        public async Task<bool> Create(enum_filetype enum_filetype)
        {
            enum_filetype enum_filetype = new enum_filetype();
            enum_filetype.Id = enum_filetype.Id;
            enum_filetype.Code = enum_filetype.Code;
            enum_filetype.Name = enum_filetype.Name;
            DataContext.enum_filetype.Add(enum_filetype);
            await DataContext.SaveChangesAsync();
            enum_filetype.Id = enum_filetype.Id;
            await SaveReference(enum_filetype);
            return true;
        }

        public async Task<bool> Update(enum_filetype enum_filetype)
        {
            enum_filetype enum_filetype = DataContext.enum_filetype.Where(x => x.Id == enum_filetype.Id).FirstOrDefault();
            if (enum_filetype == null)
                return false;
            enum_filetype.Id = enum_filetype.Id;
            enum_filetype.Code = enum_filetype.Code;
            enum_filetype.Name = enum_filetype.Name;
            await DataContext.SaveChangesAsync();
            await SaveReference(enum_filetype);
            return true;
        }

        public async Task<bool> Delete(enum_filetype enum_filetype)
        {
            await DataContext.enum_filetype.Where(x => x.Id == enum_filetype.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<enum_filetype> enum_filetypes)
        {
            List<enum_filetype> enum_filetypes = new List<enum_filetype>();
            foreach (enum_filetype enum_filetype in enum_filetypes)
            {
                enum_filetype enum_filetype = new enum_filetype();
                enum_filetype.Id = enum_filetype.Id;
                enum_filetype.Code = enum_filetype.Code;
                enum_filetype.Name = enum_filetype.Name;
                enum_filetypes.Add(enum_filetype);
            }
            await DataContext.BulkMergeAsync(enum_filetypes);
            return true;
        }

        public async Task<bool> BulkDelete(List<enum_filetype> enum_filetypes)
        {
            List<long> Ids = enum_filetypes.Select(x => x.Id).ToList();
            await DataContext.enum_filetype
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(enum_filetype enum_filetype)
        {
        }

    }
}
