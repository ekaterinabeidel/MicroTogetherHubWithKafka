using Topic.QueryService.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddQueryServices(builder.Configuration);

var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.UseApiServices();

app.Run();