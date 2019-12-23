﻿// <auto-generated />
using System;
using GhostChat.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GhostChat.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20191222150652_FriendshipStatus")]
    partial class FriendshipStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("GhostChat.Data.Models.Friendship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AcceptingUserId");

                    b.Property<bool>("AreFriends");

                    b.Property<Guid?>("RequestingUserId");

                    b.HasKey("Id");

                    b.HasIndex("AcceptingUserId");

                    b.HasIndex("RequestingUserId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("GhostChat.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GhostChat.Data.Models.Friendship", b =>
                {
                    b.HasOne("GhostChat.Data.Models.User", "AcceptingUser")
                        .WithMany()
                        .HasForeignKey("AcceptingUserId");

                    b.HasOne("GhostChat.Data.Models.User", "RequestingUser")
                        .WithMany()
                        .HasForeignKey("RequestingUserId");
                });
#pragma warning restore 612, 618
        }
    }
}