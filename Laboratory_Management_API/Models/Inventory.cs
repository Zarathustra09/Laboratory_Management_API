using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Laboratory_Management_API.Models
{
    [Table("inventory")]
    public class Inventory
    {
        [Key]
        [Column("inventory_id")]
        public int Inventory_Id { get; set; }

        [ForeignKey("Item")]
        [Column("item_id")]
        public int Item_Id { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("location")]
        public string Location { get; set; }

        [Column("last_updated")]
        public DateTime Last_Updated { get; set; }

        public Item Item { get; set; }
    }
}
