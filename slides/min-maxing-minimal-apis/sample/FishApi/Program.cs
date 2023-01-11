using System.Reflection;
using FishApi.Data;
using FishApi.Features;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FishContext>((_, opt) =>
    opt.UseSqlite($"Data Source={builder.Configuration.GetValue<string>("DbPath")}"));

builder.Services.AddValidatorsFromAssemblies(new []{ Assembly.GetExecutingAssembly() });

var app = builder.Build();

app.UseRouting(); //The order here matters: Routing -> Endpoints
app.MapEndpointsFromFeatures();


app.Run();