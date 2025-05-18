using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslationManager.Models;

public class Translation
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Key { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<TranslationValue> Values { get; set; } = new List<TranslationValue>();
}

public class TranslationValue
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(10)]
    public string Culture { get; set; } = string.Empty;

    [Required]
    public string Value { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("Translation")]
    public int TranslationId { get; set; }
    public virtual Translation Translation { get; set; } = null!;
}