using FishApi.Data;

namespace FishApi.Features.Fishes;

public class ReadAllFish : Feature
{
    public override void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapGet("/fish", HandleGetAll);
    }

    public Task<IResult> HandleGetAll(FishContext db)
    {
        var fishes = db.Fish.ToList();
        return Task.FromResult(Results.Ok(fishes));
    }
}
