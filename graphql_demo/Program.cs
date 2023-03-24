using graphql_demo.Data;
using graphql_demo.GraphQL.Mutations;
using graphql_demo.GraphQL.Types;
using graphql_demo.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Register Service
builder.Services.AddScoped<IProductService, ProductService>();

//InMemory Database
builder.Services.AddDbContext<DbContextClass>
(o => o.UseInMemoryDatabase("GraphQLDemo"));

//GraphQL Config
builder.Services.AddGraphQLServer()
    .AddQueryType<ProductQueryTypes>()
    .AddMutationType<ProductMutations>();

var app = builder.Build();

//Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DbContextClass>();

    SeedData.Initialize(services);
}

//GraphQL
app.MapGraphQL();

app.UseHttpsRedirection();

app.Run();
