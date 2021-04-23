using System;
using System.Collections.Generic;
using BASE.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BASE.Entities
{
    public class mdm_appuser : DataEntity,  IEquatable<mdm_appuser>
    {
    public long Id { get; set; }
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public long SexId { get; set; }
    public DateTime? Birthday { get; set; }
    public string Avatar { get; set; }
    public string Department { get; set; }
    public long OrganizationId { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? Latitude { get; set; }
    public long StatusId { get; set; }
    public bool Used { get; set; }
    public mdm_organization Organization { get; set; }
    public enum_sex Sex { get; set; }
    public enum_status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(mdm_appuser other)
        {
        return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
        return Id.GetHashCode();
        }
        }

        public class mdm_appuserFilter : FilterEntity
        {
        public IdFilter Id { get; set; }
        public StringFilter Username { get; set; }
        public StringFilter DisplayName { get; set; }
        public StringFilter Address { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter Phone { get; set; }
        public IdFilter SexId { get; set; }
        public DateFilter Birthday { get; set; }
        public StringFilter Avatar { get; set; }
        public StringFilter Department { get; set; }
        public IdFilter OrganizationId { get; set; }
        public DecimalFilter Longitude { get; set; }
        public DecimalFilter Latitude { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<mdm_appuserFilter>
            OrFilter { get; set; }
            public mdm_appuserOrder OrderBy {get; set;}
            public mdm_appuserSelect Selects {get; set;}
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public enum mdm_appuserOrder
            {
            Id = 0,
            Username = 1,
            DisplayName = 2,
            Address = 3,
            Email = 4,
            Phone = 5,
            Sex = 6,
            Birthday = 7,
            Avatar = 8,
            Department = 9,
            Organization = 10,
            Longitude = 11,
            Latitude = 12,
            Status = 13,
            Used = 17,
            CreatedAt = 50,
            UpdatedAt = 51,
            }

            [Flags]
            public enum mdm_appuserSelect:long
            {
            ALL = E.ALL,
            Id = E._0,
            Username = E._1,
            DisplayName = E._2,
            Address = E._3,
            Email = E._4,
            Phone = E._5,
            Sex = E._6,
            Birthday = E._7,
            Avatar = E._8,
            Department = E._9,
            Organization = E._10,
            Longitude = E._11,
            Latitude = E._12,
            Status = E._13,
            Used = E._17,
            }
            }
