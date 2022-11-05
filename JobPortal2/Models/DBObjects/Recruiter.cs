using System;
using System.Collections.Generic;

namespace JobPortal2.Models.DBObjects
{
    public partial class Recruiter
    {
        public Recruiter()
        {
            Jobs = new HashSet<Job>();
        }

        public Guid IdRecruiter { get; set; }
        public string CompanyName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
