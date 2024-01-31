using MedicalEquipmentWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MedicalEquipmentWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        DbSet<Attachment> Attachments { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Feedback> Feedbacks { get; set; }
        DbSet<MedicalEquipment> MedicalEquipments { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Order> OrderItems { get; set; }
        DbSet<PaymentMethod> PaymentMethods { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<UnlockedMedicalEquipment> UnlockedMedicalEquipments { get; set; }
        DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("User");
            //builder.Entity<User>().Ignore(e => e.LockoutEnabled);
            //builder.Entity<User>().Ignore(e => e.AccessFailedCount);
            //builder.Entity<User>().Ignore(e => e.SecurityStamp);
            //builder.Entity<User>().Ignore(e => e.LockoutEnd);
            builder.Entity<IdentityRole>().ToTable("Role");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<Cart>().HasKey(c => new { c.MedicalEquipmentId, c.UserId });
            builder.Entity<UnlockedMedicalEquipment>().HasKey(u => new { u.UserId, u.MedicalEquipmentId });
            builder.Entity<User>()
                .HasMany(u => u.UnlockedUsers)
                .WithMany(u => u.Unlocking)
                .UsingEntity(j => j.ToTable("UserUnlocks"));
            SeedRole(builder);
            SeedCategory(builder);
            SeedPaymentMethod(builder);
        }
        private void SeedRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
              new IdentityRole() { Id = "1", Name = "USER", ConcurrencyStamp = "1", NormalizedName = "USER" },
              new IdentityRole() { Id = "2", Name = "ADMIN", ConcurrencyStamp = "2", NormalizedName = "ADMIN" }
            );
        }
        private void SeedCategory(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Ultrasound Machine"
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Testers"
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Sphygmomanometer"
                },
                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Endoscopy Machine"
                }
            );
        }
        private void SeedPaymentMethod(ModelBuilder builder)
        {
            builder.Entity<PaymentMethod>().HasData(
                new PaymentMethod
                {
                    PaymentMethodId = 1,
                    PaymentMethodName = "CASH"
                },
                new PaymentMethod
                {
                    PaymentMethodId = 2,
                    PaymentMethodName = "VNPAYQR"
                },
                new PaymentMethod
                {
                    PaymentMethodId = 3,
                    PaymentMethodName = "VNBANK"
                },
                new PaymentMethod
                {
                   PaymentMethodId = 4,
                   PaymentMethodName = "INTBANK"
                }
            );
        }

    }
}
