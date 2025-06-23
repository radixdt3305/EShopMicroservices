using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = 1,
                    Name = "IPhone X",
                    Description = "10% off on all products",
                    Amount = 10
                },
                new Coupon
                {
                    Id = 2,
                    Name = "Samsung 10",
                    Description = "20% off on all products",
                    Amount = 20
                }
            );
        }
    }
}
