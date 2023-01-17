namespace FishApi.Features;

public static class StringExtensions
{
    public static string ToSingular(this string input) =>
        input switch
        {
            "fish" => "fish",
            "fishes" => "fish",
            _ => input.Remove(input.Length - 1, 1)
        };
}
