using graphql_demo.Data;
using graphql_demo.Entities;
using graphql_demo.GraphQL.Mutations;
using graphql_demo.GraphQL.Types;
using graphql_demo.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDbContext<DbContextClass>
(o => o.UseInMemoryDatabase("GraphQLDemo"));

builder.Services.AddGraphQLServer()
    .AddQueryType<ProductQueryTypes>()
    .AddMutationType<ProductMutations>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DbContextClass>();

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGraphQL();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
