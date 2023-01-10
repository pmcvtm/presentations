using FishApi.Data;

namespace FishApi.Features.Aquariums;

public class ReadAquarium : IFeature
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/aquariums", HandleGetAll);
        endpoints.MapGet("/aquariums/{id:int}", HandleGetOne);
    }

    public async Task<IResult> HandleGetAll(FishContext db)
    {
        var aquariums = db.Aquariums.ToList();
        return Results.Ok(aquariums);
    }

    public async Task<IResult> HandleGetOne(int id, FishContext db)
    {
        var aquarium = await db.Aquariums.FindAsync(id);
        return aquarium == null
            ? Results.NotFound()
            : Results.Ok(aquarium);
    }
}
