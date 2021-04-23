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
    public interface Imdm_unitofmeasureRepository
    {
        Task<int>
    Count(mdm_unitofmeasureFilter mdm_unitofmeasureFilter);
    Task<List<BASE.Entities.mdm_unitofmeasure>> List(mdm_unitofmeasureFilter mdm_unitofmeasureFilter);
        Task<BASE.Entities.mdm_unitofmeasure> Get(long Id);
        Task<bool> Create(BASE.Entities.mdm_unitofmeasure mdm_unitofmeasure);
        Task<bool> Update(BASE.Entities.mdm_unitofmeasure mdm_unitofmeasure);
        Task<bool> Delete(BASE.Entities.mdm_unitofmeasure mdm_unitofmeasure);
        Task<bool> BulkMerge(List<BASE.Entities.mdm_unitofmeasure> mdm_unitofmeasures);
        Task<bool> BulkDelete(List<BASE.Entities.mdm_unitofmeasure> mdm_unitofmeasures);
                    }
                    public class mdm_unitofmeasureRepository : Imdm_unitofmeasureRepository
                    {
                    private DataContext DataContext;
                    public mdm_unitofmeasureRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.mdm_unitofmeasure>
                        DynamicFilter(IQueryable<BASE.Models.mdm_unitofmeasure>
                            query, mdm_unitofmeasureFilter filter)
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
                            if (filter.StatusId != null)
                            query = query.Where(q => q.StatusId, filter.StatusId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.mdm_unitofmeasure>
                                OrFilter(IQueryable<BASE.Models.mdm_unitofmeasure>
                                    query, mdm_unitofmeasureFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.mdm_unitofmeasure>
                                        initQuery = query.Where(q => false);
                                        foreach (mdm_unitofmeasureFilter mdm_unitofmeasureFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.mdm_unitofmeasure>
                                            queryable = query;
                                            if (mdm_unitofmeasureFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, mdm_unitofmeasureFilter.Id);
                                            if (mdm_unitofmeasureFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, mdm_unitofmeasureFilter.Code);
                                            if (mdm_unitofmeasureFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, mdm_unitofmeasureFilter.Name);
                                            if (mdm_unitofmeasureFilter.Description != null)
                                            queryable = queryable.Where(q => q.Description, mdm_unitofmeasureFilter.Description);
                                            if (mdm_unitofmeasureFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId, mdm_unitofmeasureFilter.StatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.mdm_unitofmeasure>
                                                DynamicOrder(IQueryable<BASE.Models.mdm_unitofmeasure>
                                                    query, mdm_unitofmeasureFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_unitofmeasureOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case mdm_unitofmeasureOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case mdm_unitofmeasureOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case mdm_unitofmeasureOrder.Description:
                                                    query = query.OrderBy(q => q.Description);
                                                    break;
                                                    case mdm_unitofmeasureOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    case mdm_unitofmeasureOrder.Used:
                                                    query = query.OrderBy(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_unitofmeasureOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case mdm_unitofmeasureOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case mdm_unitofmeasureOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case mdm_unitofmeasureOrder.Description:
                                                    query = query.OrderByDescending(q => q.Description);
                                                    break;
                                                    case mdm_unitofmeasureOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    case mdm_unitofmeasureOrder.Used:
                                                    query = query.OrderByDescending(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.mdm_unitofmeasure>> DynamicSelect(IQueryable<BASE.Models.mdm_unitofmeasure> query, mdm_unitofmeasureFilter filter)
        {
            List<mdm_unitofmeasure> mdm_unitofmeasures = await query.Select(q => new mdm_unitofmeasure()
            {
                Id = filter.Selects.Contains(mdm_unitofmeasureSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(mdm_unitofmeasureSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(mdm_unitofmeasureSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(mdm_unitofmeasureSelect.Description) ? q.Description : default(string),
                StatusId = filter.Selects.Contains(mdm_unitofmeasureSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(mdm_unitofmeasureSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(mdm_unitofmeasureSelect.Status) && q.Status != null ? new enum_status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return mdm_unitofmeasures;
        }

        public async Task<int> Count(mdm_unitofmeasureFilter filter)
        {
            IQueryable<BASE.Models.mdm_unitofmeasure> mdm_unitofmeasures = DataContext.mdm_unitofmeasure.AsNoTracking();
            mdm_unitofmeasures = DynamicFilter(mdm_unitofmeasures, filter);
            return await mdm_unitofmeasures.CountAsync();
        }

        public async Task<List<mdm_unitofmeasure>> List(mdm_unitofmeasureFilter filter)
        {
            if (filter == null) return new List<mdm_unitofmeasure>();
            IQueryable<BASE.Models.mdm_unitofmeasure> mdm_unitofmeasures = DataContext.mdm_unitofmeasure.AsNoTracking();
            mdm_unitofmeasures = DynamicFilter(mdm_unitofmeasures, filter);
            mdm_unitofmeasures = DynamicOrder(mdm_unitofmeasures, filter);
            List<mdm_unitofmeasure> mdm_unitofmeasures = await DynamicSelect(mdm_unitofmeasures, filter);
            return mdm_unitofmeasures;
        }

        public async Task<mdm_unitofmeasure> Get(long Id)
        {
            mdm_unitofmeasure mdm_unitofmeasure = await DataContext.mdm_unitofmeasure.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new mdm_unitofmeasure()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Description = x.Description,
                StatusId = x.StatusId,
                Used = x.Used,
                Status = x.Status == null ? null : new enum_status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (mdm_unitofmeasure == null)
                return null;

            return mdm_unitofmeasure;
        }
        public async Task<bool> Create(mdm_unitofmeasure mdm_unitofmeasure)
        {
            mdm_unitofmeasure mdm_unitofmeasure = new mdm_unitofmeasure();
            mdm_unitofmeasure.Id = mdm_unitofmeasure.Id;
            mdm_unitofmeasure.Code = mdm_unitofmeasure.Code;
            mdm_unitofmeasure.Name = mdm_unitofmeasure.Name;
            mdm_unitofmeasure.Description = mdm_unitofmeasure.Description;
            mdm_unitofmeasure.StatusId = mdm_unitofmeasure.StatusId;
            mdm_unitofmeasure.Used = mdm_unitofmeasure.Used;
            mdm_unitofmeasure.CreatedAt = StaticParams.DateTimeNow;
            mdm_unitofmeasure.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.mdm_unitofmeasure.Add(mdm_unitofmeasure);
            await DataContext.SaveChangesAsync();
            mdm_unitofmeasure.Id = mdm_unitofmeasure.Id;
            await SaveReference(mdm_unitofmeasure);
            return true;
        }

        public async Task<bool> Update(mdm_unitofmeasure mdm_unitofmeasure)
        {
            mdm_unitofmeasure mdm_unitofmeasure = DataContext.mdm_unitofmeasure.Where(x => x.Id == mdm_unitofmeasure.Id).FirstOrDefault();
            if (mdm_unitofmeasure == null)
                return false;
            mdm_unitofmeasure.Id = mdm_unitofmeasure.Id;
            mdm_unitofmeasure.Code = mdm_unitofmeasure.Code;
            mdm_unitofmeasure.Name = mdm_unitofmeasure.Name;
            mdm_unitofmeasure.Description = mdm_unitofmeasure.Description;
            mdm_unitofmeasure.StatusId = mdm_unitofmeasure.StatusId;
            mdm_unitofmeasure.Used = mdm_unitofmeasure.Used;
            mdm_unitofmeasure.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(mdm_unitofmeasure);
            return true;
        }

        public async Task<bool> Delete(mdm_unitofmeasure mdm_unitofmeasure)
        {
            await DataContext.mdm_unitofmeasure.Where(x => x.Id == mdm_unitofmeasure.Id).UpdateFromQueryAsync(x => new mdm_unitofmeasure { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<mdm_unitofmeasure> mdm_unitofmeasures)
        {
            List<mdm_unitofmeasure> mdm_unitofmeasures = new List<mdm_unitofmeasure>();
            foreach (mdm_unitofmeasure mdm_unitofmeasure in mdm_unitofmeasures)
            {
                mdm_unitofmeasure mdm_unitofmeasure = new mdm_unitofmeasure();
                mdm_unitofmeasure.Id = mdm_unitofmeasure.Id;
                mdm_unitofmeasure.Code = mdm_unitofmeasure.Code;
                mdm_unitofmeasure.Name = mdm_unitofmeasure.Name;
                mdm_unitofmeasure.Description = mdm_unitofmeasure.Description;
                mdm_unitofmeasure.StatusId = mdm_unitofmeasure.StatusId;
                mdm_unitofmeasure.Used = mdm_unitofmeasure.Used;
                mdm_unitofmeasure.CreatedAt = StaticParams.DateTimeNow;
                mdm_unitofmeasure.UpdatedAt = StaticParams.DateTimeNow;
                mdm_unitofmeasures.Add(mdm_unitofmeasure);
            }
            await DataContext.BulkMergeAsync(mdm_unitofmeasures);
            return true;
        }

        public async Task<bool> BulkDelete(List<mdm_unitofmeasure> mdm_unitofmeasures)
        {
            List<long> Ids = mdm_unitofmeasures.Select(x => x.Id).ToList();
            await DataContext.mdm_unitofmeasure
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new mdm_unitofmeasure { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(mdm_unitofmeasure mdm_unitofmeasure)
        {
        }

    }
}
