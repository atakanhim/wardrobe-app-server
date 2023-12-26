using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wardrobe.Domain.Entities.Common;

namespace wardrobe.Domain.Entities.Categories
{
    public class Accessory : BaseCategory
    {
        public string Type { get; set; } // Örneğin, saat, kolye vb.
    }
}
