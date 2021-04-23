using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class mdm_district : DataEntity,  IEquatable<mdm_district>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public long? Priority { get; set; }
    public long ProvinceId { get; set; }
    public long StatusId { get; set; }
    public UInt64 Used { get; set; }
    public mdm_province Province { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(mdm_district other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class mdm_districtFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter Priority { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<mdm_districtFilter>
            OrFilter { get; set; }
            public mdm_districtOrder OrderBy {get; set;}
            public mdm_districtSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum mdm_districtOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            Priority = 3,
            Province = 4,
            Status = 5,
            CreatedAt = 50,
            UpdatedAt = 51,
            }

            [Flags]
            public enum mdm_districtSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            Priority = E._3,
            Province = E._4,
            Status = E._5,
            }
            }
