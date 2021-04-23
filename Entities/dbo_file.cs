using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class dbo_file : DataEntity,  IEquatable<dbo_file>
    {
    public long Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public long? AppUserId { get; set; }
    public mdm_appuser AppUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(dbo_file other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class dbo_fileFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Url { get; set; }
        public IdFilter AppUserId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<dbo_fileFilter>
            OrFilter { get; set; }
            public dbo_fileOrder OrderBy {get; set;}
            public dbo_fileSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum dbo_fileOrder
            {
            Id = 0,
            Name = 1,
            Url = 2,
            AppUser = 3,
            CreatedAt = 50,
            UpdatedAt = 51,
            }

            [Flags]
            public enum dbo_fileSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Name = E._1,
            Url = E._2,
            AppUser = E._3,
            }
            }
