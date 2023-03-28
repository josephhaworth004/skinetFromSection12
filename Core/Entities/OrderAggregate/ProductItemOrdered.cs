namespace Core.Entities.OrderAggregate
{

    // We are not replating this to the product entity.
    // It's designed be a snapshot of a product at a moment in time it was ordered
    public class ProductItemOrdered
    {
         // Entity Framework requires a paramterless constructor
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemId, string productName, string pictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductItemId { get; set; }    
        public string ProductName { get; set; }    
        public string PictureUrl { get; set; }    
    }
}