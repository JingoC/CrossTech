using System.Threading.Tasks;

namespace CrossTech.Core.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);

        Task<TEntity> InsertAsync(TEntity entity);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
