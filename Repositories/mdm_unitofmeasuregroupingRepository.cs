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
    public interface Imdm_unitofmeasuregroupingRepository
    {
        Task<int>
    Count(mdm_unitofmeasuregroupingFilter mdm_unitofmeasuregroupingFilter);
    Task<List<BASE.Entities.mdm_unitofmeasuregrouping>> List(mdm_unitofmeasuregroupingFilter mdm_unitofmeasuregroupingFilter);
        Task<BASE.Entities.mdm_unitofmeasuregrouping> Get(long Id);
        Task<bool> Create(BASE.Entities.mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping);
        Task<bool> Update(BASE.Entities.mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping);
        Task<bool> Delete(BASE.Entities.mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping);
        Task<bool> BulkMerge(List<BASE.Entities.mdm_unitofmeasuregrouping> mdm_unitofmeasuregroupings);
        Task<bool> BulkDelete(List<BASE.Entities.mdm_unitofmeasuregrouping> mdm_unitofmeasuregroupings);
                    }
                    public class mdm_unitofmeasuregroupingRepository : Imdm_unitofmeasuregroupingRepository
                    {
                    private DataContext DataContext;
                    public mdm_unitofmeasuregroupingRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.mdm_unitofmeasuregrouping>
                        DynamicFilter(IQueryable<BASE.Models.mdm_unitofmeasuregrouping>
                            query, mdm_unitofmeasuregroupingFilter filter)
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
                            if (filter.Description != null)
                            query = query.Where(q => q.Description, filter.Description);
                            if (filter.UnitOfMeasureId != null)
                            query = query.Where(q => q.UnitOfMeasureId, filter.UnitOfMeasureId);
                            if (filter.StatusId != null)
                            query = query.Where(q => q.StatusId, filter.StatusId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.mdm_unitofmeasuregrouping>
                                OrFilter(IQueryable<BASE.Models.mdm_unitofmeasuregrouping>
                                    query, mdm_unitofmeasuregroupingFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.mdm_unitofmeasuregrouping>
                                        initQuery = query.Where(q => false);
                                        foreach (mdm_unitofmeasuregroupingFilter mdm_unitofmeasuregroupingFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.mdm_unitofmeasuregrouping>
                                            queryable = query;
                                            if (mdm_unitofmeasuregroupingFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, mdm_unitofmeasuregroupingFilter.Id);
                                            if (mdm_unitofmeasuregroupingFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, mdm_unitofmeasuregroupingFilter.Code);
                                            if (mdm_unitofmeasuregroupingFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, mdm_unitofmeasuregroupingFilter.Name);
                                            if (mdm_unitofmeasuregroupingFilter.Description != null)
                                            queryable = queryable.Where(q => q.Description, mdm_unitofmeasuregroupingFilter.Description);
                                            if (mdm_unitofmeasuregroupingFilter.UnitOfMeasureId != null)
                                            queryable = queryable.Where(q => q.UnitOfMeasureId, mdm_unitofmeasuregroupingFilter.UnitOfMeasureId);
                                            if (mdm_unitofmeasuregroupingFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId, mdm_unitofmeasuregroupingFilter.StatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.mdm_unitofmeasuregrouping>
                                                DynamicOrder(IQueryable<BASE.Models.mdm_unitofmeasuregrouping>
                                                    query, mdm_unitofmeasuregroupingFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_unitofmeasuregroupingOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case mdm_unitofmeasuregroupingOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case mdm_unitofmeasuregroupingOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case mdm_unitofmeasuregroupingOrder.Description:
                                                    query = query.OrderBy(q => q.Description);
                                                    break;
                                                    case mdm_unitofmeasuregroupingOrder.UnitOfMeasure:
                                                    query = query.OrderBy(q => q.UnitOfMeasureId);
                                                    break;
                                                    case mdm_unitofmeasuregroupingOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    case mdm_unitofmeasuregroupingOrder.Used:
                                                    query = query.OrderBy(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_unitofmeasuregroupingOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case mdm_unitofmeasuregroupingOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case mdm_unitofmeasuregroupingOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case mdm_unitofmeasuregroupingOrder.Description:
                                                    query = query.OrderByDescending(q => q.Description);
                                                    break;
                                                    case mdm_unitofmeasuregroupingOrder.UnitOfMeasure:
                                                    query = query.OrderByDescending(q => q.UnitOfMeasureId);
                                                    break;
                                                    case mdm_unitofmeasuregroupingOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    case mdm_unitofmeasuregroupingOrder.Used:
                                                    query = query.OrderByDescending(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.mdm_unitofmeasuregrouping>> DynamicSelect(IQueryable<BASE.Models.mdm_unitofmeasuregrouping> query, mdm_unitofmeasuregroupingFilter filter)
        {
            List<mdm_unitofmeasuregrouping> mdm_unitofmeasuregroupings = await query.Select(q => new mdm_unitofmeasuregrouping()
            {
                Id = filter.Selects.Contains(mdm_unitofmeasuregroupingSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(mdm_unitofmeasuregroupingSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(mdm_unitofmeasuregroupingSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(mdm_unitofmeasuregroupingSelect.Description) ? q.Description : default(string),
                UnitOfMeasureId = filter.Selects.Contains(mdm_unitofmeasuregroupingSelect.UnitOfMeasure) ? q.UnitOfMeasureId : default(long),
                StatusId = filter.Selects.Contains(mdm_unitofmeasuregroupingSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(mdm_unitofmeasuregroupingSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(mdm_unitofmeasuregroupingSelect.Status) && q.Status != null ? new enum_status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                UnitOfMeasure = filter.Selects.Contains(mdm_unitofmeasuregroupingSelect.UnitOfMeasure) && q.UnitOfMeasure != null ? new mdm_unitofmeasure
                {
                    Id = q.UnitOfMeasure.Id,
                    Code = q.UnitOfMeasure.Code,
                    Name = q.UnitOfMeasure.Name,
                    Description = q.UnitOfMeasure.Description,
                    StatusId = q.UnitOfMeasure.StatusId,
                    Used = q.UnitOfMeasure.Used,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return mdm_unitofmeasuregroupings;
        }

        public async Task<int> Count(mdm_unitofmeasuregroupingFilter filter)
        {
            IQueryable<BASE.Models.mdm_unitofmeasuregrouping> mdm_unitofmeasuregroupings = DataContext.mdm_unitofmeasuregrouping.AsNoTracking();
            mdm_unitofmeasuregroupings = DynamicFilter(mdm_unitofmeasuregroupings, filter);
            return await mdm_unitofmeasuregroupings.CountAsync();
        }

        public async Task<List<mdm_unitofmeasuregrouping>> List(mdm_unitofmeasuregroupingFilter filter)
        {
            if (filter == null) return new List<mdm_unitofmeasuregrouping>();
            IQueryable<BASE.Models.mdm_unitofmeasuregrouping> mdm_unitofmeasuregroupings = DataContext.mdm_unitofmeasuregrouping.AsNoTracking();
            mdm_unitofmeasuregroupings = DynamicFilter(mdm_unitofmeasuregroupings, filter);
            mdm_unitofmeasuregroupings = DynamicOrder(mdm_unitofmeasuregroupings, filter);
            List<mdm_unitofmeasuregrouping> mdm_unitofmeasuregroupings = await DynamicSelect(mdm_unitofmeasuregroupings, filter);
            return mdm_unitofmeasuregroupings;
        }

        public async Task<mdm_unitofmeasuregrouping> Get(long Id)
        {
            mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping = await DataContext.mdm_unitofmeasuregrouping.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new mdm_unitofmeasuregrouping()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Description = x.Description,
                UnitOfMeasureId = x.UnitOfMeasureId,
                StatusId = x.StatusId,
                Used = x.Used,
                Status = x.Status == null ? null : new enum_status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
                UnitOfMeasure = x.UnitOfMeasure == null ? null : new mdm_unitofmeasure
                {
                    Id = x.UnitOfMeasure.Id,
                    Code = x.UnitOfMeasure.Code,
                    Name = x.UnitOfMeasure.Name,
                    Description = x.UnitOfMeasure.Description,
                    StatusId = x.UnitOfMeasure.StatusId,
                    Used = x.UnitOfMeasure.Used,
                },
            }).FirstOrDefaultAsync();

            if (mdm_unitofmeasuregrouping == null)
                return null;

            return mdm_unitofmeasuregrouping;
        }
        public async Task<bool> Create(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping)
        {
            mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping = new mdm_unitofmeasuregrouping();
            mdm_unitofmeasuregrouping.Id = mdm_unitofmeasuregrouping.Id;
            mdm_unitofmeasuregrouping.Code = mdm_unitofmeasuregrouping.Code;
            mdm_unitofmeasuregrouping.Name = mdm_unitofmeasuregrouping.Name;
            mdm_unitofmeasuregrouping.Description = mdm_unitofmeasuregrouping.Description;
            mdm_unitofmeasuregrouping.UnitOfMeasureId = mdm_unitofmeasuregrouping.UnitOfMeasureId;
            mdm_unitofmeasuregrouping.StatusId = mdm_unitofmeasuregrouping.StatusId;
            mdm_unitofmeasuregrouping.Used = mdm_unitofmeasuregrouping.Used;
            mdm_unitofmeasuregrouping.CreatedAt = StaticParams.DateTimeNow;
            mdm_unitofmeasuregrouping.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.mdm_unitofmeasuregrouping.Add(mdm_unitofmeasuregrouping);
            await DataContext.SaveChangesAsync();
            mdm_unitofmeasuregrouping.Id = mdm_unitofmeasuregrouping.Id;
            await SaveReference(mdm_unitofmeasuregrouping);
            return true;
        }

        public async Task<bool> Update(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping)
        {
            mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping = DataContext.mdm_unitofmeasuregrouping.Where(x => x.Id == mdm_unitofmeasuregrouping.Id).FirstOrDefault();
            if (mdm_unitofmeasuregrouping == null)
                return false;
            mdm_unitofmeasuregrouping.Id = mdm_unitofmeasuregrouping.Id;
            mdm_unitofmeasuregrouping.Code = mdm_unitofmeasuregrouping.Code;
            mdm_unitofmeasuregrouping.Name = mdm_unitofmeasuregrouping.Name;
            mdm_unitofmeasuregrouping.Description = mdm_unitofmeasuregrouping.Description;
            mdm_unitofmeasuregrouping.UnitOfMeasureId = mdm_unitofmeasuregrouping.UnitOfMeasureId;
            mdm_unitofmeasuregrouping.StatusId = mdm_unitofmeasuregrouping.StatusId;
            mdm_unitofmeasuregrouping.Used = mdm_unitofmeasuregrouping.Used;
            mdm_unitofmeasuregrouping.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(mdm_unitofmeasuregrouping);
            return true;
        }

        public async Task<bool> Delete(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping)
        {
            await DataContext.mdm_unitofmeasuregrouping.Where(x => x.Id == mdm_unitofmeasuregrouping.Id).UpdateFromQueryAsync(x => new mdm_unitofmeasuregrouping { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<mdm_unitofmeasuregrouping> mdm_unitofmeasuregroupings)
        {
            List<mdm_unitofmeasuregrouping> mdm_unitofmeasuregroupings = new List<mdm_unitofmeasuregrouping>();
            foreach (mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping in mdm_unitofmeasuregroupings)
            {
                mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping = new mdm_unitofmeasuregrouping();
                mdm_unitofmeasuregrouping.Id = mdm_unitofmeasuregrouping.Id;
                mdm_unitofmeasuregrouping.Code = mdm_unitofmeasuregrouping.Code;
                mdm_unitofmeasuregrouping.Name = mdm_unitofmeasuregrouping.Name;
                mdm_unitofmeasuregrouping.Description = mdm_unitofmeasuregrouping.Description;
                mdm_unitofmeasuregrouping.UnitOfMeasureId = mdm_unitofmeasuregrouping.UnitOfMeasureId;
                mdm_unitofmeasuregrouping.StatusId = mdm_unitofmeasuregrouping.StatusId;
                mdm_unitofmeasuregrouping.Used = mdm_unitofmeasuregrouping.Used;
                mdm_unitofmeasuregrouping.CreatedAt = StaticParams.DateTimeNow;
                mdm_unitofmeasuregrouping.UpdatedAt = StaticParams.DateTimeNow;
                mdm_unitofmeasuregroupings.Add(mdm_unitofmeasuregrouping);
            }
            await DataContext.BulkMergeAsync(mdm_unitofmeasuregroupings);
            return true;
        }

        public async Task<bool> BulkDelete(List<mdm_unitofmeasuregrouping> mdm_unitofmeasuregroupings)
        {
            List<long> Ids = mdm_unitofmeasuregroupings.Select(x => x.Id).ToList();
            await DataContext.mdm_unitofmeasuregrouping
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new mdm_unitofmeasuregrouping { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(mdm_unitofmeasuregrouping mdm_unitofmeasuregrouping)
        {
        }

    }
}
