﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModLoader.Controller;

#nullable disable

namespace ModLoader.Migrations
{
    [DbContext(typeof(DBConfigurationContext))]
    partial class DBConfigurationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("ModloaderClass.Model.Config", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Key");

                    b.ToTable("config");
                });

            modelBuilder.Entity("ModloaderClass.Model.GitHubCallApiCache", b =>
                {
                    b.Property<string>("call")
                        .HasColumnType("TEXT");

                    b.Property<string>("date")
                        .HasColumnType("TEXT");

                    b.Property<string>("value")
                        .HasColumnType("TEXT");

                    b.HasKey("call");

                    b.ToTable("gitHub");
                });

            modelBuilder.Entity("ModloaderClass.Model.ModGit", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ModRowId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("depot")
                        .HasColumnType("TEXT");

                    b.Property<string>("lastag")
                        .HasColumnType("TEXT");

                    b.Property<string>("modID")
                        .HasColumnType("TEXT");

                    b.Property<string>("owner")
                        .HasColumnType("TEXT");

                    b.Property<string>("path")
                        .HasColumnType("TEXT");

                    b.Property<string>("tag")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("mod");
                });

            modelBuilder.Entity("ModloaderClass.Model.ModSqlite", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ModRowId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ScannedFileRowId")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("isSteam")
                        .HasColumnType("INTEGER");

                    b.Property<string>("modID")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<string>("path")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("modSqlite");
                });
#pragma warning restore 612, 618
        }
    }
}
