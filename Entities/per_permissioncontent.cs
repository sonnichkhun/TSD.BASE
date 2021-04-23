using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class per_permissioncontent : DataEntity,  IEquatable<per_permissioncontent>
    {
    public long Id { get; set; }
    public long PermissionId { get; set; }
    public long FieldId { get; set; }
    public long PermissionOperatorId { get; set; }
    public string Value { get; set; }
    public per_field Field { get; set; }
    public per_permission Permission { get; set; }
    public per_permissionoperator PermissionOperator { get; set; }

        public bool Equals(per_permissioncontent other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class per_permissioncontentFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public IdFilter PermissionId { get; set; }
        public IdFilter FieldId { get; set; }
        public IdFilter PermissionOperatorId { get; set; }
        public StringFilter Value { get; set; }
        public List<per_permissioncontentFilter>
            OrFilter { get; set; }
            public per_permissioncontentOrder OrderBy {get; set;}
            public per_permissioncontentSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum per_permissioncontentOrder
            {
            Id = 0,
            Permission = 1,
            Field = 2,
            PermissionOperator = 3,
            Value = 4,
            }

            [Flags]
            public enum per_permissioncontentSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Permission = E._1,
            Field = E._2,
            PermissionOperator = E._3,
            Value = E._4,
            }
            }
