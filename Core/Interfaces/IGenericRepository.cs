using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    // T for type. Will be replaced at compile time with required type
    // Is constrained to only work with classes derived from base entity
    // BaseEntity was created in core/Entities
    public interface IGenericRepository<T> where T : BaseEntity 
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();   

        // ANother task that will take a specification as a parameter
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
       
       Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec); 

        // Method to count the number of items
        Task<int> CountAsync(ISpecification<T> spec); // Method to Count all the items and takes an arg with the filters

        // The methods below are not async as we are not using them to directly alter the db
        // Each of these methods implies to the db we will want to do something, so track me (in memory)
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity); 
    }
}