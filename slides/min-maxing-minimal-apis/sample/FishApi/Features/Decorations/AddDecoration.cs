using FishApi.Data;
using FluentValidation;

namespace FishApi.Features.Decorations;

public class AddDecoration : Feature
{
    public override void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapPost("/decorations", Handle);
    }

    public async Task<IResult> Handle(Validator validator, FishContext db, Request request)
    {
        await validator.GuardAsync(request);

        var decoration = new Decoration(request.Name)
        {
            Description = request.Description,
            AquariumId = request.AquariumId,
        };

        await db.AddAsync(decoration);
        await db.SaveChangesAsync();
        return Results.Ok();
    }

    public record Request(string Name, string? Description, int? AquariumId = null)
    {
        public string Name { get; } = Name;
        public string? Description { get; } = Description;
        public int? AquariumId { get; } = AquariumId;
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Name).NotEmpty();
        }
    }
}
