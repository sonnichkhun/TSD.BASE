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
    public interface Iper_menuRepository
    {
        Task<int>
    Count(per_menuFilter per_menuFilter);
    Task<List<BASE.Entities.per_menu>> List(per_menuFilter per_menuFilter);
        Task<BASE.Entities.per_menu> Get(long Id);
        Task<bool> Create(BASE.Entities.per_menu per_menu);
        Task<bool> Update(BASE.Entities.per_menu per_menu);
        Task<bool> Delete(BASE.Entities.per_menu per_menu);
        Task<bool> BulkMerge(List<BASE.Entities.per_menu> per_menus);
        Task<bool> BulkDelete(List<BASE.Entities.per_menu> per_menus);
                    }
                    public class per_menuRepository : Iper_menuRepository
                    {
                    private DataContext DataContext;
                    public per_menuRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.per_menu>
                        DynamicFilter(IQueryable<BASE.Models.per_menu>
                            query, per_menuFilter filter)
                            {
                            if (filter == null)
                            return query.Where(q => false);
                            if (filter.Id != null)
                            query = query.Where(q => q.Id, filter.Id);
                            if (filter.Code != null)
                            query = query.Where(q => q.Code, filter.Code);
                            if (filter.Name != null)
                            query = query.Where(q => q.Name, filter.Name);
                            if (filter.Path != null)
                            query = query.Where(q => q.Path, filter.Path);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.per_menu>
                                OrFilter(IQueryable<BASE.Models.per_menu>
                                    query, per_menuFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.per_menu>
                                        initQuery = query.Where(q => false);
                                        foreach (per_menuFilter per_menuFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.per_menu>
                                            queryable = query;
                                            if (per_menuFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, per_menuFilter.Id);
                                            if (per_menuFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, per_menuFilter.Code);
                                            if (per_menuFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, per_menuFilter.Name);
                                            if (per_menuFilter.Path != null)
                                            queryable = queryable.Where(q => q.Path, per_menuFilter.Path);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.per_menu>
                                                DynamicOrder(IQueryable<BASE.Models.per_menu>
                                                    query, per_menuFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_menuOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case per_menuOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case per_menuOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case per_menuOrder.Path:
                                                    query = query.OrderBy(q => q.Path);
                                                    break;
                                                    case per_menuOrder.IsDeleted:
                                                    query = query.OrderBy(q => q.IsDeleted);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_menuOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case per_menuOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case per_menuOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case per_menuOrder.Path:
                                                    query = query.OrderByDescending(q => q.Path);
                                                    break;
                                                    case per_menuOrder.IsDeleted:
                                                    query = query.OrderByDescending(q => q.IsDeleted);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.per_menu>> DynamicSelect(IQueryable<BASE.Models.per_menu> query, per_menuFilter filter)
        {
            List<per_menu> per_menus = await query.Select(q => new per_menu()
            {
                Id = filter.Selects.Contains(per_menuSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(per_menuSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(per_menuSelect.Name) ? q.Name : default(string),
                Path = filter.Selects.Contains(per_menuSelect.Path) ? q.Path : default(string),
                IsDeleted = filter.Selects.Contains(per_menuSelect.IsDeleted) ? q.IsDeleted : default(bool),
            }).ToListAsync();
            return per_menus;
        }

        public async Task<int> Count(per_menuFilter filter)
        {
            IQueryable<BASE.Models.per_menu> per_menus = DataContext.per_menu.AsNoTracking();
            per_menus = DynamicFilter(per_menus, filter);
            return await per_menus.CountAsync();
        }

        public async Task<List<per_menu>> List(per_menuFilter filter)
        {
            if (filter == null) return new List<per_menu>();
            IQueryable<BASE.Models.per_menu> per_menus = DataContext.per_menu.AsNoTracking();
            per_menus = DynamicFilter(per_menus, filter);
            per_menus = DynamicOrder(per_menus, filter);
            List<per_menu> per_menus = await DynamicSelect(per_menus, filter);
            return per_menus;
        }

        public async Task<per_menu> Get(long Id)
        {
            per_menu per_menu = await DataContext.per_menu.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new per_menu()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Path = x.Path,
                IsDeleted = x.IsDeleted,
            }).FirstOrDefaultAsync();

            if (per_menu == null)
                return null;

            return per_menu;
        }
        public async Task<bool> Create(per_menu per_menu)
        {
            per_menu per_menu = new per_menu();
            per_menu.Id = per_menu.Id;
            per_menu.Code = per_menu.Code;
            per_menu.Name = per_menu.Name;
            per_menu.Path = per_menu.Path;
            per_menu.IsDeleted = per_menu.IsDeleted;
            DataContext.per_menu.Add(per_menu);
            await DataContext.SaveChangesAsync();
            per_menu.Id = per_menu.Id;
            await SaveReference(per_menu);
            return true;
        }

        public async Task<bool> Update(per_menu per_menu)
        {
            per_menu per_menu = DataContext.per_menu.Where(x => x.Id == per_menu.Id).FirstOrDefault();
            if (per_menu == null)
                return false;
            per_menu.Id = per_menu.Id;
            per_menu.Code = per_menu.Code;
            per_menu.Name = per_menu.Name;
            per_menu.Path = per_menu.Path;
            per_menu.IsDeleted = per_menu.IsDeleted;
            await DataContext.SaveChangesAsync();
            await SaveReference(per_menu);
            return true;
        }

        public async Task<bool> Delete(per_menu per_menu)
        {
            await DataContext.per_menu.Where(x => x.Id == per_menu.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<per_menu> per_menus)
        {
            List<per_menu> per_menus = new List<per_menu>();
            foreach (per_menu per_menu in per_menus)
            {
                per_menu per_menu = new per_menu();
                per_menu.Id = per_menu.Id;
                per_menu.Code = per_menu.Code;
                per_menu.Name = per_menu.Name;
                per_menu.Path = per_menu.Path;
                per_menu.IsDeleted = per_menu.IsDeleted;
                per_menus.Add(per_menu);
            }
            await DataContext.BulkMergeAsync(per_menus);
            return true;
        }

        public async Task<bool> BulkDelete(List<per_menu> per_menus)
        {
            List<long> Ids = per_menus.Select(x => x.Id).ToList();
            await DataContext.per_menu
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(per_menu per_menu)
        {
        }

    }
}
