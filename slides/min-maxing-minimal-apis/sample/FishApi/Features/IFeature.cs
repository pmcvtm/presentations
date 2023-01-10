using System.Reflection;

namespace FishApi.Features;

public interface IFeature
{
    void MapEndpoints(IEndpointRouteBuilder endpoints);
}

public static class WebApplicationFeatureRegistrationExtensions
{
    public static void MapEndpointsFromFeatures(this WebApplication application)
    {
        application.UseEndpoints(endpoints =>
        {
            foreach (var feature in GetFeatures().ToList())
                feature.MapEndpoints(endpoints);
        });
    }

    private static IEnumerable<IFeature> GetFeatures()
    {
        var featureTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(typ => typeof(IFeature).IsAssignableFrom(typ) && typ.IsClass);

        foreach (var featureType in featureTypes)
        {
            if (Activator.CreateInstance(featureType) is IFeature feature)
                yield return feature;
        }
    }
}
