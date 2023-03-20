using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T> // BaseSpecification.cs will implement this interface
    {
        // Generic Methods
        // Criteria is where our "Where" method will be
        Expression<Func<T, bool>> Criteria { get; }    
        List<Expression<Func<T, object>>> Includes { get; } // object is the most generic return type
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        int Take {get;} // for use with pagination. Will be able to take a certain amount of products
        int Skip {get;} // will be able to skips some products
        bool IsPagingEnabled {get;}
    }
}