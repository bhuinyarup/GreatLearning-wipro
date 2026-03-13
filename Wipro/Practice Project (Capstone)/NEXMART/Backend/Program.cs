using EcommerceApp.Dao;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "EcommerceApp API", Version = "v1" });
});

// Register repository
builder.Services.AddScoped<IOrderProcessorRepository, OrderProcessorRepositoryImpl>();

// CORS - allow everything
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithExposedHeaders("*");
    });
});

// Serve static files (index.html) from wwwroot
builder.Services.AddDirectoryBrowser();

var app = builder.Build();

app.UseCors("AllowAll");

// Serve static files from wwwroot folder
app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
