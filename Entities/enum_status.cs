using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class enum_status : DataEntity,  IEquatable<enum_status>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

        public bool Equals(enum_status other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class enum_statusFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<enum_statusFilter>
            OrFilter { get; set; }
            public enum_statusOrder OrderBy {get; set;}
            public enum_statusSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum enum_statusOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            }

            [Flags]
            public enum enum_statusSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            }
            }
