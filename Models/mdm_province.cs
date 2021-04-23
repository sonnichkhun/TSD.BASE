using System;
using System.Collections.Generic;

namespace BASE.Models
{
    public partial class mdm_province
    {
        public mdm_province()
        {
            mdm_district = new HashSet<mdm_district>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? Priority { get; set; }
        public long StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string RowId { get; set; }
        public bool Used { get; set; }

        public virtual enum_status Status { get; set; }
        public virtual ICollection<mdm_district> mdm_district { get; set; }
    }
}
