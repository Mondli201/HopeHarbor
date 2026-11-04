using System.ComponentModel.DataAnnotations;

namespace HopeHarbor.Models
{
    public class GoodsCategory
    {
        [Key]

        public string CategoryName { get; set; }
    }
}
