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
    public interface Idbo_mailtemplateRepository
    {
        Task<int>
    Count(dbo_mailtemplateFilter dbo_mailtemplateFilter);
    Task<List<BASE.Entities.dbo_mailtemplate>> List(dbo_mailtemplateFilter dbo_mailtemplateFilter);
        Task<BASE.Entities.dbo_mailtemplate> Get(long Id);
        Task<bool> Create(BASE.Entities.dbo_mailtemplate dbo_mailtemplate);
        Task<bool> Update(BASE.Entities.dbo_mailtemplate dbo_mailtemplate);
        Task<bool> Delete(BASE.Entities.dbo_mailtemplate dbo_mailtemplate);
        Task<bool> BulkMerge(List<BASE.Entities.dbo_mailtemplate> dbo_mailtemplates);
        Task<bool> BulkDelete(List<BASE.Entities.dbo_mailtemplate> dbo_mailtemplates);
                    }
                    public class dbo_mailtemplateRepository : Idbo_mailtemplateRepository
                    {
                    private DataContext DataContext;
                    public dbo_mailtemplateRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.dbo_mailtemplate>
                        DynamicFilter(IQueryable<BASE.Models.dbo_mailtemplate>
                            query, dbo_mailtemplateFilter filter)
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
                            if (filter.Content != null)
                            query = query.Where(q => q.Content, filter.Content);
                            if (filter.StatusId != null)
                            query = query.Where(q => q.StatusId.HasValue).Where(q => q.StatusId, filter.StatusId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.dbo_mailtemplate>
                                OrFilter(IQueryable<BASE.Models.dbo_mailtemplate>
                                    query, dbo_mailtemplateFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.dbo_mailtemplate>
                                        initQuery = query.Where(q => false);
                                        foreach (dbo_mailtemplateFilter dbo_mailtemplateFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.dbo_mailtemplate>
                                            queryable = query;
                                            if (dbo_mailtemplateFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, dbo_mailtemplateFilter.Id);
                                            if (dbo_mailtemplateFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, dbo_mailtemplateFilter.Code);
                                            if (dbo_mailtemplateFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, dbo_mailtemplateFilter.Name);
                                            if (dbo_mailtemplateFilter.Content != null)
                                            queryable = queryable.Where(q => q.Content, dbo_mailtemplateFilter.Content);
                                            if (dbo_mailtemplateFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId.HasValue).Where(q => q.StatusId, dbo_mailtemplateFilter.StatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.dbo_mailtemplate>
                                                DynamicOrder(IQueryable<BASE.Models.dbo_mailtemplate>
                                                    query, dbo_mailtemplateFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case dbo_mailtemplateOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case dbo_mailtemplateOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case dbo_mailtemplateOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case dbo_mailtemplateOrder.Content:
                                                    query = query.OrderBy(q => q.Content);
                                                    break;
                                                    case dbo_mailtemplateOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case dbo_mailtemplateOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case dbo_mailtemplateOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case dbo_mailtemplateOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case dbo_mailtemplateOrder.Content:
                                                    query = query.OrderByDescending(q => q.Content);
                                                    break;
                                                    case dbo_mailtemplateOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.dbo_mailtemplate>> DynamicSelect(IQueryable<BASE.Models.dbo_mailtemplate> query, dbo_mailtemplateFilter filter)
        {
            List<dbo_mailtemplate> dbo_mailtemplates = await query.Select(q => new dbo_mailtemplate()
            {
                Id = filter.Selects.Contains(dbo_mailtemplateSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(dbo_mailtemplateSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(dbo_mailtemplateSelect.Name) ? q.Name : default(string),
                Content = filter.Selects.Contains(dbo_mailtemplateSelect.Content) ? q.Content : default(string),
                StatusId = filter.Selects.Contains(dbo_mailtemplateSelect.Status) ? q.StatusId : default(long?),
                Status = filter.Selects.Contains(dbo_mailtemplateSelect.Status) && q.Status != null ? new enum_status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return dbo_mailtemplates;
        }

        public async Task<int> Count(dbo_mailtemplateFilter filter)
        {
            IQueryable<BASE.Models.dbo_mailtemplate> dbo_mailtemplates = DataContext.dbo_mailtemplate.AsNoTracking();
            dbo_mailtemplates = DynamicFilter(dbo_mailtemplates, filter);
            return await dbo_mailtemplates.CountAsync();
        }

        public async Task<List<dbo_mailtemplate>> List(dbo_mailtemplateFilter filter)
        {
            if (filter == null) return new List<dbo_mailtemplate>();
            IQueryable<BASE.Models.dbo_mailtemplate> dbo_mailtemplates = DataContext.dbo_mailtemplate.AsNoTracking();
            dbo_mailtemplates = DynamicFilter(dbo_mailtemplates, filter);
            dbo_mailtemplates = DynamicOrder(dbo_mailtemplates, filter);
            List<dbo_mailtemplate> dbo_mailtemplates = await DynamicSelect(dbo_mailtemplates, filter);
            return dbo_mailtemplates;
        }

        public async Task<dbo_mailtemplate> Get(long Id)
        {
            dbo_mailtemplate dbo_mailtemplate = await DataContext.dbo_mailtemplate.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new dbo_mailtemplate()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Content = x.Content,
                StatusId = x.StatusId,
                Status = x.Status == null ? null : new enum_status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (dbo_mailtemplate == null)
                return null;

            return dbo_mailtemplate;
        }
        public async Task<bool> Create(dbo_mailtemplate dbo_mailtemplate)
        {
            dbo_mailtemplate dbo_mailtemplate = new dbo_mailtemplate();
            dbo_mailtemplate.Id = dbo_mailtemplate.Id;
            dbo_mailtemplate.Code = dbo_mailtemplate.Code;
            dbo_mailtemplate.Name = dbo_mailtemplate.Name;
            dbo_mailtemplate.Content = dbo_mailtemplate.Content;
            dbo_mailtemplate.StatusId = dbo_mailtemplate.StatusId;
            dbo_mailtemplate.CreatedAt = StaticParams.DateTimeNow;
            dbo_mailtemplate.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.dbo_mailtemplate.Add(dbo_mailtemplate);
            await DataContext.SaveChangesAsync();
            dbo_mailtemplate.Id = dbo_mailtemplate.Id;
            await SaveReference(dbo_mailtemplate);
            return true;
        }

        public async Task<bool> Update(dbo_mailtemplate dbo_mailtemplate)
        {
            dbo_mailtemplate dbo_mailtemplate = DataContext.dbo_mailtemplate.Where(x => x.Id == dbo_mailtemplate.Id).FirstOrDefault();
            if (dbo_mailtemplate == null)
                return false;
            dbo_mailtemplate.Id = dbo_mailtemplate.Id;
            dbo_mailtemplate.Code = dbo_mailtemplate.Code;
            dbo_mailtemplate.Name = dbo_mailtemplate.Name;
            dbo_mailtemplate.Content = dbo_mailtemplate.Content;
            dbo_mailtemplate.StatusId = dbo_mailtemplate.StatusId;
            dbo_mailtemplate.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(dbo_mailtemplate);
            return true;
        }

        public async Task<bool> Delete(dbo_mailtemplate dbo_mailtemplate)
        {
            await DataContext.dbo_mailtemplate.Where(x => x.Id == dbo_mailtemplate.Id).UpdateFromQueryAsync(x => new dbo_mailtemplate { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<dbo_mailtemplate> dbo_mailtemplates)
        {
            List<dbo_mailtemplate> dbo_mailtemplates = new List<dbo_mailtemplate>();
            foreach (dbo_mailtemplate dbo_mailtemplate in dbo_mailtemplates)
            {
                dbo_mailtemplate dbo_mailtemplate = new dbo_mailtemplate();
                dbo_mailtemplate.Id = dbo_mailtemplate.Id;
                dbo_mailtemplate.Code = dbo_mailtemplate.Code;
                dbo_mailtemplate.Name = dbo_mailtemplate.Name;
                dbo_mailtemplate.Content = dbo_mailtemplate.Content;
                dbo_mailtemplate.StatusId = dbo_mailtemplate.StatusId;
                dbo_mailtemplate.CreatedAt = StaticParams.DateTimeNow;
                dbo_mailtemplate.UpdatedAt = StaticParams.DateTimeNow;
                dbo_mailtemplates.Add(dbo_mailtemplate);
            }
            await DataContext.BulkMergeAsync(dbo_mailtemplates);
            return true;
        }

        public async Task<bool> BulkDelete(List<dbo_mailtemplate> dbo_mailtemplates)
        {
            List<long> Ids = dbo_mailtemplates.Select(x => x.Id).ToList();
            await DataContext.dbo_mailtemplate
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new dbo_mailtemplate { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(dbo_mailtemplate dbo_mailtemplate)
        {
        }

    }
}
