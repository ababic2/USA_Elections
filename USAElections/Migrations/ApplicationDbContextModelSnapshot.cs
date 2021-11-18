﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using USAElections.Data;

namespace USAElections.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CandidateConstituency", b =>
                {
                    b.Property<int>("CandidateId")
                        .HasColumnType("int");

                    b.Property<int>("ConstituencyId")
                        .HasColumnType("int");

                    b.HasKey("CandidateId", "ConstituencyId");

                    b.HasIndex("ConstituencyId");

                    b.ToTable("CandidateConstituency");
                });

            modelBuilder.Entity("USAElections.Models.Candidate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Candidate");
                });

            modelBuilder.Entity("USAElections.Models.Constituency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Constituency");
                });

            modelBuilder.Entity("USAElections.Models.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CandidateId")
                        .HasColumnType("int");

                    b.Property<int>("ConstituencyId")
                        .HasColumnType("int");

                    b.Property<int>("number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("ConstituencyId");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("CandidateConstituency", b =>
                {
                    b.HasOne("USAElections.Models.Candidate", null)
                        .WithMany()
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("USAElections.Models.Constituency", null)
                        .WithMany()
                        .HasForeignKey("ConstituencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("USAElections.Models.Vote", b =>
                {
                    b.HasOne("USAElections.Models.Candidate", "Candidate")
                        .WithMany("Vote")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("USAElections.Models.Constituency", "Constituency")
                        .WithMany("Vote")
                        .HasForeignKey("ConstituencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("Constituency");
                });

            modelBuilder.Entity("USAElections.Models.Candidate", b =>
                {
                    b.Navigation("Vote");
                });

            modelBuilder.Entity("USAElections.Models.Constituency", b =>
                {
                    b.Navigation("Vote");
                });
#pragma warning restore 612, 618
        }
    }
}
