using System;
using System.Collections.Generic;

namespace JobPortal2.Models.DBObjects
{
    public partial class Saved
    {
        public Guid IdJob { get; set; }
        public Guid IdCandidate { get; set; }

        public virtual Candidate IdCandidateNavigation { get; set; } = null!;
        public virtual Job IdJobNavigation { get; set; } = null!;
    }
}
