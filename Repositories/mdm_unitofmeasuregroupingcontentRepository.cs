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
    public interface Imdm_unitofmeasuregroupingcontentRepository
    {
        Task<int>
    Count(mdm_unitofmeasuregroupingcontentFilter mdm_unitofmeasuregroupingcontentFilter);
    Task<List<BASE.Entities.mdm_unitofmeasuregroupingcontent>> List(mdm_unitofmeasuregroupingcontentFilter mdm_unitofmeasuregroupingcontentFilter);
        Task<BASE.Entities.mdm_unitofmeasuregroupingcontent> Get(long Id);
        Task<bool> Create(BASE.Entities.mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent);
        Task<bool> Update(BASE.Entities.mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent);
        Task<bool> Delete(BASE.Entities.mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent);
        Task<bool> BulkMerge(List<BASE.Entities.mdm_unitofmeasuregroupingcontent> mdm_unitofmeasuregroupingcontents);
        Task<bool> BulkDelete(List<BASE.Entities.mdm_unitofmeasuregroupingcontent> mdm_unitofmeasuregroupingcontents);
                    }
                    public class mdm_unitofmeasuregroupingcontentRepository : Imdm_unitofmeasuregroupingcontentRepository
                    {
                    private DataContext DataContext;
                    public mdm_unitofmeasuregroupingcontentRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.mdm_unitofmeasuregroupingcontent>
                        DynamicFilter(IQueryable<BASE.Models.mdm_unitofmeasuregroupingcontent>
                            query, mdm_unitofmeasuregroupingcontentFilter filter)
                            {
                            if (filter == null)
                            return query.Where(q => false);
                            if (filter.Id != null)
                            query = query.Where(q => q.Id, filter.Id);
                            if (filter.UnitOfMeasureGroupingId != null)
                            query = query.Where(q => q.UnitOfMeasureGroupingId, filter.UnitOfMeasureGroupingId);
                            if (filter.UnitOfMeasureId != null)
                            query = query.Where(q => q.UnitOfMeasureId, filter.UnitOfMeasureId);
                            if (filter.Factor != null)
                            query = query.Where(q => q.Factor.HasValue).Where(q => q.Factor, filter.Factor);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.mdm_unitofmeasuregroupingcontent>
                                OrFilter(IQueryable<BASE.Models.mdm_unitofmeasuregroupingcontent>
                                    query, mdm_unitofmeasuregroupingcontentFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.mdm_unitofmeasuregroupingcontent>
                                        initQuery = query.Where(q => false);
                                        foreach (mdm_unitofmeasuregroupingcontentFilter mdm_unitofmeasuregroupingcontentFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.mdm_unitofmeasuregroupingcontent>
                                            queryable = query;
                                            if (mdm_unitofmeasuregroupingcontentFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, mdm_unitofmeasuregroupingcontentFilter.Id);
                                            if (mdm_unitofmeasuregroupingcontentFilter.UnitOfMeasureGroupingId != null)
                                            queryable = queryable.Where(q => q.UnitOfMeasureGroupingId, mdm_unitofmeasuregroupingcontentFilter.UnitOfMeasureGroupingId);
                                            if (mdm_unitofmeasuregroupingcontentFilter.UnitOfMeasureId != null)
                                            queryable = queryable.Where(q => q.UnitOfMeasureId, mdm_unitofmeasuregroupingcontentFilter.UnitOfMeasureId);
                                            if (mdm_unitofmeasuregroupingcontentFilter.Factor != null)
                                            queryable = queryable.Where(q => q.Factor.HasValue).Where(q => q.Factor, mdm_unitofmeasuregroupingcontentFilter.Factor);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.mdm_unitofmeasuregroupingcontent>
                                                DynamicOrder(IQueryable<BASE.Models.mdm_unitofmeasuregroupingcontent>
                                                    query, mdm_unitofmeasuregroupingcontentFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_unitofmeasuregroupingcontentOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case mdm_unitofmeasuregroupingcontentOrder.UnitOfMeasureGrouping:
                                                    query = query.OrderBy(q => q.UnitOfMeasureGroupingId);
                                                    break;
                                                    case mdm_unitofmeasuregroupingcontentOrder.UnitOfMeasure:
                                                    query = query.OrderBy(q => q.UnitOfMeasureId);
                                                    break;
                                                    case mdm_unitofmeasuregroupingcontentOrder.Factor:
                                                    query = query.OrderBy(q => q.Factor);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_unitofmeasuregroupingcontentOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case mdm_unitofmeasuregroupingcontentOrder.UnitOfMeasureGrouping:
                                                    query = query.OrderByDescending(q => q.UnitOfMeasureGroupingId);
                                                    break;
                                                    case mdm_unitofmeasuregroupingcontentOrder.UnitOfMeasure:
                                                    query = query.OrderByDescending(q => q.UnitOfMeasureId);
                                                    break;
                                                    case mdm_unitofmeasuregroupingcontentOrder.Factor:
                                                    query = query.OrderByDescending(q => q.Factor);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.mdm_unitofmeasuregroupingcontent>> DynamicSelect(IQueryable<BASE.Models.mdm_unitofmeasuregroupingcontent> query, mdm_unitofmeasuregroupingcontentFilter filter)
        {
            List<mdm_unitofmeasuregroupingcontent> mdm_unitofmeasuregroupingcontents = await query.Select(q => new mdm_unitofmeasuregroupingcontent()
            {
                Id = filter.Selects.Contains(mdm_unitofmeasuregroupingcontentSelect.Id) ? q.Id : default(long),
                UnitOfMeasureGroupingId = filter.Selects.Contains(mdm_unitofmeasuregroupingcontentSelect.UnitOfMeasureGrouping) ? q.UnitOfMeasureGroupingId : default(long),
                UnitOfMeasureId = filter.Selects.Contains(mdm_unitofmeasuregroupingcontentSelect.UnitOfMeasure) ? q.UnitOfMeasureId : default(long),
                Factor = filter.Selects.Contains(mdm_unitofmeasuregroupingcontentSelect.Factor) ? q.Factor : default(long?),
                UnitOfMeasure = filter.Selects.Contains(mdm_unitofmeasuregroupingcontentSelect.UnitOfMeasure) && q.UnitOfMeasure != null ? new mdm_unitofmeasure
                {
                    Id = q.UnitOfMeasure.Id,
                    Code = q.UnitOfMeasure.Code,
                    Name = q.UnitOfMeasure.Name,
                    Description = q.UnitOfMeasure.Description,
                    StatusId = q.UnitOfMeasure.StatusId,
                    Used = q.UnitOfMeasure.Used,
                } : null,
                UnitOfMeasureGrouping = filter.Selects.Contains(mdm_unitofmeasuregroupingcontentSelect.UnitOfMeasureGrouping) && q.UnitOfMeasureGrouping != null ? new mdm_unitofmeasuregrouping
                {
                    Id = q.UnitOfMeasureGrouping.Id,
                    Code = q.UnitOfMeasureGrouping.Code,
                    Name = q.UnitOfMeasureGrouping.Name,
                    Description = q.UnitOfMeasureGrouping.Description,
                    UnitOfMeasureId = q.UnitOfMeasureGrouping.UnitOfMeasureId,
                    StatusId = q.UnitOfMeasureGrouping.StatusId,
                    Used = q.UnitOfMeasureGrouping.Used,
                } : null,
            }).ToListAsync();
            return mdm_unitofmeasuregroupingcontents;
        }

        public async Task<int> Count(mdm_unitofmeasuregroupingcontentFilter filter)
        {
            IQueryable<BASE.Models.mdm_unitofmeasuregroupingcontent> mdm_unitofmeasuregroupingcontents = DataContext.mdm_unitofmeasuregroupingcontent.AsNoTracking();
            mdm_unitofmeasuregroupingcontents = DynamicFilter(mdm_unitofmeasuregroupingcontents, filter);
            return await mdm_unitofmeasuregroupingcontents.CountAsync();
        }

        public async Task<List<mdm_unitofmeasuregroupingcontent>> List(mdm_unitofmeasuregroupingcontentFilter filter)
        {
            if (filter == null) return new List<mdm_unitofmeasuregroupingcontent>();
            IQueryable<BASE.Models.mdm_unitofmeasuregroupingcontent> mdm_unitofmeasuregroupingcontents = DataContext.mdm_unitofmeasuregroupingcontent.AsNoTracking();
            mdm_unitofmeasuregroupingcontents = DynamicFilter(mdm_unitofmeasuregroupingcontents, filter);
            mdm_unitofmeasuregroupingcontents = DynamicOrder(mdm_unitofmeasuregroupingcontents, filter);
            List<mdm_unitofmeasuregroupingcontent> mdm_unitofmeasuregroupingcontents = await DynamicSelect(mdm_unitofmeasuregroupingcontents, filter);
            return mdm_unitofmeasuregroupingcontents;
        }

        public async Task<mdm_unitofmeasuregroupingcontent> Get(long Id)
        {
            mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent = await DataContext.mdm_unitofmeasuregroupingcontent.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new mdm_unitofmeasuregroupingcontent()
            {
                Id = x.Id,
                UnitOfMeasureGroupingId = x.UnitOfMeasureGroupingId,
                UnitOfMeasureId = x.UnitOfMeasureId,
                Factor = x.Factor,
                UnitOfMeasure = x.UnitOfMeasure == null ? null : new mdm_unitofmeasure
                {
                    Id = x.UnitOfMeasure.Id,
                    Code = x.UnitOfMeasure.Code,
                    Name = x.UnitOfMeasure.Name,
                    Description = x.UnitOfMeasure.Description,
                    StatusId = x.UnitOfMeasure.StatusId,
                    Used = x.UnitOfMeasure.Used,
                },
                UnitOfMeasureGrouping = x.UnitOfMeasureGrouping == null ? null : new mdm_unitofmeasuregrouping
                {
                    Id = x.UnitOfMeasureGrouping.Id,
                    Code = x.UnitOfMeasureGrouping.Code,
                    Name = x.UnitOfMeasureGrouping.Name,
                    Description = x.UnitOfMeasureGrouping.Description,
                    UnitOfMeasureId = x.UnitOfMeasureGrouping.UnitOfMeasureId,
                    StatusId = x.UnitOfMeasureGrouping.StatusId,
                    Used = x.UnitOfMeasureGrouping.Used,
                },
            }).FirstOrDefaultAsync();

            if (mdm_unitofmeasuregroupingcontent == null)
                return null;

            return mdm_unitofmeasuregroupingcontent;
        }
        public async Task<bool> Create(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent)
        {
            mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent = new mdm_unitofmeasuregroupingcontent();
            mdm_unitofmeasuregroupingcontent.Id = mdm_unitofmeasuregroupingcontent.Id;
            mdm_unitofmeasuregroupingcontent.UnitOfMeasureGroupingId = mdm_unitofmeasuregroupingcontent.UnitOfMeasureGroupingId;
            mdm_unitofmeasuregroupingcontent.UnitOfMeasureId = mdm_unitofmeasuregroupingcontent.UnitOfMeasureId;
            mdm_unitofmeasuregroupingcontent.Factor = mdm_unitofmeasuregroupingcontent.Factor;
            DataContext.mdm_unitofmeasuregroupingcontent.Add(mdm_unitofmeasuregroupingcontent);
            await DataContext.SaveChangesAsync();
            mdm_unitofmeasuregroupingcontent.Id = mdm_unitofmeasuregroupingcontent.Id;
            await SaveReference(mdm_unitofmeasuregroupingcontent);
            return true;
        }

        public async Task<bool> Update(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent)
        {
            mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent = DataContext.mdm_unitofmeasuregroupingcontent.Where(x => x.Id == mdm_unitofmeasuregroupingcontent.Id).FirstOrDefault();
            if (mdm_unitofmeasuregroupingcontent == null)
                return false;
            mdm_unitofmeasuregroupingcontent.Id = mdm_unitofmeasuregroupingcontent.Id;
            mdm_unitofmeasuregroupingcontent.UnitOfMeasureGroupingId = mdm_unitofmeasuregroupingcontent.UnitOfMeasureGroupingId;
            mdm_unitofmeasuregroupingcontent.UnitOfMeasureId = mdm_unitofmeasuregroupingcontent.UnitOfMeasureId;
            mdm_unitofmeasuregroupingcontent.Factor = mdm_unitofmeasuregroupingcontent.Factor;
            await DataContext.SaveChangesAsync();
            await SaveReference(mdm_unitofmeasuregroupingcontent);
            return true;
        }

        public async Task<bool> Delete(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent)
        {
            await DataContext.mdm_unitofmeasuregroupingcontent.Where(x => x.Id == mdm_unitofmeasuregroupingcontent.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<mdm_unitofmeasuregroupingcontent> mdm_unitofmeasuregroupingcontents)
        {
            List<mdm_unitofmeasuregroupingcontent> mdm_unitofmeasuregroupingcontents = new List<mdm_unitofmeasuregroupingcontent>();
            foreach (mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent in mdm_unitofmeasuregroupingcontents)
            {
                mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent = new mdm_unitofmeasuregroupingcontent();
                mdm_unitofmeasuregroupingcontent.Id = mdm_unitofmeasuregroupingcontent.Id;
                mdm_unitofmeasuregroupingcontent.UnitOfMeasureGroupingId = mdm_unitofmeasuregroupingcontent.UnitOfMeasureGroupingId;
                mdm_unitofmeasuregroupingcontent.UnitOfMeasureId = mdm_unitofmeasuregroupingcontent.UnitOfMeasureId;
                mdm_unitofmeasuregroupingcontent.Factor = mdm_unitofmeasuregroupingcontent.Factor;
                mdm_unitofmeasuregroupingcontents.Add(mdm_unitofmeasuregroupingcontent);
            }
            await DataContext.BulkMergeAsync(mdm_unitofmeasuregroupingcontents);
            return true;
        }

        public async Task<bool> BulkDelete(List<mdm_unitofmeasuregroupingcontent> mdm_unitofmeasuregroupingcontents)
        {
            List<long> Ids = mdm_unitofmeasuregroupingcontents.Select(x => x.Id).ToList();
            await DataContext.mdm_unitofmeasuregroupingcontent
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(mdm_unitofmeasuregroupingcontent mdm_unitofmeasuregroupingcontent)
        {
        }

    }
}
