using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wardrobe.Domain.Entities.Common
{
    public abstract class BaseCategory:BaseEntity
    {
        public virtual ICollection<WardrobeItem> WardrobeItems { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

    }
}
