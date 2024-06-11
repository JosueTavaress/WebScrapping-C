﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebScrapping_C.Data;

#nullable disable

namespace WebScrapping_C.Migrations
{
    [DbContext(typeof(FoodsContex))]
    [Migration("20240610221111_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebScrapping_C.Model.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .HasColumnType("text")
                        .HasColumnName("brand");

                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Group")
                        .HasColumnType("text")
                        .HasColumnName("group");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("ScientificName")
                        .HasColumnType("text")
                        .HasColumnName("scientific_name");

                    b.HasKey("Id");

                    b.ToTable("items", (string)null);
                });

            modelBuilder.Entity("WebScrapping_C.Model.Properties", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Component")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("component");

                    b.Property<string>("DataType")
                        .HasColumnType("text")
                        .HasColumnName("data_type");

                    b.Property<int?>("ItemId")
                        .HasColumnType("integer");

                    b.Property<string>("MaximumValue")
                        .HasColumnType("text")
                        .HasColumnName("maximum_value");

                    b.Property<string>("MinimumValue")
                        .HasColumnType("text")
                        .HasColumnName("minimum_value");

                    b.Property<string>("NumberOfDataUsed")
                        .HasColumnType("text")
                        .HasColumnName("number_of_data_used");

                    b.Property<string>("References")
                        .HasColumnType("text")
                        .HasColumnName("references");

                    b.Property<string>("StandardDeviation")
                        .HasColumnType("text")
                        .HasColumnName("standard_daviation");

                    b.Property<string>("Units")
                        .HasColumnType("text")
                        .HasColumnName("units");

                    b.Property<string>("ValuePer100G")
                        .HasColumnType("text")
                        .HasColumnName("value_per_100_g");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("properties", (string)null);
                });

            modelBuilder.Entity("WebScrapping_C.Model.Properties", b =>
                {
                    b.HasOne("WebScrapping_C.Model.Item", null)
                        .WithMany("Details")
                        .HasForeignKey("ItemId");
                });

            modelBuilder.Entity("WebScrapping_C.Model.Item", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
