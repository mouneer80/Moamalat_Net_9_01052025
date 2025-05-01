using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moamalat.Entities;

namespace Moamalat.Data;

public partial class MoamalatDb_Context : DbContext
{
    public MoamalatDb_Context()
    {
    }

    public MoamalatDb_Context(DbContextOptions<MoamalatDb_Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Corporate> Corporates { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Parameter> Parameters { get; set; }

    public virtual DbSet<PaymentData> PaymentDatas { get; set; }

    public virtual DbSet<PaymentRequest> PaymentRequests { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<PersonCorporate> PersonCorporates { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<RequestData> RequestDatas { get; set; }

    public virtual DbSet<RequestDetail> RequestDetails { get; set; }

    public virtual DbSet<RequestDocument> RequestDocuments { get; set; }

    public virtual DbSet<RequestStatu> RequestStatus { get; set; }

    public virtual DbSet<RequestTransaction> RequestTransactions { get; set; }

    public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }

    public virtual DbSet<ServiceData> ServiceDatas { get; set; }

    public virtual DbSet<ServiceParameter> ServiceParameters { get; set; }

    public virtual DbSet<ServiceRequiredDocument> ServiceRequiredDocuments { get; set; }

    public virtual DbSet<TopUpHistory> TopUpHistories { get; set; }

    public virtual DbSet<VwRequestData> VwRequestDatas { get; set; }

    public virtual DbSet<Wilayat> Wilayats { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=MOAMALAT;User Id=sa;Password=DBSsql123456;Encrypt=False;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(e => e.AddressId).IsFixedLength();
            entity.Property(e => e.PostalBox).IsFixedLength();
            entity.Property(e => e.PostalCode).IsFixedLength();

            entity.HasOne(d => d.Corporate).WithMany(p => p.Addresses).HasConstraintName("FK_Address_CORPORATE");

            entity.HasOne(d => d.Person).WithMany(p => p.Addresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Address_PERSON");

            entity.HasOne(d => d.Region).WithMany(p => p.Addresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Address_Region");

            entity.HasOne(d => d.Wilayat).WithMany(p => p.Addresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Address_Wilayat");
        });

        modelBuilder.Entity<Corporate>(entity =>
        {
            entity.HasKey(e => e.CorporateId).HasName("PK_CORPORATE");

            entity.HasOne(d => d.Grade).WithMany(p => p.Corporates).HasConstraintName("FK_CORPORATE_Grade");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.Property(e => e.Status)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasComment("A active D Inactive");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.Documents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Document_DocumentType");

            entity.HasOne(d => d.Person).WithMany(p => p.Documents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Document_PERSON");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.Property(e => e.GradeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<PaymentData>(entity =>
        {
            entity.HasOne(d => d.Request).WithMany(p => p.PaymentDatas).HasConstraintName("FK_PaymentData_RequestData");
        });

        modelBuilder.Entity<PaymentRequest>(entity =>
        {
            entity.Property(e => e.BankResultDescription).UseCollation("Arabic_CI_AI");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK_PERSON");

            entity.Property(e => e.Email).UseCollation("Arabic_CI_AI");
            entity.Property(e => e.FullArName).UseCollation("Arabic_CI_AI");
            entity.Property(e => e.FullEnName).UseCollation("Arabic_CI_AI");
            entity.Property(e => e.GenderId)
                .IsFixedLength()
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Mobile).UseCollation("Arabic_CI_AI");
            entity.Property(e => e.NationalId).UseCollation("Arabic_CI_AI");
        });

        modelBuilder.Entity<PersonCorporate>(entity =>
        {
            entity.HasOne(d => d.Corporate).WithMany(p => p.PersonCorporates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonCorporate_CORPORATE");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonCorporates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonCorporate_PERSON");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.Property(e => e.RegionId).ValueGeneratedNever();
            entity.Property(e => e.RegionArName).UseCollation("Arabic_CI_AI");
            entity.Property(e => e.RegionEnName).UseCollation("Arabic_CI_AI");
        });

        modelBuilder.Entity<RequestData>(entity =>
        {
            entity.HasOne(d => d.Corporate).WithMany(p => p.RequestDatas).HasConstraintName("FK_RequestData_CORPORATE");

            entity.HasOne(d => d.Person).WithMany(p => p.RequestDatas).HasConstraintName("FK_RequestData_PERSON");
        });

        modelBuilder.Entity<RequestDetail>(entity =>
        {
            entity.Property(e => e.Status).IsFixedLength();

            entity.HasOne(d => d.Parameter).WithMany(p => p.RequestDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestDetail_Parameter");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestDetail_RequestData");
        });

        modelBuilder.Entity<RequestDocument>(entity =>
        {
            entity.Property(e => e.Status)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasComment("A active D Inactive");

            entity.HasOne(d => d.Document).WithMany(p => p.RequestDocuments).HasConstraintName("FK_RequestDocument_Document");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestDocuments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestDocument_RequestData");
        });

        modelBuilder.Entity<RequestTransaction>(entity =>
        {
            entity.HasOne(d => d.Request).WithMany(p => p.RequestTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestTransaction_RequestData");

            entity.HasOne(d => d.Status).WithMany(p => p.RequestTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestTransaction_RequestStatus");
        });

        modelBuilder.Entity<ServiceData>(entity =>
        {
            entity.HasOne(d => d.ServiceCategory).WithMany(p => p.ServiceDatas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceData_ServiceCategory");
        });

        modelBuilder.Entity<ServiceParameter>(entity =>
        {
            entity.HasOne(d => d.Parameter).WithMany(p => p.ServiceParameters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceParameter_Parameter");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceParameters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceParameter_ServiceData");
        });

        modelBuilder.Entity<ServiceRequiredDocument>(entity =>
        {
            entity.Property(e => e.Status)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasComment("A Active D Inactive");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.ServiceRequiredDocuments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceRequiredDocument_DocumentType");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceRequiredDocuments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceRequiredDocument_ServiceData");
        });

        modelBuilder.Entity<VwRequestData>(entity =>
        {
            entity.ToView("VwRequestData");
        });

        modelBuilder.Entity<Wilayat>(entity =>
        {
            entity.Property(e => e.WilayatId).ValueGeneratedNever();
            entity.Property(e => e.WilayatArName).UseCollation("Arabic_CI_AI");
            entity.Property(e => e.WilayatEnName).UseCollation("Arabic_CI_AI");

            entity.HasOne(d => d.Region).WithMany(p => p.Wilayats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wilayat_Region");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

