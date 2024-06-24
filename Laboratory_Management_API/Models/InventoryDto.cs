namespace Laboratory_Management_API.Models
{
    public class InventoryDto
    {
        public int Inventory_Id { get; set; }
        public int Item_Id { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
        public DateTime Last_Updated { get; set; }
    }

}
