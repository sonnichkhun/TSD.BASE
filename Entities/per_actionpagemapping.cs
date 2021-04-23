using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class per_actionpagemapping : DataEntity,  IEquatable<per_actionpagemapping>
    {
    public long ActionId { get; set; }
    public long PageId { get; set; }
    public per_action Action { get; set; }
    public per_page Page { get; set; }

        public bool Equals(per_actionpagemapping other)
        {
        return true;
        }
        public override int GetHashCode()
        {
        return base.GetHashCode();
        }
        }

        public class per_actionpagemappingFilter : FilterEntity
        {
        public IdFilter ActionId { get; set; }
        public IdFilter PageId { get; set; }
        public List<per_actionpagemappingFilter>
            OrFilter { get; set; }
            public per_actionpagemappingOrder OrderBy {get; set;}
            public per_actionpagemappingSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum per_actionpagemappingOrder
            {
            Action = 0,
            Page = 1,
            }

            [Flags]
            public enum per_actionpagemappingSelect:long
            {
            ALL = E.ALL,
            Action = E._0,
            Page = E._1,
            }
            }
