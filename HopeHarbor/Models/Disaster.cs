using System.ComponentModel.DataAnnotations;

namespace HopeHarbor.Models
{
    public class Disaster
    {
        [Key]
        public int DisasterId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string Location { get; set; }

        public string Description { get; set; }

        public string RequiredAidTypes { get; set; }
    }
}
