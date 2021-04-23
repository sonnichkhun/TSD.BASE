using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class dbo_notification : DataEntity,  IEquatable<dbo_notification>
    {
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public long? OrganizationId { get; set; }
    public long NotificationStatusId { get; set; }
    public enum_notificationstatus NotificationStatus { get; set; }
    public mdm_organization Organization { get; set; }

        public bool Equals(dbo_notification other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class dbo_notificationFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Content { get; set; }
        public IdFilter OrganizationId { get; set; }
        public IdFilter NotificationStatusId { get; set; }
        public List<dbo_notificationFilter>
            OrFilter { get; set; }
            public dbo_notificationOrder OrderBy {get; set;}
            public dbo_notificationSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum dbo_notificationOrder
            {
            Id = 0,
            Title = 1,
            Content = 2,
            Organization = 3,
            NotificationStatus = 4,
            }

            [Flags]
            public enum dbo_notificationSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Title = E._1,
            Content = E._2,
            Organization = E._3,
            NotificationStatus = E._4,
            }
            }
