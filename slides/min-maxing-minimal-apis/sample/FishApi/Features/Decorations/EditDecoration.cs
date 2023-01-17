using FishApi.Data;
using FluentValidation;

namespace FishApi.Features.Decorations;

public class EditDecoration : Feature
{
    public override void ConfigureEndpoint(OpinionatedEndpointBuilder builder)
    {
        builder.MapPut("/decorations/{id:int}", Handle)
            .WithResponseCode<Decoration>(200, "Decoration updated");
    }

    public async Task<IResult> Handle(int id, Request request, Validator validator, FishContext db)
    {
        await validator.GuardAsync(request);
        var existing = await db.Decorations.FindAsync(id);
        if (existing == null)
            return Results.NotFound();

        existing.Name = request.Name;
        existing.Description = request.Description;
        existing.AquariumId = request.AquariumId;

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
