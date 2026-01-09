
namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        // delete the order by id
        // save to database
        // return result

        var orderId = OrderId.Of(command.OrderId);
        var order = await dbContext.Orders.FindAsync([orderId.Value], cancellationToken: cancellationToken);

        if (orderId is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }

        dbContext.Orders.Remove(order!);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult(IsSuccess: true);
    }
}
