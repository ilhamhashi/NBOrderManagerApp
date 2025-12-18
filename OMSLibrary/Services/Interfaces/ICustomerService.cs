using OrderManagerLibrary.Model.Classes;

namespace OrderManagerLibrary.Services.Interfaces;
public interface ICustomerService
{
    Customer CreateCustomer(Customer customer);
    IEnumerable<Customer> GetAllCustomers();
    Customer? GetCustomerById(int id);
    void RemoveCustomer(int id);
    void UpdateCustomer(Customer customer);
}