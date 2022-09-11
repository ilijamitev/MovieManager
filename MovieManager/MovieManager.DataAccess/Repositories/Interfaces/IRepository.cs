using MovieManager.Domain;

namespace MovieManager.DataAccess.Repositories.Interfaces;
public interface IRepository<T> where T : BaseEntity
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Filter(Func<T, bool> filter);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);

}
