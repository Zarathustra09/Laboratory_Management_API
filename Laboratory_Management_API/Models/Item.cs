using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Laboratory_Management_API.Models
{
    [Table("items")]
    public class Item
    {
        [Key]
        [Column("item_id")]
        public int Item_Id { get; set; }

        [Required]
        [Column("item_name")]
        public string Item_Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [ForeignKey("Category")]
        [Column("category_id")]
        public int? Category_Id { get; set; }

        [Column("created_at")]
        public DateTime Created_At { get; set; }

        public Category Category { get; set; }
    }
}
