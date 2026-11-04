using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HopeHarbor.Models
{
    public class GoodsDonation
    {
        [Key]
        public int DonationId { get; set; }

        [Required]
        [MaxLength(255)]
        public string DonorName { get; set; }

        [Required]
        public DateTime DonationDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string ItemCategory { get; set; }

        [MaxLength(100)]
        public string ItemDescription { get; set; }

        [Required]
        public int ItemCount { get; set; }

        public bool IsAnonymous { get; set; }



        [ForeignKey("GoodsCategory")]
        public string CategoryName { get; set; }


        public GoodsCategory? GoodsCategory { get; set; }
    }
}
