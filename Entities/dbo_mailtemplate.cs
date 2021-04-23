using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class dbo_mailtemplate : DataEntity,  IEquatable<dbo_mailtemplate>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public long? StatusId { get; set; }
    public enum_status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(dbo_mailtemplate other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class dbo_mailtemplateFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Content { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<dbo_mailtemplateFilter>
            OrFilter { get; set; }
            public dbo_mailtemplateOrder OrderBy {get; set;}
            public dbo_mailtemplateSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum dbo_mailtemplateOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            Content = 3,
            Status = 4,
            CreatedAt = 50,
            UpdatedAt = 51,
            }

            [Flags]
            public enum dbo_mailtemplateSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            Content = E._3,
            Status = E._4,
            }
            }
