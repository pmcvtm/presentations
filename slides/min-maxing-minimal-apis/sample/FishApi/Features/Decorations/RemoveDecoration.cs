using FishApi.Data;

namespace FishApi.Features.Decorations;

public class RemoveDecoration : Feature
{
    public override void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapDelete("/decorations/{id:int}", Handle)
            .WithResponseCode<Decoration>(200, "Decor deleted");
    }

    public async Task<IResult> Handle(int id, FishContext db)
    {
        var existing = await db.Decorations.FindAsync(id);
        if (existing != null)
        {
            db.Decorations.Remove(existing);
            await db.SaveChangesAsync();
        }
        return Results.Ok();
    }
}
