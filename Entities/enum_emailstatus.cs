using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class enum_emailstatus : DataEntity,  IEquatable<enum_emailstatus>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

        public bool Equals(enum_emailstatus other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class enum_emailstatusFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<enum_emailstatusFilter>
            OrFilter { get; set; }
            public enum_emailstatusOrder OrderBy {get; set;}
            public enum_emailstatusSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum enum_emailstatusOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            }

            [Flags]
            public enum enum_emailstatusSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            }
            }
