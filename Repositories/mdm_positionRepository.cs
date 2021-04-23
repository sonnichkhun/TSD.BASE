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
    public interface Imdm_positionRepository
    {
        Task<int>
    Count(mdm_positionFilter mdm_positionFilter);
    Task<List<BASE.Entities.mdm_position>> List(mdm_positionFilter mdm_positionFilter);
        Task<BASE.Entities.mdm_position> Get(long Id);
        Task<bool> Create(BASE.Entities.mdm_position mdm_position);
        Task<bool> Update(BASE.Entities.mdm_position mdm_position);
        Task<bool> Delete(BASE.Entities.mdm_position mdm_position);
        Task<bool> BulkMerge(List<BASE.Entities.mdm_position> mdm_positions);
        Task<bool> BulkDelete(List<BASE.Entities.mdm_position> mdm_positions);
                    }
                    public class mdm_positionRepository : Imdm_positionRepository
                    {
                    private DataContext DataContext;
                    public mdm_positionRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.mdm_position>
                        DynamicFilter(IQueryable<BASE.Models.mdm_position>
                            query, mdm_positionFilter filter)
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
                            if (filter.StatusId != null)
                            query = query.Where(q => q.StatusId, filter.StatusId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.mdm_position>
                                OrFilter(IQueryable<BASE.Models.mdm_position>
                                    query, mdm_positionFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.mdm_position>
                                        initQuery = query.Where(q => false);
                                        foreach (mdm_positionFilter mdm_positionFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.mdm_position>
                                            queryable = query;
                                            if (mdm_positionFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, mdm_positionFilter.Id);
                                            if (mdm_positionFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, mdm_positionFilter.Code);
                                            if (mdm_positionFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, mdm_positionFilter.Name);
                                            if (mdm_positionFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId, mdm_positionFilter.StatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.mdm_position>
                                                DynamicOrder(IQueryable<BASE.Models.mdm_position>
                                                    query, mdm_positionFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_positionOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case mdm_positionOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case mdm_positionOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case mdm_positionOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    case mdm_positionOrder.Used:
                                                    query = query.OrderBy(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_positionOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case mdm_positionOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case mdm_positionOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case mdm_positionOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    case mdm_positionOrder.Used:
                                                    query = query.OrderByDescending(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.mdm_position>> DynamicSelect(IQueryable<BASE.Models.mdm_position> query, mdm_positionFilter filter)
        {
            List<mdm_position> mdm_positions = await query.Select(q => new mdm_position()
            {
                Id = filter.Selects.Contains(mdm_positionSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(mdm_positionSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(mdm_positionSelect.Name) ? q.Name : default(string),
                StatusId = filter.Selects.Contains(mdm_positionSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(mdm_positionSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(mdm_positionSelect.Status) && q.Status != null ? new enum_status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return mdm_positions;
        }

        public async Task<int> Count(mdm_positionFilter filter)
        {
            IQueryable<BASE.Models.mdm_position> mdm_positions = DataContext.mdm_position.AsNoTracking();
            mdm_positions = DynamicFilter(mdm_positions, filter);
            return await mdm_positions.CountAsync();
        }

        public async Task<List<mdm_position>> List(mdm_positionFilter filter)
        {
            if (filter == null) return new List<mdm_position>();
            IQueryable<BASE.Models.mdm_position> mdm_positions = DataContext.mdm_position.AsNoTracking();
            mdm_positions = DynamicFilter(mdm_positions, filter);
            mdm_positions = DynamicOrder(mdm_positions, filter);
            List<mdm_position> mdm_positions = await DynamicSelect(mdm_positions, filter);
            return mdm_positions;
        }

        public async Task<mdm_position> Get(long Id)
        {
            mdm_position mdm_position = await DataContext.mdm_position.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new mdm_position()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
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

            if (mdm_position == null)
                return null;

            return mdm_position;
        }
        public async Task<bool> Create(mdm_position mdm_position)
        {
            mdm_position mdm_position = new mdm_position();
            mdm_position.Id = mdm_position.Id;
            mdm_position.Code = mdm_position.Code;
            mdm_position.Name = mdm_position.Name;
            mdm_position.StatusId = mdm_position.StatusId;
            mdm_position.Used = mdm_position.Used;
            mdm_position.CreatedAt = StaticParams.DateTimeNow;
            mdm_position.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.mdm_position.Add(mdm_position);
            await DataContext.SaveChangesAsync();
            mdm_position.Id = mdm_position.Id;
            await SaveReference(mdm_position);
            return true;
        }

        public async Task<bool> Update(mdm_position mdm_position)
        {
            mdm_position mdm_position = DataContext.mdm_position.Where(x => x.Id == mdm_position.Id).FirstOrDefault();
            if (mdm_position == null)
                return false;
            mdm_position.Id = mdm_position.Id;
            mdm_position.Code = mdm_position.Code;
            mdm_position.Name = mdm_position.Name;
            mdm_position.StatusId = mdm_position.StatusId;
            mdm_position.Used = mdm_position.Used;
            mdm_position.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(mdm_position);
            return true;
        }

        public async Task<bool> Delete(mdm_position mdm_position)
        {
            await DataContext.mdm_position.Where(x => x.Id == mdm_position.Id).UpdateFromQueryAsync(x => new mdm_position { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<mdm_position> mdm_positions)
        {
            List<mdm_position> mdm_positions = new List<mdm_position>();
            foreach (mdm_position mdm_position in mdm_positions)
            {
                mdm_position mdm_position = new mdm_position();
                mdm_position.Id = mdm_position.Id;
                mdm_position.Code = mdm_position.Code;
                mdm_position.Name = mdm_position.Name;
                mdm_position.StatusId = mdm_position.StatusId;
                mdm_position.Used = mdm_position.Used;
                mdm_position.CreatedAt = StaticParams.DateTimeNow;
                mdm_position.UpdatedAt = StaticParams.DateTimeNow;
                mdm_positions.Add(mdm_position);
            }
            await DataContext.BulkMergeAsync(mdm_positions);
            return true;
        }

        public async Task<bool> BulkDelete(List<mdm_position> mdm_positions)
        {
            List<long> Ids = mdm_positions.Select(x => x.Id).ToList();
            await DataContext.mdm_position
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new mdm_position { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(mdm_position mdm_position)
        {
        }

    }
}
