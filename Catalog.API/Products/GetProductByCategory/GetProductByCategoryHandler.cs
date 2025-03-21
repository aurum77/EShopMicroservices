﻿namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

public class GetProductByCategoryResultValidator : AbstractValidator<GetProductByCategoryQuery>
{
    public GetProductByCategoryResultValidator()
    {
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category name cannot be empty");
    }
}

internal class GetProductByCategoryQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(
        GetProductByCategoryQuery query,
        CancellationToken cancellationToken
    )
    {
        var products = await session
            .Query<Product>()
            .Where(x => x.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}
