using System.Reflection;
using FishApi.Data;
using FishApi.Documentation;
using FishApi.Features;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FishContext>((_, opt) =>
    opt.UseSqlite($"Data Source={builder.Configuration.GetValue<string>("DbPath")}"));

builder.Services.AddValidatorsFromAssemblies(new []{ Assembly.GetExecutingAssembly() });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
    opt.OperationFilter<GroupEndpointsByUrlFilter>();
});

var app = builder.Build();

app.UseRouting(); //The order here matters: Routing -> Endpoints
app.MapEndpointsFromFeatures();

app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.RoutePrefix = "";
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.Run();
