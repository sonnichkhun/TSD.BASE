using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class mdm_organization : DataEntity,  IEquatable<mdm_organization>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public long? ParentId { get; set; }
    public string Path { get; set; }
    public long Level { get; set; }
    public long StatusId { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public bool Used { get; set; }
    public mdm_organization Parent { get; set; }
    public enum_status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(mdm_organization other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class mdm_organizationFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter ParentId { get; set; }
        public StringFilter Path { get; set; }
        public LongFilter Level { get; set; }
        public IdFilter StatusId { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter Address { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<mdm_organizationFilter>
            OrFilter { get; set; }
            public mdm_organizationOrder OrderBy {get; set;}
            public mdm_organizationSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum mdm_organizationOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            Parent = 3,
            Path = 4,
            Level = 5,
            Status = 6,
            Phone = 7,
            Email = 8,
            Address = 9,
            Used = 13,
            CreatedAt = 50,
            UpdatedAt = 51,
            }

            [Flags]
            public enum mdm_organizationSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            Parent = E._3,
            Path = E._4,
            Level = E._5,
            Status = E._6,
            Phone = E._7,
            Email = E._8,
            Address = E._9,
            Used = E._13,
            }
            }
