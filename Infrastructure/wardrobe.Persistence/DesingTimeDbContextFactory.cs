using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wardrobe.Persistence.Contexts;

namespace wardrobe.Persistence
{
    public class DesingTimeDbContextFactory : IDesignTimeDbContextFactory<WardrobeDBContext>
    {
        public WardrobeDBContext CreateDbContext(string[] args)
        {
            //egerki talimat powershell üzerinden geliyorsa ,
            //hangi options parametlerini default olarak kabul etmesi gerektigini belirtiyor.

            DbContextOptionsBuilder<WardrobeDBContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);
            return new WardrobeDBContext(dbContextOptionsBuilder.Options); // burda dbconteximizde options verdik
        }
    }
}
