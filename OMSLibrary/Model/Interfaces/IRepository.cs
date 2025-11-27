namespace OrderManagerLibrary.Model.Interfaces;
public interface IRepository<T> where T : class
{
    int Insert(T entity);
    void Update(T entity);
    void Delete(params object[] keyValues);
    T GetById(params object[] keyValues);
    IEnumerable<T> GetAll();
}
