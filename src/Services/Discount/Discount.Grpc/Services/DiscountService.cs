using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Servicescount;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(
        GetDiscountRequest request,
        ServerCallContext context
    )
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x =>
            x.ProductName.Equals(request.ProductName)
        );

        if (coupon is null)
        {
            coupon = new Coupon
            {
                ProductName = "No Discount",
                Amount = 0,
                Description = "No discount description",
            };
        }

        logger.LogInformation(
            "Discount is retrieved for ProductName: {productName}, Amount: {amount}",
            coupon.ProductName,
            coupon.Amount
        );

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(
        CreateDiscountRequest request,
        ServerCallContext context
    )
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Invalid request object")
            );
        }

        await dbContext.Coupons.AddAsync(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Discount created successfully. ProductName: {productName}",
            coupon.ProductName
        );

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(
        UpdateDiscountRequest request,
        ServerCallContext context
    )
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Invalid request object")
            );
        }

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Discount ProductName: {productName} is successfully updated",
            coupon.ProductName
        );

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(
        DeleteDiscountRequest request,
        ServerCallContext context
    )
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x =>
            x.ProductName.Equals(request.ProductName)
        );

        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Requested discount not found"));
        }

        dbContext.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Discount ProductName: {productName} is successfully deleted",
            coupon.ProductName
        );

        return new DeleteDiscountResponse { Success = true };
    }
}
