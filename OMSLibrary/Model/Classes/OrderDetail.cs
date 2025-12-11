using OrderManagerLibrary.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagerLibrary.Model.Classes
{
    public class OrderDetail
    {
        public Order Order { get; set; }
        public string Note { get; set; }
        public Customer Customer { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public List<Payment> Payments { get; set; } // List??
        public ICollectionType Collection { get; set; }

        public OrderDetail(Order order, string note, Customer customer, List<OrderLine> orderLines, List<Payment> payments, ICollectionType collection)
        {
            Order = order;
            Note = note;
            Customer = customer;
            OrderLines = orderLines;
            Payments = payments;
            Collection = collection;
        }
    }
}
