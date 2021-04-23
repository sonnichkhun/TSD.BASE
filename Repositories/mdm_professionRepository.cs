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
    public interface Imdm_professionRepository
    {
        Task<int>
    Count(mdm_professionFilter mdm_professionFilter);
    Task<List<BASE.Entities.mdm_profession>> List(mdm_professionFilter mdm_professionFilter);
        Task<BASE.Entities.mdm_profession> Get(long Id);
        Task<bool> Create(BASE.Entities.mdm_profession mdm_profession);
        Task<bool> Update(BASE.Entities.mdm_profession mdm_profession);
        Task<bool> Delete(BASE.Entities.mdm_profession mdm_profession);
        Task<bool> BulkMerge(List<BASE.Entities.mdm_profession> mdm_professions);
        Task<bool> BulkDelete(List<BASE.Entities.mdm_profession> mdm_professions);
                    }
                    public class mdm_professionRepository : Imdm_professionRepository
                    {
                    private DataContext DataContext;
                    public mdm_professionRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.mdm_profession>
                        DynamicFilter(IQueryable<BASE.Models.mdm_profession>
                            query, mdm_professionFilter filter)
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

                            private IQueryable<BASE.Models.mdm_profession>
                                OrFilter(IQueryable<BASE.Models.mdm_profession>
                                    query, mdm_professionFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.mdm_profession>
                                        initQuery = query.Where(q => false);
                                        foreach (mdm_professionFilter mdm_professionFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.mdm_profession>
                                            queryable = query;
                                            if (mdm_professionFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, mdm_professionFilter.Id);
                                            if (mdm_professionFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, mdm_professionFilter.Code);
                                            if (mdm_professionFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, mdm_professionFilter.Name);
                                            if (mdm_professionFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId, mdm_professionFilter.StatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.mdm_profession>
                                                DynamicOrder(IQueryable<BASE.Models.mdm_profession>
                                                    query, mdm_professionFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_professionOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case mdm_professionOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case mdm_professionOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case mdm_professionOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    case mdm_professionOrder.Used:
                                                    query = query.OrderBy(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_professionOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case mdm_professionOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case mdm_professionOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case mdm_professionOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    case mdm_professionOrder.Used:
                                                    query = query.OrderByDescending(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.mdm_profession>> DynamicSelect(IQueryable<BASE.Models.mdm_profession> query, mdm_professionFilter filter)
        {
            List<mdm_profession> mdm_professions = await query.Select(q => new mdm_profession()
            {
                Id = filter.Selects.Contains(mdm_professionSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(mdm_professionSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(mdm_professionSelect.Name) ? q.Name : default(string),
                StatusId = filter.Selects.Contains(mdm_professionSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(mdm_professionSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(mdm_professionSelect.Status) && q.Status != null ? new enum_status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return mdm_professions;
        }

        public async Task<int> Count(mdm_professionFilter filter)
        {
            IQueryable<BASE.Models.mdm_profession> mdm_professions = DataContext.mdm_profession.AsNoTracking();
            mdm_professions = DynamicFilter(mdm_professions, filter);
            return await mdm_professions.CountAsync();
        }

        public async Task<List<mdm_profession>> List(mdm_professionFilter filter)
        {
            if (filter == null) return new List<mdm_profession>();
            IQueryable<BASE.Models.mdm_profession> mdm_professions = DataContext.mdm_profession.AsNoTracking();
            mdm_professions = DynamicFilter(mdm_professions, filter);
            mdm_professions = DynamicOrder(mdm_professions, filter);
            List<mdm_profession> mdm_professions = await DynamicSelect(mdm_professions, filter);
            return mdm_professions;
        }

        public async Task<mdm_profession> Get(long Id)
        {
            mdm_profession mdm_profession = await DataContext.mdm_profession.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new mdm_profession()
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

            if (mdm_profession == null)
                return null;

            return mdm_profession;
        }
        public async Task<bool> Create(mdm_profession mdm_profession)
        {
            mdm_profession mdm_profession = new mdm_profession();
            mdm_profession.Id = mdm_profession.Id;
            mdm_profession.Code = mdm_profession.Code;
            mdm_profession.Name = mdm_profession.Name;
            mdm_profession.StatusId = mdm_profession.StatusId;
            mdm_profession.Used = mdm_profession.Used;
            mdm_profession.CreatedAt = StaticParams.DateTimeNow;
            mdm_profession.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.mdm_profession.Add(mdm_profession);
            await DataContext.SaveChangesAsync();
            mdm_profession.Id = mdm_profession.Id;
            await SaveReference(mdm_profession);
            return true;
        }

        public async Task<bool> Update(mdm_profession mdm_profession)
        {
            mdm_profession mdm_profession = DataContext.mdm_profession.Where(x => x.Id == mdm_profession.Id).FirstOrDefault();
            if (mdm_profession == null)
                return false;
            mdm_profession.Id = mdm_profession.Id;
            mdm_profession.Code = mdm_profession.Code;
            mdm_profession.Name = mdm_profession.Name;
            mdm_profession.StatusId = mdm_profession.StatusId;
            mdm_profession.Used = mdm_profession.Used;
            mdm_profession.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(mdm_profession);
            return true;
        }

        public async Task<bool> Delete(mdm_profession mdm_profession)
        {
            await DataContext.mdm_profession.Where(x => x.Id == mdm_profession.Id).UpdateFromQueryAsync(x => new mdm_profession { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<mdm_profession> mdm_professions)
        {
            List<mdm_profession> mdm_professions = new List<mdm_profession>();
            foreach (mdm_profession mdm_profession in mdm_professions)
            {
                mdm_profession mdm_profession = new mdm_profession();
                mdm_profession.Id = mdm_profession.Id;
                mdm_profession.Code = mdm_profession.Code;
                mdm_profession.Name = mdm_profession.Name;
                mdm_profession.StatusId = mdm_profession.StatusId;
                mdm_profession.Used = mdm_profession.Used;
                mdm_profession.CreatedAt = StaticParams.DateTimeNow;
                mdm_profession.UpdatedAt = StaticParams.DateTimeNow;
                mdm_professions.Add(mdm_profession);
            }
            await DataContext.BulkMergeAsync(mdm_professions);
            return true;
        }

        public async Task<bool> BulkDelete(List<mdm_profession> mdm_professions)
        {
            List<long> Ids = mdm_professions.Select(x => x.Id).ToList();
            await DataContext.mdm_profession
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new mdm_profession { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(mdm_profession mdm_profession)
        {
        }

    }
}
