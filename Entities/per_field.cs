using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class per_field : DataEntity,  IEquatable<per_field>
    {
    public long Id { get; set; }
    public string Name { get; set; }
    public long FieldTypeId { get; set; }
    public long MenuId { get; set; }
    public bool IsDeleted { get; set; }
    public per_fieldtype FieldType { get; set; }
    public per_menu Menu { get; set; }

        public bool Equals(per_field other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class per_fieldFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter FieldTypeId { get; set; }
        public IdFilter MenuId { get; set; }
        public List<per_fieldFilter>
            OrFilter { get; set; }
            public per_fieldOrder OrderBy {get; set;}
            public per_fieldSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum per_fieldOrder
            {
            Id = 0,
            Name = 1,
            FieldType = 2,
            Menu = 3,
            IsDeleted = 4,
            }

            [Flags]
            public enum per_fieldSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Name = E._1,
            FieldType = E._2,
            Menu = E._3,
            IsDeleted = E._4,
            }
            }
