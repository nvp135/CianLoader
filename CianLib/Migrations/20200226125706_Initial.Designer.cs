﻿// <auto-generated />
using System;
using CianLib.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CianLib.Migrations
{
    [DbContext(typeof(CianContext))]
    [Migration("20200226125706_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CianLib.Model.CianObject", b =>
                {
                    b.Property<long>("row_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("added")
                        .HasColumnType("int");

                    b.Property<string>("category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("cian_id")
                        .HasColumnType("int");

                    b.Property<int>("city")
                        .HasColumnType("int");

                    b.Property<DateTime>("creation_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("deal_type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("filter_type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("from_developer")
                        .HasColumnType("bit");

                    b.Property<int?>("house_id")
                        .HasColumnType("int");

                    b.Property<string>("id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("insert_date")
                        .HasColumnType("date");

                    b.Property<float>("lat")
                        .HasColumnType("real");

                    b.Property<float>("lon")
                        .HasColumnType("real");

                    b.Property<int?>("newobject_id")
                        .HasColumnType("int");

                    b.Property<int?>("object_type")
                        .HasColumnType("int");

                    b.Property<string>("photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("property_type")
                        .HasColumnType("int");

                    b.Property<int>("service_id")
                        .HasColumnType("int");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.Property<int?>("village_id")
                        .HasColumnType("int");

                    b.HasKey("row_id");

                    b.ToTable("CianObjects");
                });

            modelBuilder.Entity("CianLib.Model.CianObjectPrice", b =>
                {
                    b.Property<long>("row_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("cian_objectrow_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("insert_date")
                        .HasColumnType("date");

                    b.Property<long>("price")
                        .HasColumnType("bigint");

                    b.HasKey("row_id");

                    b.HasIndex("cian_objectrow_id");

                    b.ToTable("CianObjectPrices");
                });

            modelBuilder.Entity("CianLib.Model.Offer", b =>
                {
                    b.Property<long>("row_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("added")
                        .HasColumnType("int");

                    b.Property<string>("category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("cian_id")
                        .HasColumnType("int");

                    b.Property<int>("city")
                        .HasColumnType("int");

                    b.Property<DateTime>("creation_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("deal_type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("filter_type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("from_developer")
                        .HasColumnType("bit");

                    b.Property<int?>("house_id")
                        .HasColumnType("int");

                    b.Property<string>("id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("insert_date")
                        .HasColumnType("date");

                    b.Property<float>("lat")
                        .HasColumnType("real");

                    b.Property<float>("lon")
                        .HasColumnType("real");

                    b.Property<int?>("newobject_id")
                        .HasColumnType("int");

                    b.Property<int?>("object_type")
                        .HasColumnType("int");

                    b.Property<string>("photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("price")
                        .HasColumnType("bigint");

                    b.Property<int>("property_type")
                        .HasColumnType("int");

                    b.Property<int>("service_id")
                        .HasColumnType("int");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.Property<int?>("village_id")
                        .HasColumnType("int");

                    b.HasKey("row_id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("CianLib.Model.CianObjectPrice", b =>
                {
                    b.HasOne("CianLib.Model.CianObject", "cian_object")
                        .WithMany("prices")
                        .HasForeignKey("cian_objectrow_id");
                });
#pragma warning restore 612, 618
        }
    }
}
