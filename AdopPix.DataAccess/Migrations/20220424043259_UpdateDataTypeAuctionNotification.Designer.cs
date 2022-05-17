﻿// <auto-generated />
using System;
using AdopPix.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdopPix.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220424043259_UpdateDataTypeAuctionNotification")]
    partial class UpdateDataTypeAuctionNotification
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.15");

            modelBuilder.Entity("AdopPix.Models.Auction", b =>
                {
                    b.Property<string>("AuctionId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("HotClose")
                        .HasColumnType("decimal(65,2)");

                    b.Property<int>("HourId")
                        .HasColumnType("int");

                    b.Property<decimal>("OpeningPrice")
                        .HasColumnType("decimal(65,2)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("StopTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("AuctionId");

                    b.HasIndex("HourId");

                    b.HasIndex("UserId");

                    b.ToTable("Auctions");
                });

            modelBuilder.Entity("AdopPix.Models.AuctionBid", b =>
                {
                    b.Property<int>("BidId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,2)");

                    b.Property<string>("AuctionId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("BidId");

                    b.HasIndex("AuctionId");

                    b.HasIndex("UserId");

                    b.ToTable("AuctionBids");
                });

            modelBuilder.Entity("AdopPix.Models.AuctionImage", b =>
                {
                    b.Property<string>("ImageId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("AuctionId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ImageTypeId")
                        .HasColumnType("int");

                    b.HasKey("ImageId");

                    b.HasIndex("AuctionId");

                    b.HasIndex("ImageTypeId");

                    b.ToTable("AuctionImages");
                });

            modelBuilder.Entity("AdopPix.Models.AuctionNotification", b =>
                {
                    b.Property<int>("AucNotiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AuctionId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("isOpen")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("AucNotiId");

                    b.HasIndex("AuctionId");

                    b.HasIndex("UserId");

                    b.ToTable("AuctionNotifications");
                });

            modelBuilder.Entity("AdopPix.Models.AuctionTag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<string>("AuctionId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.HasKey("TagId", "AuctionId");

                    b.HasIndex("AuctionId");

                    b.ToTable("AuctionTags");
                });

            modelBuilder.Entity("AdopPix.Models.HourType", b =>
                {
                    b.Property<int>("HourId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Hour")
                        .HasColumnType("int");

                    b.HasKey("HourId");

                    b.ToTable("HourType");
                });

            modelBuilder.Entity("AdopPix.Models.ImageType", b =>
                {
                    b.Property<int>("ImageTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ImageTypeId");

                    b.ToTable("ImageTypes");
                });

            modelBuilder.Entity("AdopPix.Models.Notification", b =>
                {
                    b.Property<int>("NotiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("RedirectToUrl")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("isOpen")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("NotiId");

                    b.HasIndex("UserId");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("AdopPix.Models.PaymentLogging", b =>
                {
                    b.Property<int>("PLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,2)");

                    b.Property<string>("Brand")
                        .HasColumnType("longtext");

                    b.Property<string>("Charge")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Currency")
                        .HasColumnType("longtext");

                    b.Property<string>("Financing")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("PLogId");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentLoggings");
                });

            modelBuilder.Entity("AdopPix.Models.PointLogging", b =>
                {
                    b.Property<int>("pLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(65,2)");

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("userId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("pLogId");

                    b.HasIndex("userId");

                    b.ToTable("PointLoggings");
                });

            modelBuilder.Entity("AdopPix.Models.Post", b =>
                {
                    b.Property<string>("PostId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(160)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(160)");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("AdopPix.Models.PostComment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("PostId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("PostComments");
                });

            modelBuilder.Entity("AdopPix.Models.PostImage", b =>
                {
                    b.Property<string>("ImageId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PostId")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ImageId");

                    b.HasIndex("PostId");

                    b.ToTable("PostImage");
                });

            modelBuilder.Entity("AdopPix.Models.PostLike", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PostId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("PostLikes");
                });

            modelBuilder.Entity("AdopPix.Models.PostTag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<string>("PostId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.HasKey("TagId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("AdopPix.Models.PostView", b =>
                {
                    b.Property<int>("ViewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PostId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("ViewId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("PostViews");
                });

            modelBuilder.Entity("AdopPix.Models.RankLogging", b =>
                {
                    b.Property<int>("rLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(65,2)");

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("userId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("rLogId");

                    b.HasIndex("userId");

                    b.ToTable("RankLoggings");
                });

            modelBuilder.Entity("AdopPix.Models.SocialMedia", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SocialId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("SocialMediaTypeSocialId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId", "SocialId");

                    b.HasIndex("SocialMediaTypeSocialId");

                    b.ToTable("SocialMedias");
                });

            modelBuilder.Entity("AdopPix.Models.SocialMediaType", b =>
                {
                    b.Property<int>("SocialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SocialId");

                    b.ToTable("SocialMediaTypes");
                });

            modelBuilder.Entity("AdopPix.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("AdopPix.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AdopPix.Models.UserFollow", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("IsFollowing")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId", "IsFollowing");

                    b.ToTable("UserFollows");
                });

            modelBuilder.Entity("AdopPix.Models.UserProfile", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AvatarName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CoverName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Fname")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Lname")
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Money")
                        .HasColumnType("decimal(65,2)");

                    b.Property<decimal>("Point")
                        .HasColumnType("decimal(65,2)");

                    b.Property<decimal>("Rank")
                        .HasColumnType("decimal(65,2)");

                    b.HasKey("UserId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("AdopPix.Models.WinningBidder", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AuctionId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(65,2)");

                    b.HasKey("UserId", "AuctionId");

                    b.HasIndex("AuctionId")
                        .IsUnique();

                    b.ToTable("WinningBidders");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("AdopPix.Models.Auction", b =>
                {
                    b.HasOne("AdopPix.Models.HourType", "HourType")
                        .WithMany("Auctions")
                        .HasForeignKey("HourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdopPix.Models.User", "User")
                        .WithMany("Auctions")
                        .HasForeignKey("UserId");

                    b.Navigation("HourType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AdopPix.Models.AuctionBid", b =>
                {
                    b.HasOne("AdopPix.Models.Auction", "Auction")
                        .WithMany("AuctionBids")
                        .HasForeignKey("AuctionId");

                    b.HasOne("AdopPix.Models.User", "User")
                        .WithMany("AuctionBids")
                        .HasForeignKey("UserId");

                    b.Navigation("Auction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AdopPix.Models.AuctionImage", b =>
                {
                    b.HasOne("AdopPix.Models.Auction", "Auction")
                        .WithMany()
                        .HasForeignKey("AuctionId");

                    b.HasOne("AdopPix.Models.ImageType", "ImageType")
                        .WithMany("AuctionImages")
                        .HasForeignKey("ImageTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auction");

                    b.Navigation("ImageType");
                });

            modelBuilder.Entity("AdopPix.Models.AuctionNotification", b =>
                {
                    b.HasOne("AdopPix.Models.Auction", "Auction")
                        .WithMany("AuctionNotifications")
                        .HasForeignKey("AuctionId");

                    b.HasOne("AdopPix.Models.User", "User")
                        .WithMany("AuctionNotifications")
                        .HasForeignKey("UserId");

                    b.Navigation("Auction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AdopPix.Models.AuctionTag", b =>
                {
                    b.HasOne("AdopPix.Models.Auction", "Auction")
                        .WithMany("AuctionTags")
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdopPix.Models.Tag", "Tag")
                        .WithMany("AuctionTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auction");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("AdopPix.Models.Notification", b =>
                {
                    b.HasOne("AdopPix.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AdopPix.Models.PaymentLogging", b =>
                {
                    b.HasOne("AdopPix.Models.User", "User")
                        .WithMany("PaymentLoggings")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AdopPix.Models.PointLogging", b =>
                {
                    b.HasOne("AdopPix.Models.UserProfile", "UserProfile")
                        .WithMany("PointLogging")
                        .HasForeignKey("userId");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("AdopPix.Models.Post", b =>
                {
                    b.HasOne("AdopPix.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AdopPix.Models.PostComment", b =>
                {
                    b.HasOne("AdopPix.Models.Post", "Post")
                        .WithMany("PostComments")
                        .HasForeignKey("PostId");

                    b.HasOne("AdopPix.Models.User", "User")
                        .WithMany("PostComments")
                        .HasForeignKey("UserId");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AdopPix.Models.PostImage", b =>
                {
                    b.HasOne("AdopPix.Models.Post", "Post")
                        .WithMany("PostImages")
                        .HasForeignKey("PostId");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("AdopPix.Models.PostLike", b =>
                {
                    b.HasOne("AdopPix.Models.Post", "Post")
                        .WithMany("PostLikes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdopPix.Models.User", "User")
                        .WithMany("PostLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AdopPix.Models.PostTag", b =>
                {
                    b.HasOne("AdopPix.Models.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdopPix.Models.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("AdopPix.Models.PostView", b =>
                {
                    b.HasOne("AdopPix.Models.Post", "Post")
                        .WithMany("PostViews")
                        .HasForeignKey("PostId");

                    b.HasOne("AdopPix.Models.User", "User")
                        .WithMany("PostViews")
                        .HasForeignKey("UserId");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AdopPix.Models.RankLogging", b =>
                {
                    b.HasOne("AdopPix.Models.UserProfile", "UserProfile")
                        .WithMany("RankLogging")
                        .HasForeignKey("userId");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("AdopPix.Models.SocialMedia", b =>
                {
                    b.HasOne("AdopPix.Models.SocialMediaType", "SocialMediaType")
                        .WithMany("UserSocials")
                        .HasForeignKey("SocialMediaTypeSocialId");

                    b.HasOne("AdopPix.Models.User", "User")
                        .WithMany("SocialMedias")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SocialMediaType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AdopPix.Models.UserFollow", b =>
                {
                    b.HasOne("AdopPix.Models.User", "User")
                        .WithMany("UserFollows")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AdopPix.Models.UserProfile", b =>
                {
                    b.HasOne("AdopPix.Models.User", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("AdopPix.Models.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AdopPix.Models.WinningBidder", b =>
                {
                    b.HasOne("AdopPix.Models.Auction", "Auction")
                        .WithOne("WinningBidder")
                        .HasForeignKey("AdopPix.Models.WinningBidder", "AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdopPix.Models.User", "User")
                        .WithMany("WinningBidders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AdopPix.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AdopPix.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdopPix.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AdopPix.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AdopPix.Models.Auction", b =>
                {
                    b.Navigation("AuctionBids");

                    b.Navigation("AuctionNotifications");

                    b.Navigation("AuctionTags");

                    b.Navigation("WinningBidder");
                });

            modelBuilder.Entity("AdopPix.Models.HourType", b =>
                {
                    b.Navigation("Auctions");
                });

            modelBuilder.Entity("AdopPix.Models.ImageType", b =>
                {
                    b.Navigation("AuctionImages");
                });

            modelBuilder.Entity("AdopPix.Models.Post", b =>
                {
                    b.Navigation("PostComments");

                    b.Navigation("PostImages");

                    b.Navigation("PostLikes");

                    b.Navigation("PostTags");

                    b.Navigation("PostViews");
                });

            modelBuilder.Entity("AdopPix.Models.SocialMediaType", b =>
                {
                    b.Navigation("UserSocials");
                });

            modelBuilder.Entity("AdopPix.Models.Tag", b =>
                {
                    b.Navigation("AuctionTags");

                    b.Navigation("PostTags");
                });

            modelBuilder.Entity("AdopPix.Models.User", b =>
                {
                    b.Navigation("AuctionBids");

                    b.Navigation("AuctionNotifications");

                    b.Navigation("Auctions");

                    b.Navigation("Notifications");

                    b.Navigation("PaymentLoggings");

                    b.Navigation("PostComments");

                    b.Navigation("PostLikes");

                    b.Navigation("Posts");

                    b.Navigation("PostViews");

                    b.Navigation("SocialMedias");

                    b.Navigation("UserFollows");

                    b.Navigation("UserProfile");

                    b.Navigation("WinningBidders");
                });

            modelBuilder.Entity("AdopPix.Models.UserProfile", b =>
                {
                    b.Navigation("PointLogging");

                    b.Navigation("RankLogging");
                });
#pragma warning restore 612, 618
        }
    }
}
