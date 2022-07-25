﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PersonalFinanceApp.Database;

#nullable disable

namespace PersonalFinanceApp.Migrations
{
    [DbContext(typeof(TransactionsDbContext))]
    [Migration("20220725123147_Migration9")]
    partial class Migration9
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("PersonalFinanceApp.Database.Entities.CategoryEntity", b =>
                {
                    b.Property<string>("code")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("parent_code")
                        .HasColumnType("text");

                    b.HasKey("code");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("PersonalFinanceApp.Database.Entities.SplitTransactionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("Catcode")
                        .HasColumnType("text");

                    b.Property<string>("Categorycode")
                        .HasColumnType("text");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Categorycode");

                    b.HasIndex("TransactionId");

                    b.ToTable("SplittedTransactions");
                });

            modelBuilder.Entity("PersonalFinanceApp.Database.Entities.TransactionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<string>("Beneficiary_Name")
                        .HasColumnType("text");

                    b.Property<string>("Catcode")
                        .HasColumnType("text");

                    b.Property<string>("Currency")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Direction")
                        .HasColumnType("text");

                    b.Property<string>("Kind")
                        .HasColumnType("text");

                    b.Property<int?>("Mcc")
                        .HasColumnType("integer");

                    b.Property<string>("categorycode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("categorycode");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("PersonalFinanceApp.Database.Entities.SplitTransactionEntity", b =>
                {
                    b.HasOne("PersonalFinanceApp.Database.Entities.CategoryEntity", "Category")
                        .WithMany("SplitTransactions")
                        .HasForeignKey("Categorycode");

                    b.HasOne("PersonalFinanceApp.Database.Entities.TransactionEntity", "Transaction")
                        .WithMany("SplitTransactions")
                        .HasForeignKey("TransactionId");

                    b.Navigation("Category");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("PersonalFinanceApp.Database.Entities.TransactionEntity", b =>
                {
                    b.HasOne("PersonalFinanceApp.Database.Entities.CategoryEntity", "category")
                        .WithMany("transactions")
                        .HasForeignKey("categorycode");

                    b.Navigation("category");
                });

            modelBuilder.Entity("PersonalFinanceApp.Database.Entities.CategoryEntity", b =>
                {
                    b.Navigation("SplitTransactions");

                    b.Navigation("transactions");
                });

            modelBuilder.Entity("PersonalFinanceApp.Database.Entities.TransactionEntity", b =>
                {
                    b.Navigation("SplitTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
