using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class mdm_taxtype : DataEntity,  IEquatable<mdm_taxtype>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Percentage { get; set; }
    public long StatusId { get; set; }
    public bool Used { get; set; }
    public enum_status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(mdm_taxtype other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class mdm_taxtypeFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public DecimalFilter Percentage { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<mdm_taxtypeFilter>
            OrFilter { get; set; }
            public mdm_taxtypeOrder OrderBy {get; set;}
            public mdm_taxtypeSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum mdm_taxtypeOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            Percentage = 3,
            Status = 4,
            Used = 8,
            CreatedAt = 50,
            UpdatedAt = 51,
            }

            [Flags]
            public enum mdm_taxtypeSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            Percentage = E._3,
            Status = E._4,
            Used = E._8,
            }
            }
