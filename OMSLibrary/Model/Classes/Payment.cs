namespace OrderManagerLibrary.Model.Classes;
public class Payment
{
    public int PaymentId { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int OrderId { get; set; }
    public int PaymentMethodId { get; set; }
}
