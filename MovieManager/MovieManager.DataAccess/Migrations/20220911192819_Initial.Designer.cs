﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieManager.DataAccess.Data;

#nullable disable

namespace MovieManager.DataAccess.Migrations
{
    [DbContext(typeof(MovieManagerDbContext))]
    [Migration("20220911192819_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MovieManager.Domain.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Greg Focker and his fiancee Pam decide to make their respective parents meet before their wedding. However, the Fockers' relaxed attitude does not go down well with Pam's family.",
                            Genre = 2,
                            Title = "Meet the Fockers",
                            Year = 2004
                        },
                        new
                        {
                            Id = 2,
                            Description = "James takes Ben along to pull the plug on a drug racket involving an influential businessman, Antonio Pope. However, with Ben's wedding day approaching, the two have little time to expose the crime.",
                            Genre = 2,
                            Title = "Ride Along 2",
                            Year = 2016
                        },
                        new
                        {
                            Id = 3,
                            Description = "In the world of international crime, an Interpol agent attempts to hunt down and capture the world's most wanted art thief.",
                            Genre = 1,
                            Title = "Red Notice",
                            Year = 2021
                        },
                        new
                        {
                            Id = 4,
                            Description = "Victor Sullivan recruits Nathan Drake to help him find the lost fortune of Ferdinand Magellan. However, they face competition from Santiago Moncada, who believes that the treasure belongs to him.",
                            Genre = 33,
                            Title = "Uncharted",
                            Year = 2022
                        },
                        new
                        {
                            Id = 5,
                            Description = "The revival of Emperor Palpatine resurrects the battle between the Resistance and the First Order while the Jedi's legendary conflict with the Sith Lord comes to a head.",
                            Genre = 48,
                            Title = "Star Wars: The Rise of Skywalker (Episode IX)",
                            Year = 2022
                        });
                });

            modelBuilder.Entity("MovieManager.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Ilija",
                            LastName = "Mitev",
                            Password = "ile123",
                            Role = 2,
                            Username = "ile123"
                        });
                });

            modelBuilder.Entity("MovieManager.Domain.Models.UserMovie", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("MovieId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersMovies");

                    b.HasData(
                        new
                        {
                            MovieId = 1,
                            UserId = 1,
                            Id = 1
                        },
                        new
                        {
                            MovieId = 5,
                            UserId = 1,
                            Id = 2
                        },
                        new
                        {
                            MovieId = 3,
                            UserId = 1,
                            Id = 3
                        });
                });

            modelBuilder.Entity("MovieManager.Domain.Models.UserMovie", b =>
                {
                    b.HasOne("MovieManager.Domain.Models.Movie", "Movie")
                        .WithMany("UserMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieManager.Domain.Models.User", "User")
                        .WithMany("UserMovies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieManager.Domain.Models.Movie", b =>
                {
                    b.Navigation("UserMovies");
                });

            modelBuilder.Entity("MovieManager.Domain.Models.User", b =>
                {
                    b.Navigation("UserMovies");
                });
#pragma warning restore 612, 618
        }
    }
}
