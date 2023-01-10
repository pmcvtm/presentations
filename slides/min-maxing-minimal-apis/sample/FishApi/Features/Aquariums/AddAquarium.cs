using FishApi.Data;

namespace FishApi.Features.Aquariums;

public class AddAquarium : IFeature
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/aquariums", Handle);
    }

    public async Task<IResult> Handle(FishContext db, Aquarium aquarium)
    {
        await db.AddAsync(aquarium);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
}
