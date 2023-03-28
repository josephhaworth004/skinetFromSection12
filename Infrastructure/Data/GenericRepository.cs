using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    // Needs adding to services container in program.cs
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        // Constructor
        private readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            // Use set to assign the type to <T>
            return await _context.Set<T>().FindAsync(id);        
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();    
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            // spec is IQueryable
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        //Implements method from IGenericRepository.
        // ProductsWithFiltersForCountSpecification is a new spec to use this method in our repository
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync(); 
        }
        
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            // _context.Set<T> will return a product which is converted into a queryable
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity); // Add here will save to the database when SaveChanges() is called
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

         public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}

