namespace Laboratory_Management_API.Models
{
    public class ItemDto
    {
        public int Item_Id { get; set; }
        public string Item_Name { get; set; }
        public string Description { get; set; }
        public int? Category_Id { get; set; }
        public DateTime Created_At { get; set; }
    }

}
