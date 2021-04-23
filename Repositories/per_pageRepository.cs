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
    public interface Iper_pageRepository
    {
        Task<int>
    Count(per_pageFilter per_pageFilter);
    Task<List<BASE.Entities.per_page>> List(per_pageFilter per_pageFilter);
        Task<BASE.Entities.per_page> Get(long Id);
        Task<bool> Create(BASE.Entities.per_page per_page);
        Task<bool> Update(BASE.Entities.per_page per_page);
        Task<bool> Delete(BASE.Entities.per_page per_page);
        Task<bool> BulkMerge(List<BASE.Entities.per_page> per_pages);
        Task<bool> BulkDelete(List<BASE.Entities.per_page> per_pages);
                    }
                    public class per_pageRepository : Iper_pageRepository
                    {
                    private DataContext DataContext;
                    public per_pageRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.per_page>
                        DynamicFilter(IQueryable<BASE.Models.per_page>
                            query, per_pageFilter filter)
                            {
                            if (filter == null)
                            return query.Where(q => false);
                            if (filter.Id != null)
                            query = query.Where(q => q.Id, filter.Id);
                            if (filter.Name != null)
                            query = query.Where(q => q.Name, filter.Name);
                            if (filter.Path != null)
                            query = query.Where(q => q.Path, filter.Path);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.per_page>
                                OrFilter(IQueryable<BASE.Models.per_page>
                                    query, per_pageFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.per_page>
                                        initQuery = query.Where(q => false);
                                        foreach (per_pageFilter per_pageFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.per_page>
                                            queryable = query;
                                            if (per_pageFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, per_pageFilter.Id);
                                            if (per_pageFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, per_pageFilter.Name);
                                            if (per_pageFilter.Path != null)
                                            queryable = queryable.Where(q => q.Path, per_pageFilter.Path);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.per_page>
                                                DynamicOrder(IQueryable<BASE.Models.per_page>
                                                    query, per_pageFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_pageOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case per_pageOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case per_pageOrder.Path:
                                                    query = query.OrderBy(q => q.Path);
                                                    break;
                                                    case per_pageOrder.IsDeleted:
                                                    query = query.OrderBy(q => q.IsDeleted);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_pageOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case per_pageOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case per_pageOrder.Path:
                                                    query = query.OrderByDescending(q => q.Path);
                                                    break;
                                                    case per_pageOrder.IsDeleted:
                                                    query = query.OrderByDescending(q => q.IsDeleted);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.per_page>> DynamicSelect(IQueryable<BASE.Models.per_page> query, per_pageFilter filter)
        {
            List<per_page> per_pages = await query.Select(q => new per_page()
            {
                Id = filter.Selects.Contains(per_pageSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(per_pageSelect.Name) ? q.Name : default(string),
                Path = filter.Selects.Contains(per_pageSelect.Path) ? q.Path : default(string),
                IsDeleted = filter.Selects.Contains(per_pageSelect.IsDeleted) ? q.IsDeleted : default(bool),
            }).ToListAsync();
            return per_pages;
        }

        public async Task<int> Count(per_pageFilter filter)
        {
            IQueryable<BASE.Models.per_page> per_pages = DataContext.per_page.AsNoTracking();
            per_pages = DynamicFilter(per_pages, filter);
            return await per_pages.CountAsync();
        }

        public async Task<List<per_page>> List(per_pageFilter filter)
        {
            if (filter == null) return new List<per_page>();
            IQueryable<BASE.Models.per_page> per_pages = DataContext.per_page.AsNoTracking();
            per_pages = DynamicFilter(per_pages, filter);
            per_pages = DynamicOrder(per_pages, filter);
            List<per_page> per_pages = await DynamicSelect(per_pages, filter);
            return per_pages;
        }

        public async Task<per_page> Get(long Id)
        {
            per_page per_page = await DataContext.per_page.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new per_page()
            {
                Id = x.Id,
                Name = x.Name,
                Path = x.Path,
                IsDeleted = x.IsDeleted,
            }).FirstOrDefaultAsync();

            if (per_page == null)
                return null;

            return per_page;
        }
        public async Task<bool> Create(per_page per_page)
        {
            per_page per_page = new per_page();
            per_page.Id = per_page.Id;
            per_page.Name = per_page.Name;
            per_page.Path = per_page.Path;
            per_page.IsDeleted = per_page.IsDeleted;
            DataContext.per_page.Add(per_page);
            await DataContext.SaveChangesAsync();
            per_page.Id = per_page.Id;
            await SaveReference(per_page);
            return true;
        }

        public async Task<bool> Update(per_page per_page)
        {
            per_page per_page = DataContext.per_page.Where(x => x.Id == per_page.Id).FirstOrDefault();
            if (per_page == null)
                return false;
            per_page.Id = per_page.Id;
            per_page.Name = per_page.Name;
            per_page.Path = per_page.Path;
            per_page.IsDeleted = per_page.IsDeleted;
            await DataContext.SaveChangesAsync();
            await SaveReference(per_page);
            return true;
        }

        public async Task<bool> Delete(per_page per_page)
        {
            await DataContext.per_page.Where(x => x.Id == per_page.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<per_page> per_pages)
        {
            List<per_page> per_pages = new List<per_page>();
            foreach (per_page per_page in per_pages)
            {
                per_page per_page = new per_page();
                per_page.Id = per_page.Id;
                per_page.Name = per_page.Name;
                per_page.Path = per_page.Path;
                per_page.IsDeleted = per_page.IsDeleted;
                per_pages.Add(per_page);
            }
            await DataContext.BulkMergeAsync(per_pages);
            return true;
        }

        public async Task<bool> BulkDelete(List<per_page> per_pages)
        {
            List<long> Ids = per_pages.Select(x => x.Id).ToList();
            await DataContext.per_page
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(per_page per_page)
        {
        }

    }
}
