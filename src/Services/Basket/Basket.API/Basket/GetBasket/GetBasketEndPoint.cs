namespace Basket.API.Basket.GetBasket
{
    public record GetBasketResponse(ShoppingCart ShoppingCart);
    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("basket/{username}", async (string username, ISender sender) =>
            {
                var query = new GetBasketQuery(username);
                var result = await sender.Send(query);
                var response = result.Adapt<GetBasketResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id");
        }
    }
}
