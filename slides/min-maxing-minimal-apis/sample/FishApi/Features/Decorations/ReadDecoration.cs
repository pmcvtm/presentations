using FishApi.Data;

namespace FishApi.Features.Decorations;

public class ReadDecoration : Feature
{
    public override void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapGet("/decorations/{id:int}", HandleGetOne);
    }

    public async Task<IResult> HandleGetOne(int id, FishContext db)
    {
        var decoration = await db.Decorations.FindAsync(id);
        return decoration == null
            ? Results.NotFound()
            : Results.Ok(decoration);
    }
}
