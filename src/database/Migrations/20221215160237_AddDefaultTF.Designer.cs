﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using database;

#nullable disable

namespace database.Migrations
{
    [DbContext(typeof(SQSDbContext))]
    [Migration("20221215160237_AddDefaultTF")]
    partial class AddDefaultTF
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("database.Entity.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CloseDecimalLen")
                        .HasColumnType("integer");

                    b.Property<int>("CloseValue")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("HighDecimalLen")
                        .HasColumnType("integer");

                    b.Property<int>("HighValue")
                        .HasColumnType("integer");

                    b.Property<int>("LowDecimalLen")
                        .HasColumnType("integer");

                    b.Property<int>("LowValue")
                        .HasColumnType("integer");

                    b.Property<int>("OpenDecimalLen")
                        .HasColumnType("integer");

                    b.Property<int>("OpenValue")
                        .HasColumnType("integer");

                    b.Property<short>("SourceId")
                        .HasColumnType("smallint");

                    b.Property<int>("StockId")
                        .HasColumnType("integer");

                    b.Property<short>("TimeFrameId")
                        .HasColumnType("smallint");

                    b.Property<int>("VolumeDecimalLen")
                        .HasColumnType("integer");

                    b.Property<int>("VolumeValue")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.HasIndex("TimeFrameId");

                    b.HasIndex("StockId", "SourceId")
                        .IsUnique();

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("database.Entity.Source", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<string>("BaseClientUri")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("Priority")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("Priority")
                        .IsUnique();

                    b.ToTable("Sources");
                });

            modelBuilder.Entity("database.Entity.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("database.Entity.StockSourceMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("SourceCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("SourceId")
                        .HasColumnType("smallint");

                    b.Property<int>("StockId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.HasIndex("StockId", "SourceId")
                        .IsUnique();

                    b.ToTable("StockSourceMaps");
                });

            modelBuilder.Entity("database.Entity.TimeFrame", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<int>("Seconds")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TimeFrames");

                    b.HasData(
                        new
                        {
                            Id = (short)1,
                            Code = "1D",
                            Name = "Day",
                            Seconds = 86400
                        },
                        new
                        {
                            Id = (short)2,
                            Code = "1H",
                            Name = "Hour",
                            Seconds = 3600
                        });
                });

            modelBuilder.Entity("database.Entity.Quote", b =>
                {
                    b.HasOne("database.Entity.Source", "Source")
                        .WithMany("Quotes")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("database.Entity.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("database.Entity.TimeFrame", "TimeFrame")
                        .WithMany()
                        .HasForeignKey("TimeFrameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Source");

                    b.Navigation("Stock");

                    b.Navigation("TimeFrame");
                });

            modelBuilder.Entity("database.Entity.StockSourceMap", b =>
                {
                    b.HasOne("database.Entity.Source", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("database.Entity.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Source");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("database.Entity.Source", b =>
                {
                    b.Navigation("Quotes");
                });
#pragma warning restore 612, 618
        }
    }
}
