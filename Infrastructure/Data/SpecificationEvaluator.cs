using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    
    // <TEntity> is replaced by product
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, 
            ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                //Get a product for the selected criteria
                // I.E. p => p.ProductTypeId == id
                query = query.Where(spec.Criteria);
            }

            // Ordering is important. We can't sort (or page) before we've filtered
            // as we wouldn't know which subset of data we had.
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            // Ordering is important. We can't page (or sort) before we've filtered
            // as we wouldn't know which subset of data we had.
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            } //end-if


            /* Code below takes out includes...
                 .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
            and aggregates them into out query
            */
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            // Then pass the query to a method that will query the database
            return query;
        }    
        
    }
}