using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagerLibrary.Model.Repositories
{
    public class OrderLineRepository : IRepository<OrderLine>
    {
        public Task Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderLine>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OrderLine?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Insert(OrderLine entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(OrderLine entity)
        {
            throw new NotImplementedException();
        }
    }
}
