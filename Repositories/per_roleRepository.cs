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
    public interface Iper_roleRepository
    {
        Task<int>
    Count(per_roleFilter per_roleFilter);
    Task<List<BASE.Entities.per_role>> List(per_roleFilter per_roleFilter);
        Task<BASE.Entities.per_role> Get(long Id);
        Task<bool> Create(BASE.Entities.per_role per_role);
        Task<bool> Update(BASE.Entities.per_role per_role);
        Task<bool> Delete(BASE.Entities.per_role per_role);
        Task<bool> BulkMerge(List<BASE.Entities.per_role> per_roles);
        Task<bool> BulkDelete(List<BASE.Entities.per_role> per_roles);
                    }
                    public class per_roleRepository : Iper_roleRepository
                    {
                    private DataContext DataContext;
                    public per_roleRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.per_role>
                        DynamicFilter(IQueryable<BASE.Models.per_role>
                            query, per_roleFilter filter)
                            {
                            if (filter == null)
                            return query.Where(q => false);
                            if (filter.Id != null)
                            query = query.Where(q => q.Id, filter.Id);
                            if (filter.Code != null)
                            query = query.Where(q => q.Code, filter.Code);
                            if (filter.Name != null)
                            query = query.Where(q => q.Name, filter.Name);
                            if (filter.StatusId != null)
                            query = query.Where(q => q.StatusId, filter.StatusId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.per_role>
                                OrFilter(IQueryable<BASE.Models.per_role>
                                    query, per_roleFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.per_role>
                                        initQuery = query.Where(q => false);
                                        foreach (per_roleFilter per_roleFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.per_role>
                                            queryable = query;
                                            if (per_roleFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, per_roleFilter.Id);
                                            if (per_roleFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, per_roleFilter.Code);
                                            if (per_roleFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, per_roleFilter.Name);
                                            if (per_roleFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId, per_roleFilter.StatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.per_role>
                                                DynamicOrder(IQueryable<BASE.Models.per_role>
                                                    query, per_roleFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_roleOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case per_roleOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case per_roleOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case per_roleOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    case per_roleOrder.Used:
                                                    query = query.OrderBy(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_roleOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case per_roleOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case per_roleOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case per_roleOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    case per_roleOrder.Used:
                                                    query = query.OrderByDescending(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.per_role>> DynamicSelect(IQueryable<BASE.Models.per_role> query, per_roleFilter filter)
        {
            List<per_role> per_roles = await query.Select(q => new per_role()
            {
                Id = filter.Selects.Contains(per_roleSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(per_roleSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(per_roleSelect.Name) ? q.Name : default(string),
                StatusId = filter.Selects.Contains(per_roleSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(per_roleSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(per_roleSelect.Status) && q.Status != null ? new enum_status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
            }).ToListAsync();
            return per_roles;
        }

        public async Task<int> Count(per_roleFilter filter)
        {
            IQueryable<BASE.Models.per_role> per_roles = DataContext.per_role.AsNoTracking();
            per_roles = DynamicFilter(per_roles, filter);
            return await per_roles.CountAsync();
        }

        public async Task<List<per_role>> List(per_roleFilter filter)
        {
            if (filter == null) return new List<per_role>();
            IQueryable<BASE.Models.per_role> per_roles = DataContext.per_role.AsNoTracking();
            per_roles = DynamicFilter(per_roles, filter);
            per_roles = DynamicOrder(per_roles, filter);
            List<per_role> per_roles = await DynamicSelect(per_roles, filter);
            return per_roles;
        }

        public async Task<per_role> Get(long Id)
        {
            per_role per_role = await DataContext.per_role.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new per_role()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                StatusId = x.StatusId,
                Used = x.Used,
                Status = x.Status == null ? null : new enum_status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (per_role == null)
                return null;

            return per_role;
        }
        public async Task<bool> Create(per_role per_role)
        {
            per_role per_role = new per_role();
            per_role.Id = per_role.Id;
            per_role.Code = per_role.Code;
            per_role.Name = per_role.Name;
            per_role.StatusId = per_role.StatusId;
            per_role.Used = per_role.Used;
            DataContext.per_role.Add(per_role);
            await DataContext.SaveChangesAsync();
            per_role.Id = per_role.Id;
            await SaveReference(per_role);
            return true;
        }

        public async Task<bool> Update(per_role per_role)
        {
            per_role per_role = DataContext.per_role.Where(x => x.Id == per_role.Id).FirstOrDefault();
            if (per_role == null)
                return false;
            per_role.Id = per_role.Id;
            per_role.Code = per_role.Code;
            per_role.Name = per_role.Name;
            per_role.StatusId = per_role.StatusId;
            per_role.Used = per_role.Used;
            await DataContext.SaveChangesAsync();
            await SaveReference(per_role);
            return true;
        }

        public async Task<bool> Delete(per_role per_role)
        {
            await DataContext.per_role.Where(x => x.Id == per_role.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<per_role> per_roles)
        {
            List<per_role> per_roles = new List<per_role>();
            foreach (per_role per_role in per_roles)
            {
                per_role per_role = new per_role();
                per_role.Id = per_role.Id;
                per_role.Code = per_role.Code;
                per_role.Name = per_role.Name;
                per_role.StatusId = per_role.StatusId;
                per_role.Used = per_role.Used;
                per_roles.Add(per_role);
            }
            await DataContext.BulkMergeAsync(per_roles);
            return true;
        }

        public async Task<bool> BulkDelete(List<per_role> per_roles)
        {
            List<long> Ids = per_roles.Select(x => x.Id).ToList();
            await DataContext.per_role
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(per_role per_role)
        {
        }

    }
}
