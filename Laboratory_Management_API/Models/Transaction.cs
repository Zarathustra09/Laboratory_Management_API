using Laboratory_Management_API.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Laboratory_Management_API.Models
{
    [Table("transactions")]
    public class Transaction
    {
        [Key]
        [Column("transaction_id")]
        public int Transaction_Id { get; set; }

        [ForeignKey("Item")]
        [Column("item_id")]
        public int Item_Id { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int User_Id { get; set; }

        [Column("transaction_type")]
        public string Transaction_Type { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("transaction_date")]
        public DateTime Transaction_Date { get; set; }

        [Column("notes")]
        public string Notes { get; set; }

        public Item Item { get; set; }
        public User User { get; set; }
    }
}
