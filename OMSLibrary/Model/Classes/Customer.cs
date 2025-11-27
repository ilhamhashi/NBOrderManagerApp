using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Classes;
public class Customer : ICustomer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }

    public Customer(int personId, string firstName, string lastName, string phoneNumber)
    {
        CustomerId = personId;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }

    public Customer(string firstName, string lastName, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }
}
