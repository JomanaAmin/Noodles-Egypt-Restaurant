namespace WebApplication1.Data.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        public void Update(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> GetAllAsQueryable();
    }
}
