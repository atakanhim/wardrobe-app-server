using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wardrobe.Domain.Entities.Common;

namespace wardrobe.Domain.Entities
{
    public class WardrobeItem:BaseEntity
    {

        public Guid WardrobeId { get; set; }
        public virtual Wardrobe Wardrobe { get; set; }

        // Öğe detayları
        public Guid CategoryId { get; set; } // Yabancı anahtar
        public virtual BaseCategory ItemCategory { get; set; } // kategori secimi



        public DateTime AddedDate { get; set; }

        public WardrobeItem()
        {
            AddedDate = DateTime.UtcNow;
        }
    }
}
