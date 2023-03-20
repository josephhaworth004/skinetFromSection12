using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    // Implement the interface create din IProductRepository.cs
    // This is the implementation class
    public class ProductRepository : IProductRepository
    {
        // Inject StoreContext
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        // This repository will be interacting with our store context
        // Controllers will use these methods to retrieve the data from db

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            /*
            // Examples of how specifications work
            var typeId=1;

            // The Where method returns IQueryable
            // This doesn't execute agaonst the db at this time. It waits for the ToListAsync()
            // It is building up a list of expressions to query the db against
            // Multiple Where methods can be chained on

            var products = _context.Products
                .Where(x => x.ProductTypeId == typeId).Include(x => x.ProductType).ToListAsync();
            */
            
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}