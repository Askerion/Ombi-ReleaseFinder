﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OmbiReleaseFinder.Models;

namespace OmbiReleaseFinder.Migrations
{
    [DbContext(typeof(MovieDatabaseContext))]
    [Migration("20180914071511_RankingtoMovie")]
    partial class RankingtoMovie
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932");

            modelBuilder.Entity("OmbiReleaseFinder.Models.CustomMovie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("date('now')");

                    b.Property<int>("MovieDbId");

                    b.Property<string>("OriginalTitle")
                        .IsRequired();

                    b.Property<string>("Overview");

                    b.Property<string>("PosterPath");

                    b.Property<double>("Rating");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("CustomMovie");
                });

            modelBuilder.Entity("OmbiReleaseFinder.Models.FtpRelease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("date('now')");

                    b.Property<string>("FtpFolder")
                        .IsUnicode(false);

                    b.Property<string>("FtpReleaseGroup")
                        .IsUnicode(false);

                    b.Property<string>("FtpReleasename")
                        .IsUnicode(false);

                    b.Property<int?>("MovieId");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("FtpRelease");
                });

            modelBuilder.Entity("OmbiReleaseFinder.Models.Releasenames", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("date('now')");

                    b.Property<int>("MovieId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Releasenames");
                });

            modelBuilder.Entity("OmbiReleaseFinder.Models.FtpRelease", b =>
                {
                    b.HasOne("OmbiReleaseFinder.Models.CustomMovie", "Movie")
                        .WithMany("FtpRelease")
                        .HasForeignKey("MovieId")
                        .HasConstraintName("FK__FtpReleas__Movie__5AEE82B9");
                });

            modelBuilder.Entity("OmbiReleaseFinder.Models.Releasenames", b =>
                {
                    b.HasOne("OmbiReleaseFinder.Models.CustomMovie", "Movie")
                        .WithMany("Releasenames")
                        .HasForeignKey("MovieId")
                        .HasConstraintName("FK__Releasena__Movie__276EDEB3");
                });
#pragma warning restore 612, 618
        }
    }
}
