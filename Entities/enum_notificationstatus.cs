using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class enum_notificationstatus : DataEntity,  IEquatable<enum_notificationstatus>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

        public bool Equals(enum_notificationstatus other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class enum_notificationstatusFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<enum_notificationstatusFilter>
            OrFilter { get; set; }
            public enum_notificationstatusOrder OrderBy {get; set;}
            public enum_notificationstatusSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum enum_notificationstatusOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            }

            [Flags]
            public enum enum_notificationstatusSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            }
            }
