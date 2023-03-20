// Data Transfer Object is a container for moving data between layers
// Dto's typically contain no business logic
// Below is what we will retutn to the client - a flat object

namespace API.Dtos
{
    public class ProductToReturnDto
    {        
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; } 
        public decimal Price { get; set; } 
        public string PictureUrl { get; set; } 
        public string ProductType { get; set; } 
        public string ProductBrand { get; set; } 
    }
}