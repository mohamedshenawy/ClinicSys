using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }

        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Clinic> Clinic { get; set; }
        public virtual DbSet<DoctorWorkingDay> DoctorWorkingDay { get; set; }
        public virtual DbSet<WorkingDay> WorkingDay { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<PatientAppointment> PatientAppointment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.UpdatedDate)
                      .HasDefaultValue(null);

                entity.Property(e => e.IsDeleted)
                      .HasDefaultValue(false);
            });
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Clinic)
                .WithMany(c => c.Doctors)
                .HasForeignKey(d => d.ClinicId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.Property(e => e.UpdatedDate)
                .HasDefaultValue(null);

                entity.Property(e => e.IsDeleted)
                      .HasDefaultValue(false);
            });
            
            
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.UpdatedDate)
                .HasDefaultValue(null);

                entity.Property(e => e.IsDeleted)
                      .HasDefaultValue(false);
            });

            modelBuilder.Entity<PatientAppointment>(entity =>
            {
                entity.Property(e => e.UpdatedDate)
                .HasDefaultValue(null);

                entity.Property(e => e.Date)
                .HasColumnType("date");

                entity.Property(e => e.IsDeleted)
                      .HasDefaultValue(false);
            });
            modelBuilder.Entity<PatientAppointment>()
                .Property(p => p.Date)
                .HasConversion(
                    v => v.Date,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)) 
                .HasColumnType("date");

            modelBuilder.Entity<PatientAppointment>()
                .HasOne(d => d.Doctor)
                .WithMany(c => c.PatientAppointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PatientAppointment>()
                .HasOne(d => d.Patient)
                .WithMany(c => c.PatientAppointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DoctorWorkingDay>()
                .HasOne(d => d.Doctor)
                .WithMany(c => c.DoctorWorkingDays)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DoctorWorkingDay>()
                .HasOne(d => d.WorkingDay)
                .WithMany(c => c.DoctorWorkingDays)
                .HasForeignKey(d => d.WorkingDayId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed WorkingDay
            modelBuilder.Entity<WorkingDay>().HasData(
                new WorkingDay { Id = 1, Name = "Sunday" },
                new WorkingDay { Id = 2, Name = "Monday" },
                new WorkingDay { Id = 3, Name = "Tuesday" },
                new WorkingDay { Id = 4, Name = "Wednesday" },
                new WorkingDay { Id = 5, Name = "Thursday" },
                new WorkingDay { Id = 6, Name = "Friday" },
                new WorkingDay { Id = 7, Name = "Saturday" }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}