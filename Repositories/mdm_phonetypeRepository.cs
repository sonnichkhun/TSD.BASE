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
    public interface Imdm_phonetypeRepository
    {
        Task<int>
    Count(mdm_phonetypeFilter mdm_phonetypeFilter);
    Task<List<BASE.Entities.mdm_phonetype>> List(mdm_phonetypeFilter mdm_phonetypeFilter);
        Task<BASE.Entities.mdm_phonetype> Get(long Id);
        Task<bool> Create(BASE.Entities.mdm_phonetype mdm_phonetype);
        Task<bool> Update(BASE.Entities.mdm_phonetype mdm_phonetype);
        Task<bool> Delete(BASE.Entities.mdm_phonetype mdm_phonetype);
        Task<bool> BulkMerge(List<BASE.Entities.mdm_phonetype> mdm_phonetypes);
        Task<bool> BulkDelete(List<BASE.Entities.mdm_phonetype> mdm_phonetypes);
                    }
                    public class mdm_phonetypeRepository : Imdm_phonetypeRepository
                    {
                    private DataContext DataContext;
                    public mdm_phonetypeRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.mdm_phonetype>
                        DynamicFilter(IQueryable<BASE.Models.mdm_phonetype>
                            query, mdm_phonetypeFilter filter)
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

                            private IQueryable<BASE.Models.mdm_phonetype>
                                OrFilter(IQueryable<BASE.Models.mdm_phonetype>
                                    query, mdm_phonetypeFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.mdm_phonetype>
                                        initQuery = query.Where(q => false);
                                        foreach (mdm_phonetypeFilter mdm_phonetypeFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.mdm_phonetype>
                                            queryable = query;
                                            if (mdm_phonetypeFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, mdm_phonetypeFilter.Id);
                                            if (mdm_phonetypeFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, mdm_phonetypeFilter.Code);
                                            if (mdm_phonetypeFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, mdm_phonetypeFilter.Name);
                                            if (mdm_phonetypeFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId, mdm_phonetypeFilter.StatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.mdm_phonetype>
                                                DynamicOrder(IQueryable<BASE.Models.mdm_phonetype>
                                                    query, mdm_phonetypeFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_phonetypeOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case mdm_phonetypeOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case mdm_phonetypeOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case mdm_phonetypeOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    case mdm_phonetypeOrder.Used:
                                                    query = query.OrderBy(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_phonetypeOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case mdm_phonetypeOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case mdm_phonetypeOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case mdm_phonetypeOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    case mdm_phonetypeOrder.Used:
                                                    query = query.OrderByDescending(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.mdm_phonetype>> DynamicSelect(IQueryable<BASE.Models.mdm_phonetype> query, mdm_phonetypeFilter filter)
        {
            List<mdm_phonetype> mdm_phonetypes = await query.Select(q => new mdm_phonetype()
            {
                Id = filter.Selects.Contains(mdm_phonetypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(mdm_phonetypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(mdm_phonetypeSelect.Name) ? q.Name : default(string),
                StatusId = filter.Selects.Contains(mdm_phonetypeSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(mdm_phonetypeSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(mdm_phonetypeSelect.Status) && q.Status != null ? new enum_status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return mdm_phonetypes;
        }

        public async Task<int> Count(mdm_phonetypeFilter filter)
        {
            IQueryable<BASE.Models.mdm_phonetype> mdm_phonetypes = DataContext.mdm_phonetype.AsNoTracking();
            mdm_phonetypes = DynamicFilter(mdm_phonetypes, filter);
            return await mdm_phonetypes.CountAsync();
        }

        public async Task<List<mdm_phonetype>> List(mdm_phonetypeFilter filter)
        {
            if (filter == null) return new List<mdm_phonetype>();
            IQueryable<BASE.Models.mdm_phonetype> mdm_phonetypes = DataContext.mdm_phonetype.AsNoTracking();
            mdm_phonetypes = DynamicFilter(mdm_phonetypes, filter);
            mdm_phonetypes = DynamicOrder(mdm_phonetypes, filter);
            List<mdm_phonetype> mdm_phonetypes = await DynamicSelect(mdm_phonetypes, filter);
            return mdm_phonetypes;
        }

        public async Task<mdm_phonetype> Get(long Id)
        {
            mdm_phonetype mdm_phonetype = await DataContext.mdm_phonetype.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new mdm_phonetype()
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

            if (mdm_phonetype == null)
                return null;

            return mdm_phonetype;
        }
        public async Task<bool> Create(mdm_phonetype mdm_phonetype)
        {
            mdm_phonetype mdm_phonetype = new mdm_phonetype();
            mdm_phonetype.Id = mdm_phonetype.Id;
            mdm_phonetype.Code = mdm_phonetype.Code;
            mdm_phonetype.Name = mdm_phonetype.Name;
            mdm_phonetype.StatusId = mdm_phonetype.StatusId;
            mdm_phonetype.Used = mdm_phonetype.Used;
            mdm_phonetype.CreatedAt = StaticParams.DateTimeNow;
            mdm_phonetype.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.mdm_phonetype.Add(mdm_phonetype);
            await DataContext.SaveChangesAsync();
            mdm_phonetype.Id = mdm_phonetype.Id;
            await SaveReference(mdm_phonetype);
            return true;
        }

        public async Task<bool> Update(mdm_phonetype mdm_phonetype)
        {
            mdm_phonetype mdm_phonetype = DataContext.mdm_phonetype.Where(x => x.Id == mdm_phonetype.Id).FirstOrDefault();
            if (mdm_phonetype == null)
                return false;
            mdm_phonetype.Id = mdm_phonetype.Id;
            mdm_phonetype.Code = mdm_phonetype.Code;
            mdm_phonetype.Name = mdm_phonetype.Name;
            mdm_phonetype.StatusId = mdm_phonetype.StatusId;
            mdm_phonetype.Used = mdm_phonetype.Used;
            mdm_phonetype.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(mdm_phonetype);
            return true;
        }

        public async Task<bool> Delete(mdm_phonetype mdm_phonetype)
        {
            await DataContext.mdm_phonetype.Where(x => x.Id == mdm_phonetype.Id).UpdateFromQueryAsync(x => new mdm_phonetype { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<mdm_phonetype> mdm_phonetypes)
        {
            List<mdm_phonetype> mdm_phonetypes = new List<mdm_phonetype>();
            foreach (mdm_phonetype mdm_phonetype in mdm_phonetypes)
            {
                mdm_phonetype mdm_phonetype = new mdm_phonetype();
                mdm_phonetype.Id = mdm_phonetype.Id;
                mdm_phonetype.Code = mdm_phonetype.Code;
                mdm_phonetype.Name = mdm_phonetype.Name;
                mdm_phonetype.StatusId = mdm_phonetype.StatusId;
                mdm_phonetype.Used = mdm_phonetype.Used;
                mdm_phonetype.CreatedAt = StaticParams.DateTimeNow;
                mdm_phonetype.UpdatedAt = StaticParams.DateTimeNow;
                mdm_phonetypes.Add(mdm_phonetype);
            }
            await DataContext.BulkMergeAsync(mdm_phonetypes);
            return true;
        }

        public async Task<bool> BulkDelete(List<mdm_phonetype> mdm_phonetypes)
        {
            List<long> Ids = mdm_phonetypes.Select(x => x.Id).ToList();
            await DataContext.mdm_phonetype
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new mdm_phonetype { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(mdm_phonetype mdm_phonetype)
        {
        }

    }
}
