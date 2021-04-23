using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class mdm_image : DataEntity,  IEquatable<mdm_image>
    {
    public long Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string ThumbnailUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(mdm_image other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class mdm_imageFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Url { get; set; }
        public StringFilter ThumbnailUrl { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<mdm_imageFilter>
            OrFilter { get; set; }
            public mdm_imageOrder OrderBy {get; set;}
            public mdm_imageSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum mdm_imageOrder
            {
            Id = 0,
            Name = 1,
            Url = 2,
            ThumbnailUrl = 3,
            CreatedAt = 50,
            UpdatedAt = 51,
            }

            [Flags]
            public enum mdm_imageSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Name = E._1,
            Url = E._2,
            ThumbnailUrl = E._3,
            }
            }
