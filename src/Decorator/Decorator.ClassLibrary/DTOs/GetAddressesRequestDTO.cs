namespace Decorator.ClassLibrary.Dtos
{
    public class GetAddressesRequestDTO
    {
        public string SearchText { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
