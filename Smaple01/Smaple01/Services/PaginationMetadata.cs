namespace Smaple01.Services
{
    public class PaginationMetadata
    {
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItmes { get; set; }
        public int CurrentPage { get; set; }

        public PaginationMetadata(int pageSize, int totalItmes, int currentPage)
        {
            PageSize = pageSize;
            TotalItmes = totalItmes;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalItmes / (double)PageSize);
        }
    }
}