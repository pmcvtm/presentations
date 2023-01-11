using FishApi.Data;
using FluentValidation;

namespace FishApi.Features.Fishes;

public class EditFish : IFeature
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/fish/{id:int}", Handle);
    }

    public async Task<IResult> Handle(int id, Request request, Validator validator, FishContext db)
    {
        await validator.GuardAsync(request);
        var existing = await db.Fish.FindAsync(id);
        if (existing == null)
            return Results.NotFound();

        existing.Name = request.Name;
        existing.Variety = request.Variety;
        existing.AquariumId = request.AquariumId;

        await db.SaveChangesAsync();
        return Results.Ok();
    }

    public record Request(string Name, string Variety, int? AquariumId)
    {
        public string Name { get; } = Name;
        public string Variety { get; } = Variety;
        public int? AquariumId { get; } = AquariumId;
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(a => a.Name).NotEmpty();
            RuleFor(a => a.Variety).NotEmpty();
        }
    }
}
