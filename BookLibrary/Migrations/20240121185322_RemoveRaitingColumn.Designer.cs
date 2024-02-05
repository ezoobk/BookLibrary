﻿// <auto-generated />
using System;
using BookLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookLibrary.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240121185322_RemoveRaitingColumn")]
    partial class RemoveRaitingColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BookLibrary.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("bookAuthorId")
                        .HasColumnType("int");

                    b.Property<int>("bookCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("bookName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("releaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("bookAuthorId");

                    b.HasIndex("bookCategoryId");

                    b.ToTable("books");
                });

            modelBuilder.Entity("BookLibrary.Models.BookAuthor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("bookAuthors");
                });

            modelBuilder.Entity("BookLibrary.Models.BookCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("bookCategories");
                });

            modelBuilder.Entity("BookLibrary.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("joiningDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("phoneNum")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("members");
                });

            modelBuilder.Entity("BookLibrary.Models.RentedBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("bookId")
                        .HasColumnType("int");

                    b.Property<int>("memberId")
                        .HasColumnType("int");

                    b.Property<DateTime>("rentDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("rentDue")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("returnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("bookId");

                    b.HasIndex("memberId");

                    b.ToTable("rentedBooks");
                });

            modelBuilder.Entity("BookLibrary.Models.Book", b =>
                {
                    b.HasOne("BookLibrary.Models.BookAuthor", "bookAuthor")
                        .WithMany("Books")
                        .HasForeignKey("bookAuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookLibrary.Models.BookCategory", "bookCategory")
                        .WithMany("books")
                        .HasForeignKey("bookCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("bookAuthor");

                    b.Navigation("bookCategory");
                });

            modelBuilder.Entity("BookLibrary.Models.RentedBook", b =>
                {
                    b.HasOne("BookLibrary.Models.Book", "book")
                        .WithMany("RentedBooks")
                        .HasForeignKey("bookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookLibrary.Models.Member", "member")
                        .WithMany("RentedBooks")
                        .HasForeignKey("memberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("book");

                    b.Navigation("member");
                });

            modelBuilder.Entity("BookLibrary.Models.Book", b =>
                {
                    b.Navigation("RentedBooks");
                });

            modelBuilder.Entity("BookLibrary.Models.BookAuthor", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookLibrary.Models.BookCategory", b =>
                {
                    b.Navigation("books");
                });

            modelBuilder.Entity("BookLibrary.Models.Member", b =>
                {
                    b.Navigation("RentedBooks");
                });
#pragma warning restore 612, 618
        }
    }
}
