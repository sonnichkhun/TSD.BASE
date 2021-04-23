using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class mdm_position : DataEntity,  IEquatable<mdm_position>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public long StatusId { get; set; }
    public bool Used { get; set; }
    public enum_status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(mdm_position other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class mdm_positionFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<mdm_positionFilter>
            OrFilter { get; set; }
            public mdm_positionOrder OrderBy {get; set;}
            public mdm_positionSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum mdm_positionOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            Status = 3,
            Used = 7,
            CreatedAt = 50,
            UpdatedAt = 51,
            }

            [Flags]
            public enum mdm_positionSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            Status = E._3,
            Used = E._7,
            }
            }
