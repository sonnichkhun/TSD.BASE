using BASE.Common;
using BASE.Models;
using BASE.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Runtime.CompilerServices;
using BASE.Repositories;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.ComponentModel;
using BASE.Enums;
using System.Collections.Generic;

namespace BASE.Helpers
{
    public interface ILogProperty : IServiceScoped
    {
        Task<List<AuditLogPropertyDAO>> CreateAuditLog(DataContext DataContext);
    }
    public class LogProperty : ILogProperty
    {
        public LogProperty()
        {
        }
        public async Task<List<AuditLogPropertyDAO>> CreateAuditLog(DataContext DataContext)
        {
            List<AuditLogPropertyDAO> AuditLogPropertyDAOs = new List<AuditLogPropertyDAO>();
            var modifiedEntities = DataContext.ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified
                || p.State == EntityState.Added
                || p.State == EntityState.Deleted
                || p.State == EntityState.Modified
                || p.State == EntityState.Detached)
                .ToList();


            foreach (var change in modifiedEntities)
            {
                var type = change.GetType();
                var entityName = type.Name;

                var EntityDisplayName = type.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .Select(x => ((DisplayNameAttribute)x).DisplayName)
                .DefaultIfEmpty(type.Name)
                .First();

                if (change.State == EntityState.Added)
                {
                    // Log Added
                }
                else if (change.State == EntityState.Modified)
                {
                    foreach (var prop in change.Entity.GetType().GetTypeInfo().DeclaredProperties)
                    {
                        if (!prop.GetGetMethod().IsVirtual)
                        {
                            var currentValue = change.Property(prop.Name).CurrentValue;
                            var originalValue = change.Property(prop.Name).OriginalValue;
                            if (!Equals(originalValue, currentValue))
                            {
                                var attributes = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                                if (attributes != null && attributes.Length > 0)
                                {
                                    var displayName = (DisplayNameAttribute)attributes[0];
                                    AuditLogPropertyDAO AuditLogPropertyDAO = new AuditLogPropertyDAO();
                                    AuditLogPropertyDAO.Property = displayName.ToString();
                                    AuditLogPropertyDAO.ActionName = AuditLogPropertyActionEnum.EDIT.Name;
                                    AuditLogPropertyDAO.OldValue = originalValue.ToString();
                                    AuditLogPropertyDAO.NewValue = currentValue.ToString();
                                    AuditLogPropertyDAO.Time = StaticParams.DateTimeNow;
                                    AuditLogPropertyDAO.ClassName = EntityDisplayName;
                                    DataContext.AuditLogProperty.Add(AuditLogPropertyDAO);
                                    AuditLogPropertyDAOs.Add(AuditLogPropertyDAO);
                                }
                            }
                        }

                    }
                }
                else if (change.State == EntityState.Deleted)
                {
                    AuditLogPropertyDAO AuditLogPropertyDAO = new AuditLogPropertyDAO();
                    AuditLogPropertyDAO.ActionName = AuditLogPropertyActionEnum.CREATE.Name;
                    AuditLogPropertyDAO.Time = StaticParams.DateTimeNow;
                    AuditLogPropertyDAO.ClassName = EntityDisplayName;
                    DataContext.AuditLogProperty.Add(AuditLogPropertyDAO);
                    AuditLogPropertyDAOs.Add(AuditLogPropertyDAO);
                }
            }
            return AuditLogPropertyDAOs;
        }
    }
}
