namespace MobilivaProject.Entities
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        //Id, CustomerName, CustomerEmail, CustomerGSM, TotalAmount
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerGSM { get; set; }
        public int Amount { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
