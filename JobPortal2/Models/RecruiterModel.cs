namespace JobPortal2.Models
{
    public class RecruiterModel
    {
        public Guid IdRecruiter { get; set; }
        public string CompanyName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;

    }
}
