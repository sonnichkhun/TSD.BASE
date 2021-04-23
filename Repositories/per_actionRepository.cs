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
    public interface Iper_actionRepository
    {
        Task<int>
    Count(per_actionFilter per_actionFilter);
    Task<List<BASE.Entities.per_action>> List(per_actionFilter per_actionFilter);
        Task<BASE.Entities.per_action> Get(long Id);
        Task<bool> Create(BASE.Entities.per_action per_action);
        Task<bool> Update(BASE.Entities.per_action per_action);
        Task<bool> Delete(BASE.Entities.per_action per_action);
        Task<bool> BulkMerge(List<BASE.Entities.per_action> per_actions);
        Task<bool> BulkDelete(List<BASE.Entities.per_action> per_actions);
                    }
                    public class per_actionRepository : Iper_actionRepository
                    {
                    private DataContext DataContext;
                    public per_actionRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.per_action>
                        DynamicFilter(IQueryable<BASE.Models.per_action>
                            query, per_actionFilter filter)
                            {
                            if (filter == null)
                            return query.Where(q => false);
                            if (filter.Id != null)
                            query = query.Where(q => q.Id, filter.Id);
                            if (filter.Name != null)
                            query = query.Where(q => q.Name, filter.Name);
                            if (filter.MenuId != null)
                            query = query.Where(q => q.MenuId, filter.MenuId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.per_action>
                                OrFilter(IQueryable<BASE.Models.per_action>
                                    query, per_actionFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.per_action>
                                        initQuery = query.Where(q => false);
                                        foreach (per_actionFilter per_actionFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.per_action>
                                            queryable = query;
                                            if (per_actionFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, per_actionFilter.Id);
                                            if (per_actionFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, per_actionFilter.Name);
                                            if (per_actionFilter.MenuId != null)
                                            queryable = queryable.Where(q => q.MenuId, per_actionFilter.MenuId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.per_action>
                                                DynamicOrder(IQueryable<BASE.Models.per_action>
                                                    query, per_actionFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_actionOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case per_actionOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case per_actionOrder.Menu:
                                                    query = query.OrderBy(q => q.MenuId);
                                                    break;
                                                    case per_actionOrder.IsDeleted:
                                                    query = query.OrderBy(q => q.IsDeleted);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_actionOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case per_actionOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case per_actionOrder.Menu:
                                                    query = query.OrderByDescending(q => q.MenuId);
                                                    break;
                                                    case per_actionOrder.IsDeleted:
                                                    query = query.OrderByDescending(q => q.IsDeleted);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.per_action>> DynamicSelect(IQueryable<BASE.Models.per_action> query, per_actionFilter filter)
        {
            List<per_action> per_actions = await query.Select(q => new per_action()
            {
                Id = filter.Selects.Contains(per_actionSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(per_actionSelect.Name) ? q.Name : default(string),
                MenuId = filter.Selects.Contains(per_actionSelect.Menu) ? q.MenuId : default(long),
                IsDeleted = filter.Selects.Contains(per_actionSelect.IsDeleted) ? q.IsDeleted : default(bool),
                Menu = filter.Selects.Contains(per_actionSelect.Menu) && q.Menu != null ? new per_menu
                {
                    Id = q.Menu.Id,
                    Code = q.Menu.Code,
                    Name = q.Menu.Name,
                    Path = q.Menu.Path,
                    IsDeleted = q.Menu.IsDeleted,
                } : null,
            }).ToListAsync();
            return per_actions;
        }

        public async Task<int> Count(per_actionFilter filter)
        {
            IQueryable<BASE.Models.per_action> per_actions = DataContext.per_action.AsNoTracking();
            per_actions = DynamicFilter(per_actions, filter);
            return await per_actions.CountAsync();
        }

        public async Task<List<per_action>> List(per_actionFilter filter)
        {
            if (filter == null) return new List<per_action>();
            IQueryable<BASE.Models.per_action> per_actions = DataContext.per_action.AsNoTracking();
            per_actions = DynamicFilter(per_actions, filter);
            per_actions = DynamicOrder(per_actions, filter);
            List<per_action> per_actions = await DynamicSelect(per_actions, filter);
            return per_actions;
        }

        public async Task<per_action> Get(long Id)
        {
            per_action per_action = await DataContext.per_action.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new per_action()
            {
                Id = x.Id,
                Name = x.Name,
                MenuId = x.MenuId,
                IsDeleted = x.IsDeleted,
                Menu = x.Menu == null ? null : new per_menu
                {
                    Id = x.Menu.Id,
                    Code = x.Menu.Code,
                    Name = x.Menu.Name,
                    Path = x.Menu.Path,
                    IsDeleted = x.Menu.IsDeleted,
                },
            }).FirstOrDefaultAsync();

            if (per_action == null)
                return null;

            return per_action;
        }
        public async Task<bool> Create(per_action per_action)
        {
            per_action per_action = new per_action();
            per_action.Id = per_action.Id;
            per_action.Name = per_action.Name;
            per_action.MenuId = per_action.MenuId;
            per_action.IsDeleted = per_action.IsDeleted;
            DataContext.per_action.Add(per_action);
            await DataContext.SaveChangesAsync();
            per_action.Id = per_action.Id;
            await SaveReference(per_action);
            return true;
        }

        public async Task<bool> Update(per_action per_action)
        {
            per_action per_action = DataContext.per_action.Where(x => x.Id == per_action.Id).FirstOrDefault();
            if (per_action == null)
                return false;
            per_action.Id = per_action.Id;
            per_action.Name = per_action.Name;
            per_action.MenuId = per_action.MenuId;
            per_action.IsDeleted = per_action.IsDeleted;
            await DataContext.SaveChangesAsync();
            await SaveReference(per_action);
            return true;
        }

        public async Task<bool> Delete(per_action per_action)
        {
            await DataContext.per_action.Where(x => x.Id == per_action.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<per_action> per_actions)
        {
            List<per_action> per_actions = new List<per_action>();
            foreach (per_action per_action in per_actions)
            {
                per_action per_action = new per_action();
                per_action.Id = per_action.Id;
                per_action.Name = per_action.Name;
                per_action.MenuId = per_action.MenuId;
                per_action.IsDeleted = per_action.IsDeleted;
                per_actions.Add(per_action);
            }
            await DataContext.BulkMergeAsync(per_actions);
            return true;
        }

        public async Task<bool> BulkDelete(List<per_action> per_actions)
        {
            List<long> Ids = per_actions.Select(x => x.Id).ToList();
            await DataContext.per_action
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(per_action per_action)
        {
        }

    }
}
