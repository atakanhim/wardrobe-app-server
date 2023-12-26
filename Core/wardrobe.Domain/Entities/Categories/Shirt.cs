using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wardrobe.Domain.Entities.Common;

namespace wardrobe.Domain.Entities.Categories
{
    public class Shirt : BaseCategory
    {
        public string Size { get; set; }
        public string Material { get; set; } // Örneğin, pamuk, polyester vb.
    }
}
