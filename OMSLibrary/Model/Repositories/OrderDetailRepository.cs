using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagerLibrary.Model.Repositories
{
    public class OrderDetailRepository
    {
        private readonly IDataAccess _db;

        public OrderDetailRepository(IDataAccess db)
        {
            _db = db;
        }

        // Method: Retrieve composite order details
        public IEnumerable<OrderDetail> GetOrderDetails()
        {
            var orderDetails = new List<OrderDetail>();

            using SqlConnection connection = _db.GetConnection();
            using (SqlCommand command = new SqlCommand("spOrderDetail_GetAll", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var order = new Order
                    {
                        OrderId = (int)reader["OrderId"],
                        OrderDate = (DateTime)reader["OrderDate"],
                        Status = (OrderStatus)reader["Status"],
                        CustomerId = (int)reader["CustomerId"]
                    };
                    var customer = new Customer
                    {
                        CustomerId = (int)reader["CustomerId"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        PhoneNumber = (string)reader["PhoneNumber"]
                    };
                    var orderLines = new List<OrderLine>(); 
                    var payments = new List<Payment>(); 
                    var orderDetail = new OrderDetail(
                        order,
                        (string)reader["Note"],
                        customer,
                        orderLines,
                        payments,
                        (ICollectionType)reader["CollectionType"] 
                    );
                    orderDetails.Add(orderDetail);
                }
            }
        }
    }
}
