using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Web_API.Entities
{
    public partial class OmgContext : DbContext
    {
        public OmgContext()
        {
        }

        public OmgContext(DbContextOptions<OmgContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OData> ODatas { get; set; }

        public virtual DbSet<ODataType> ODataTypes { get; set; }

        public virtual DbSet<OError> OErrors { get; set; }

        public virtual DbSet<OErrorsUpdate> OErrorsUpdates { get; set; }

        public virtual DbSet<OPage> OPages { get; set; }

        public virtual DbSet<OPlc> OPlcs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=ADMINISTRATOR\\WINCC;Database=OMG;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OData>(entity =>
            {
                entity.HasKey(e => e.DataId);

                entity.ToTable("O_Datas");

                entity.Property(e => e.DataId).HasColumnName("data_id");
                entity.Property(e => e.DataJson)
                    .IsUnicode(false)
                    .HasColumnName("data_json");
                entity.Property(e => e.DokumNo).HasColumnName("dokum_no");
                entity.Property(e => e.ElektrikAriza)
                    .HasDefaultValue(0.0)
                    .HasColumnName("elektrik_ariza");
                entity.Property(e => e.Exportdate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("exportdate");
                entity.Property(e => e.GenelAriza)
                    .HasDefaultValue(0.0)
                    .HasColumnName("genel_ariza");
                entity.Property(e => e.IsletmeAriza)
                    .HasDefaultValue(0.0)
                    .HasColumnName("isletme_ariza");
                entity.Property(e => e.MekanikAriza)
                    .HasDefaultValue(0.0)
                    .HasColumnName("mekanik_ariza");
                entity.Property(e => e.PageId).HasColumnName("page_id");
            });

            modelBuilder.Entity<ODataType>(entity =>
            {
                entity.HasKey(e => e.DataId).HasName("PK_O_DataTypes2");

                entity.ToTable("O_DataTypes");

                entity.Property(e => e.DataId)
                    .ValueGeneratedNever()
                    .HasColumnName("data_id");
                entity.Property(e => e.DataAdress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("data_adress");
                entity.Property(e => e.DataDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("data_description");
                entity.Property(e => e.DataName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("data_name");
                entity.Property(e => e.DataPage).HasColumnName("data_page");
                entity.Property(e => e.DataPlc).HasColumnName("data_plc");
                entity.Property(e => e.DataType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("data_type");
                entity.Property(e => e.Exportdate)
                    .HasColumnType("datetime")
                    .HasColumnName("exportdate");
            });

            modelBuilder.Entity<OError>(entity =>
            {
                entity.HasKey(e => e.ErrorId);

                entity.ToTable("O_Errors");

                entity.Property(e => e.ErrorId).HasColumnName("error_id");
                entity.Property(e => e.ElektrikAriza).HasColumnName("elektrik_ariza");
                entity.Property(e => e.Exportdate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("exportdate");
                entity.Property(e => e.GenelAriza).HasColumnName("genel_ariza");
                entity.Property(e => e.IsletmeAriza).HasColumnName("isletme_ariza");
                entity.Property(e => e.MekanikAriza).HasColumnName("mekanik_ariza");
                entity.Property(e => e.PageId).HasColumnName("page_id");
            });

            modelBuilder.Entity<OErrorsUpdate>(entity =>
            {
                entity.ToTable("O_ErrorsUpdate");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Updated).HasColumnName("updated");
            });

            modelBuilder.Entity<OPage>(entity =>
            {
                entity.HasKey(e => e.PageId);

                entity.ToTable("O_Pages");

                entity.Property(e => e.PageId).HasColumnName("page_id");
                entity.Property(e => e.Exportdate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("exportdate");
                entity.Property(e => e.PageDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("page_description");
                entity.Property(e => e.PageTitle)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("page_title");
            });

            modelBuilder.Entity<OPlc>(entity =>
            {
                entity.ToTable("O_Plcs");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Exportdate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("exportdate");
                entity.Property(e => e.PlcIp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("plc_ip");
                entity.Property(e => e.PlcName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("plc_name");
                entity.Property(e => e.PlcRack).HasColumnName("plc_rack");
                entity.Property(e => e.PlcSlot).HasColumnName("plc_slot");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
