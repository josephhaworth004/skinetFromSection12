using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using API.Dtos;
using AutoMapper;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{
    public class ProductsController : BaseApiController // Derives from framework class called controllerbase. Allows http end-points
    {
        // Get access to db context so we can query db
        // Set up dependency injection on constructor
        // See's 'StoreContext' in constructor and gets the service from program .cs
        // Will have access to all the db methods inside controller
       
        //private readonly IProductRepository _repo;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        public ProductsController(IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IMapper mapper)
        {
            //_context = context; is the Same as this.context. Convention to use '_'
            // _repo = repo;   
            _mapper = mapper;
            _productsRepo = productsRepo;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
        }
        
        //End points
        [HttpGet]
        // Return an action result that can be used as a http response
        // Return a list of products
        // Make async by wrapping ActionResult inside a task
        // Each time a http request is made that will consume a thread on our web server
        // If it takes time for a thread to complete, that thread is now blocked
        // async avoids this. The thread is freed up while that request is processing.
        // Helps concurrent threading

        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            // We don't have a body with this http request
            // Add [FromQuery] tells the api to look for properties in the query string
            [FromQuery]ProductSpecParams productParams)
        {
            // ListAsync declared in IGenericRepo and implemented in GenericRepository 
            // It has (ISpecification<T> spec) as an arg
            // Will create a specification class: ProductsWithTypesAndBrandsSpecification.cs
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);
            
            var products = await _productsRepo.ListAsync(spec); 

            var data = _mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products); 
            
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,
                productParams.PageSize, totalItems, data));
        }

        // To get a specific thing we use pass a route parameter and then send it to the function as an arg
        [HttpGet("{id}")]
        
        // Tell Swagger about status codes
        // Below is an example. It would make verbose code to do this for all http requests
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id) // If a non integer is sent, the [Route] controller will flag an error
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}