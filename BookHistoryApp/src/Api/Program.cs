using Application.Interfaces;
using Application.Services;
using Infrastructure;
using Application.Mappings;
using Microsoft.EntityFrameworkCore;
using BookHistoryApp.src.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBookService, BookService>(); 
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IChangeHistoryService, ChangeHistoryService>();
builder.Services.AddScoped<IChangeHistoryRepository, ChangeHistoryRepository>();

builder.Services.AddAutoMapper(typeof(BookMappingProfile).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Book History API V1"));
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
