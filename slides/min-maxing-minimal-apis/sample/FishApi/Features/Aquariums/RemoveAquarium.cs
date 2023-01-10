using FishApi.Data;

namespace FishApi.Features.Aquariums;

public class RemoveAquarium : IFeature
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/aquariums/{id:int}", Handle);
    }

    public async Task<IResult> Handle(int id, FishContext db)
    {
        var existing = await db.Aquariums.FindAsync(id);
        if (existing != null)
        {
            db.Aquariums.Remove(existing);
            await db.SaveChangesAsync();
        }
        return Results.Ok();
    }
}
