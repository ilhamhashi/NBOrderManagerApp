namespace OrderManagerLibrary.Model.Interfaces;
public interface ICustomer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}
