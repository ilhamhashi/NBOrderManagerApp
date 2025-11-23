using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;
public class NoteRepository : IRepository<Note>
{
    public Task Delete(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Note>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Note?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Insert(Note entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Note entity)
    {
        throw new NotImplementedException();
    }
}
