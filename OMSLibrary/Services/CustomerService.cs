using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Repositories;
using OrderManagerLibrary.Services.Interfaces;

namespace OrderManagerLibrary.Services;
public class CustomerService : ICustomerService
{
    private readonly IDataAccess _db;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IOrderService _orderService;

    public CustomerService(IDataAccess dataAccess, IRepository<Customer> customerRepository, IOrderService orderService)
    {
        _db = dataAccess;
        _customerRepository = customerRepository;
        _orderService = orderService;
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        var customers = _customerRepository.GetAll();

        foreach (var customer in customers)
        {
            customer.Orders.AddRange(_orderService.GetAllOrdersByCustomer(customer));
        }

        return customers;
    }

    public Customer GetCustomerById(int id) => _customerRepository.GetById(id);
    public Customer CreateCustomer(Customer customer)
    {
        customer.Id = _customerRepository.Insert(customer);
        return customer;
    }
    public void UpdateCustomer(Customer customer) => _customerRepository.Update(customer);
    public void RemoveCustomer(int id) => _customerRepository.Delete(id);
}
