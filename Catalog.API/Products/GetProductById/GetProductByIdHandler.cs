namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id must not be empty");
    }
}

internal class GetProductByIdQueryHandler(
    IDocumentSession session,
    ILogger<GetProductByIdQueryHandler> logger
) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(
        GetProductByIdQuery query,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle was called with Id:{}", query.Id);
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        return new GetProductByIdResult(product);
    }
}
