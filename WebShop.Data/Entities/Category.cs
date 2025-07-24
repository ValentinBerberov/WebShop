using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Data.Entities.Abstractions;

namespace WebShop.Data.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public virtual IEnumerable<ProductCategory>? ProductCategories { get; set; }
    }
}
