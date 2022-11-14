using System.ComponentModel.DataAnnotations;

namespace JobPortal2.Models
{
    public class ApplicationModel
    {
        public Guid IdApplication { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime DateTimeAdded { get; set; }
        public Guid IdJob { get; set; }
        public  Guid IdCandidate { get; set; }
    }
}
