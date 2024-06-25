﻿// <auto-generated />
using System;
using FourMinator.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FourMinator.Persistence.Migrations
{
    [DbContext(typeof(FourminatorContext))]
    [Migration("20240625114802_EmqxRequirementsMatchSuperUser")]
    partial class EmqxRequirementsMatchSuperUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("FourMinator.Persistence.Domain.Game.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("AbortedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FinishedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<uint>("PlayerRedId")
                        .HasColumnType("int unsigned");

                    b.Property<uint>("PlayerYellowId")
                        .HasColumnType("int unsigned");

                    b.Property<uint?>("RobotId")
                        .HasColumnType("int unsigned");

                    b.Property<DateTime?>("StartedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<short>("State")
                        .HasColumnType("smallint");

                    b.Property<uint?>("WinnerId")
                        .HasColumnType("int unsigned");

                    b.HasKey("Id");

                    b.HasIndex("PlayerRedId");

                    b.HasIndex("PlayerYellowId");

                    b.HasIndex("RobotId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("FourMinator.Persistence.Domain.Game.MatchMoves", b =>
                {
                    b.Property<Guid>("MatchId")
                        .HasColumnType("char(36)");

                    b.Property<short>("MoveNumber")
                        .HasColumnType("smallint");

                    b.Property<bool>("Color")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Joker")
                        .HasColumnType("tinyint(1)");

                    b.Property<uint>("MoveTime")
                        .HasColumnType("int unsigned");

                    b.Property<DateTime>("MoveTimestamp")
                        .HasColumnType("datetime(6)");

                    b.Property<uint>("PlayerId")
                        .HasColumnType("int unsigned");

                    b.Property<bool>("Skipped")
                        .HasColumnType("tinyint(1)");

                    b.Property<short>("X")
                        .HasColumnType("smallint");

                    b.Property<short>("Y")
                        .HasColumnType("smallint");

                    b.HasKey("MatchId", "MoveNumber");

                    b.HasIndex("PlayerId");

                    b.ToTable("MatchMoves");
                });

            modelBuilder.Entity("FourMinator.Persistence.Domain.Game.Player", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("Id"));

                    b.Property<bool>("IsBot")
                        .HasColumnType("tinyint(1)");

                    b.Property<short>("State")
                        .HasColumnType("smallint");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Players");
                });

            modelBuilder.Entity("FourMinator.Persistence.Domain.IdentityProvider", b =>
                {
                    b.Property<Guid>("IdentityProviderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AuthKey")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Domain")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SourceIp")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("IdentityProviderId");

                    b.ToTable("IdentityProviders");
                });

            modelBuilder.Entity("FourMinator.Persistence.Domain.Robot", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSuperUser")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_superuser");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("username");

                    b.Property<string>("Password")
                        .HasColumnType("longtext")
                        .HasColumnName("password_hash");

                    b.Property<string>("PublicKey")
                        .HasColumnType("longtext");

                    b.Property<string>("Salt")
                        .HasColumnType("longtext")
                        .HasColumnName("salt");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.ToTable("Robots");
                });

            modelBuilder.Entity("FourMinator.Persistence.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FourMinator.Persistence.Domain.Game.Match", b =>
                {
                    b.HasOne("FourMinator.Persistence.Domain.Game.Player", "PlayerRed")
                        .WithMany("MatchesAsRed")
                        .HasForeignKey("PlayerRedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FourMinator.Persistence.Domain.Game.Player", "PlayerYellow")
                        .WithMany("MatchesAsYellow")
                        .HasForeignKey("PlayerYellowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FourMinator.Persistence.Domain.Robot", "Robot")
                        .WithMany("Matches")
                        .HasForeignKey("RobotId");

                    b.HasOne("FourMinator.Persistence.Domain.Game.Player", "PlayerWinner")
                        .WithMany("MatchesAsWinner")
                        .HasForeignKey("WinnerId");

                    b.Navigation("PlayerRed");

                    b.Navigation("PlayerWinner");

                    b.Navigation("PlayerYellow");

                    b.Navigation("Robot");
                });

            modelBuilder.Entity("FourMinator.Persistence.Domain.Game.MatchMoves", b =>
                {
                    b.HasOne("FourMinator.Persistence.Domain.Game.Match", "Match")
                        .WithMany("Moves")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FourMinator.Persistence.Domain.Game.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("FourMinator.Persistence.Domain.Game.Player", b =>
                {
                    b.HasOne("FourMinator.Persistence.Domain.User", "User")
                        .WithOne("Player")
                        .HasForeignKey("FourMinator.Persistence.Domain.Game.Player", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FourMinator.Persistence.Domain.Robot", b =>
                {
                    b.HasOne("FourMinator.Persistence.Domain.User", "CreatedByUser")
                        .WithMany("Robots")
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("FourMinator.Persistence.Domain.Game.Match", b =>
                {
                    b.Navigation("Moves");
                });

            modelBuilder.Entity("FourMinator.Persistence.Domain.Game.Player", b =>
                {
                    b.Navigation("MatchesAsRed");

                    b.Navigation("MatchesAsWinner");

                    b.Navigation("MatchesAsYellow");
                });

            modelBuilder.Entity("FourMinator.Persistence.Domain.Robot", b =>
                {
                    b.Navigation("Matches");
                });

            modelBuilder.Entity("FourMinator.Persistence.Domain.User", b =>
                {
                    b.Navigation("Player")
                        .IsRequired();

                    b.Navigation("Robots");
                });
#pragma warning restore 612, 618
        }
    }
}
