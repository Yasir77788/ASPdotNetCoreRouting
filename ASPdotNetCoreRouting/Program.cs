var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// enable routing
app.UseRouting();

// use GetEndpoint()
app.Use(async(context, next)=> {
    Microsoft.AspNetCore.Http.Endpoint? endPoint =
        context.GetEndpoint();
    if (endPoint != null)
    {
        await context.Response.WriteAsync($"Endpont: {endPoint.DisplayName}\n");
    }
    await next(context);
}); 

// creating endpoints
app.UseEndpoints(endpoints =>
{
    // add endpoints
    endpoints.MapGet("map1", async (context) =>
    {
        await context.Response.WriteAsync("In Map 1...");
        await context.Response.WriteAsync("In Map 1...");
    });

    endpoints.MapPost("map2", async (context) =>
    {
        await context.Response.WriteAsync("In Map 2 or second...");
    });
});

// without specific endpoint
app.Run(async context =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

app.Run();
