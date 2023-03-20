using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
            :  base(x => // Bring in from BaseSpecification
                // Call the Search() method from ProductSpecParams
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains
                    (productParams.Search)) && 
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)  
            ) 
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name); // Default ordering

            // Need to pass in the skip and take which are properties of ProductSpecParams
            // PageSize is the take and skip?? 6*(1-1) = 0???? 0 is the skip operator 
            // If page 2 was required the expression would be 6*(2-1) = 6. This is the skip operator and would skip the first page
            ApplyPaging(productParams.PageSize * (productParams.PageIndex -1),
                productParams.PageSize);

            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }    
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}