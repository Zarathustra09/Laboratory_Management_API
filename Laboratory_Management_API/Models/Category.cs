using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Laboratory_Management_API.Models
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int Category_Id { get; set; }

        [Required]
        [Column("category_name")]
        public string Category_Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("created_at")]
        public DateTime Created_At { get; set; } = DateTime.UtcNow;
    }
}
