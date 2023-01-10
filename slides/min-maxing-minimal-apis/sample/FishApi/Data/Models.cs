using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishApi.Data;

public class Aquarium
{
    public Aquarium(string name, string location, int capacity)
    {
        Name = name;
        Location = location;
        Capacity = capacity;
        Cleanliness = AquariumCleanliness.Clean;
    }

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }

    [Required] public string Name { get; set; }
    [Required] public string Location { get; set; }
    [Required] public int Capacity { get; set; }
    [Required] public AquariumCleanliness Cleanliness { get; set; }
}

public enum AquariumCleanliness { Clean = 1, Dirty = 2, }

public class Fish
{
    public Fish(string name, string variety)
    {
        Name = name;
        Variety = variety;
        Status = FishStatus.Content;
    }

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }
    public int? AquariumId { get; set; }

    [Required] public string Name { get; set; }
    [Required] public string Variety { get; set; }
    [Required] public FishStatus Status { get; set; }

    [ForeignKey("AquariumId")]
    public Aquarium? Aquarium { get; set; }
}

public class Decoration
{
    public Decoration(string name) => Name = name;

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int? AquariumId { get; set; }

    [Required] public string Name { get; set; }

    public string Description { get; set; }

    [ForeignKey("AquariumId")]
    public Aquarium? Aquarium { get; set; }
}
