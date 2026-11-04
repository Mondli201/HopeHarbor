using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HopeHarbor.Models
{
    public class Good_Allocation
    {
        [Key]
        public int Goods_Id { get; set; }


        [Required]
        [ForeignKey("Disaster")]
        public int DisasterId { get; set; }


        [Required]
        [ForeignKey("GoodsDonation")]
        public int DonationId { get; set; }

        [Required]

        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public Disaster? Disaster { get; set; }
        public GoodsDonation? GoodsDonation { get; set; }
    }
}
