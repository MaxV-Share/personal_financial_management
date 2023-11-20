using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Models.Entities.Identities;

namespace PersonalFinancialManagement.Models.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<TransactionCategoryType> TransactionCategoryTypes { set; get; }
        public virtual DbSet<TransactionCategory> TransactionCategories { set; get; }
        public virtual DbSet<Transaction> Transactions { set; get; }
        public virtual DbSet<Currency> Currencies { set; get; }
        public virtual DbSet<PaymentAccountType> PaymentAccountTypes { set; get; }
        public virtual DbSet<PaymentAccount> PaymentAccounts { set; get; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TransactionCategoryType>().HasQueryFilter(p => p.Deleted == null);
            builder.Entity<TransactionCategory>().HasQueryFilter(p => p.Deleted == null);
            builder.Entity<Transaction>().HasQueryFilter(p => p.Deleted == null);
            builder.Entity<Currency>().HasQueryFilter(p => p.Deleted == null);
            builder.Entity<PaymentAccountType>().HasQueryFilter(p => p.Deleted == null);
            builder.Entity<PaymentAccount>().HasQueryFilter(p => p.Deleted == null);

            builder.Entity<User>()
                .HasMany(e => e.UserRoles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserRole>()
                .HasOne(e => e.User)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.UserId);

            builder.Entity<UserRole>()
                .HasOne(e => e.Role)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.RoleId);

            builder.Entity<UserClaim>()
                .HasOne(e => e.User)
                .WithMany(e => e.UserClaims)
                .HasForeignKey(e => e.UserId);

            builder.Entity<RoleClaim>()
                .HasOne(e => e.Role)
                .WithMany(e => e.RoleClaims)
                .HasForeignKey(e => e.RoleId);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Method intentionally left empty.
            //optionsBuilder.UseLazyLoadingProxies();
            //optionsBuilder.ConfigureWarnings(w => w.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
        }
    }
}
