﻿// <auto-generated />
using Giveaway.Data.EF.DataContext;
using Giveaway.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Giveaway.Data.EF.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20180824111103_change_Image_table")]
    partial class change_Image_table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Giveaway.Data.Models.Database.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("CategoryStatus");

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<string>("ImageUrl");

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CommentMessage")
                        .IsRequired();

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<Guid>("PostId");

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Conversation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("Conversation");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<string>("OriginalImage")
                        .IsRequired();

                    b.Property<Guid>("PostId");

                    b.Property<string>("ResizedImage")
                        .IsRequired();

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<string>("MessageContent")
                        .IsRequired();

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.Property<Guid>("UserConversationId");

                    b.HasKey("Id");

                    b.HasIndex("UserConversationId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CategoryId");

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("EntityStatus");

                    b.Property<int>("PostStatus");

                    b.Property<Guid>("ProvinceCityId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProvinceCityId");

                    b.HasIndex("UserId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.ProvinceCity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<string>("ProvinceCityName")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("ProvinceCity");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Report", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<Guid>("PostId");

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Request", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<Guid?>("PostId");

                    b.Property<string>("RequestMessage")
                        .IsRequired();

                    b.Property<int>("RequestStatus");

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Response", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<Guid>("RequestId");

                    b.Property<string>("ResponseMessage")
                        .IsRequired();

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.HasKey("Id");

                    b.HasIndex("RequestId")
                        .IsUnique();

                    b.ToTable("Response");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<string>("RoleName")
                        .IsRequired();

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<DateTimeOffset?>("AllowTokensSince");

                    b.Property<int>("AppreciationNumber");

                    b.Property<string>("AvatarUrl");

                    b.Property<DateTime?>("BirthDate");

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<string>("Email");

                    b.Property<int>("EntityStatus");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<int?>("Gender");

                    b.Property<DateTimeOffset?>("LastLogin");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired();

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("SocialAccountId");

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.UserConversation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ConversationId");

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserConversation");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<Guid>("RoleId");

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.WarningMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedTime");

                    b.Property<int>("EntityStatus");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<DateTimeOffset>("UpdatedTime");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("WarningMessage");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Comment", b =>
                {
                    b.HasOne("Giveaway.Data.Models.Database.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Giveaway.Data.Models.Database.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Feedback", b =>
                {
                    b.HasOne("Giveaway.Data.Models.Database.User", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Image", b =>
                {
                    b.HasOne("Giveaway.Data.Models.Database.Post", "Post")
                        .WithMany("Images")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Message", b =>
                {
                    b.HasOne("Giveaway.Data.Models.Database.UserConversation", "UserConversation")
                        .WithMany("Messages")
                        .HasForeignKey("UserConversationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Post", b =>
                {
                    b.HasOne("Giveaway.Data.Models.Database.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Giveaway.Data.Models.Database.ProvinceCity", "ProvinceCity")
                        .WithMany("Posts")
                        .HasForeignKey("ProvinceCityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Giveaway.Data.Models.Database.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Report", b =>
                {
                    b.HasOne("Giveaway.Data.Models.Database.Post", "Post")
                        .WithMany("Reports")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Giveaway.Data.Models.Database.User", "User")
                        .WithMany("Reports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Request", b =>
                {
                    b.HasOne("Giveaway.Data.Models.Database.Post", "Post")
                        .WithMany("Requests")
                        .HasForeignKey("PostId");

                    b.HasOne("Giveaway.Data.Models.Database.User", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.Response", b =>
                {
                    b.HasOne("Giveaway.Data.Models.Database.Request", "Request")
                        .WithOne("Response")
                        .HasForeignKey("Giveaway.Data.Models.Database.Response", "RequestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.UserConversation", b =>
                {
                    b.HasOne("Giveaway.Data.Models.Database.Conversation", "Conversation")
                        .WithMany("Conversations")
                        .HasForeignKey("ConversationId");

                    b.HasOne("Giveaway.Data.Models.Database.User", "User")
                        .WithMany("UserConversations")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.UserRole", b =>
                {
                    b.HasOne("Giveaway.Data.Models.Database.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Giveaway.Data.Models.Database.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Giveaway.Data.Models.Database.WarningMessage", b =>
                {
                    b.HasOne("Giveaway.Data.Models.Database.User", "User")
                        .WithMany("WarningMessages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
