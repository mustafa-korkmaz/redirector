
using Redirector;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Extensions.Html("homepage.html"));

app.MapGet("{slug}", (string slug) =>
{
    var redirectTo = builder.Configuration["routes:" + slug.ToLowerInvariant()];

    if (redirectTo != null)
    {
        return Results.Redirect(redirectTo, true, true);
    }

    return Results.NotFound();
});

app.Run();