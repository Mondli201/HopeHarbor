using HopeHarbor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HopeHarbor.Data
{
    public class DJContext : IdentityDbContext
    {
        public DJContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Disaster> Disaster { get; set; }
        public DbSet<GoodsCategory> GoodsCategory { get; set; }
        public DbSet<GoodsDonation> GoodsDonation { get; set; }
        public DbSet<MonetaryDonation> MonetaryDonation { get; set; }
        public DbSet<Money_Allocation> Money_Allocation { get; set; }
        public DbSet<Good_Allocation> Good_Allocation { get; set; }

    }
}
