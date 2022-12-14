using System.ComponentModel.DataAnnotations;

namespace SpaceMetaApi.Dtos;

public class RoleDto
{
    [Required]
    [StringLength(50)]
    public string RoleName { get; set; } = string.Empty;

    [Required]
    public bool IsActive { get; set; } = true;
}
