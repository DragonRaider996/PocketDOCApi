using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PocDoctor.Entitiess
{
    public partial class ApplicationContext : DbContext
    {
        public virtual DbSet<DisPrev> DisPrev { get; set; }
        public virtual DbSet<DisRem> DisRem { get; set; }
        public virtual DbSet<DisSymp> DisSymp { get; set; }
        public virtual DbSet<Diseases> Diseases { get; set; }
        public virtual DbSet<FirstAidAns> FirstAidAns { get; set; }
        public virtual DbSet<FirstAidQues> FirstAidQues { get; set; }
        public virtual DbSet<FirstAidRel> FirstAidRel { get; set; }
        public virtual DbSet<Forum> Forum { get; set; }
        public virtual DbSet<ForumComments> ForumComments { get; set; }
        public virtual DbSet<Hospital> Hospital { get; set; }
        public virtual DbSet<HumanAnatomy> HumanAnatomy { get; set; }
        public virtual DbSet<HumanPara> HumanPara { get; set; }
        public virtual DbSet<HumanPicture> HumanPicture { get; set; }
        public virtual DbSet<HumanRelation> HumanRelation { get; set; }
        public virtual DbSet<MainSymptoms> MainSymptoms { get; set; }
        public virtual DbSet<Prevention> Prevention { get; set; }
        public virtual DbSet<Remedies> Remedies { get; set; }
        public virtual DbSet<Symptoms> Symptoms { get; set; }
        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<VerifyCode> VerifyCode { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(@"Data Source=pocketdoctor.database.windows.net;Initial Catalog=PocketDoctor;Integrated Security=False;User ID=abcd;Password=abcd;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DisPrev>(entity =>
            {
                entity.HasKey(e => new { e.Did, e.Pid })
                    .HasName("PK_DisPrev");
            });

            modelBuilder.Entity<DisRem>(entity =>
            {
                entity.HasKey(e => new { e.Did, e.Rid })
                    .HasName("PK_DisRem");
            });

            modelBuilder.Entity<DisSymp>(entity =>
            {
                entity.HasKey(e => new { e.Did, e.Sid })
                    .HasName("PK_DisSymp");
            });

            modelBuilder.Entity<FirstAidRel>(entity =>
            {
                entity.HasKey(e => new { e.Faid, e.Aid })
                    .HasName("PK_FirstAidRel");
            });

            modelBuilder.Entity<HumanPara>(entity =>
            {
                entity.HasKey(e => e.Hapid)
                    .HasName("PK_HumanAnantomyPara");
            });

            modelBuilder.Entity<HumanRelation>(entity =>
            {
                entity.HasKey(e => new { e.Haid, e.Hapid })
                    .HasName("PK_HumanRelation");
            });

            modelBuilder.Entity<MainSymptoms>(entity =>
            {
                entity.Property(e => e.Sid).ValueGeneratedNever();
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.Property(e => e.Uid).ValueGeneratedNever();
            });

            modelBuilder.Entity<VerifyCode>(entity =>
            {
                entity.Property(e => e.Uid).ValueGeneratedNever();
            });
        }
    }
}