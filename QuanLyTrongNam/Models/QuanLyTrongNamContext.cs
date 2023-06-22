using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuanLyTrongNam.Models
{
    public partial class QuanLyTrongNamContext : DbContext
    {
        public QuanLyTrongNamContext()
        {
        }

        public QuanLyTrongNamContext(DbContextOptions<QuanLyTrongNamContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Farm> Farms { get; set; } = null!;
        public virtual DbSet<Farmer> Farmers { get; set; } = null!;
        public virtual DbSet<Mushroom> Mushrooms { get; set; } = null!;
        public virtual DbSet<Sensor> Sensors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=QuanLyTrongNam;User ID=sa;Password=1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Farm>(entity =>
            {
                entity.ToTable("Farm");

                entity.Property(e => e.FarmId).HasColumnName("farm_id");

                entity.Property(e => e.FarmLocation)
                    .HasMaxLength(50)
                    .HasColumnName("farm_location");

                entity.Property(e => e.FarmName)
                    .HasMaxLength(50)
                    .HasColumnName("farm_name");

                entity.Property(e => e.FarmPicture)
                    .HasMaxLength(150)
                    .HasColumnName("farm_picture");
            });

            modelBuilder.Entity<Farmer>(entity =>
            {
                entity.ToTable("Farmer");

                entity.Property(e => e.FarmerId).HasColumnName("farmer_id");

                entity.Property(e => e.FarmId).HasColumnName("farm_id");

                entity.Property(e => e.FarmerAddress)
                    .HasMaxLength(100)
                    .HasColumnName("farmer_address");

                entity.Property(e => e.FarmerName)
                    .HasMaxLength(30)
                    .HasColumnName("farmer_name");

                entity.Property(e => e.FarmerPhone)
                    .HasMaxLength(12)
                    .HasColumnName("farmer_phone");

                entity.Property(e => e.FarmerPicture)
                    .HasMaxLength(150)
                    .HasColumnName("farmer_picture");

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.Farmers)
                    .HasForeignKey(d => d.FarmId)
                    .HasConstraintName("FK__Farmer__farm_id__398D8EEE");
            });

            modelBuilder.Entity<Mushroom>(entity =>
            {
                entity.ToTable("Mushroom");

                entity.Property(e => e.MushroomId).HasColumnName("mushroom_id");

                entity.Property(e => e.FarmId).HasColumnName("farm_id");

                entity.Property(e => e.MushroomDescription)
                    .HasMaxLength(50)
                    .HasColumnName("mushroom_description");

                entity.Property(e => e.MushroomName)
                    .HasMaxLength(50)
                    .HasColumnName("mushroom_name");

                entity.Property(e => e.MushroomPicture)
                    .HasMaxLength(150)
                    .HasColumnName("mushroom_picture");

                entity.Property(e => e.MushroomPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("mushroom_price");

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.Mushrooms)
                    .HasForeignKey(d => d.FarmId)
                    .HasConstraintName("FK__Mushroom__farm_i__3F466844");
            });

            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.ToTable("Sensor");

                entity.Property(e => e.SensorId).HasColumnName("sensor_id");

                entity.Property(e => e.FarmId).HasColumnName("farm_id");

                entity.Property(e => e.SensorName)
                    .HasMaxLength(50)
                    .HasColumnName("sensor_name");

                entity.Property(e => e.SensorType)
                    .HasMaxLength(50)
                    .HasColumnName("sensor_type");

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.Sensors)
                    .HasForeignKey(d => d.FarmId)
                    .HasConstraintName("FK__Sensor__farm_id__3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
