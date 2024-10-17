using Application.Interfaces;
using Application.Services;
using Infrastructure;
using Application.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connectionString);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=src/Infrastructure/Data/DB/BookHistory.db"));

builder.Services.AddScoped<IBookService, BookService>(); 
builder.Services.AddScoped<IBookRepository, BookRepository>(); 

builder.Services.AddAutoMapper(typeof(BookMappingProfile).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    dbContext.Database.Migrate();
//}

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
