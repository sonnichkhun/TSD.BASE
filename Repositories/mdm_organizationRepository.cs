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
    public interface Imdm_organizationRepository
    {
        Task<int>
    Count(mdm_organizationFilter mdm_organizationFilter);
    Task<List<BASE.Entities.mdm_organization>> List(mdm_organizationFilter mdm_organizationFilter);
        Task<BASE.Entities.mdm_organization> Get(long Id);
        Task<bool> Create(BASE.Entities.mdm_organization mdm_organization);
        Task<bool> Update(BASE.Entities.mdm_organization mdm_organization);
        Task<bool> Delete(BASE.Entities.mdm_organization mdm_organization);
        Task<bool> BulkMerge(List<BASE.Entities.mdm_organization> mdm_organizations);
        Task<bool> BulkDelete(List<BASE.Entities.mdm_organization> mdm_organizations);
                    }
                    public class mdm_organizationRepository : Imdm_organizationRepository
                    {
                    private DataContext DataContext;
                    public mdm_organizationRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.mdm_organization>
                        DynamicFilter(IQueryable<BASE.Models.mdm_organization>
                            query, mdm_organizationFilter filter)
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
                            if (filter.ParentId != null)
                            query = query.Where(q => q.ParentId.HasValue).Where(q => q.ParentId, filter.ParentId);
                            if (filter.Path != null)
                            query = query.Where(q => q.Path, filter.Path);
                            if (filter.Level != null)
                            query = query.Where(q => q.Level, filter.Level);
                            if (filter.StatusId != null)
                            query = query.Where(q => q.StatusId, filter.StatusId);
                            if (filter.Phone != null)
                            query = query.Where(q => q.Phone, filter.Phone);
                            if (filter.Email != null)
                            query = query.Where(q => q.Email, filter.Email);
                            if (filter.Address != null)
                            query = query.Where(q => q.Address, filter.Address);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.mdm_organization>
                                OrFilter(IQueryable<BASE.Models.mdm_organization>
                                    query, mdm_organizationFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.mdm_organization>
                                        initQuery = query.Where(q => false);
                                        foreach (mdm_organizationFilter mdm_organizationFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.mdm_organization>
                                            queryable = query;
                                            if (mdm_organizationFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, mdm_organizationFilter.Id);
                                            if (mdm_organizationFilter.Code != null)
                                            queryable = queryable.Where(q => q.Code, mdm_organizationFilter.Code);
                                            if (mdm_organizationFilter.Name != null)
                                            queryable = queryable.Where(q => q.Name, mdm_organizationFilter.Name);
                                            if (mdm_organizationFilter.ParentId != null)
                                            queryable = queryable.Where(q => q.ParentId.HasValue).Where(q => q.ParentId, mdm_organizationFilter.ParentId);
                                            if (mdm_organizationFilter.Path != null)
                                            queryable = queryable.Where(q => q.Path, mdm_organizationFilter.Path);
                                            if (mdm_organizationFilter.Level != null)
                                            queryable = queryable.Where(q => q.Level, mdm_organizationFilter.Level);
                                            if (mdm_organizationFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId, mdm_organizationFilter.StatusId);
                                            if (mdm_organizationFilter.Phone != null)
                                            queryable = queryable.Where(q => q.Phone, mdm_organizationFilter.Phone);
                                            if (mdm_organizationFilter.Email != null)
                                            queryable = queryable.Where(q => q.Email, mdm_organizationFilter.Email);
                                            if (mdm_organizationFilter.Address != null)
                                            queryable = queryable.Where(q => q.Address, mdm_organizationFilter.Address);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.mdm_organization>
                                                DynamicOrder(IQueryable<BASE.Models.mdm_organization>
                                                    query, mdm_organizationFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_organizationOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case mdm_organizationOrder.Code:
                                                    query = query.OrderBy(q => q.Code);
                                                    break;
                                                    case mdm_organizationOrder.Name:
                                                    query = query.OrderBy(q => q.Name);
                                                    break;
                                                    case mdm_organizationOrder.Parent:
                                                    query = query.OrderBy(q => q.ParentId);
                                                    break;
                                                    case mdm_organizationOrder.Path:
                                                    query = query.OrderBy(q => q.Path);
                                                    break;
                                                    case mdm_organizationOrder.Level:
                                                    query = query.OrderBy(q => q.Level);
                                                    break;
                                                    case mdm_organizationOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    case mdm_organizationOrder.Phone:
                                                    query = query.OrderBy(q => q.Phone);
                                                    break;
                                                    case mdm_organizationOrder.Email:
                                                    query = query.OrderBy(q => q.Email);
                                                    break;
                                                    case mdm_organizationOrder.Address:
                                                    query = query.OrderBy(q => q.Address);
                                                    break;
                                                    case mdm_organizationOrder.Used:
                                                    query = query.OrderBy(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_organizationOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case mdm_organizationOrder.Code:
                                                    query = query.OrderByDescending(q => q.Code);
                                                    break;
                                                    case mdm_organizationOrder.Name:
                                                    query = query.OrderByDescending(q => q.Name);
                                                    break;
                                                    case mdm_organizationOrder.Parent:
                                                    query = query.OrderByDescending(q => q.ParentId);
                                                    break;
                                                    case mdm_organizationOrder.Path:
                                                    query = query.OrderByDescending(q => q.Path);
                                                    break;
                                                    case mdm_organizationOrder.Level:
                                                    query = query.OrderByDescending(q => q.Level);
                                                    break;
                                                    case mdm_organizationOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    case mdm_organizationOrder.Phone:
                                                    query = query.OrderByDescending(q => q.Phone);
                                                    break;
                                                    case mdm_organizationOrder.Email:
                                                    query = query.OrderByDescending(q => q.Email);
                                                    break;
                                                    case mdm_organizationOrder.Address:
                                                    query = query.OrderByDescending(q => q.Address);
                                                    break;
                                                    case mdm_organizationOrder.Used:
                                                    query = query.OrderByDescending(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.mdm_organization>> DynamicSelect(IQueryable<BASE.Models.mdm_organization> query, mdm_organizationFilter filter)
        {
            List<mdm_organization> mdm_organizations = await query.Select(q => new mdm_organization()
            {
                Id = filter.Selects.Contains(mdm_organizationSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(mdm_organizationSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(mdm_organizationSelect.Name) ? q.Name : default(string),
                ParentId = filter.Selects.Contains(mdm_organizationSelect.Parent) ? q.ParentId : default(long?),
                Path = filter.Selects.Contains(mdm_organizationSelect.Path) ? q.Path : default(string),
                Level = filter.Selects.Contains(mdm_organizationSelect.Level) ? q.Level : default(long),
                StatusId = filter.Selects.Contains(mdm_organizationSelect.Status) ? q.StatusId : default(long),
                Phone = filter.Selects.Contains(mdm_organizationSelect.Phone) ? q.Phone : default(string),
                Email = filter.Selects.Contains(mdm_organizationSelect.Email) ? q.Email : default(string),
                Address = filter.Selects.Contains(mdm_organizationSelect.Address) ? q.Address : default(string),
                Used = filter.Selects.Contains(mdm_organizationSelect.Used) ? q.Used : default(bool),
                Parent = filter.Selects.Contains(mdm_organizationSelect.Parent) && q.Parent != null ? new mdm_organization
                {
                    Id = q.Parent.Id,
                    Code = q.Parent.Code,
                    Name = q.Parent.Name,
                    ParentId = q.Parent.ParentId,
                    Path = q.Parent.Path,
                    Level = q.Parent.Level,
                    StatusId = q.Parent.StatusId,
                    Phone = q.Parent.Phone,
                    Email = q.Parent.Email,
                    Address = q.Parent.Address,
                    Used = q.Parent.Used,
                } : null,
                Status = filter.Selects.Contains(mdm_organizationSelect.Status) && q.Status != null ? new enum_status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return mdm_organizations;
        }

        public async Task<int> Count(mdm_organizationFilter filter)
        {
            IQueryable<BASE.Models.mdm_organization> mdm_organizations = DataContext.mdm_organization.AsNoTracking();
            mdm_organizations = DynamicFilter(mdm_organizations, filter);
            return await mdm_organizations.CountAsync();
        }

        public async Task<List<mdm_organization>> List(mdm_organizationFilter filter)
        {
            if (filter == null) return new List<mdm_organization>();
            IQueryable<BASE.Models.mdm_organization> mdm_organizations = DataContext.mdm_organization.AsNoTracking();
            mdm_organizations = DynamicFilter(mdm_organizations, filter);
            mdm_organizations = DynamicOrder(mdm_organizations, filter);
            List<mdm_organization> mdm_organizations = await DynamicSelect(mdm_organizations, filter);
            return mdm_organizations;
        }

        public async Task<mdm_organization> Get(long Id)
        {
            mdm_organization mdm_organization = await DataContext.mdm_organization.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new mdm_organization()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                ParentId = x.ParentId,
                Path = x.Path,
                Level = x.Level,
                StatusId = x.StatusId,
                Phone = x.Phone,
                Email = x.Email,
                Address = x.Address,
                Used = x.Used,
                Parent = x.Parent == null ? null : new mdm_organization
                {
                    Id = x.Parent.Id,
                    Code = x.Parent.Code,
                    Name = x.Parent.Name,
                    ParentId = x.Parent.ParentId,
                    Path = x.Parent.Path,
                    Level = x.Parent.Level,
                    StatusId = x.Parent.StatusId,
                    Phone = x.Parent.Phone,
                    Email = x.Parent.Email,
                    Address = x.Parent.Address,
                    Used = x.Parent.Used,
                },
                Status = x.Status == null ? null : new enum_status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (mdm_organization == null)
                return null;

            return mdm_organization;
        }
        public async Task<bool> Create(mdm_organization mdm_organization)
        {
            mdm_organization mdm_organization = new mdm_organization();
            mdm_organization.Id = mdm_organization.Id;
            mdm_organization.Code = mdm_organization.Code;
            mdm_organization.Name = mdm_organization.Name;
            mdm_organization.ParentId = mdm_organization.ParentId;
            mdm_organization.Path = mdm_organization.Path;
            mdm_organization.Level = mdm_organization.Level;
            mdm_organization.StatusId = mdm_organization.StatusId;
            mdm_organization.Phone = mdm_organization.Phone;
            mdm_organization.Email = mdm_organization.Email;
            mdm_organization.Address = mdm_organization.Address;
            mdm_organization.Used = mdm_organization.Used;
            mdm_organization.Path = "";
            mdm_organization.Level = 1;
            mdm_organization.CreatedAt = StaticParams.DateTimeNow;
            mdm_organization.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.mdm_organization.Add(mdm_organization);
            await DataContext.SaveChangesAsync();
            mdm_organization.Id = mdm_organization.Id;
            await SaveReference(mdm_organization);
            await BuildPath();
            return true;
        }

        public async Task<bool> Update(mdm_organization mdm_organization)
        {
            mdm_organization mdm_organization = DataContext.mdm_organization.Where(x => x.Id == mdm_organization.Id).FirstOrDefault();
            if (mdm_organization == null)
                return false;
            mdm_organization.Id = mdm_organization.Id;
            mdm_organization.Code = mdm_organization.Code;
            mdm_organization.Name = mdm_organization.Name;
            mdm_organization.ParentId = mdm_organization.ParentId;
            mdm_organization.Path = mdm_organization.Path;
            mdm_organization.Level = mdm_organization.Level;
            mdm_organization.StatusId = mdm_organization.StatusId;
            mdm_organization.Phone = mdm_organization.Phone;
            mdm_organization.Email = mdm_organization.Email;
            mdm_organization.Address = mdm_organization.Address;
            mdm_organization.Used = mdm_organization.Used;
            mdm_organization.Path = "";
            mdm_organization.Level = 1;
            mdm_organization.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(mdm_organization);
            await BuildPath();
            return true;
        }

        public async Task<bool> Delete(mdm_organization mdm_organization)
        {
            mdm_organization mdm_organization = await DataContext.mdm_organization.Where(x => x.Id == mdm_organization.Id).FirstOrDefaultAsync();
            await DataContext.mdm_organization.Where(x => x.Path.StartsWith(mdm_organization.Id + ".")).UpdateFromQueryAsync(x => new mdm_organization { DeletedAt = StaticParams.DateTimeNow });
            await DataContext.mdm_organization.Where(x => x.Id == mdm_organization.Id).UpdateFromQueryAsync(x => new mdm_organization { DeletedAt = StaticParams.DateTimeNow });
            await BuildPath();
            return true;
        }

        public async Task<bool> BulkMerge(List<mdm_organization> mdm_organizations)
        {
            List<mdm_organization> mdm_organizations = new List<mdm_organization>();
            foreach (mdm_organization mdm_organization in mdm_organizations)
            {
                mdm_organization mdm_organization = new mdm_organization();
                mdm_organization.Id = mdm_organization.Id;
                mdm_organization.Code = mdm_organization.Code;
                mdm_organization.Name = mdm_organization.Name;
                mdm_organization.ParentId = mdm_organization.ParentId;
                mdm_organization.Path = mdm_organization.Path;
                mdm_organization.Level = mdm_organization.Level;
                mdm_organization.StatusId = mdm_organization.StatusId;
                mdm_organization.Phone = mdm_organization.Phone;
                mdm_organization.Email = mdm_organization.Email;
                mdm_organization.Address = mdm_organization.Address;
                mdm_organization.Used = mdm_organization.Used;
                mdm_organization.CreatedAt = StaticParams.DateTimeNow;
                mdm_organization.UpdatedAt = StaticParams.DateTimeNow;
                mdm_organizations.Add(mdm_organization);
            }
            await DataContext.BulkMergeAsync(mdm_organizations);
            await BuildPath();
            return true;
        }

        public async Task<bool> BulkDelete(List<mdm_organization> mdm_organizations)
        {
            List<long> Ids = mdm_organizations.Select(x => x.Id).ToList();
            await DataContext.mdm_organization
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new mdm_organization { DeletedAt = StaticParams.DateTimeNow });
            await BuildPath();
            return true;
        }

        private async Task SaveReference(mdm_organization mdm_organization)
        {
        }

        private async Task BuildPath()
        {
            List<mdm_organization> mdm_organizations = await DataContext.mdm_organization
                .Where(x => x.DeletedAt == null)
                .AsNoTracking().ToListAsync();
            Queue<mdm_organization> queue = new Queue<mdm_organization>();
            mdm_organizations.ForEach(x =>
            {
                if (!x.ParentId.HasValue)
                {
                    x.Path = x.Id + ".";
                    x.Level = 1;
                    queue.Enqueue(x);
                }
            });
            while(queue.Count > 0)
            {
                mdm_organization Parent = queue.Dequeue();
                foreach (mdm_organization mdm_organization in mdm_organizations)
                {
                    if (mdm_organization.ParentId == Parent.Id)
                    {
                        mdm_organization.Path = Parent.Path + mdm_organization.Id + ".";
                        mdm_organization.Level = Parent.Level + 1;
                        queue.Enqueue(mdm_organization);
                    }
                }
            }
            await DataContext.BulkMergeAsync(mdm_organizations);
        }
    }
}
