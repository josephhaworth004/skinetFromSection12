using Core.Entities;

namespace Core.Specifications
{
    // Uses CountAsync() from GenericRepository to populate Pagination.cs
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        // Get the count of items so we can populate it in pagination class
        public ProductsWithFiltersForCountSpecification(ProductSpecParams productParams)
            :  base(x => // Bring in from BaseSpecification
            // Call the Search() method from ProductSpecParams
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains
                    (productParams.Search)) &&
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)  
            ) 
        {

        }
    }
}