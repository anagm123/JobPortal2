using System;
using System.Collections.Generic;

namespace JobPortal2.Models.DBObjects
{
    public partial class Candidate
    {
        public Candidate()
        {
            Applications = new HashSet<Application>();
        }

        public Guid IdCandidate { get; set; }
        public string FullName { get; set; } = null!;
        public string Resume { get; set; } = null!;
        public string? Skills { get; set; }
        public string? Experience { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Location { get; set; } = null!;

        public virtual ICollection<Application> Applications { get; set; }
    }
}
