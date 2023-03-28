using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable // when transaction finished dipose of context
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}