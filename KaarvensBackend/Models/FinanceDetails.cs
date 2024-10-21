using System.ComponentModel.DataAnnotations;

namespace KaarvensBackend.Models
{
    public class FinanceDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProjectName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        [Required]
        public string Comments { get; set; } = string.Empty;
    }
}
