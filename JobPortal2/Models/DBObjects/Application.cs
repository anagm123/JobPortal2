using System;
using System.Collections.Generic;

namespace JobPortal2.Models.DBObjects
{
    public partial class Application
    {
        public Guid IdApplication { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public Guid IdJob { get; set; }
        public Guid IdCandidate { get; set; }

        public virtual Candidate IdCandidateNavigation { get; set; } = null!;
        public virtual Job IdJobNavigation { get; set; } = null!;
    }
}
