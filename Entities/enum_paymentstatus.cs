using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class enum_paymentstatus : DataEntity,  IEquatable<enum_paymentstatus>
    {
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

        public bool Equals(enum_paymentstatus other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class enum_paymentstatusFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<enum_paymentstatusFilter>
            OrFilter { get; set; }
            public enum_paymentstatusOrder OrderBy {get; set;}
            public enum_paymentstatusSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum enum_paymentstatusOrder
            {
            Id = 0,
            Code = 1,
            Name = 2,
            }

            [Flags]
            public enum enum_paymentstatusSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Code = E._1,
            Name = E._2,
            }
            }
