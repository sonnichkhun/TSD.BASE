using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class mdm_appuserrolemapping : DataEntity,  IEquatable<mdm_appuserrolemapping>
    {
    public long AppUserId { get; set; }
    public long RoleId { get; set; }
    public mdm_appuser AppUser { get; set; }
    public per_role Role { get; set; }

        public bool Equals(mdm_appuserrolemapping other)
        {
        return true;
        }
        public override int GetHashCode()
        {
        return base.GetHashCode();
        }
        }

        public class mdm_appuserrolemappingFilter : FilterEntity
        {
        public IdFilter AppUserId { get; set; }
        public IdFilter RoleId { get; set; }
        public List<mdm_appuserrolemappingFilter>
            OrFilter { get; set; }
            public mdm_appuserrolemappingOrder OrderBy {get; set;}
            public mdm_appuserrolemappingSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum mdm_appuserrolemappingOrder
            {
            AppUser = 0,
            Role = 1,
            }

            [Flags]
            public enum mdm_appuserrolemappingSelect:long
            {
            ALL = E.ALL,
            AppUser = E._0,
            Role = E._1,
            }
            }
