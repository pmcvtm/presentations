using FishApi.Data;

namespace FishApi.Features.Fishes;

public class RemoveFish : Feature
{
    public override void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapDelete("/fish/{id:int}", Handle);
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
