using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class mdm_ward : DataEntity,  IEquatable<mdm_ward>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public long? Priority { get; set; }
    public long DistrictId { get; set; }
    public long StatusId { get; set; }
    public bool Used { get; set; }
    public mdm_district District { get; set; }
    public enum_status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(mdm_ward other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class mdm_wardFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter Priority { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<mdm_wardFilter>
            OrFilter { get; set; }
            public mdm_wardOrder OrderBy {get; set;}
            public mdm_wardSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum mdm_wardOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            Priority = 3,
            District = 4,
            Status = 5,
            Used = 9,
            CreatedAt = 50,
            UpdatedAt = 51,
            }

            [Flags]
            public enum mdm_wardSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            Priority = E._3,
            District = E._4,
            Status = E._5,
            Used = E._9,
            }
            }
