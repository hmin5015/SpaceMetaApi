using System.ComponentModel.DataAnnotations;

namespace SpaceMetaApi.Entities;

public class Role : EntityBase
{
    [Required]
    [StringLength(50)]
    public string RoleName { get; set; } = string.Empty;

    [Required]
    public bool IsActive { get; set; } = true;
}
