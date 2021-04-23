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
    public interface Iper_permissionoperatorRepository
    {
        Task<int>
    Count(per_permissionoperatorFilter per_permissionoperatorFilter);
    Task<List<BASE.Entities.per_permissionoperator>> List(per_permissionoperatorFilter per_permissionoperatorFilter);
        Task<BASE.Entities.per_permissionoperator> Get(long Id);
        Task<bool> Create(BASE.Entities.per_permissionoperator per_permissionoperator);
        Task<bool> Update(BASE.Entities.per_permissionoperator per_permissionoperator);
        Task<bool> Delete(BASE.Entities.per_permissionoperator per_permissionoperator);
        Task<bool> BulkMerge(List<BASE.Entities.per_permissionoperator> per_permissionoperators);
        Task<bool> BulkDelete(List<BASE.Entities.per_permissionoperator> per_permissionoperators);
                    }
                    public class per_permissionoperatorRepository : Iper_permissionoperatorRepository
                    {
                    private DataContext DataContext;
                    public per_permissionoperatorRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.per_permissionoperator>
                        DynamicFilter(IQueryable<BASE.Models.per_permissionoperator>
                            query, per_permissionoperatorFilter filter)
                            {
                            if (filter == null)
                            return query.Where(q => false);
                            if (filter.Id != null)
                            query = query.Where(q => q.Id, filter.Id);
                            if (filter.Code != null)
                            query = query.Where(q => q.Code, filter.Code);
                            if (filter.Name != null)
                            query = query.Where(q => q.Name, filter.Name);
                            if (filter.FieldTypeId != null)
                            query = query.Where(q => q.FieldTypeId, filter.FieldTypeId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.per_permissionoperator>
                                OrFilter(IQueryable<BASE.Models.per_permissionoperator>
                                    query, per_permissionoperatorFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.per_permissionoperator>
                                        initQuery = query.Where(q => false);
                                        foreach (per_permissionoperatorFilter per_permissionoperatorFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.per_permissionoperator>
                                            queryable = query;
                                            if (per_permissionoperatorFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, per_permissionoperatorFilter.Id);
                                            if (per_permissionoperatorFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, per_permissionoperatorFilter.Code);
                                            if (per_permissionoperatorFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, per_permissionoperatorFilter.Name);
                                            if (per_permissionoperatorFilter.FieldTypeId != null)
                                            queryable = queryable.Where(q => q.FieldTypeId, per_permissionoperatorFilter.FieldTypeId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.per_permissionoperator>
                                                DynamicOrder(IQueryable<BASE.Models.per_permissionoperator>
                                                    query, per_permissionoperatorFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_permissionoperatorOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case per_permissionoperatorOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case per_permissionoperatorOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case per_permissionoperatorOrder.FieldType:
                                                    query = query.OrderBy(q => q.FieldTypeId);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case per_permissionoperatorOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case per_permissionoperatorOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case per_permissionoperatorOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case per_permissionoperatorOrder.FieldType:
                                                    query = query.OrderByDescending(q => q.FieldTypeId);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.per_permissionoperator>> DynamicSelect(IQueryable<BASE.Models.per_permissionoperator> query, per_permissionoperatorFilter filter)
        {
            List<per_permissionoperator> per_permissionoperators = await query.Select(q => new per_permissionoperator()
            {
                Id = filter.Selects.Contains(per_permissionoperatorSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(per_permissionoperatorSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(per_permissionoperatorSelect.Name) ? q.Name : default(string),
                FieldTypeId = filter.Selects.Contains(per_permissionoperatorSelect.FieldType) ? q.FieldTypeId : default(long),
                FieldType = filter.Selects.Contains(per_permissionoperatorSelect.FieldType) && q.FieldType != null ? new per_fieldtype
                {
                    Id = q.FieldType.Id,
                    Code = q.FieldType.Code,
                    Name = q.FieldType.Name,
                } : null,
            }).ToListAsync();
            return per_permissionoperators;
        }

        public async Task<int> Count(per_permissionoperatorFilter filter)
        {
            IQueryable<BASE.Models.per_permissionoperator> per_permissionoperators = DataContext.per_permissionoperator.AsNoTracking();
            per_permissionoperators = DynamicFilter(per_permissionoperators, filter);
            return await per_permissionoperators.CountAsync();
        }

        public async Task<List<per_permissionoperator>> List(per_permissionoperatorFilter filter)
        {
            if (filter == null) return new List<per_permissionoperator>();
            IQueryable<BASE.Models.per_permissionoperator> per_permissionoperators = DataContext.per_permissionoperator.AsNoTracking();
            per_permissionoperators = DynamicFilter(per_permissionoperators, filter);
            per_permissionoperators = DynamicOrder(per_permissionoperators, filter);
            List<per_permissionoperator> per_permissionoperators = await DynamicSelect(per_permissionoperators, filter);
            return per_permissionoperators;
        }

        public async Task<per_permissionoperator> Get(long Id)
        {
            per_permissionoperator per_permissionoperator = await DataContext.per_permissionoperator.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new per_permissionoperator()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                FieldTypeId = x.FieldTypeId,
                FieldType = x.FieldType == null ? null : new per_fieldtype
                {
                    Id = x.FieldType.Id,
                    Code = x.FieldType.Code,
                    Name = x.FieldType.Name,
                },
            }).FirstOrDefaultAsync();

            if (per_permissionoperator == null)
                return null;

            return per_permissionoperator;
        }
        public async Task<bool> Create(per_permissionoperator per_permissionoperator)
        {
            per_permissionoperator per_permissionoperator = new per_permissionoperator();
            per_permissionoperator.Id = per_permissionoperator.Id;
            per_permissionoperator.Code = per_permissionoperator.Code;
            per_permissionoperator.Name = per_permissionoperator.Name;
            per_permissionoperator.FieldTypeId = per_permissionoperator.FieldTypeId;
            DataContext.per_permissionoperator.Add(per_permissionoperator);
            await DataContext.SaveChangesAsync();
            per_permissionoperator.Id = per_permissionoperator.Id;
            await SaveReference(per_permissionoperator);
            return true;
        }

        public async Task<bool> Update(per_permissionoperator per_permissionoperator)
        {
            per_permissionoperator per_permissionoperator = DataContext.per_permissionoperator.Where(x => x.Id == per_permissionoperator.Id).FirstOrDefault();
            if (per_permissionoperator == null)
                return false;
            per_permissionoperator.Id = per_permissionoperator.Id;
            per_permissionoperator.Code = per_permissionoperator.Code;
            per_permissionoperator.Name = per_permissionoperator.Name;
            per_permissionoperator.FieldTypeId = per_permissionoperator.FieldTypeId;
            await DataContext.SaveChangesAsync();
            await SaveReference(per_permissionoperator);
            return true;
        }

        public async Task<bool> Delete(per_permissionoperator per_permissionoperator)
        {
            await DataContext.per_permissionoperator.Where(x => x.Id == per_permissionoperator.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<per_permissionoperator> per_permissionoperators)
        {
            List<per_permissionoperator> per_permissionoperators = new List<per_permissionoperator>();
            foreach (per_permissionoperator per_permissionoperator in per_permissionoperators)
            {
                per_permissionoperator per_permissionoperator = new per_permissionoperator();
                per_permissionoperator.Id = per_permissionoperator.Id;
                per_permissionoperator.Code = per_permissionoperator.Code;
                per_permissionoperator.Name = per_permissionoperator.Name;
                per_permissionoperator.FieldTypeId = per_permissionoperator.FieldTypeId;
                per_permissionoperators.Add(per_permissionoperator);
            }
            await DataContext.BulkMergeAsync(per_permissionoperators);
            return true;
        }

        public async Task<bool> BulkDelete(List<per_permissionoperator> per_permissionoperators)
        {
            List<long> Ids = per_permissionoperators.Select(x => x.Id).ToList();
            await DataContext.per_permissionoperator
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(per_permissionoperator per_permissionoperator)
        {
        }

    }
}
