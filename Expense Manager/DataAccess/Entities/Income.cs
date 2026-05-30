using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Manager.DataAccess.Entities
{
    public class Income
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(0.01, 1000000)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;
        [Required ]
        public DateTime Date { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

    }
}
