﻿// <auto-generated />
using Game2048.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Game2048.DAL.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20180305160803_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Game2048.DAL.Entities.Game2048Entity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BoardXml");

                    b.Property<int>("Score");

                    b.Property<int>("SizeBoard");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.ToTable("Games");
                });
#pragma warning restore 612, 618
        }
    }
}