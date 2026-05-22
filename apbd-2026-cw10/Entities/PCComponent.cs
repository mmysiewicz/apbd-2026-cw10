using System.ComponentModel.DataAnnotations;

namespace apbd_2026_cw10.Entities;

public class PCComponent
{
    public int PCId { get; set; }
    public string ComponentCode { get; set; }
    public int Amount { get; set; }
    
    public PC PC { get; set; } = null!;
    public Component Component { get; set; } = null!;
    
}