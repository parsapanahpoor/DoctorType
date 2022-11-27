﻿// <auto-generated />
using System;
using DoctorType.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoctorType.Data.Migrations
{
    [DbContext(typeof(DoctorTypeDbContext))]
    partial class DoctorTypeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DoctorType.Domain.Entities.Account.Permission", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<decimal?>("ParentId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("PermissionUniqueName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Account.Role", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<decimal?>("ParentId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("RoleUniqueName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1m,
                            CreateDate = new DateTime(2022, 11, 24, 11, 46, 54, 771, DateTimeKind.Local).AddTicks(3010),
                            IsDelete = false,
                            RoleUniqueName = "Admin",
                            Title = "Admin"
                        });
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Account.RolePermission", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<decimal>("PermissionId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("RoleId")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Account.User", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"), 1L, 1);

                    b.Property<string>("Avatar")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("BanForComment")
                        .HasColumnType("bit");

                    b.Property<bool>("BanForTicket")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("BithDay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("EmailActivationCode")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ExpireMobileSMSDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExtraPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomePhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBan")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEmailConfirm")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMobileConfirm")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("MobileActivationCode")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NationalId")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("WalletBalance")
                        .HasColumnType("int");

                    b.Property<string>("WorkAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Account.UserRole", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<decimal>("RoleId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("UserId")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.SiteSetting.EmailSetting", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("EnableSsL")
                        .HasColumnType("bit");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsDefaultEmail")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<string>("Smtp")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("EmailSetting");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.SiteSetting.SiteSetting", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"), 1L, 1);

                    b.Property<string>("CopyRightText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("SendSMSTimer")
                        .HasColumnType("int");

                    b.Property<string>("SiteDomain")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SiteSettings");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Tariff.Tariff", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"), 1L, 1);

                    b.Property<int>("CountOfAcceptRequest")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("TariffName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TariffPrice")
                        .HasColumnType("int");

                    b.Property<int>("tariffDuration")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tariffs");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Tariff.UserSelectedTariff", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("CurrentTarriff")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Startdate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TariffId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("UserId")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("TariffId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSelectedTariffs");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Wallet.Wallet", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("GatewayType")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFinally")
                        .HasColumnType("bit");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<decimal?>("RequestId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.Property<int>("TransactionWay")
                        .HasColumnType("int");

                    b.Property<decimal>("UserId")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Wallet.WalletData", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GatewayType")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("ReferenceCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackingCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("WalletId")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("WalletId")
                        .IsUnique();

                    b.ToTable("WalletData");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Account.Permission", b =>
                {
                    b.HasOne("DoctorType.Domain.Entities.Account.Permission", null)
                        .WithMany("Permissions")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Account.Role", b =>
                {
                    b.HasOne("DoctorType.Domain.Entities.Account.Role", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Account.RolePermission", b =>
                {
                    b.HasOne("DoctorType.Domain.Entities.Account.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DoctorType.Domain.Entities.Account.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Account.UserRole", b =>
                {
                    b.HasOne("DoctorType.Domain.Entities.Account.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DoctorType.Domain.Entities.Account.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Tariff.UserSelectedTariff", b =>
                {
                    b.HasOne("DoctorType.Domain.Entities.Tariff.Tariff", "Tariff")
                        .WithMany("UserSelectedTariff")
                        .HasForeignKey("TariffId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DoctorType.Domain.Entities.Account.User", "User")
                        .WithMany("UserSelectedTariff")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tariff");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Wallet.Wallet", b =>
                {
                    b.HasOne("DoctorType.Domain.Entities.Account.User", "User")
                        .WithMany("Wallets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Wallet.WalletData", b =>
                {
                    b.HasOne("DoctorType.Domain.Entities.Wallet.Wallet", "Wallet")
                        .WithOne("WalletData")
                        .HasForeignKey("DoctorType.Domain.Entities.Wallet.WalletData", "WalletId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Account.Permission", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Account.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Account.User", b =>
                {
                    b.Navigation("UserRoles");

                    b.Navigation("UserSelectedTariff");

                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Tariff.Tariff", b =>
                {
                    b.Navigation("UserSelectedTariff");
                });

            modelBuilder.Entity("DoctorType.Domain.Entities.Wallet.Wallet", b =>
                {
                    b.Navigation("WalletData")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
