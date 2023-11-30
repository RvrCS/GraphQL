

using FirebaseAdminAuthentication.DependencyInjection.Extensions;
using GraphQL.DataLoaders;
using GraphQL.Schema;
using GraphQL.Schema.Queries;
using GraphQL.Schema.Subscriptions;
using GraphQL.Services;
using GraphQL.Services.Courses;
using GraphQL.Services.Instructors;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddAuthorization();

builder.Services.AddFirebaseAuthentication();


string connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddPooledDbContextFactory<SchoolDbContext>(
    o => o.UseSqlite(connectionString).LogTo(Console.WriteLine));

builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<InstructorsRepository>();
builder.Services.AddScoped<InstructorDataLoader>();

var app = builder.Build();



IServiceScope scope = app.Services.CreateScope();
IDbContextFactory<SchoolDbContext> dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SchoolDbContext>>();

SchoolDbContext schoolDbContext = dbContextFactory.CreateDbContext();

schoolDbContext.Database.Migrate();

app.UseAuthentication();

app.UseWebSockets();

app.MapGraphQL();

app.Run();
