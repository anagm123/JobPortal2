using System.ComponentModel.DataAnnotations;

namespace JobPortal2.Models
{
    public class CandidateModel
    {
        public Guid IdCandidate { get; set; }
        public string FullName { get; set; } = null!;
        public string Resume { get; set; } = null!;
        public string? Skills { get; set; }
        public string? Experience { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Location { get; set; } = null;

    }
}
