using System.Collections.Generic;

#nullable disable

namespace DataAccess.DataAccess
{
    public partial class Customer
    {
        public Customer()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
