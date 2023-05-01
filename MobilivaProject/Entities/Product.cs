namespace MobilivaProject.Entities
{
    public class Product
    {
        //Id, Description, Category, Unit, UnitPrice, Status, CreateDate, UpdateDate
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public string Status { get; set; }
        public DateOnly CreateDate { get; set; }
        public DateOnly UpdateDate { get; set; }
    }
}
