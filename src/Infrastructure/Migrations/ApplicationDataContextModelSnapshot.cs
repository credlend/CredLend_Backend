﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDataContext))]
    partial class ApplicationDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("Domain.Models.PlanModel.InvestmentPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReturnDeadLine")
                        .HasColumnType("TEXT");

                    b.Property<float>("ReturnRate")
                        .HasColumnType("REAL");

                    b.Property<string>("TransactionWay")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TypePlan")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.Property<float>("ValuePlan")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("InvestmentPlan");
                });

            modelBuilder.Entity("Domain.Models.PlanModel.LoanPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<float>("InterestRate")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("PaymentTerm")
                        .HasColumnType("TEXT");

                    b.Property<string>("TransactionWay")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TypePlan")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.Property<float>("ValuePlan")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("LoanPlan");
                });

            modelBuilder.Entity("Domain.Models.UserModel.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdm")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
