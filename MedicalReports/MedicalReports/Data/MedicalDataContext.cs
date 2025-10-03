using MedicalReports.Data.Entities;
using MedicalReports.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalReports.Data;

public class MedicalDataContext : DbContext
{
    public MedicalDataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<MedicalDataEntity> MedicalData { get; set; }
    public DbSet<BloodworkEntity> Bloodwork { get; set; }
    public DbSet<QuestionaireEntity> Questionnaires { get; set; }
    public DbSet<GuidelineEntity> Guidelines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Client - MedicalData (1:1)
        modelBuilder.Entity<ClientEntity>()
            .HasOne(c => c.MedicalData)
            .WithOne(m => m.Client)
            .HasForeignKey<MedicalDataEntity>(m => m.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        //MedicalData - Bloodwork (1:1)
        modelBuilder.Entity<MedicalDataEntity>()
            .HasOne(m => m.Bloodwork)
            .WithOne(b => b.MedicalData)
            .HasForeignKey<BloodworkEntity>(b => b.MedicalDataId)
            .OnDelete(DeleteBehavior.Cascade);

        //MedicalData - Questionnaire (1:1)
        modelBuilder.Entity<MedicalDataEntity>()
            .HasOne(m => m.Questionnaire)
            .WithOne(q => q.MedicalData)
            .HasForeignKey<QuestionaireEntity>(q => q.MedicalDataId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ClientEntity>()
            .Property(c => c.Name)
            .HasMaxLength(200);

        modelBuilder.Entity<ClientEntity>()
            .Property(c => c.Gender)
            .HasMaxLength(50);

        base.OnModelCreating(modelBuilder);
    }
}
