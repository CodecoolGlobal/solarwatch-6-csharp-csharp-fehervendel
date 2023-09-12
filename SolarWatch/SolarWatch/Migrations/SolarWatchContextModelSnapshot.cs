﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SolarWatch.Context;

#nullable disable

namespace SolarWatch.Migrations
{
    [DbContext(typeof(SolarWatchContext))]
    partial class SolarWatchContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SolarWatch.Model.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityId"), 1L, 1);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lon")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CityId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("SolarWatch.Model.SunSetSunRiseResponse", b =>
                {
                    b.Property<int>("SunId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SunId"), 1L, 1);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Sunrise")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sunset")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SunId");

                    b.HasIndex("CityId")
                        .IsUnique();

                    b.ToTable("SunsetSunrise");
                });

            modelBuilder.Entity("SolarWatch.Model.SunSetSunRiseResponse", b =>
                {
                    b.HasOne("SolarWatch.Model.City", "city")
                        .WithOne("SunRiseResponse")
                        .HasForeignKey("SolarWatch.Model.SunSetSunRiseResponse", "CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("city");
                });

            modelBuilder.Entity("SolarWatch.Model.City", b =>
                {
                    b.Navigation("SunRiseResponse")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
