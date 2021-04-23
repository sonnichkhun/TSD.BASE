using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class per_action : DataEntity,  IEquatable<per_action>
    {
    public long Id { get; set; }
    public string Name { get; set; }
    public long MenuId { get; set; }
    public bool IsDeleted { get; set; }
    public per_menu Menu { get; set; }

        public bool Equals(per_action other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class per_actionFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter MenuId { get; set; }
        public List<per_actionFilter>
            OrFilter { get; set; }
            public per_actionOrder OrderBy {get; set;}
            public per_actionSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum per_actionOrder
            {
            Id = 0,
            Name = 1,
            Menu = 2,
            IsDeleted = 3,
            }

            [Flags]
            public enum per_actionSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Name = E._1,
            Menu = E._2,
            IsDeleted = E._3,
            }
            }
