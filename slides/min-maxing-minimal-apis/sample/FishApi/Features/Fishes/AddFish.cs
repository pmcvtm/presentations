using FishApi.Data;
using FluentValidation;

namespace FishApi.Features.Fishes;

public class AddFish : IFeature
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/fish", Handle);
    }

    public async Task<IResult> Handle(Validator validator, FishContext db, Request request)
    {
        await validator.GuardAsync(request);

        var fish = new Fish(request.Name, request.Variety)
        {
            AquariumId = request.AquariumId,
        };

        await db.AddAsync(fish);
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
