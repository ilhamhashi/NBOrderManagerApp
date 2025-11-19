using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Classes;
using Dapper;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;

internal class OrderRepository : IRepository<Order>
{
    private readonly ISqlDataAccess _db;

    public OrderRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<Order>> GetAll() =>
        _db.LoadData<Order, dynamic>(storedProcedure:"dbo.spOrder_GetAll", new { });

    public async Task<Order?> GetById(int id)
    {
        var results = await _db.LoadData<Order, dynamic>(
            storedProcedure: "dbo.spOrder_GetById",
            new { Id = id });
        return results.FirstOrDefault();
    }

    public async Task<int?> Insert(Order entity)
    {
        var p = new DynamicParameters();
        p.Add("@OrderId", DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@OrderDate",entity.OrderDate);
        p.Add("@CustomerId", entity.CustomerId);
        p.Add("@IsDraft", entity.IsDraft);

        await _db.SaveData(storedProcedure: "dbo.spOrder_Insert", p);
        int newId = p.Get<int>("@OrderId");
        return newId;

            public Task Insert(Order entity) =>
        _db.SaveData(storedProcedure: "", new { entity.OrderDate, entity.CustomerId, entity.IsDraft });
    }

    public Task Update(Order entity) => 
        _db.SaveData(storedProcedure: "", entity);


    public Task Delete(int Id) => 
        _db.SaveData(storedProcedure: "", new { Id });
}
