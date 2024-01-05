using Backend.Models;
using Backend.services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
    // inyecci�n de dependencias:
builder.Services.AddSingleton<IPeopleService, PeopleService>();
builder.Services.AddSingleton<IRandomService, RandomService>();
//builder.Services.AddScoped<IRandomService, RandomService>();
//builder.Services.AddTransient<IRandomService, RandomService>();

builder.Services.AddScoped<IPostsService, PostsService>();

//HttpClient servicio jsonplaceholder
builder.Services.AddHttpClient<IPostsService, PostsService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["BaseUrlPost"]) ;
});

//Entity Framework
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
