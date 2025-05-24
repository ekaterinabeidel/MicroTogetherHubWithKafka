var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("YarpProxy"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
