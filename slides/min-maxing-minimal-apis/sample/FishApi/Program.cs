using FishApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FishContext>((_, opt) =>
    opt.UseSqlite($"Data Source={builder.Configuration.GetValue<string>("DbPath")}"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();