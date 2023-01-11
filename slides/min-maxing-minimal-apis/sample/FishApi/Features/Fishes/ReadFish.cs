using FishApi.Data;

namespace FishApi.Features.Fishes;

public class ReadFish : IFeature
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/fish", HandleGetAll);
        endpoints.MapGet("/fish/{id:int}", HandleGetOne);
    }

    public Task<IResult> HandleGetAll(FishContext db)
    {
        var fishes = db.Fish.ToList();
        return Task.FromResult(Results.Ok(fishes));
    }

    public async Task<IResult> HandleGetOne(int id, FishContext db)
    {
        var fish = await db.Fish.FindAsync(id);
        return fish == null
            ? Results.NotFound()
            : Results.Ok(fish);
    }
}
