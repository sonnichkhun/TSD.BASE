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
    public interface Imdm_taxtypeRepository
    {
        Task<int>
    Count(mdm_taxtypeFilter mdm_taxtypeFilter);
    Task<List<BASE.Entities.mdm_taxtype>> List(mdm_taxtypeFilter mdm_taxtypeFilter);
        Task<BASE.Entities.mdm_taxtype> Get(long Id);
        Task<bool> Create(BASE.Entities.mdm_taxtype mdm_taxtype);
        Task<bool> Update(BASE.Entities.mdm_taxtype mdm_taxtype);
        Task<bool> Delete(BASE.Entities.mdm_taxtype mdm_taxtype);
        Task<bool> BulkMerge(List<BASE.Entities.mdm_taxtype> mdm_taxtypes);
        Task<bool> BulkDelete(List<BASE.Entities.mdm_taxtype> mdm_taxtypes);
                    }
                    public class mdm_taxtypeRepository : Imdm_taxtypeRepository
                    {
                    private DataContext DataContext;
                    public mdm_taxtypeRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.mdm_taxtype>
                        DynamicFilter(IQueryable<BASE.Models.mdm_taxtype>
                            query, mdm_taxtypeFilter filter)
                            {
                            if (filter == null)
                            return query.Where(q => false);
                            query = query.Where(q => !q.DeletedAt.HasValue);
                            if (filter.CreatedAt != null)
                            query = query.Where(q => q.CreatedAt, filter.CreatedAt);
                            if (filter.UpdatedAt != null)
                            query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
                            if (filter.Id != null)
                            query = query.Where(q => q.Id, filter.Id);
                            if (filter.Code != null)
                            query = query.Where(q => q.Code, filter.Code);
                            if (filter.Name != null)
                            query = query.Where(q => q.Name, filter.Name);
                            if (filter.Percentage != null)
                            query = query.Where(q => q.Percentage, filter.Percentage);
                            if (filter.StatusId != null)
                            query = query.Where(q => q.StatusId, filter.StatusId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.mdm_taxtype>
                                OrFilter(IQueryable<BASE.Models.mdm_taxtype>
                                    query, mdm_taxtypeFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.mdm_taxtype>
                                        initQuery = query.Where(q => false);
                                        foreach (mdm_taxtypeFilter mdm_taxtypeFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.mdm_taxtype>
                                            queryable = query;
                                            if (mdm_taxtypeFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, mdm_taxtypeFilter.Id);
                                            if (mdm_taxtypeFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, mdm_taxtypeFilter.Code);
                                            if (mdm_taxtypeFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, mdm_taxtypeFilter.Name);
                                            if (mdm_taxtypeFilter.Percentage != null)
                                            queryable = queryable.Where(q => q.Percentage, mdm_taxtypeFilter.Percentage);
                                            if (mdm_taxtypeFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId, mdm_taxtypeFilter.StatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.mdm_taxtype>
                                                DynamicOrder(IQueryable<BASE.Models.mdm_taxtype>
                                                    query, mdm_taxtypeFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_taxtypeOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case mdm_taxtypeOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case mdm_taxtypeOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case mdm_taxtypeOrder.Percentage:
                                                    query = query.OrderBy(q => q.Percentage);
                                                    break;
                                                    case mdm_taxtypeOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    case mdm_taxtypeOrder.Used:
                                                    query = query.OrderBy(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_taxtypeOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case mdm_taxtypeOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case mdm_taxtypeOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case mdm_taxtypeOrder.Percentage:
                                                    query = query.OrderByDescending(q => q.Percentage);
                                                    break;
                                                    case mdm_taxtypeOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    case mdm_taxtypeOrder.Used:
                                                    query = query.OrderByDescending(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.mdm_taxtype>> DynamicSelect(IQueryable<BASE.Models.mdm_taxtype> query, mdm_taxtypeFilter filter)
        {
            List<mdm_taxtype> mdm_taxtypes = await query.Select(q => new mdm_taxtype()
            {
                Id = filter.Selects.Contains(mdm_taxtypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(mdm_taxtypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(mdm_taxtypeSelect.Name) ? q.Name : default(string),
                Percentage = filter.Selects.Contains(mdm_taxtypeSelect.Percentage) ? q.Percentage : default(decimal),
                StatusId = filter.Selects.Contains(mdm_taxtypeSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(mdm_taxtypeSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(mdm_taxtypeSelect.Status) && q.Status != null ? new enum_status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return mdm_taxtypes;
        }

        public async Task<int> Count(mdm_taxtypeFilter filter)
        {
            IQueryable<BASE.Models.mdm_taxtype> mdm_taxtypes = DataContext.mdm_taxtype.AsNoTracking();
            mdm_taxtypes = DynamicFilter(mdm_taxtypes, filter);
            return await mdm_taxtypes.CountAsync();
        }

        public async Task<List<mdm_taxtype>> List(mdm_taxtypeFilter filter)
        {
            if (filter == null) return new List<mdm_taxtype>();
            IQueryable<BASE.Models.mdm_taxtype> mdm_taxtypes = DataContext.mdm_taxtype.AsNoTracking();
            mdm_taxtypes = DynamicFilter(mdm_taxtypes, filter);
            mdm_taxtypes = DynamicOrder(mdm_taxtypes, filter);
            List<mdm_taxtype> mdm_taxtypes = await DynamicSelect(mdm_taxtypes, filter);
            return mdm_taxtypes;
        }

        public async Task<mdm_taxtype> Get(long Id)
        {
            mdm_taxtype mdm_taxtype = await DataContext.mdm_taxtype.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new mdm_taxtype()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Percentage = x.Percentage,
                StatusId = x.StatusId,
                Used = x.Used,
                Status = x.Status == null ? null : new enum_status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (mdm_taxtype == null)
                return null;

            return mdm_taxtype;
        }
        public async Task<bool> Create(mdm_taxtype mdm_taxtype)
        {
            mdm_taxtype mdm_taxtype = new mdm_taxtype();
            mdm_taxtype.Id = mdm_taxtype.Id;
            mdm_taxtype.Code = mdm_taxtype.Code;
            mdm_taxtype.Name = mdm_taxtype.Name;
            mdm_taxtype.Percentage = mdm_taxtype.Percentage;
            mdm_taxtype.StatusId = mdm_taxtype.StatusId;
            mdm_taxtype.Used = mdm_taxtype.Used;
            mdm_taxtype.CreatedAt = StaticParams.DateTimeNow;
            mdm_taxtype.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.mdm_taxtype.Add(mdm_taxtype);
            await DataContext.SaveChangesAsync();
            mdm_taxtype.Id = mdm_taxtype.Id;
            await SaveReference(mdm_taxtype);
            return true;
        }

        public async Task<bool> Update(mdm_taxtype mdm_taxtype)
        {
            mdm_taxtype mdm_taxtype = DataContext.mdm_taxtype.Where(x => x.Id == mdm_taxtype.Id).FirstOrDefault();
            if (mdm_taxtype == null)
                return false;
            mdm_taxtype.Id = mdm_taxtype.Id;
            mdm_taxtype.Code = mdm_taxtype.Code;
            mdm_taxtype.Name = mdm_taxtype.Name;
            mdm_taxtype.Percentage = mdm_taxtype.Percentage;
            mdm_taxtype.StatusId = mdm_taxtype.StatusId;
            mdm_taxtype.Used = mdm_taxtype.Used;
            mdm_taxtype.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(mdm_taxtype);
            return true;
        }

        public async Task<bool> Delete(mdm_taxtype mdm_taxtype)
        {
            await DataContext.mdm_taxtype.Where(x => x.Id == mdm_taxtype.Id).UpdateFromQueryAsync(x => new mdm_taxtype { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<mdm_taxtype> mdm_taxtypes)
        {
            List<mdm_taxtype> mdm_taxtypes = new List<mdm_taxtype>();
            foreach (mdm_taxtype mdm_taxtype in mdm_taxtypes)
            {
                mdm_taxtype mdm_taxtype = new mdm_taxtype();
                mdm_taxtype.Id = mdm_taxtype.Id;
                mdm_taxtype.Code = mdm_taxtype.Code;
                mdm_taxtype.Name = mdm_taxtype.Name;
                mdm_taxtype.Percentage = mdm_taxtype.Percentage;
                mdm_taxtype.StatusId = mdm_taxtype.StatusId;
                mdm_taxtype.Used = mdm_taxtype.Used;
                mdm_taxtype.CreatedAt = StaticParams.DateTimeNow;
                mdm_taxtype.UpdatedAt = StaticParams.DateTimeNow;
                mdm_taxtypes.Add(mdm_taxtype);
            }
            await DataContext.BulkMergeAsync(mdm_taxtypes);
            return true;
        }

        public async Task<bool> BulkDelete(List<mdm_taxtype> mdm_taxtypes)
        {
            List<long> Ids = mdm_taxtypes.Select(x => x.Id).ToList();
            await DataContext.mdm_taxtype
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new mdm_taxtype { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(mdm_taxtype mdm_taxtype)
        {
        }

    }
}
