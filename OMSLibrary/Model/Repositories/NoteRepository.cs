using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;
public class NoteRepository : IRepository<Note>
{
    private readonly ISqlDataAccess _db;

    public NoteRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public void Add(Note entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Note> GetAll()
    {
        throw new NotImplementedException();
    }

    public Note GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Note entity)
    {
        throw new NotImplementedException();
    }
}
