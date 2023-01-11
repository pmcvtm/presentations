using FishApi.Data;
using FluentValidation;

namespace FishApi.Features.Aquariums;

public class AddAquarium : IFeature
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/aquariums", Handle);
    }

    public async Task<IResult> Handle(Validator validator, FishContext db, Request request)
    {
        await validator.GuardAsync(request);

        var aquarium = new Aquarium(request.Name, request.Location, request.Capacity);

        await db.AddAsync(aquarium);
        await db.SaveChangesAsync();
        return Results.Ok();
    }

    public record Request(string Name, string Location, int Capacity)
    {
        public string Name { get; } = Name;
        public string Location { get; } = Location;
        public int Capacity { get; } = Capacity;
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(a => a.Name).NotEmpty();
            RuleFor(a => a.Location).NotEmpty();
            RuleFor(a => a.Capacity).GreaterThan(0);
        }
    }
}
