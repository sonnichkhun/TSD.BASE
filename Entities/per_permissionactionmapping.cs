using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class per_permissionactionmapping : DataEntity,  IEquatable<per_permissionactionmapping>
    {
    public long ActionId { get; set; }
    public long PermissionId { get; set; }
    public per_action Action { get; set; }
    public per_permission Permission { get; set; }

        public bool Equals(per_permissionactionmapping other)
        {
        return true;
        }
        public override int GetHashCode()
        {
        return base.GetHashCode();
        }
        }

        public class per_permissionactionmappingFilter : FilterEntity
        {
        public IdFilter ActionId { get; set; }
        public IdFilter PermissionId { get; set; }
        public List<per_permissionactionmappingFilter>
            OrFilter { get; set; }
            public per_permissionactionmappingOrder OrderBy {get; set;}
            public per_permissionactionmappingSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum per_permissionactionmappingOrder
            {
            Action = 0,
            Permission = 1,
            }

            [Flags]
            public enum per_permissionactionmappingSelect:long
            {
            ALL = E.ALL,
            Action = E._0,
            Permission = E._1,
            }
            }
