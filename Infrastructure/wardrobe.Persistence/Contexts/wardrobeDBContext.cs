using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wardrobe.Domain.Entities.Identity;

namespace wardrobe.Persistence.Contexts
{
    public class WardrobeDBContext : IdentityDbContext<AppUser, AppRole, string>
    {
   
        public WardrobeDBContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
    }
}
