using System.Data;
using GenerateMock.Dal.Models.DB;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace GenerateMock.Dal.Context
{
    public class PublicContext : DbContext
    {
        private readonly string _connectionString;

        public PublicContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PublicContext(DbContextOptions<PublicContext> options, string connectionString)
            : base(options)
        {
            _connectionString = connectionString;
        }

        internal IDbConnection Connection => new NpgsqlConnection(_connectionString);

        public virtual DbSet<UserDb> User { get; set; }
        public virtual DbSet<RepositoryDb> Repository { get; set; }
        public virtual DbSet<RepositoryDatabaseDb> RepositoryDatabase { get; set; }
        public virtual DbSet<UserSecurity> UserSecurity { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<UserSecurity>()
                .HasKey(x => x.UserId);

            modelBuilder.Entity<Role>()
                .HasKey(x => x.RoleId);

            modelBuilder.Entity<UserSecurity>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UserSecurities)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<UserDb>()
                .HasOne(x => x.UserSecurity)
                .WithOne(x => x.User);

            modelBuilder.Entity<UserDb>()
                .ToTable("User")
                .HasKey(x => x.UserId);

            modelBuilder.Entity<RepositoryDb>()
                .ToTable("Repository")
                .HasKey(x => x.RepositoryId);

            modelBuilder.Entity<RepositoryDb>()
                .HasOne(x => x.UserDb)
                .WithMany(x => x.Repositories)
                .HasForeignKey(x => x.OwnerId);

            modelBuilder.Entity<RepositoryDatabaseDb>()
                .ToTable("RepositoryDatabase")
                .HasKey(x => x.DatabaseId);

            modelBuilder.Entity<RepositoryDatabaseDb>()
                .HasOne(x => x.Repository);

            modelBuilder.Entity<RepositoryDb>()
                .HasMany(x => x.RepositoryDatabase)
                .WithOne(x => x.Repository)
                .HasForeignKey(x => x.RepositoryId);
        }
    }
}
