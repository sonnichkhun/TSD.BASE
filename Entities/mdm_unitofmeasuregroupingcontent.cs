using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class mdm_unitofmeasuregroupingcontent : DataEntity,  IEquatable<mdm_unitofmeasuregroupingcontent>
    {
    public long Id { get; set; }
    public long UnitOfMeasureGroupingId { get; set; }
    public long UnitOfMeasureId { get; set; }
    public long? Factor { get; set; }
    public mdm_unitofmeasure UnitOfMeasure { get; set; }
    public mdm_unitofmeasuregrouping UnitOfMeasureGrouping { get; set; }

        public bool Equals(mdm_unitofmeasuregroupingcontent other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class mdm_unitofmeasuregroupingcontentFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public IdFilter UnitOfMeasureGroupingId { get; set; }
        public IdFilter UnitOfMeasureId { get; set; }
        public LongFilter Factor { get; set; }
        public List<mdm_unitofmeasuregroupingcontentFilter>
            OrFilter { get; set; }
            public mdm_unitofmeasuregroupingcontentOrder OrderBy {get; set;}
            public mdm_unitofmeasuregroupingcontentSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum mdm_unitofmeasuregroupingcontentOrder
            {
            Id = 0,
            UnitOfMeasureGrouping = 1,
            UnitOfMeasure = 2,
            Factor = 3,
            }

            [Flags]
            public enum mdm_unitofmeasuregroupingcontentSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            UnitOfMeasureGrouping = E._1,
            UnitOfMeasure = E._2,
            Factor = E._3,
            }
            }
