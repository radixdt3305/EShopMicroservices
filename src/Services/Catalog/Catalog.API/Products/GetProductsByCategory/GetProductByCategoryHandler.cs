
namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product >Products);
    public class GetProductByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var result  = await  session.Query<Product>()
                .Where(p => p.Category.Contains(request.category))
                .ToListAsync(cancellationToken);
            //logger.LogInformation("Handling GetProductByCategory with {@Query}", request);
            return new GetProductByCategoryResult(result);
        }
    }
}
