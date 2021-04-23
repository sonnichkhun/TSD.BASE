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
    public interface Iper_permissionRepository
    {
        Task<int>
    Count(per_permissionFilter per_permissionFilter);
    Task<List<BASE.Entities.per_permission>> List(per_permissionFilter per_permissionFilter);
        Task<BASE.Entities.per_permission> Get(long Id);
        Task<bool> Create(BASE.Entities.per_permission per_permission);
        Task<bool> Update(BASE.Entities.per_permission per_permission);
        Task<bool> Delete(BASE.Entities.per_permission per_permission);
        Task<bool> BulkMerge(List<BASE.Entities.per_permission> per_permissions);
        Task<bool> BulkDelete(List<BASE.Entities.per_permission> per_permissions);
                    }
                    public class per_permissionRepository : Iper_permissionRepository
                    {
                    private DataContext DataContext;
                    public per_permissionRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.per_permission>
                        DynamicFilter(IQueryable<BASE.Models.per_permission>
                            query, per_permissionFilter filter)
                            {
                            if (filter == null)
                            return query.Where(q => false);
                            if (filter.Id != null)
                            query = query.Where(q => q.Id, filter.Id);
                            if (filter.Code != null)
                            query = query.Where(q => q.Code, filter.Code);
                            if (filter.Name != null)
                            query = query.Where(q => q.Name, filter.Name);
                            if (filter.RoleId != null)
                            query = query.Where(q => q.RoleId, filter.RoleId);
                            if (filter.MenuId != null)
                            query = query.Where(q => q.MenuId, filter.MenuId);
                            if (filter.StatusId != null)
                            query = query.Where(q => q.StatusId, filter.StatusId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.per_permission>
                                OrFilter(IQueryable<BASE.Models.per_permission>
                                    query, per_permissionFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.per_permission>
                                        initQuery = query.Where(q => false);
                                        foreach (per_permissionFilter per_permissionFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.per_permission>
                                            queryable = query;
                                            if (per_permissionFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, per_permissionFilter.Id);
                                            if (per_permissionFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, per_permissionFilter.Code);
                                            if (per_permissionFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, per_permissionFilter.Name);
                                            if (per_permissionFilter.RoleId != null)
                                            queryable = queryable.Where(q => q.RoleId, per_permissionFilter.RoleId);
                                            if (per_permissionFilter.MenuId != null)
                                            queryable = queryable.Where(q => q.MenuId, per_permissionFilter.MenuId);
                                            if (per_permissionFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId, per_permissionFilter.StatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.per_permission>
                                                DynamicOrder(IQueryable<BASE.Models.per_permission>
                                                    query, per_permissionFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_permissionOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case per_permissionOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case per_permissionOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case per_permissionOrder.Role:
                                                    query = query.OrderBy(q => q.RoleId);
                                                    break;
                                                    case per_permissionOrder.Menu:
                                                    query = query.OrderBy(q => q.MenuId);
                                                    break;
                                                    case per_permissionOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_permissionOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case per_permissionOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case per_permissionOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case per_permissionOrder.Role:
                                                    query = query.OrderByDescending(q => q.RoleId);
                                                    break;
                                                    case per_permissionOrder.Menu:
                                                    query = query.OrderByDescending(q => q.MenuId);
                                                    break;
                                                    case per_permissionOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.per_permission>> DynamicSelect(IQueryable<BASE.Models.per_permission> query, per_permissionFilter filter)
        {
            List<per_permission> per_permissions = await query.Select(q => new per_permission()
            {
                Id = filter.Selects.Contains(per_permissionSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(per_permissionSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(per_permissionSelect.Name) ? q.Name : default(string),
                RoleId = filter.Selects.Contains(per_permissionSelect.Role) ? q.RoleId : default(long),
                MenuId = filter.Selects.Contains(per_permissionSelect.Menu) ? q.MenuId : default(long),
                StatusId = filter.Selects.Contains(per_permissionSelect.Status) ? q.StatusId : default(long),
                Menu = filter.Selects.Contains(per_permissionSelect.Menu) && q.Menu != null ? new per_menu
                {
                    Id = q.Menu.Id,
                    Code = q.Menu.Code,
                    Name = q.Menu.Name,
                    Path = q.Menu.Path,
                    IsDeleted = q.Menu.IsDeleted,
                } : null,
                Role = filter.Selects.Contains(per_permissionSelect.Role) && q.Role != null ? new per_role
                {
                    Id = q.Role.Id,
                    Code = q.Role.Code,
                    Name = q.Role.Name,
                    StatusId = q.Role.StatusId,
                    Used = q.Role.Used,
                } : null,
                Status = filter.Selects.Contains(per_permissionSelect.Status) && q.Status != null ? new enum_status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
            }).ToListAsync();
            return per_permissions;
        }

        public async Task<int> Count(per_permissionFilter filter)
        {
            IQueryable<BASE.Models.per_permission> per_permissions = DataContext.per_permission.AsNoTracking();
            per_permissions = DynamicFilter(per_permissions, filter);
            return await per_permissions.CountAsync();
        }

        public async Task<List<per_permission>> List(per_permissionFilter filter)
        {
            if (filter == null) return new List<per_permission>();
            IQueryable<BASE.Models.per_permission> per_permissions = DataContext.per_permission.AsNoTracking();
            per_permissions = DynamicFilter(per_permissions, filter);
            per_permissions = DynamicOrder(per_permissions, filter);
            List<per_permission> per_permissions = await DynamicSelect(per_permissions, filter);
            return per_permissions;
        }

        public async Task<per_permission> Get(long Id)
        {
            per_permission per_permission = await DataContext.per_permission.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new per_permission()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                RoleId = x.RoleId,
                MenuId = x.MenuId,
                StatusId = x.StatusId,
                Menu = x.Menu == null ? null : new per_menu
                {
                    Id = x.Menu.Id,
                    Code = x.Menu.Code,
                    Name = x.Menu.Name,
                    Path = x.Menu.Path,
                    IsDeleted = x.Menu.IsDeleted,
                },
                Role = x.Role == null ? null : new per_role
                {
                    Id = x.Role.Id,
                    Code = x.Role.Code,
                    Name = x.Role.Name,
                    StatusId = x.Role.StatusId,
                    Used = x.Role.Used,
                },
                Status = x.Status == null ? null : new enum_status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (per_permission == null)
                return null;

            return per_permission;
        }
        public async Task<bool> Create(per_permission per_permission)
        {
            per_permission per_permission = new per_permission();
            per_permission.Id = per_permission.Id;
            per_permission.Code = per_permission.Code;
            per_permission.Name = per_permission.Name;
            per_permission.RoleId = per_permission.RoleId;
            per_permission.MenuId = per_permission.MenuId;
            per_permission.StatusId = per_permission.StatusId;
            DataContext.per_permission.Add(per_permission);
            await DataContext.SaveChangesAsync();
            per_permission.Id = per_permission.Id;
            await SaveReference(per_permission);
            return true;
        }

        public async Task<bool> Update(per_permission per_permission)
        {
            per_permission per_permission = DataContext.per_permission.Where(x => x.Id == per_permission.Id).FirstOrDefault();
            if (per_permission == null)
                return false;
            per_permission.Id = per_permission.Id;
            per_permission.Code = per_permission.Code;
            per_permission.Name = per_permission.Name;
            per_permission.RoleId = per_permission.RoleId;
            per_permission.MenuId = per_permission.MenuId;
            per_permission.StatusId = per_permission.StatusId;
            await DataContext.SaveChangesAsync();
            await SaveReference(per_permission);
            return true;
        }

        public async Task<bool> Delete(per_permission per_permission)
        {
            await DataContext.per_permission.Where(x => x.Id == per_permission.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<per_permission> per_permissions)
        {
            List<per_permission> per_permissions = new List<per_permission>();
            foreach (per_permission per_permission in per_permissions)
            {
                per_permission per_permission = new per_permission();
                per_permission.Id = per_permission.Id;
                per_permission.Code = per_permission.Code;
                per_permission.Name = per_permission.Name;
                per_permission.RoleId = per_permission.RoleId;
                per_permission.MenuId = per_permission.MenuId;
                per_permission.StatusId = per_permission.StatusId;
                per_permissions.Add(per_permission);
            }
            await DataContext.BulkMergeAsync(per_permissions);
            return true;
        }

        public async Task<bool> BulkDelete(List<per_permission> per_permissions)
        {
            List<long> Ids = per_permissions.Select(x => x.Id).ToList();
            await DataContext.per_permission
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(per_permission per_permission)
        {
        }

    }
}
