using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wardrobe.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
   
        public string NameSurname { get; set; }

        public bool IsPremiumMember { get; set; }
        public DateTime? PremiumMembershipExpiryDate { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenEndDate { get; set; }

        public virtual Wardrobe Wardrobe { get; set; }
    }
}
