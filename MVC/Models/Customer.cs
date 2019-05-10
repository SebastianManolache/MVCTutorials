namespace MVC.Models
{
    public class Customer
    {
        public string Address { get; set; }
        public string CustomerName { get; set; }
        public override string ToString() => CustomerName + "|" + Address;
    }
}
