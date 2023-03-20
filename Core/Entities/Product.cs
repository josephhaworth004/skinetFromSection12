namespace Core.Entities
{
    // Entity Framework 2.0: Object Relational Mapper
    // ORM stores objects in database
    
    public class Product : BaseEntity
    {        
        public string Name { get; set; } 
        public string Description { get; set; } 
        public decimal Price { get; set; } 
        public string PictureUrl { get; set; } 

        // Related Entities. Helps out EF. When it creates a new migration,
        // It will know that the product has a relation with Product Type and ProductBrand.
        // Will use this info to set up the relation between them as well as the foreign keys
        public ProductType ProductType { get; set; } 
        public int ProductTypeId { get; set; }  
        public ProductBrand ProductBrand { get; set; } 
        public int ProductBrandId { get; set; }
    }
}

