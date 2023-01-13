using FishApi.Data;

namespace FishApi.Features.Aquariums;

public class RemoveAquarium : Feature
{
    public override void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapDelete("/aquariums/{id:int}", Handle);
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
