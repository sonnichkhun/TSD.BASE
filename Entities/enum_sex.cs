using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class enum_sex : DataEntity,  IEquatable<enum_sex>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

        public bool Equals(enum_sex other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class enum_sexFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<enum_sexFilter>
            OrFilter { get; set; }
            public enum_sexOrder OrderBy {get; set;}
            public enum_sexSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum enum_sexOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            }

            [Flags]
            public enum enum_sexSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            }
            }
