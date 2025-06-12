namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery():IQuery<GetProductResult>;

    public record GetProductResult(IEnumerable<Product> Products);
    public class GetProductQueryhandler(IDocumentSession documentSession,ILogger<GetProductQueryhandler> logger) : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductsQuery with {@Query}",request);
            
            var products  = await documentSession.Query<Product>().ToListAsync(cancellationToken);
            
            logger.LogInformation("Retrieved {Count} products", products.Count);
            
            return new GetProductResult(products);
        }
    }
}
