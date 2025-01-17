using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Server.DataBase;

public partial class CloudLibriryContext : DbContext
{
    public CloudLibriryContext()
    {
    }

    public CloudLibriryContext(DbContextOptions<CloudLibriryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientTicket> ClientTickets { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TypeCover> TypeCovers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Ilias\\ILYASQL;Database=CloudLibriry;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IdAutor);

            entity.Property(e => e.IdAutor).HasColumnName("idAutor");
            entity.Property(e => e.CountryOfBirth).HasMaxLength(50);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Fio)
                .HasMaxLength(152)
                .HasComputedColumnSql("(((([Surname]+' ')+[Name])+' ')+[Patronymic])", false)
                .HasColumnName("FIO");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.IdBooks);

            entity.Property(e => e.IdBooks).HasColumnName("idBooks");
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.CountPages).HasMaxLength(50);
            entity.Property(e => e.DatePublished).HasMaxLength(50);
            entity.Property(e => e.IdAutor).HasColumnName("idAutor");
            entity.Property(e => e.IdPublisher).HasColumnName("idPublisher");
            entity.Property(e => e.IdTypeCover).HasColumnName("idTypeCover");
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdAutor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_Autors");

            entity.HasOne(d => d.IdPublisherNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdPublisher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_Publishers");

            entity.HasOne(d => d.IdTypeCoverNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdTypeCover)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_TypeCovers");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient);

            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.Adress).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Fio)
                .HasMaxLength(452)
                .HasComputedColumnSql("(((([Surname]+' ')+[Name])+' ')+[Patronymic])", false)
                .HasColumnName("FIO");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Patronymic).HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.RegistrationAdress).HasMaxLength(150);
            entity.Property(e => e.Surname).HasMaxLength(150);
        });

        modelBuilder.Entity<ClientTicket>(entity =>
        {
            entity.HasKey(e => e.IdClientTicket);

            entity.Property(e => e.IdClientTicket).HasColumnName("idClientTicket");
            entity.Property(e => e.DateRegistration).HasColumnType("datetime");
            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.Number).HasMaxLength(50);

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientTickets)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientTickets_Clients");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.IdHistory);

            entity.ToTable("History");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.IdClientTickert).HasColumnName("idClientTickert");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.IdBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_Books");

            entity.HasOne(d => d.IdClientTickertNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.IdClientTickert)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_ClientTickets");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.IdPublisher);

            entity.Property(e => e.IdPublisher).HasColumnName("idPublisher");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole);

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TypeCover>(entity =>
        {
            entity.HasKey(e => e.IdTypeCover);

            entity.Property(e => e.IdTypeCover).HasColumnName("idTypeCover");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("User");

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.IdRole).HasColumnName("idRole");
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
