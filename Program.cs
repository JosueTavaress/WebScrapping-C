using Microsoft.EntityFrameworkCore;
using WebScrapping_C;
using WebScrapping_C.Data;
using WebScrapping_C.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FoodsContex>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IFoodsRepository, FoodsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


var scrapingTask = Task.Run(async () =>
{
    var contextOptions = new DbContextOptionsBuilder<FoodsContex>()
                            .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                            .Options;
    var context = new FoodsContex(contextOptions);
    var repository = new FoodsRepository(context);
    var scrapping = new Scrapping(repository);
    await scrapping.ExecuteAsync();
});

app.Run();

await scrapingTask;
