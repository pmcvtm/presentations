using FishApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FishContext>((_, opt) =>
    opt.UseSqlite($"Data Source={builder.Configuration.GetValue<string>("DbPath")}"));

var app = builder.Build();

app.MapGet("/aquariums", (FishContext db) => db.Aquariums.ToList());
app.MapGet("/aquariums/{id:int}", async (int id, FishContext db) => await db.Aquariums.FindAsync(id));
app.MapPost("/aquariums", async (Aquarium aquarium, FishContext db) =>
{
    await db.AddAsync(aquarium);
    await db.SaveChangesAsync();
    return Results.Ok();
});
app.MapPut("/aquariums/{id:int}", async (int id, Aquarium updates, FishContext db) =>
{
    var existing = await db.Aquariums.FindAsync(id);
    if (existing == null)
        return Results.NotFound();

    existing.Name = updates.Name;
    existing.Location = updates.Location;
    existing.Capacity = updates.Capacity;

    await db.SaveChangesAsync();
    return Results.Ok();
});
app.MapDelete("/aquariums/{id:int}", async (int id, FishContext db) =>
{
    var existing = await db.Aquariums.FindAsync(id);
    if (existing != null) db.Aquariums.Remove(existing);
    return Results.Ok();
});

app.Run();