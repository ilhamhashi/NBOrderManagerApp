using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;
public class NoteRepository : IRepository<Note>
{
    private readonly SqlConnection _connection;

    public NoteRepository(IConfiguration config)
    {
        _connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
    }

    public void Delete(params object[] keyValues)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Note> GetAll()
    {
        throw new NotImplementedException();
    }

    public Note GetById(params object[] keyValues)
    {
        throw new NotImplementedException();
    }

    public int Insert(Note entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Note entity)
    {
        throw new NotImplementedException();
    }
}
