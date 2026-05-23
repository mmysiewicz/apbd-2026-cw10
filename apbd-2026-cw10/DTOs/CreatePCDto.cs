using System.ComponentModel.DataAnnotations;

namespace apbd_2026_cw10.DTOs;

public class CreatePCDto
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public float Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}