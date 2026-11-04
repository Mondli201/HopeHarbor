using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HopeHarbor.Models
{
    public class Money_Allocation
    {
        [Key]
        public int MoneyID { get; set; }

        [ForeignKey("Disaster")]
        public int DisasterId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Disaster? Disaster { get; set; }
    }
}
