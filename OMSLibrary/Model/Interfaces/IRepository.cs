namespace OrderManagerLibrary.Model.Interfaces;
internal interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task Insert(T entity);
    Task Update(T entity);
    Task Delete(int Id);
}
