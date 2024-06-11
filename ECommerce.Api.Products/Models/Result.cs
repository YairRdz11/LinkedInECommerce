namespace ECommerce.Api.Products.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T ResultObject { get; set; }
        public string ErrorMessage { get; set; }

    }
}
