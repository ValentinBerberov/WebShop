using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Data.Entities
{
    public class ProductCategory
    {
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
