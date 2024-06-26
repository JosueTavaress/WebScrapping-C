using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebScrapping_C.Core.Services;
using WebScrapping_C.Data;
using WebScrapping_C.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
  .AddJsonOptions(options =>
  {
      options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
      options.JsonSerializerOptions.MaxDepth = 64;
  });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<FoodsContex>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IFoodsRepository, FoodsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

// processes data scraping in parallel
var scrapingTask = Task.Run(async () =>
{
    var contextOptions = new DbContextOptionsBuilder<FoodsContex>()
                            .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                            .Options;
    var context = new FoodsContex(contextOptions);
    var repository = new FoodsRepository(context);
    var htmlScraping = new HtmInteractions();

    var scrapping = new Scrapping(repository, htmlScraping);
    await scrapping.ExecuteAsync();
});

app.Run();

await scrapingTask;
