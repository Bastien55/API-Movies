namespace API_Movies.Repository.Interface
{
    public interface IRepositoryService<T>
    {
        IEnumerable<T> GetAll();
        T GetByID(int id);

        void Save(T entity);

        void Delete(T entity);
    }
}
