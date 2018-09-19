﻿// <auto-generated />
using MarketPlaceBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MarketPlaceBackend.Migrations
{
    [DbContext(typeof(MarketPlaceBackendContext))]
    partial class MarketPlaceBackendContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MarketPlaceBackend.Models.Application", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppUrl");

                    b.Property<string>("Developer");

                    b.Property<string>("Info");

                    b.Property<string>("LogoUrl");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Application");
                });
#pragma warning restore 612, 618
        }
    }
}
