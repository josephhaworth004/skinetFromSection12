using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        // A Concrete class (the repository) in the Data folder will use the below asynchronously.
        // That is called ProductRepository.cs and is an "implementation class"
        Task<Product> GetProductByIdAsync(int id); 
        
        // Previously we've just been returning a list. 
        // IReadOnlyList can only be read. We don't need the functionality that comes with a normal list 
        Task<IReadOnlyList<Product>> GetProductsAsync();   
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();   
    } 
}