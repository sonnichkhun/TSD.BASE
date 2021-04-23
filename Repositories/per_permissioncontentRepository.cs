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
    public interface Iper_permissioncontentRepository
    {
        Task<int>
    Count(per_permissioncontentFilter per_permissioncontentFilter);
    Task<List<BASE.Entities.per_permissioncontent>> List(per_permissioncontentFilter per_permissioncontentFilter);
        Task<BASE.Entities.per_permissioncontent> Get(long Id);
        Task<bool> Create(BASE.Entities.per_permissioncontent per_permissioncontent);
        Task<bool> Update(BASE.Entities.per_permissioncontent per_permissioncontent);
        Task<bool> Delete(BASE.Entities.per_permissioncontent per_permissioncontent);
        Task<bool> BulkMerge(List<BASE.Entities.per_permissioncontent> per_permissioncontents);
        Task<bool> BulkDelete(List<BASE.Entities.per_permissioncontent> per_permissioncontents);
                    }
                    public class per_permissioncontentRepository : Iper_permissioncontentRepository
                    {
                    private DataContext DataContext;
                    public per_permissioncontentRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.per_permissioncontent>
                        DynamicFilter(IQueryable<BASE.Models.per_permissioncontent>
                            query, per_permissioncontentFilter filter)
                            {
                            if (filter == null)
                            return query.Where(q => false);
                            if (filter.Id != null)
                            query = query.Where(q => q.Id, filter.Id);
                            if (filter.PermissionId != null)
                            query = query.Where(q => q.PermissionId, filter.PermissionId);
                            if (filter.FieldId != null)
                            query = query.Where(q => q.FieldId, filter.FieldId);
                            if (filter.PermissionOperatorId != null)
                            query = query.Where(q => q.PermissionOperatorId, filter.PermissionOperatorId);
                            if (filter.Value != null)
                            query = query.Where(q => q.Value, filter.Value);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.per_permissioncontent>
                                OrFilter(IQueryable<BASE.Models.per_permissioncontent>
                                    query, per_permissioncontentFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.per_permissioncontent>
                                        initQuery = query.Where(q => false);
                                        foreach (per_permissioncontentFilter per_permissioncontentFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.per_permissioncontent>
                                            queryable = query;
                                            if (per_permissioncontentFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, per_permissioncontentFilter.Id);
                                            if (per_permissioncontentFilter.PermissionId != null)
                                            queryable = queryable.Where(q => q.PermissionId, per_permissioncontentFilter.PermissionId);
                                            if (per_permissioncontentFilter.FieldId != null)
                                            queryable = queryable.Where(q => q.FieldId, per_permissioncontentFilter.FieldId);
                                            if (per_permissioncontentFilter.PermissionOperatorId != null)
                                            queryable = queryable.Where(q => q.PermissionOperatorId, per_permissioncontentFilter.PermissionOperatorId);
                                            if (per_permissioncontentFilter.Value != null)
                                            queryable = queryable.Where(q => q.Value, per_permissioncontentFilter.Value);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.per_permissioncontent>
                                                DynamicOrder(IQueryable<BASE.Models.per_permissioncontent>
                                                    query, per_permissioncontentFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_permissioncontentOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case per_permissioncontentOrder.Permission:
                                                    query = query.OrderBy(q => q.PermissionId);
                                                    break;
                                                    case per_permissioncontentOrder.Field:
                                                    query = query.OrderBy(q => q.FieldId);
                                                    break;
                                                    case per_permissioncontentOrder.PermissionOperator:
                                                    query = query.OrderBy(q => q.PermissionOperatorId);
                                                    break;
                                                    case per_permissioncontentOrder.Value:
                                                    query = query.OrderBy(q => q.Value);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_permissioncontentOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case per_permissioncontentOrder.Permission:
                                                    query = query.OrderByDescending(q => q.PermissionId);
                                                    break;
                                                    case per_permissioncontentOrder.Field:
                                                    query = query.OrderByDescending(q => q.FieldId);
                                                    break;
                                                    case per_permissioncontentOrder.PermissionOperator:
                                                    query = query.OrderByDescending(q => q.PermissionOperatorId);
                                                    break;
                                                    case per_permissioncontentOrder.Value:
                                                    query = query.OrderByDescending(q => q.Value);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.per_permissioncontent>> DynamicSelect(IQueryable<BASE.Models.per_permissioncontent> query, per_permissioncontentFilter filter)
        {
            List<per_permissioncontent> per_permissioncontents = await query.Select(q => new per_permissioncontent()
            {
                Id = filter.Selects.Contains(per_permissioncontentSelect.Id) ? q.Id : default(long),
                PermissionId = filter.Selects.Contains(per_permissioncontentSelect.Permission) ? q.PermissionId : default(long),
                FieldId = filter.Selects.Contains(per_permissioncontentSelect.Field) ? q.FieldId : default(long),
                PermissionOperatorId = filter.Selects.Contains(per_permissioncontentSelect.PermissionOperator) ? q.PermissionOperatorId : default(long),
                Value = filter.Selects.Contains(per_permissioncontentSelect.Value) ? q.Value : default(string),
                Field = filter.Selects.Contains(per_permissioncontentSelect.Field) && q.Field != null ? new per_field
                {
                    Id = q.Field.Id,
                    Name = q.Field.Name,
                    FieldTypeId = q.Field.FieldTypeId,
                    MenuId = q.Field.MenuId,
                    IsDeleted = q.Field.IsDeleted,
                } : null,
                Permission = filter.Selects.Contains(per_permissioncontentSelect.Permission) && q.Permission != null ? new per_permission
                {
                    Id = q.Permission.Id,
                    Code = q.Permission.Code,
                    Name = q.Permission.Name,
                    RoleId = q.Permission.RoleId,
                    MenuId = q.Permission.MenuId,
                    StatusId = q.Permission.StatusId,
                } : null,
                PermissionOperator = filter.Selects.Contains(per_permissioncontentSelect.PermissionOperator) && q.PermissionOperator != null ? new per_permissionoperator
                {
                    Id = q.PermissionOperator.Id,
                    Code = q.PermissionOperator.Code,
                    Name = q.PermissionOperator.Name,
                    FieldTypeId = q.PermissionOperator.FieldTypeId,
                } : null,
            }).ToListAsync();
            return per_permissioncontents;
        }

        public async Task<int> Count(per_permissioncontentFilter filter)
        {
            IQueryable<BASE.Models.per_permissioncontent> per_permissioncontents = DataContext.per_permissioncontent.AsNoTracking();
            per_permissioncontents = DynamicFilter(per_permissioncontents, filter);
            return await per_permissioncontents.CountAsync();
        }

        public async Task<List<per_permissioncontent>> List(per_permissioncontentFilter filter)
        {
            if (filter == null) return new List<per_permissioncontent>();
            IQueryable<BASE.Models.per_permissioncontent> per_permissioncontents = DataContext.per_permissioncontent.AsNoTracking();
            per_permissioncontents = DynamicFilter(per_permissioncontents, filter);
            per_permissioncontents = DynamicOrder(per_permissioncontents, filter);
            List<per_permissioncontent> per_permissioncontents = await DynamicSelect(per_permissioncontents, filter);
            return per_permissioncontents;
        }

        public async Task<per_permissioncontent> Get(long Id)
        {
            per_permissioncontent per_permissioncontent = await DataContext.per_permissioncontent.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new per_permissioncontent()
            {
                Id = x.Id,
                PermissionId = x.PermissionId,
                FieldId = x.FieldId,
                PermissionOperatorId = x.PermissionOperatorId,
                Value = x.Value,
                Field = x.Field == null ? null : new per_field
                {
                    Id = x.Field.Id,
                    Name = x.Field.Name,
                    FieldTypeId = x.Field.FieldTypeId,
                    MenuId = x.Field.MenuId,
                    IsDeleted = x.Field.IsDeleted,
                },
                Permission = x.Permission == null ? null : new per_permission
                {
                    Id = x.Permission.Id,
                    Code = x.Permission.Code,
                    Name = x.Permission.Name,
                    RoleId = x.Permission.RoleId,
                    MenuId = x.Permission.MenuId,
                    StatusId = x.Permission.StatusId,
                },
                PermissionOperator = x.PermissionOperator == null ? null : new per_permissionoperator
                {
                    Id = x.PermissionOperator.Id,
                    Code = x.PermissionOperator.Code,
                    Name = x.PermissionOperator.Name,
                    FieldTypeId = x.PermissionOperator.FieldTypeId,
                },
            }).FirstOrDefaultAsync();

            if (per_permissioncontent == null)
                return null;

            return per_permissioncontent;
        }
        public async Task<bool> Create(per_permissioncontent per_permissioncontent)
        {
            per_permissioncontent per_permissioncontent = new per_permissioncontent();
            per_permissioncontent.Id = per_permissioncontent.Id;
            per_permissioncontent.PermissionId = per_permissioncontent.PermissionId;
            per_permissioncontent.FieldId = per_permissioncontent.FieldId;
            per_permissioncontent.PermissionOperatorId = per_permissioncontent.PermissionOperatorId;
            per_permissioncontent.Value = per_permissioncontent.Value;
            DataContext.per_permissioncontent.Add(per_permissioncontent);
            await DataContext.SaveChangesAsync();
            per_permissioncontent.Id = per_permissioncontent.Id;
            await SaveReference(per_permissioncontent);
            return true;
        }

        public async Task<bool> Update(per_permissioncontent per_permissioncontent)
        {
            per_permissioncontent per_permissioncontent = DataContext.per_permissioncontent.Where(x => x.Id == per_permissioncontent.Id).FirstOrDefault();
            if (per_permissioncontent == null)
                return false;
            per_permissioncontent.Id = per_permissioncontent.Id;
            per_permissioncontent.PermissionId = per_permissioncontent.PermissionId;
            per_permissioncontent.FieldId = per_permissioncontent.FieldId;
            per_permissioncontent.PermissionOperatorId = per_permissioncontent.PermissionOperatorId;
            per_permissioncontent.Value = per_permissioncontent.Value;
            await DataContext.SaveChangesAsync();
            await SaveReference(per_permissioncontent);
            return true;
        }

        public async Task<bool> Delete(per_permissioncontent per_permissioncontent)
        {
            await DataContext.per_permissioncontent.Where(x => x.Id == per_permissioncontent.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<per_permissioncontent> per_permissioncontents)
        {
            List<per_permissioncontent> per_permissioncontents = new List<per_permissioncontent>();
            foreach (per_permissioncontent per_permissioncontent in per_permissioncontents)
            {
                per_permissioncontent per_permissioncontent = new per_permissioncontent();
                per_permissioncontent.Id = per_permissioncontent.Id;
                per_permissioncontent.PermissionId = per_permissioncontent.PermissionId;
                per_permissioncontent.FieldId = per_permissioncontent.FieldId;
                per_permissioncontent.PermissionOperatorId = per_permissioncontent.PermissionOperatorId;
                per_permissioncontent.Value = per_permissioncontent.Value;
                per_permissioncontents.Add(per_permissioncontent);
            }
            await DataContext.BulkMergeAsync(per_permissioncontents);
            return true;
        }

        public async Task<bool> BulkDelete(List<per_permissioncontent> per_permissioncontents)
        {
            List<long> Ids = per_permissioncontents.Select(x => x.Id).ToList();
            await DataContext.per_permissioncontent
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(per_permissioncontent per_permissioncontent)
        {
        }

    }
}
