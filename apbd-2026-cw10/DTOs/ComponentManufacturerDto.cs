namespace apbd_2026_cw10.DTOs;

public class ComponentManufacturerDto
{
    public int Id { get; set; }
    public string Abbreviation {set;get;} = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime FoundationDate { get; set; }
}