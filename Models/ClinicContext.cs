using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace wswpf.Models;

public class ClinicContext : DbContext
{
    public ClinicContext()
    {
    }

    public ClinicContext(DbContextOptions<ClinicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<MedicalCard> MedicalCards { get; set; }

    public virtual DbSet<MedicalHistory> MedicalHistories { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Reception> Receptions { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Data Source=DESKTOP-VOSGIAN;Initial Catalog=Clinic;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
        => optionsBuilder.UseSqlServer("Data Source=teacher209;Initial Catalog=Clinic;User ID=user1;Password=12345;Encrypt=False;Trust Server Certificate=True;MultiSubnetFailover=True");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK_Doctor_1");

            entity.ToTable("Doctor");

            entity.Property(e => e.DoctorId).HasColumnName("Doctor_ID");
            entity.Property(e => e.DoctorLogin)
                .HasMaxLength(50)
                .HasColumnName("Doctor_Login");
            entity.Property(e => e.DoctorName)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Doctor_name");
            entity.Property(e => e.DoctorPassword)
                .HasMaxLength(50)
                .HasColumnName("Doctor_Password");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event");

            entity.Property(e => e.EventId).HasColumnName("Event_ID");
            entity.Property(e => e.DoctorId).HasColumnName("Doctor_ID");
            entity.Property(e => e.EventDate).HasColumnName("Event_date");
            entity.Property(e => e.EventName)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("Event_name");
            entity.Property(e => e.EventRecommendations)
                .HasMaxLength(500)
                .IsFixedLength()
                .HasColumnName("Event_recommendations");
            entity.Property(e => e.EventResult)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("Event_result");
            entity.Property(e => e.EventType)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Event_type");
            entity.Property(e => e.PatientId).HasColumnName("Patient_ID");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Events)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Events)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event_Patient");
        });

        modelBuilder.Entity<MedicalCard>(entity =>
        {
            entity.ToTable("Medical_card");

            entity.Property(e => e.MedicalCardId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Medical_card_ID");
            entity.Property(e => e.DateOfIssue).HasColumnName("Date_of_issue");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.LastAppeal).HasColumnName("Last_appeal");
            entity.Property(e => e.MedicalHistory)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Medical_history");
            entity.Property(e => e.NextVisitDate).HasColumnName("Next_visit_date");
        });

        modelBuilder.Entity<MedicalHistory>(entity =>
        {
            entity.ToTable("Medical_history");

            entity.Property(e => e.MedicalHistoryId)
                .ValueGeneratedNever()
                .HasColumnName("Medical_history_ID");
            entity.Property(e => e.IsVisited).HasColumnName("isVisited");
            entity.Property(e => e.MedicalHistoryDate).HasColumnName("Medical_history_date");
            entity.Property(e => e.PatientMedicalCard)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Patient_medical_card");

            entity.HasOne(d => d.PatientMedicalCardNavigation).WithMany(p => p.MedicalHistories)
                .HasForeignKey(d => d.PatientMedicalCard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medical_history_Medical_card");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");

            entity.Property(e => e.PatientId).HasColumnName("Patient_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.BirthDate).HasColumnName("Birth_date");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Gender)
                .HasMaxLength(7)
                .IsFixedLength();
            entity.Property(e => e.InsurancePolicy).HasColumnName("Insurance_policy");
            entity.Property(e => e.InsurancePolicyExpireDate).HasColumnName("Insurance_policy_expire_date");
            entity.Property(e => e.Lastname).HasMaxLength(50);
            entity.Property(e => e.MedicalCard)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Medical_card");
            entity.Property(e => e.PassportSeriesAndNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Passport_series_and_number");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("Phone_number");
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.MedicalCardNavigation).WithMany(p => p.Patients)
                .HasForeignKey(d => d.MedicalCard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_Medical_card");
        });

        modelBuilder.Entity<Reception>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK_Table_1");

            entity.ToTable("Reception");

            entity.Property(e => e.AppointmentId).HasColumnName("Appointment_ID");
            entity.Property(e => e.Anamnesis).HasMaxLength(50);
            entity.Property(e => e.Diagnosis).HasMaxLength(50);
            entity.Property(e => e.DoctorId).HasColumnName("Doctor_ID");
            entity.Property(e => e.InstrumentalOrLaboratoryTests)
                .HasMaxLength(50)
                .HasColumnName("Instrumental_or_laboratory_tests");
            entity.Property(e => e.PatientId).HasColumnName("Patient_ID");
            entity.Property(e => e.Procedures).HasMaxLength(50);
            entity.Property(e => e.Recomendations).HasMaxLength(50);
            entity.Property(e => e.ReferralForConsultation)
                .HasMaxLength(50)
                .HasColumnName("Referral_for_consultation");
            entity.Property(e => e.SymptomsDetails)
                .HasMaxLength(50)
                .HasColumnName("Symptoms_details");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Receptions)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reception_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Receptions)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reception_Patient");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Appointment");

            entity.ToTable("Recipe");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AppointmentId).HasColumnName("Appointment_ID");
            entity.Property(e => e.Format).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Appointment).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recipe_Reception");
        });
    }
}
