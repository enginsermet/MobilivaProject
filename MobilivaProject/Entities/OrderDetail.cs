namespace MobilivaProject.Entities
{
    public class OrderDetail
    {
        //Id, OrderId, ProductId, UnitPrice,
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual Order? Order { get; set; }
    }
}
