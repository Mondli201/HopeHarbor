using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HopeHarbor.Models
{
    public class MonetaryDonation
    {
        [Key]
        public int DonationId { get; set; }

        [Required]
        [MaxLength(255)]
        public string DonorName { get; set; }

        [Required]
        public DateTime DonationDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public bool IsAnonymous { get; set; }

        public string DonationDescription { get; set; }
    }
}
