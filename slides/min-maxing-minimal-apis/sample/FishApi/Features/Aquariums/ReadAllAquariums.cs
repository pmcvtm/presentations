using FishApi.Data;

namespace FishApi.Features.Aquariums;

public class ReadAllAquariums : Feature
{
    public override void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapGet("/aquariums", HandleGetAll)
            .WithResponseCode<Aquarium[]>(200, "All aquariums");
    }

    public Task<IResult> HandleGetAll(FishContext db)
    {
        var aquariums = db.Aquariums.ToList();
        return Task.FromResult(Results.Ok(aquariums));
    }
}
