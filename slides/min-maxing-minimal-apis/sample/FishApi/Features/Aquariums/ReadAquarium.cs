using FishApi.Data;

namespace FishApi.Features.Aquariums;

public class ReadAquarium : Feature
{
    public override void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapGet("/aquariums/{id:int}", HandleGetOne)
            .WithResponseCode<Aquarium>(200, "The specified aquarium");
    }

    public async Task<IResult> HandleGetOne(int id, FishContext db)
    {
        var aquarium = await db.Aquariums.FindAsync(id);
        return aquarium == null
            ? Results.NotFound()
            : Results.Ok(aquarium);
    }
}
