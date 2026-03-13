using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Shared in-memory store to demonstrate dynamic Razor Page updates.
builder.Services.AddSingleton<ItemStore>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // Custom error page for unhandled exceptions.
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Logs request details and response status code for every request.
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("RequestResponseLogger");
    logger.LogInformation("Request: {Method} {Path}", context.Request.Method, context.Request.Path);

    await next();

    logger.LogInformation("Response: {StatusCode} for {Method} {Path}", context.Response.StatusCode, context.Request.Method, context.Request.Path);
});

// Basic CSP to reduce XSS risk for served content, including static files.
app.Use(async (context, next) =>
{
    context.Response.Headers.ContentSecurityPolicy = "default-src 'self'; script-src 'self'; style-src 'self'; img-src 'self' data:; object-src 'none'; base-uri 'self'; frame-ancestors 'none'";
    await next();
});

// Enforce HTTPS before serving app/static content.
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

public sealed class ItemStore
{
    private readonly ConcurrentBag<string> _items = new([
        "Learn ASP.NET Core middleware",
        "Build Razor Pages with PageModel"
    ]);

    public IReadOnlyCollection<string> GetAll() => _items.ToArray();

    public void Add(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            _items.Add(value.Trim());
        }
    }
}
