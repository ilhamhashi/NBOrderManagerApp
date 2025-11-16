namespace OrderManagerLibrary.Model.Classes
{
    public class MobilePayment : IPaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string Name { get; set; }
    }
}
