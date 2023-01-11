using FishApi.Data;
using FluentValidation;

namespace FishApi.Features.Aquariums;

public class EditAquarium : IFeature
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/aquariums/{id:int}", Handle);
    }

    public async Task<IResult> Handle(int id, Request request, Validator validator, FishContext db)
    {
        await validator.GuardAsync(request);
        var existing = await db.Aquariums.FindAsync(id);
        if (existing == null)
            return Results.NotFound();

        existing.Name = request.Name;
        existing.Location = request.Location;
        existing.Capacity = request.Capacity;

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
