using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class per_menu : DataEntity,  IEquatable<per_menu>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public bool IsDeleted { get; set; }

        public bool Equals(per_menu other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class per_menuFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Path { get; set; }
        public List<per_menuFilter>
            OrFilter { get; set; }
            public per_menuOrder OrderBy {get; set;}
            public per_menuSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum per_menuOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            Path = 3,
            IsDeleted = 4,
            }

            [Flags]
            public enum per_menuSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            Path = E._3,
            IsDeleted = E._4,
            }
            }
