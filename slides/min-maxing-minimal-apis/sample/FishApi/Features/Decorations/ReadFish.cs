using FishApi.Data;

namespace FishApi.Features.Decorations;

public class ReadDecorations : IFeature
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/decorations", HandleGetAll);
        endpoints.MapGet("/decorations/{id:int}", HandleGetOne);
    }

    public Task<IResult> HandleGetAll(FishContext db)
    {
        var decorations = db.Decorations.ToList();
        return Task.FromResult(Results.Ok(decorations));
    }

    public async Task<IResult> HandleGetOne(int id, FishContext db)
    {
        var decoration = await db.Decorations.FindAsync(id);
        return decoration == null
            ? Results.NotFound()
            : Results.Ok(decoration);
    }
}
