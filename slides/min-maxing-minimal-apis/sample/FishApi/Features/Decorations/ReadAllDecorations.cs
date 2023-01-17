using FishApi.Data;

namespace FishApi.Features.Decorations;

public class ReadAllDecorations : Feature
{
    public override void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapGet("/decorations", HandleGetAll)
            .WithResponseCode<Decoration[]>(200, "All decorations");
    }

    public Task<IResult> HandleGetAll(FishContext db)
    {
        var decorations = db.Decorations.ToList();
        return Task.FromResult(Results.Ok(decorations));
    }
}
