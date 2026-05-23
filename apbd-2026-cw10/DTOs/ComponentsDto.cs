using apbd_2026_cw10.Entities;

namespace apbd_2026_cw10.DTOs;

public class ComponentsDto
{
    public string Code { set; get; }
    public string Name { set; get; }
    public string Description { set; get; }
    public ComponentManufacturerDto ComponentManufacturers { get; set; }
    public ComponentTypeDto ComponentTypes { get; set; }
}