using FishApi.Data;

namespace FishApi.Features.Fishes;

public class ReadFish : Feature
{
    public override void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapGet("/fish/{id:int}", HandleGetOne)
            .WithResponseCode<Fish>(200, "The specified fish");
    }

    public async Task<IResult> HandleGetOne(int id, FishContext db)
    {
        var fish = await db.Fish.FindAsync(id);
        return fish == null
            ? Results.NotFound()
            : Results.Ok(fish);
    }
}
