namespace Laboratory_Management_API.Models
{
    public class TransactionDto
    {
        public int Transaction_Id { get; set; }
        public int Item_Id { get; set; }
        public int User_Id { get; set; }
        public string Transaction_Type { get; set; }
        public int Quantity { get; set; }
        public DateTime Transaction_Date { get; set; }
        public string Notes { get; set; }
    }

}
