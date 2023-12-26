using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wardrobe.Domain.Entities.Common;
using wardrobe.Domain.Entities.Identity;

namespace wardrobe.Domain.Entities
{
    public class Wardrobe:BaseEntity
    {

        // Her gardırop, bir AppUser'a aittir
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }

        // Gardırop öğelerini saklayacak koleksiyon
        public virtual ICollection<WardrobeItem> Items { get; set; }

        // Gardırop kapasitesi (Opsiyonel: Premium kullanıcılar için farklı olabilir)
        public int Capacity { get; set; }

        // Gardırobu tanımlayan diğer özellikler
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        // Opsiyonel: Gardırop özelleştirme seçenekleri
        public string Theme { get; set; } // Örneğin, renk teması veya tasarım

        public Wardrobe()
        {
            Items = new HashSet<WardrobeItem>();
            CreatedDate = DateTime.UtcNow;
            // Varsayılan kapasite ve diğer başlangıç değerleri
        }
            
    }
}
