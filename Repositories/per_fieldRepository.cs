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
    public interface Iper_fieldRepository
    {
        Task<int>
    Count(per_fieldFilter per_fieldFilter);
    Task<List<BASE.Entities.per_field>> List(per_fieldFilter per_fieldFilter);
        Task<BASE.Entities.per_field> Get(long Id);
        Task<bool> Create(BASE.Entities.per_field per_field);
        Task<bool> Update(BASE.Entities.per_field per_field);
        Task<bool> Delete(BASE.Entities.per_field per_field);
        Task<bool> BulkMerge(List<BASE.Entities.per_field> per_fields);
        Task<bool> BulkDelete(List<BASE.Entities.per_field> per_fields);
                    }
                    public class per_fieldRepository : Iper_fieldRepository
                    {
                    private DataContext DataContext;
                    public per_fieldRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.per_field>
                        DynamicFilter(IQueryable<BASE.Models.per_field>
                            query, per_fieldFilter filter)
                            {
                            if (filter == null)
                            return query.Where(q => false);
                            if (filter.Id != null)
                            query = query.Where(q => q.Id, filter.Id);
                            if (filter.Name != null)
                            query = query.Where(q => q.Name, filter.Name);
                            if (filter.FieldTypeId != null)
                            query = query.Where(q => q.FieldTypeId, filter.FieldTypeId);
                            if (filter.MenuId != null)
                            query = query.Where(q => q.MenuId, filter.MenuId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.per_field>
                                OrFilter(IQueryable<BASE.Models.per_field>
                                    query, per_fieldFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.per_field>
                                        initQuery = query.Where(q => false);
                                        foreach (per_fieldFilter per_fieldFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.per_field>
                                            queryable = query;
                                            if (per_fieldFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, per_fieldFilter.Id);
                                            if (per_fieldFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, per_fieldFilter.Name);
                                            if (per_fieldFilter.FieldTypeId != null)
                                            queryable = queryable.Where(q => q.FieldTypeId, per_fieldFilter.FieldTypeId);
                                            if (per_fieldFilter.MenuId != null)
                                            queryable = queryable.Where(q => q.MenuId, per_fieldFilter.MenuId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.per_field>
                                                DynamicOrder(IQueryable<BASE.Models.per_field>
                                                    query, per_fieldFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_fieldOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case per_fieldOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case per_fieldOrder.FieldType:
                                                    query = query.OrderBy(q => q.FieldTypeId);
                                                    break;
                                                    case per_fieldOrder.Menu:
                                                    query = query.OrderBy(q => q.MenuId);
                                                    break;
                                                    case per_fieldOrder.IsDeleted:
                                                    query = query.OrderBy(q => q.IsDeleted);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_fieldOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case per_fieldOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case per_fieldOrder.FieldType:
                                                    query = query.OrderByDescending(q => q.FieldTypeId);
                                                    break;
                                                    case per_fieldOrder.Menu:
                                                    query = query.OrderByDescending(q => q.MenuId);
                                                    break;
                                                    case per_fieldOrder.IsDeleted:
                                                    query = query.OrderByDescending(q => q.IsDeleted);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.per_field>> DynamicSelect(IQueryable<BASE.Models.per_field> query, per_fieldFilter filter)
        {
            List<per_field> per_fields = await query.Select(q => new per_field()
            {
                Id = filter.Selects.Contains(per_fieldSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(per_fieldSelect.Name) ? q.Name : default(string),
                FieldTypeId = filter.Selects.Contains(per_fieldSelect.FieldType) ? q.FieldTypeId : default(long),
                MenuId = filter.Selects.Contains(per_fieldSelect.Menu) ? q.MenuId : default(long),
                IsDeleted = filter.Selects.Contains(per_fieldSelect.IsDeleted) ? q.IsDeleted : default(bool),
                FieldType = filter.Selects.Contains(per_fieldSelect.FieldType) && q.FieldType != null ? new per_fieldtype
                {
                    Id = q.FieldType.Id,
                    Code = q.FieldType.Code,
                    Name = q.FieldType.Name,
                } : null,
                Menu = filter.Selects.Contains(per_fieldSelect.Menu) && q.Menu != null ? new per_menu
                {
                    Id = q.Menu.Id,
                    Code = q.Menu.Code,
                    Name = q.Menu.Name,
                    Path = q.Menu.Path,
                    IsDeleted = q.Menu.IsDeleted,
                } : null,
            }).ToListAsync();
            return per_fields;
        }

        public async Task<int> Count(per_fieldFilter filter)
        {
            IQueryable<BASE.Models.per_field> per_fields = DataContext.per_field.AsNoTracking();
            per_fields = DynamicFilter(per_fields, filter);
            return await per_fields.CountAsync();
        }

        public async Task<List<per_field>> List(per_fieldFilter filter)
        {
            if (filter == null) return new List<per_field>();
            IQueryable<BASE.Models.per_field> per_fields = DataContext.per_field.AsNoTracking();
            per_fields = DynamicFilter(per_fields, filter);
            per_fields = DynamicOrder(per_fields, filter);
            List<per_field> per_fields = await DynamicSelect(per_fields, filter);
            return per_fields;
        }

        public async Task<per_field> Get(long Id)
        {
            per_field per_field = await DataContext.per_field.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new per_field()
            {
                Id = x.Id,
                Name = x.Name,
                FieldTypeId = x.FieldTypeId,
                MenuId = x.MenuId,
                IsDeleted = x.IsDeleted,
                FieldType = x.FieldType == null ? null : new per_fieldtype
                {
                    Id = x.FieldType.Id,
                    Code = x.FieldType.Code,
                    Name = x.FieldType.Name,
                },
                Menu = x.Menu == null ? null : new per_menu
                {
                    Id = x.Menu.Id,
                    Code = x.Menu.Code,
                    Name = x.Menu.Name,
                    Path = x.Menu.Path,
                    IsDeleted = x.Menu.IsDeleted,
                },
            }).FirstOrDefaultAsync();

            if (per_field == null)
                return null;

            return per_field;
        }
        public async Task<bool> Create(per_field per_field)
        {
            per_field per_field = new per_field();
            per_field.Id = per_field.Id;
            per_field.Name = per_field.Name;
            per_field.FieldTypeId = per_field.FieldTypeId;
            per_field.MenuId = per_field.MenuId;
            per_field.IsDeleted = per_field.IsDeleted;
            DataContext.per_field.Add(per_field);
            await DataContext.SaveChangesAsync();
            per_field.Id = per_field.Id;
            await SaveReference(per_field);
            return true;
        }

        public async Task<bool> Update(per_field per_field)
        {
            per_field per_field = DataContext.per_field.Where(x => x.Id == per_field.Id).FirstOrDefault();
            if (per_field == null)
                return false;
            per_field.Id = per_field.Id;
            per_field.Name = per_field.Name;
            per_field.FieldTypeId = per_field.FieldTypeId;
            per_field.MenuId = per_field.MenuId;
            per_field.IsDeleted = per_field.IsDeleted;
            await DataContext.SaveChangesAsync();
            await SaveReference(per_field);
            return true;
        }

        public async Task<bool> Delete(per_field per_field)
        {
            await DataContext.per_field.Where(x => x.Id == per_field.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<per_field> per_fields)
        {
            List<per_field> per_fields = new List<per_field>();
            foreach (per_field per_field in per_fields)
            {
                per_field per_field = new per_field();
                per_field.Id = per_field.Id;
                per_field.Name = per_field.Name;
                per_field.FieldTypeId = per_field.FieldTypeId;
                per_field.MenuId = per_field.MenuId;
                per_field.IsDeleted = per_field.IsDeleted;
                per_fields.Add(per_field);
            }
            await DataContext.BulkMergeAsync(per_fields);
            return true;
        }

        public async Task<bool> BulkDelete(List<per_field> per_fields)
        {
            List<long> Ids = per_fields.Select(x => x.Id).ToList();
            await DataContext.per_field
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(per_field per_field)
        {
        }

    }
}
