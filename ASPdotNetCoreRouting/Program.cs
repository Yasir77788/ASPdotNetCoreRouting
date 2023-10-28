using System.Runtime.InteropServices;

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
    });

    // route parameter
    // this matches with any file name and any file extesnion
    // for url localhost:8888/files/sample.txt
    endpoints.Map("files/{filename}.{extension}",
        async context =>
        {
            // use route values to get file name and file extension
            string? fName = Convert.ToString(context.Request.RouteValues["filename"]);
            string? ext = Convert.ToString(context.Request.RouteValues["extension"]);
            await context.Response.WriteAsync($"In files - {fName} and extension - {ext}");

        }
        );

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
