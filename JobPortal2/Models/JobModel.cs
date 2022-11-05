using System.ComponentModel.DataAnnotations;

namespace JobPortal2.Models
{
    public class JobModel
    {
        public Guid IdJob { get; set; }
        public string JobTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid IdRecruiter { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime DateTimeAdded { get; set; }
    }
}
