using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FishApi.Documentation;

public class GroupEndpointsByUrlFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var urlParts = context.ApiDescription.RelativePath?.Split("/") ?? Array.Empty<string>();

        if (urlParts.Length == 0)
            return;

        var isVersionPart = new Regex("(v)\\d+");
        var resourceName = isVersionPart.IsMatch(urlParts[0])
            ? urlParts[1] : urlParts[0];

        if(!string.IsNullOrWhiteSpace(resourceName))
            operation.Tags = new List<OpenApiTag>
            {
                new() { Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(resourceName.Trim('/')) }
            };
    }
}
