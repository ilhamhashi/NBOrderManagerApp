namespace OrderManagerLibrary.Model.Interfaces;
public interface IRepository<T> where T : class
{
    int Insert(T entity);
    void Update(T entity);
    void Delete(int id);
    T GetById(int id);
    IEnumerable<T> GetAll();
}
