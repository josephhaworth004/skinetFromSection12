namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex {get; set;} = 1; // {get; set;} is an "auto property"
        private int _pagesize = 6;
        
        // Using a "full property"
        public int PageSize
        {
            get => _pagesize;
            set => _pagesize = (value > MaxPageSize) ? MaxPageSize : value;
        }    

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set; }
        
        // Backing field
        private string _search;

        public string Search
        {
            get => _search;
            set => _search = value.ToLower(); // Convert to lower case
        }


    }
}