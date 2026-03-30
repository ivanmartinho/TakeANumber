namespace TakeANumberShared.ViewModels
{
    public class PagedViewModel<T>
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public T Data { get; set; }

        public PagedViewModel() { }
    }
}
