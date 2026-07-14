namespace WebApplication1.Data.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        public void Update(T entity);
        Task<T?> DeleteAsync(int id);
        IQueryable<T> GetAllAsQueryable();
    }
}
