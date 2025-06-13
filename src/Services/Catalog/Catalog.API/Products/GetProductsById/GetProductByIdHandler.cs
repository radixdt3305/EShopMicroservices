using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductsById
{
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);

    public class GetProductByIdQueryhandler(IDocumentSession session, ILogger<GetProductByIdQueryhandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductsQuery with {@Query}", request);

            var product = await session.LoadAsync<Product>(request.id, cancellationToken);

            return product is null ? throw new ProductNotFoundException(request.id) : new GetProductByIdResult(product);
        }
    }
}