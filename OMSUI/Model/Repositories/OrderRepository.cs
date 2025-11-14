using Microsoft.Data.SqlClient;
using OMSDesktopUI.Model.Classes;
using OMSDesktopUI.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMSDesktopUI.Model.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = new List<Order>();
            string query = "SELECT * FROM ORDER";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        (
                            (int)reader["OrderId"],
                            (int)reader["CustomerId"],
                            (DateTime)reader["OrderDate"],
                            (bool)reader["IsDraft"]
                         ));
                    }
                }
            }
            return orders;
        }

        public Order GetById(int id)
        {
            Order order = null;
            string query = "SELECT * FROM ORDER WHERE ORDER = @ORDERId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrderId", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order = new Order
                        (
                            (int)reader["OrderId"],
                            (int)reader["CustomerId"],
                            (DateTime)reader["OrderDate"],
                            (bool)reader["IsDraft"]
                        );
                    }
                }
            }
            return order;
        }

        public void Add(Order entity)
        {
            string query = "INSERT INTO Order (OrderId) VALUES (@OrderId)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrderId", entity.OrderId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int GetLastInsertedId()
        {
            string query = "SELECT CAST(IDENT_CURRENT('ORDER') AS INT)";
            Int32 newId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                newId = (Int32)command.ExecuteScalar();
            }
            return (int)newId;
        }

        public void Update(Order entity)
        {
            string query = "UPDATE ORDER SET OrderId = @OrderId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrderId", entity.OrderId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM ORDER WHERE OrderId = @OrderId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrderId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
