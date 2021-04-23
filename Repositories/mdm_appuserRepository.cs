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
    public interface Imdm_appuserRepository
    {
        Task<int>
    Count(mdm_appuserFilter mdm_appuserFilter);
    Task<List<BASE.Entities.mdm_appuser>> List(mdm_appuserFilter mdm_appuserFilter);
        Task<BASE.Entities.mdm_appuser> Get(long Id);
        Task<bool> Create(BASE.Entities.mdm_appuser mdm_appuser);
        Task<bool> Update(BASE.Entities.mdm_appuser mdm_appuser);
        Task<bool> Delete(BASE.Entities.mdm_appuser mdm_appuser);
        Task<bool> BulkMerge(List<BASE.Entities.mdm_appuser> mdm_appusers);
        Task<bool> BulkDelete(List<BASE.Entities.mdm_appuser> mdm_appusers);
                    }
                    public class mdm_appuserRepository : Imdm_appuserRepository
                    {
                    private DataContext DataContext;
                    public mdm_appuserRepository(DataContext DataContext)
                    {
                    this.DataContext = DataContext;
                    }

                    private IQueryable<BASE.Models.mdm_appuser>
                        DynamicFilter(IQueryable<BASE.Models.mdm_appuser>
                            query, mdm_appuserFilter filter)
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
                            if (filter.Username != null)
                            query = query.Where(q => q.Username, filter.Username);
                            if (filter.DisplayName != null)
                            query = query.Where(q => q.DisplayName, filter.DisplayName);
                            if (filter.Address != null)
                            query = query.Where(q => q.Address, filter.Address);
                            if (filter.Email != null)
                            query = query.Where(q => q.Email, filter.Email);
                            if (filter.Phone != null)
                            query = query.Where(q => q.Phone, filter.Phone);
                            if (filter.SexId != null)
                            query = query.Where(q => q.SexId, filter.SexId);
                            if (filter.Birthday != null)
                            query = query.Where(q => q.Birthday == null).Union(query.Where(q => q.Birthday.HasValue).Where(q => q.Birthday, filter.Birthday));
                            if (filter.Avatar != null)
                            query = query.Where(q => q.Avatar, filter.Avatar);
                            if (filter.Department != null)
                            query = query.Where(q => q.Department, filter.Department);
                            if (filter.OrganizationId != null)
                            query = query.Where(q => q.OrganizationId, filter.OrganizationId);
                            if (filter.Longitude != null)
                            query = query.Where(q => q.Longitude.HasValue).Where(q => q.Longitude, filter.Longitude);
                            if (filter.Latitude != null)
                            query = query.Where(q => q.Latitude.HasValue).Where(q => q.Latitude, filter.Latitude);
                            if (filter.StatusId != null)
                            query = query.Where(q => q.StatusId, filter.StatusId);
                            query = OrFilter(query, filter);
                            return query;
                            }

                            private IQueryable<BASE.Models.mdm_appuser>
                                OrFilter(IQueryable<BASE.Models.mdm_appuser>
                                    query, mdm_appuserFilter filter)
                                    {
                                    if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                                    return query;
                                    IQueryable<BASE.Models.mdm_appuser>
                                        initQuery = query.Where(q => false);
                                        foreach (mdm_appuserFilter mdm_appuserFilter in filter.OrFilter)
                                        {
                                        IQueryable<BASE.Models.mdm_appuser>
                                            queryable = query;
                                            if (mdm_appuserFilter.Id != null)
                                            queryable = queryable.Where(q => q.Id, mdm_appuserFilter.Id);
                                            if (mdm_appuserFilter.Username != null)
                                            queryable = queryable.Where(q => q.Username, mdm_appuserFilter.Username);
                                            if (mdm_appuserFilter.DisplayName != null)
                                            queryable = queryable.Where(q => q.DisplayName, mdm_appuserFilter.DisplayName);
                                            if (mdm_appuserFilter.Address != null)
                                            queryable = queryable.Where(q => q.Address, mdm_appuserFilter.Address);
                                            if (mdm_appuserFilter.Email != null)
                                            queryable = queryable.Where(q => q.Email, mdm_appuserFilter.Email);
                                            if (mdm_appuserFilter.Phone != null)
                                            queryable = queryable.Where(q => q.Phone, mdm_appuserFilter.Phone);
                                            if (mdm_appuserFilter.SexId != null)
                                            queryable = queryable.Where(q => q.SexId, mdm_appuserFilter.SexId);
                                            if (mdm_appuserFilter.Birthday != null)
                                            queryable = queryable.Where(q => q.Birthday.HasValue).Where(q => q.Birthday, mdm_appuserFilter.Birthday);
                                            if (mdm_appuserFilter.Avatar != null)
                                            queryable = queryable.Where(q => q.Avatar, mdm_appuserFilter.Avatar);
                                            if (mdm_appuserFilter.Department != null)
                                            queryable = queryable.Where(q => q.Department, mdm_appuserFilter.Department);
                                            if (mdm_appuserFilter.OrganizationId != null)
                                            queryable = queryable.Where(q => q.OrganizationId, mdm_appuserFilter.OrganizationId);
                                            if (mdm_appuserFilter.Longitude != null)
                                            queryable = queryable.Where(q => q.Longitude.HasValue).Where(q => q.Longitude, mdm_appuserFilter.Longitude);
                                            if (mdm_appuserFilter.Latitude != null)
                                            queryable = queryable.Where(q => q.Latitude.HasValue).Where(q => q.Latitude, mdm_appuserFilter.Latitude);
                                            if (mdm_appuserFilter.StatusId != null)
                                            queryable = queryable.Where(q => q.StatusId, mdm_appuserFilter.StatusId);
                                            initQuery = initQuery.Union(queryable);
                                            }
                                            return initQuery;
                                            }

                                            private IQueryable<BASE.Models.mdm_appuser>
                                                DynamicOrder(IQueryable<BASE.Models.mdm_appuser>
                                                    query, mdm_appuserFilter filter)
                                                    {
                                                    switch (filter.OrderType)
                                                    {
                                                    case OrderType.ASC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_appuserOrder.Id:
                                                    query = query.OrderBy(q => q.Id);
                                                    break;
                                                    case mdm_appuserOrder.Username:
                                                    query = query.OrderBy(q => q.Username);
                                                    break;
                                                    case mdm_appuserOrder.DisplayName:
                                                    query = query.OrderBy(q => q.DisplayName);
                                                    break;
                                                    case mdm_appuserOrder.Address:
                                                    query = query.OrderBy(q => q.Address);
                                                    break;
                                                    case mdm_appuserOrder.Email:
                                                    query = query.OrderBy(q => q.Email);
                                                    break;
                                                    case mdm_appuserOrder.Phone:
                                                    query = query.OrderBy(q => q.Phone);
                                                    break;
                                                    case mdm_appuserOrder.Sex:
                                                    query = query.OrderBy(q => q.SexId);
                                                    break;
                                                    case mdm_appuserOrder.Birthday:
                                                    query = query.OrderBy(q => q.Birthday);
                                                    break;
                                                    case mdm_appuserOrder.Avatar:
                                                    query = query.OrderBy(q => q.Avatar);
                                                    break;
                                                    case mdm_appuserOrder.Department:
                                                    query = query.OrderBy(q => q.Department);
                                                    break;
                                                    case mdm_appuserOrder.Organization:
                                                    query = query.OrderBy(q => q.OrganizationId);
                                                    break;
                                                    case mdm_appuserOrder.Longitude:
                                                    query = query.OrderBy(q => q.Longitude);
                                                    break;
                                                    case mdm_appuserOrder.Latitude:
                                                    query = query.OrderBy(q => q.Latitude);
                                                    break;
                                                    case mdm_appuserOrder.Status:
                                                    query = query.OrderBy(q => q.StatusId);
                                                    break;
                                                    case mdm_appuserOrder.Used:
                                                    query = query.OrderBy(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    case OrderType.DESC:
                                                    switch (filter.OrderBy)
                                                    {
                                                    case mdm_appuserOrder.Id:
                                                    query = query.OrderByDescending(q => q.Id);
                                                    break;
                                                    case mdm_appuserOrder.Username:
                                                    query = query.OrderByDescending(q => q.Username);
                                                    break;
                                                    case mdm_appuserOrder.DisplayName:
                                                    query = query.OrderByDescending(q => q.DisplayName);
                                                    break;
                                                    case mdm_appuserOrder.Address:
                                                    query = query.OrderByDescending(q => q.Address);
                                                    break;
                                                    case mdm_appuserOrder.Email:
                                                    query = query.OrderByDescending(q => q.Email);
                                                    break;
                                                    case mdm_appuserOrder.Phone:
                                                    query = query.OrderByDescending(q => q.Phone);
                                                    break;
                                                    case mdm_appuserOrder.Sex:
                                                    query = query.OrderByDescending(q => q.SexId);
                                                    break;
                                                    case mdm_appuserOrder.Birthday:
                                                    query = query.OrderByDescending(q => q.Birthday);
                                                    break;
                                                    case mdm_appuserOrder.Avatar:
                                                    query = query.OrderByDescending(q => q.Avatar);
                                                    break;
                                                    case mdm_appuserOrder.Department:
                                                    query = query.OrderByDescending(q => q.Department);
                                                    break;
                                                    case mdm_appuserOrder.Organization:
                                                    query = query.OrderByDescending(q => q.OrganizationId);
                                                    break;
                                                    case mdm_appuserOrder.Longitude:
                                                    query = query.OrderByDescending(q => q.Longitude);
                                                    break;
                                                    case mdm_appuserOrder.Latitude:
                                                    query = query.OrderByDescending(q => q.Latitude);
                                                    break;
                                                    case mdm_appuserOrder.Status:
                                                    query = query.OrderByDescending(q => q.StatusId);
                                                    break;
                                                    case mdm_appuserOrder.Used:
                                                    query = query.OrderByDescending(q => q.Used);
                                                    break;
                                                    }
                                                    break;
                                                    }
                                                    query = query.Skip(filter.Skip).Take(filter.Take);
                                                    return query;
                                                    }

                                                    private async Task<List<BASE.Models.mdm_appuser>> DynamicSelect(IQueryable<BASE.Models.mdm_appuser> query, mdm_appuserFilter filter)
        {
            List<mdm_appuser> mdm_appusers = await query.Select(q => new mdm_appuser()
            {
                Id = filter.Selects.Contains(mdm_appuserSelect.Id) ? q.Id : default(long),
                Username = filter.Selects.Contains(mdm_appuserSelect.Username) ? q.Username : default(string),
                DisplayName = filter.Selects.Contains(mdm_appuserSelect.DisplayName) ? q.DisplayName : default(string),
                Address = filter.Selects.Contains(mdm_appuserSelect.Address) ? q.Address : default(string),
                Email = filter.Selects.Contains(mdm_appuserSelect.Email) ? q.Email : default(string),
                Phone = filter.Selects.Contains(mdm_appuserSelect.Phone) ? q.Phone : default(string),
                SexId = filter.Selects.Contains(mdm_appuserSelect.Sex) ? q.SexId : default(long),
                Birthday = filter.Selects.Contains(mdm_appuserSelect.Birthday) ? q.Birthday : default(DateTime?),
                Avatar = filter.Selects.Contains(mdm_appuserSelect.Avatar) ? q.Avatar : default(string),
                Department = filter.Selects.Contains(mdm_appuserSelect.Department) ? q.Department : default(string),
                OrganizationId = filter.Selects.Contains(mdm_appuserSelect.Organization) ? q.OrganizationId : default(long),
                Longitude = filter.Selects.Contains(mdm_appuserSelect.Longitude) ? q.Longitude : default(decimal?),
                Latitude = filter.Selects.Contains(mdm_appuserSelect.Latitude) ? q.Latitude : default(decimal?),
                StatusId = filter.Selects.Contains(mdm_appuserSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(mdm_appuserSelect.Used) ? q.Used : default(bool),
                Organization = filter.Selects.Contains(mdm_appuserSelect.Organization) && q.Organization != null ? new mdm_organization
                {
                    Id = q.Organization.Id,
                    Code = q.Organization.Code,
                    Name = q.Organization.Name,
                    ParentId = q.Organization.ParentId,
                    Path = q.Organization.Path,
                    Level = q.Organization.Level,
                    StatusId = q.Organization.StatusId,
                    Phone = q.Organization.Phone,
                    Email = q.Organization.Email,
                    Address = q.Organization.Address,
                    Used = q.Organization.Used,
                } : null,
                Sex = filter.Selects.Contains(mdm_appuserSelect.Sex) && q.Sex != null ? new enum_sex
                {
                    Id = q.Sex.Id,
                    Code = q.Sex.Code,
                    Name = q.Sex.Name,
                } : null,
                Status = filter.Selects.Contains(mdm_appuserSelect.Status) && q.Status != null ? new enum_status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return mdm_appusers;
        }

        public async Task<int> Count(mdm_appuserFilter filter)
        {
            IQueryable<BASE.Models.mdm_appuser> mdm_appusers = DataContext.mdm_appuser.AsNoTracking();
            mdm_appusers = DynamicFilter(mdm_appusers, filter);
            return await mdm_appusers.CountAsync();
        }

        public async Task<List<mdm_appuser>> List(mdm_appuserFilter filter)
        {
            if (filter == null) return new List<mdm_appuser>();
            IQueryable<BASE.Models.mdm_appuser> mdm_appusers = DataContext.mdm_appuser.AsNoTracking();
            mdm_appusers = DynamicFilter(mdm_appusers, filter);
            mdm_appusers = DynamicOrder(mdm_appusers, filter);
            List<mdm_appuser> mdm_appusers = await DynamicSelect(mdm_appusers, filter);
            return mdm_appusers;
        }

        public async Task<mdm_appuser> Get(long Id)
        {
            mdm_appuser mdm_appuser = await DataContext.mdm_appuser.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new mdm_appuser()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Username = x.Username,
                DisplayName = x.DisplayName,
                Address = x.Address,
                Email = x.Email,
                Phone = x.Phone,
                SexId = x.SexId,
                Birthday = x.Birthday,
                Avatar = x.Avatar,
                Department = x.Department,
                OrganizationId = x.OrganizationId,
                Longitude = x.Longitude,
                Latitude = x.Latitude,
                StatusId = x.StatusId,
                Used = x.Used,
                Organization = x.Organization == null ? null : new mdm_organization
                {
                    Id = x.Organization.Id,
                    Code = x.Organization.Code,
                    Name = x.Organization.Name,
                    ParentId = x.Organization.ParentId,
                    Path = x.Organization.Path,
                    Level = x.Organization.Level,
                    StatusId = x.Organization.StatusId,
                    Phone = x.Organization.Phone,
                    Email = x.Organization.Email,
                    Address = x.Organization.Address,
                    Used = x.Organization.Used,
                },
                Sex = x.Sex == null ? null : new enum_sex
                {
                    Id = x.Sex.Id,
                    Code = x.Sex.Code,
                    Name = x.Sex.Name,
                },
                Status = x.Status == null ? null : new enum_status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (mdm_appuser == null)
                return null;

            return mdm_appuser;
        }
        public async Task<bool> Create(mdm_appuser mdm_appuser)
        {
            mdm_appuser mdm_appuser = new mdm_appuser();
            mdm_appuser.Id = mdm_appuser.Id;
            mdm_appuser.Username = mdm_appuser.Username;
            mdm_appuser.DisplayName = mdm_appuser.DisplayName;
            mdm_appuser.Address = mdm_appuser.Address;
            mdm_appuser.Email = mdm_appuser.Email;
            mdm_appuser.Phone = mdm_appuser.Phone;
            mdm_appuser.SexId = mdm_appuser.SexId;
            mdm_appuser.Birthday = mdm_appuser.Birthday;
            mdm_appuser.Avatar = mdm_appuser.Avatar;
            mdm_appuser.Department = mdm_appuser.Department;
            mdm_appuser.OrganizationId = mdm_appuser.OrganizationId;
            mdm_appuser.Longitude = mdm_appuser.Longitude;
            mdm_appuser.Latitude = mdm_appuser.Latitude;
            mdm_appuser.StatusId = mdm_appuser.StatusId;
            mdm_appuser.Used = mdm_appuser.Used;
            mdm_appuser.CreatedAt = StaticParams.DateTimeNow;
            mdm_appuser.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.mdm_appuser.Add(mdm_appuser);
            await DataContext.SaveChangesAsync();
            mdm_appuser.Id = mdm_appuser.Id;
            await SaveReference(mdm_appuser);
            return true;
        }

        public async Task<bool> Update(mdm_appuser mdm_appuser)
        {
            mdm_appuser mdm_appuser = DataContext.mdm_appuser.Where(x => x.Id == mdm_appuser.Id).FirstOrDefault();
            if (mdm_appuser == null)
                return false;
            mdm_appuser.Id = mdm_appuser.Id;
            mdm_appuser.Username = mdm_appuser.Username;
            mdm_appuser.DisplayName = mdm_appuser.DisplayName;
            mdm_appuser.Address = mdm_appuser.Address;
            mdm_appuser.Email = mdm_appuser.Email;
            mdm_appuser.Phone = mdm_appuser.Phone;
            mdm_appuser.SexId = mdm_appuser.SexId;
            mdm_appuser.Birthday = mdm_appuser.Birthday;
            mdm_appuser.Avatar = mdm_appuser.Avatar;
            mdm_appuser.Department = mdm_appuser.Department;
            mdm_appuser.OrganizationId = mdm_appuser.OrganizationId;
            mdm_appuser.Longitude = mdm_appuser.Longitude;
            mdm_appuser.Latitude = mdm_appuser.Latitude;
            mdm_appuser.StatusId = mdm_appuser.StatusId;
            mdm_appuser.Used = mdm_appuser.Used;
            mdm_appuser.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(mdm_appuser);
            return true;
        }

        public async Task<bool> Delete(mdm_appuser mdm_appuser)
        {
            await DataContext.mdm_appuser.Where(x => x.Id == mdm_appuser.Id).UpdateFromQueryAsync(x => new mdm_appuser { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<mdm_appuser> mdm_appusers)
        {
            List<mdm_appuser> mdm_appusers = new List<mdm_appuser>();
            foreach (mdm_appuser mdm_appuser in mdm_appusers)
            {
                mdm_appuser mdm_appuser = new mdm_appuser();
                mdm_appuser.Id = mdm_appuser.Id;
                mdm_appuser.Username = mdm_appuser.Username;
                mdm_appuser.DisplayName = mdm_appuser.DisplayName;
                mdm_appuser.Address = mdm_appuser.Address;
                mdm_appuser.Email = mdm_appuser.Email;
                mdm_appuser.Phone = mdm_appuser.Phone;
                mdm_appuser.SexId = mdm_appuser.SexId;
                mdm_appuser.Birthday = mdm_appuser.Birthday;
                mdm_appuser.Avatar = mdm_appuser.Avatar;
                mdm_appuser.Department = mdm_appuser.Department;
                mdm_appuser.OrganizationId = mdm_appuser.OrganizationId;
                mdm_appuser.Longitude = mdm_appuser.Longitude;
                mdm_appuser.Latitude = mdm_appuser.Latitude;
                mdm_appuser.StatusId = mdm_appuser.StatusId;
                mdm_appuser.Used = mdm_appuser.Used;
                mdm_appuser.CreatedAt = StaticParams.DateTimeNow;
                mdm_appuser.UpdatedAt = StaticParams.DateTimeNow;
                mdm_appusers.Add(mdm_appuser);
            }
            await DataContext.BulkMergeAsync(mdm_appusers);
            return true;
        }

        public async Task<bool> BulkDelete(List<mdm_appuser> mdm_appusers)
        {
            List<long> Ids = mdm_appusers.Select(x => x.Id).ToList();
            await DataContext.mdm_appuser
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new mdm_appuser { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(mdm_appuser mdm_appuser)
        {
        }

    }
}
