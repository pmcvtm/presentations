using FishApi.Data;

namespace FishApi.Features.Fishes;

public class RemoveFish : IFeature
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/fish/{id:int}", Handle);
    }

    public async Task<IResult> Handle(int id, FishContext db)
    {
        var existing = await db.Fish.FindAsync(id);
        if (existing != null)
        {
            db.Fish.Remove(existing);
            await db.SaveChangesAsync();
        }
        return Results.Ok();
    }
}
