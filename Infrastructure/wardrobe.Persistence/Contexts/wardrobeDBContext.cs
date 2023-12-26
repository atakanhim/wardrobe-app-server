using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wardrobe.Domain.Entities;
using wardrobe.Domain.Entities.Categories;
using wardrobe.Domain.Entities.Common;
using wardrobe.Domain.Entities.Identity;

namespace wardrobe.Persistence.Contexts
{
    public class WardrobeDBContext : IdentityDbContext<AppUser, AppRole, string>
    {
   
        public WardrobeDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Wardrobe> Wardrobes { get; set; }
        public DbSet<WardrobeItem> WardrobeItems { get; set; }
        public DbSet<Shirt> Shirts { get; set; }
        public DbSet<Pants> Pants { get; set; }
        public DbSet<Accessory> Accessories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TPT yapılandırması ve ilişkiler
            modelBuilder.Entity<Shirt>().ToTable("Shirts");
            modelBuilder.Entity<Pants>().ToTable("Pants");
            modelBuilder.Entity<Accessory>().ToTable("Accessories");
            // Kullanıcı ve Gardırop arasındaki ilişkiyi yapılandır
            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Wardrobe)
                .WithOne(w => w.User)
                .HasForeignKey<Wardrobe>(w => w.UserId);

            // Wardrobe ve WardrobeItem arasındaki ilişkiyi yapılandır
            modelBuilder.Entity<Wardrobe>()
                .HasMany(w => w.Items)
                .WithOne(i => i.Wardrobe)
                .HasForeignKey(i => i.WardrobeId);

            // WardrobeItem ve Category arasındaki ilişkiyi yapılandır
            modelBuilder.Entity<WardrobeItem>()
                .HasOne(wi => wi.ItemCategory)
                .WithMany(c => c.WardrobeItems)
                .HasForeignKey(wi => wi.CategoryId);
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
