namespace API.Helpers
{
    // Will be used by different classes so make generic
    // One of these will be ProductsWithTypesAndBrandsSpecification.cs
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count; // Count of items after filters applied
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; } // Count of items after filters applied
        public IReadOnlyList<T> Data { get; set; } // 
    }
}