using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class per_permissionoperator : DataEntity,  IEquatable<per_permissionoperator>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public long FieldTypeId { get; set; }
    public per_fieldtype FieldType { get; set; }

        public bool Equals(per_permissionoperator other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class per_permissionoperatorFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter FieldTypeId { get; set; }
        public List<per_permissionoperatorFilter>
            OrFilter { get; set; }
            public per_permissionoperatorOrder OrderBy {get; set;}
            public per_permissionoperatorSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum per_permissionoperatorOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            FieldType = 3,
            }

            [Flags]
            public enum per_permissionoperatorSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            FieldType = E._3,
            }
            }
