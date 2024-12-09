using FastEndpoints;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price
    );

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint(ISender sender)
        : Endpoint<CreateProductRequest, CreateProductResponse>
    {
        public override void Configure()
        {
            Post("/products");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateProductRequest req, CancellationToken ct)
        {
            var command = req.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            await SendAsync(response, StatusCodes.Status201Created);
        }
    }
}
