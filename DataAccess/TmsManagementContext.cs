using DataAccess.DataAccess;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
#nullable disable

namespace DataAccess
{
    public partial class TmsManagementContext : IdentityDbContext<ApplicationUser>
    {
        private IConfiguration Configuration;
        public TmsManagementContext()
        {
        }

        public TmsManagementContext(DbContextOptions<TmsManagementContext> options, IConfiguration _configuration)
            : base(options)
        {
            Configuration = _configuration;
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Strategy> Strategies { get; set; }
        public virtual DbSet<UpdateSpecification> UpdateSpecifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Log");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.UpdateRecievedDate).HasColumnType("datetime");

                entity.Property(e => e.VersionNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Log)
                    .HasForeignKey<Log>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Log_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ProductModel)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Product");
            });

            modelBuilder.Entity<Strategy>(entity =>
            {
                entity.ToTable("Strategy");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateRange)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DeviceFirmwareVersion)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NumberSegment).IsRequired();

                entity.Property(e => e.ServerFirmwareVersion)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StrategyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TimeLimit)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.UpdateSpecification)
                    .WithMany(p => p.Strategies)
                    .HasForeignKey(d => d.UpdateSpecificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Strategy_Strategy");
            });

            modelBuilder.Entity<UpdateSpecification>(entity =>
            {
                entity.ToTable("UpdateSpecification");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FilePath).IsRequired();

                entity.Property(e => e.VersionNumber)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
