using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SpaceMetaApi.Entities;

public class EntityBase
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [JsonIgnore]
    [StringLength(50, ErrorMessage = "Created By can't be exceed more than 50")]
    public string CreatedBy { get; set; } = "System";

    [Required]
    [JsonIgnore]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    [Required]
    [JsonIgnore]
    [StringLength(50, ErrorMessage = "Modified By can't be exceed more than 50")]
    public string? ModifiedBy { get; set; }

    [Required]
    [JsonIgnore]
    public DateTime? ModifiedOn { get; set; }
}
