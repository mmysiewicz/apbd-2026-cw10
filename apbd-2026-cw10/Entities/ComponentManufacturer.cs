using System.ComponentModel.DataAnnotations;

namespace apbd_2026_cw10.Entities;

public class ComponentManufacturer
{
    public int Id { get; set; }
    public string Abbreviation {set;get;} = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime FoundationDate { get; set; }

    public ICollection<Component> Components { get; set; } = [];
}