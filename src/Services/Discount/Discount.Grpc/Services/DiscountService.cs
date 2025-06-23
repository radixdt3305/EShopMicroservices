using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbcontext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbcontext.Coupons.FirstOrDefaultAsync(x => x.Name == request.ProductName);
            if (coupon == null)
                coupon = new Coupon { Name = "No Discount", Description = "No Discount Desc", Amount = 0 };

            logger.LogInformation("Discount is retrieved for product {ProductName}, Amount: {Amount}", coupon.Name, coupon.Amount);

            var config = new TypeAdapterConfig();
            config.NewConfig<Coupon, CouponModel>()
                  .Map(dest => dest.ProductName, src => src.Name);

            var couponModel = coupon.Adapt<CouponModel>(config);
            return couponModel;
        }


        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            // Map request to Coupon entity
            var coupon = new Coupon
            {
                Name = request.Coupon.ProductName,
                Description = request.Coupon.Description,
                Amount = request.Coupon.Amount
            };

            // Save to database
            dbcontext.Coupons.Add(coupon);
            await dbcontext.SaveChangesAsync();

            // Log success
            logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.Name);

            // Configure mapping
            var config = new TypeAdapterConfig();
            config.NewConfig<Coupon, CouponModel>()
                  .Map(dest => dest.ProductName, src => src.Name);

            // Map entity to response model
            var couponModel = coupon.Adapt<CouponModel>(config);
            return couponModel;
        }


        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            // Map request to Coupon entity
            var coupon = new Coupon
            {
                Id = request.Coupon.Id,
                Name = request.Coupon.ProductName,
                Description = request.Coupon.Description,
                Amount = request.Coupon.Amount
            };

            // Save to database
            dbcontext.Coupons.Update(coupon);
            await dbcontext.SaveChangesAsync();

            // Log success
            logger.LogInformation("Discount is successfully Updated. ProductName : {ProductName}", coupon.Name);

            // Configure mapping
            var config = new TypeAdapterConfig();
            config.NewConfig<Coupon, CouponModel>()
                  .Map(dest => dest.ProductName, src => src.Name);

            // Map entity to response model
            var couponModel = coupon.Adapt<CouponModel>(config);
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbcontext.Coupons.FirstOrDefaultAsync(x => x.Name == request.ProductName);
            if (coupon == null)
            {
                logger.LogError("Discount not found for product {ProductName}", request.ProductName);
                return new DeleteDiscountResponse { Success = false };
            }
            dbcontext.Coupons.Remove(coupon);
            await dbcontext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully deleted for product {ProductName}", request.ProductName);
            return new DeleteDiscountResponse
            {
                Success = true
            };
        }
    }
}
