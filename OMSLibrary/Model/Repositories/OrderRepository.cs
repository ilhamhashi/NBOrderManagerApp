using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Classes;

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

    public Task Insert(Order entity) =>
        _db.SaveData(storedProcedure: "", new  { entity });
    

    public Task Update(Order entity) => 
        _db.SaveData(storedProcedure: "", entity);


    public Task Delete(int Id) => 
        _db.SaveData(storedProcedure: "", new { Id });
}

