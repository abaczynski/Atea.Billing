namespace Billing.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentGateways PaymentProvider { get; set; }
        public string Description { get; set; }
    }
}
