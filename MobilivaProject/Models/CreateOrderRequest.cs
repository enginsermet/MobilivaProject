using MobilivaProject.Entities;

namespace MobilivaProject.Models
{
    public class CreateOrderRequest
    {
        //CustomerName, CustomerEmail, CustomerGSM, List<ProductDetail>. 
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerGSM { get; set; }
        public ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
