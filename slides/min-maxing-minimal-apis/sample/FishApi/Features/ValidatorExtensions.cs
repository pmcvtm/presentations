using FluentValidation;

namespace FishApi.Features;

public static class ValidatorExtensions
{
    public static async Task GuardAsync<TRequest>(this IValidator<TRequest> validator, TRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}
