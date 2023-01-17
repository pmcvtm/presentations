using FishApi.Data;
using FluentValidation;

namespace FishApi.Features.Aquariums;

public class AddAquarium : Feature
{
    public override void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapPost("/aquariums", Handle)
            .WithResponseCode<Aquarium>(201, "Aquarium created");
    }

    public async Task<IResult> Handle(Validator validator, FishContext db, Request request)
    {
        await validator.GuardAsync(request);

        var aquarium = new Aquarium(request.Name, request.Location, request.Capacity);

        await db.AddAsync(aquarium);
        await db.SaveChangesAsync();
        return Results.Created($"/aquariums/{aquarium.Id}", aquarium);
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
