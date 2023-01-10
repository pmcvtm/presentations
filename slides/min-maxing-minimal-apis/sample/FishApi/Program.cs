using FishApi.Data;
using FishApi.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FishContext>((_, opt) =>
    opt.UseSqlite($"Data Source={builder.Configuration.GetValue<string>("DbPath")}"));

var app = builder.Build();

app.UseRouting(); //The order here matters: Routing -> Endpoints
app.MapEndpointsFromFeatures();


app.Run();