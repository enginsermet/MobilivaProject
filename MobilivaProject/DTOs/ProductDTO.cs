namespace MobilivaProject.DTOs
{
    public class ProductDTO
    {
        //Id, Description,Category,Unit,UnitPrice
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Unit { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
