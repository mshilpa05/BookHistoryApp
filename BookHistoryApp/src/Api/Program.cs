using Application.Interfaces;
using Application.Services;
using Infrastructure;
using Application.Mappings;
using Microsoft.EntityFrameworkCore;
using BookHistoryApp.src.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

builder.Configuration.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "src/API"));
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddScoped<IBookService, BookService>(); 
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IChangeHistoryService, ChangeHistoryService>();
builder.Services.AddScoped<IChangeHistoryRepository, ChangeHistoryRepository>();

builder.Services.AddAutoMapper(typeof(BookMappingProfile).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Book History API V1"));
}

app.UseRouting();
app.MapControllers();
app.Run();
