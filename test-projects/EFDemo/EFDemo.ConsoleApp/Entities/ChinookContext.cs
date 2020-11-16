using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EFDemo.ConsoleApp.Entities
{
    public partial class ChinookContext : DbContext
    {
        public ChinookContext()
        {
        }

        public ChinookContext(DbContextOptions<ChinookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseStudent> CourseStudents { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<EmpDetail> EmpDetails { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Employee1> Employees1 { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<PlaylistTrack> PlaylistTracks { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //REMOVE BEFORE PUSHING TO GIT
                optionsBuilder.UseSqlServer("Server=tcp:towner-training.database.windows.net,1433;Initial Catalog=Chinook;Persist Security Info=False;User ID=rtowner;Password=MazdaSpeed3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.ToTable("Album");

                entity.HasIndex(e => e.ArtistId, "IFK_AlbumArtistId");

                entity.Property(e => e.AlbumId).ValueGeneratedNever();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(160);

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlbumArtistId");
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist");

                entity.Property(e => e.ArtistId).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(120);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course", "School");

                entity.Property(e => e.Id).HasMaxLength(32);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_Course_TeacherId");
            });

            modelBuilder.Entity<CourseStudent>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.StudentId })
                    .HasName("PK__CourseSt__4A01231E486A08CF");

                entity.ToTable("CourseStudent", "School");

                entity.Property(e => e.CourseId).HasMaxLength(32);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseStudents)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CourseStu__Cours__0D7A0286");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CourseStudents)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CourseStu__Stude__0E6E26BF");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.SupportRepId, "IFK_CustomerSupportRepId");

                entity.Property(e => e.CustomerId).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(70);

                entity.Property(e => e.City).HasMaxLength(40);

                entity.Property(e => e.Company).HasMaxLength(80);

                entity.Property(e => e.Country).HasMaxLength(40);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.State).HasMaxLength(40);

                entity.HasOne(d => d.SupportRep)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.SupportRepId)
                    .HasConstraintName("FK_CustomerSupportRepId");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department", "Business");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<EmpDetail>(entity =>
            {
                entity.ToTable("EmpDetails", "Business");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Address2).HasMaxLength(300);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Salary).HasColumnType("money");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmpDetails)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EmpDetail__Emplo__1DB06A4F");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee", "Business");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Ssn)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("SSN");

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DeptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__DeptId__1AD3FDA4");
            });

            modelBuilder.Entity<Employee1>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("Employee");

                entity.HasIndex(e => e.ReportsTo, "IFK_EmployeeReportsTo");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(70);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(40);

                entity.Property(e => e.Country).HasMaxLength(40);

                entity.Property(e => e.Email).HasMaxLength(60);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.State).HasMaxLength(40);

                entity.Property(e => e.Title).HasMaxLength(30);

                entity.HasOne(d => d.ReportsToNavigation)
                    .WithMany(p => p.InverseReportsToNavigation)
                    .HasForeignKey(d => d.ReportsTo)
                    .HasConstraintName("FK_EmployeeReportsTo");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.GenreId).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(120);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.HasIndex(e => e.CustomerId, "IFK_InvoiceCustomerId");

                entity.Property(e => e.InvoiceId).ValueGeneratedNever();

                entity.Property(e => e.BillingAddress).HasMaxLength(70);

                entity.Property(e => e.BillingCity).HasMaxLength(40);

                entity.Property(e => e.BillingCountry).HasMaxLength(40);

                entity.Property(e => e.BillingPostalCode).HasMaxLength(10);

                entity.Property(e => e.BillingState).HasMaxLength(40);

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("numeric(10, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceCustomerId");
            });

            modelBuilder.Entity<InvoiceLine>(entity =>
            {
                entity.ToTable("InvoiceLine");

                entity.HasIndex(e => e.InvoiceId, "IFK_InvoiceLineInvoiceId");

                entity.HasIndex(e => e.TrackId, "IFK_InvoiceLineTrackId");

                entity.Property(e => e.InvoiceLineId).ValueGeneratedNever();

                entity.Property(e => e.UnitPrice).HasColumnType("numeric(10, 2)");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceLines)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceLineInvoiceId");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.InvoiceLines)
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceLineTrackId");
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.ToTable("MediaType");

                entity.Property(e => e.MediaTypeId).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(120);
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.ToTable("Playlist");

                entity.Property(e => e.PlaylistId).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(120);
            });

            modelBuilder.Entity<PlaylistTrack>(entity =>
            {
                entity.HasKey(e => new { e.PlaylistId, e.TrackId })
                    .IsClustered(false);

                entity.ToTable("PlaylistTrack");

                entity.HasIndex(e => e.TrackId, "IFK_PlaylistTrackTrackId");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.PlaylistTracks)
                    .HasForeignKey(d => d.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaylistTrackPlaylistId");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.PlaylistTracks)
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaylistTrackTrackId");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student", "School");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher", "School");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.ToTable("Track");

                entity.HasIndex(e => e.AlbumId, "IFK_TrackAlbumId");

                entity.HasIndex(e => e.GenreId, "IFK_TrackGenreId");

                entity.HasIndex(e => e.MediaTypeId, "IFK_TrackMediaTypeId");

                entity.Property(e => e.TrackId).ValueGeneratedNever();

                entity.Property(e => e.Composer).HasMaxLength(220);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UnitPrice).HasColumnType("numeric(10, 2)");

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.AlbumId)
                    .HasConstraintName("FK_TrackAlbumId");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_TrackGenreId");

                entity.HasOne(d => d.MediaType)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.MediaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrackMediaTypeId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
