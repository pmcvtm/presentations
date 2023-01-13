using System.Reflection;

namespace FishApi.Features;

public abstract class Feature
{
    public abstract void ConfigureEndpoint(OpinionatedEndpointBuilder builder);
}

public static class WebApplicationFeatureRegistrationExtensions
{
    public static void MapEndpointsFromFeatures(this WebApplication application)
    {
        application.UseEndpoints(endpoints =>
        {
            foreach (var feature in GetFeatures().ToList())
            {
                var builder = new OpinionatedEndpointBuilder(endpoints);
                feature.ConfigureEndpoint(builder);
                builder.Build();
            }
        });
    }

    private static IEnumerable<Feature> GetFeatures()
    {
        var featureTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(typ => typeof(Feature).IsAssignableFrom(typ) && typ.IsClass && !typ.IsAbstract);

        foreach (var featureType in featureTypes)
        {
            if (Activator.CreateInstance(featureType) is Feature feature)
                yield return feature;
        }
    }
}
