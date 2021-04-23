using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class per_role : DataEntity,  IEquatable<per_role>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public long StatusId { get; set; }
    public bool Used { get; set; }
    public enum_status Status { get; set; }

        public bool Equals(per_role other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class per_roleFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter StatusId { get; set; }
        public List<per_roleFilter>
            OrFilter { get; set; }
            public per_roleOrder OrderBy {get; set;}
            public per_roleSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum per_roleOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            Status = 3,
            Used = 4,
            }

            [Flags]
            public enum per_roleSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            Status = E._3,
            Used = E._4,
            }
            }
