using FishApi.Data;

namespace FishApi.Features.Fishes;

public class ReadAllFish : Feature
{
    public override void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapGet("/fish", HandleGetAll)
            .WithResponseCode<Fish[]>(200, "All fish");
    }

    public Task<IResult> HandleGetAll(FishContext db)
    {
        var fishes = db.Fish.ToList();
        return Task.FromResult(Results.Ok(fishes));
    }
}
