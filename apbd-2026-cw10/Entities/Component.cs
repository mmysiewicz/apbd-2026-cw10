using System.ComponentModel.DataAnnotations;

namespace apbd_2026_cw10.Entities;

public class Component
{
    public string Code { set; get; }
    public string Name { set; get; }
    public string Description { set; get; }
    public int ComponentManufacturersId { set; get; }
    public int ComponentTypesId { set; get; }
    
    public ComponentManufacturer ComponentManufacturer { get; set; }
    public ComponentType ComponentType { get; set; }
    
    public ICollection<PCComponent> PCComponents { get; set; } = [];
}