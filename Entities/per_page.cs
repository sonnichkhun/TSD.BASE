using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class per_page : DataEntity,  IEquatable<per_page>
    {
    public long Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public bool IsDeleted { get; set; }

        public bool Equals(per_page other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class per_pageFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Path { get; set; }
        public List<per_pageFilter>
            OrFilter { get; set; }
            public per_pageOrder OrderBy {get; set;}
            public per_pageSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum per_pageOrder
            {
            Id = 0,
            Name = 1,
            Path = 2,
            IsDeleted = 3,
            }

            [Flags]
            public enum per_pageSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Name = E._1,
            Path = E._2,
            IsDeleted = E._3,
            }
            }
