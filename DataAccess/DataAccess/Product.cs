#nullable disable

namespace DataAccess.DataAccess
{
    public partial class Product
    {
        public int Id { get; set; }
        public string ProductModel { get; set; }
        public int CustomerId { get; set; }
        public string SerialNumber { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Log Log { get; set; }
    }
}
