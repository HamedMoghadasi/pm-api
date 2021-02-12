namespace DataAccess.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductModel { get; set; }
        public int CustomerId { get; set; }
        public string SerialNumber { get; set; }
    }
}
