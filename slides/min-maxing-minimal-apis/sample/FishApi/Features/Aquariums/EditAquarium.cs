using FishApi.Data;

namespace FishApi.Features.Aquariums;

public class EditAquarium : IFeature
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/aquariums/{id:int}", Handle);
    }

    public async Task<IResult> Handle(int id, Aquarium updates, FishContext db)
    {
        var existing = await db.Aquariums.FindAsync(id);
        if (existing == null)
            return Results.NotFound();

        existing.Name = updates.Name;
        existing.Location = updates.Location;
        existing.Capacity = updates.Capacity;

        await db.SaveChangesAsync();
        return Results.Ok();
    }
}
