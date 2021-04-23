using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class per_permission : DataEntity,  IEquatable<per_permission>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public long RoleId { get; set; }
    public long MenuId { get; set; }
    public long StatusId { get; set; }
    public per_menu Menu { get; set; }
    public per_role Role { get; set; }
    public enum_status Status { get; set; }

        public bool Equals(per_permission other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class per_permissionFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter RoleId { get; set; }
        public IdFilter MenuId { get; set; }
        public IdFilter StatusId { get; set; }
        public List<per_permissionFilter>
            OrFilter { get; set; }
            public per_permissionOrder OrderBy {get; set;}
            public per_permissionSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum per_permissionOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            Role = 3,
            Menu = 4,
            Status = 5,
            }

            [Flags]
            public enum per_permissionSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            Role = E._3,
            Menu = E._4,
            Status = E._5,
            }
            }
