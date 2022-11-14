using System;
using System.Collections.Generic;

namespace JobPortal2.Models.DBObjects
{
    public partial class Job
    {
        public Job()
        {
            Applications = new HashSet<Application>();
            Saveds = new HashSet<Saved>();
        }

        public Guid IdJob { get; set; }
        public string JobTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid IdRecruiter { get; set; }
        public DateTime DateTimeAdded { get; set; }

        public virtual Recruiter? IdRecruiterNavigation { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<Saved> Saveds { get; set; }
    }
}
