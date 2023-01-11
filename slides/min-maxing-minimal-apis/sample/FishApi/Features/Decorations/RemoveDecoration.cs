using FishApi.Data;

namespace FishApi.Features.Decorations;

public class RemoveDecoration : IFeature
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/decorations/{id:int}", Handle);
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
