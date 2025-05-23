using Carter;

namespace Topic.CommandService.Api.Endpoints;

public class HomeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => "Hello Topic.CommandService");
    }
}