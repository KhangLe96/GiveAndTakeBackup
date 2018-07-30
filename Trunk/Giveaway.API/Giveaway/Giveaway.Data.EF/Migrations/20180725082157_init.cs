using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Giveaway.Data.EF.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    GroupType = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SettingName = table.Column<string>(nullable: false),
                    SettingValue = table.Column<string>(nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    AllowTokensSince = table.Column<DateTimeOffset>(nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    Dob = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 40, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    IsActivated = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastLogin = table.Column<DateTimeOffset>(nullable: false),
                    LastName = table.Column<string>(maxLength: 40, nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    UserName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avatar",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BigImagePath = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MediumImagePath = table.Column<string>(nullable: true),
                    SmallImagePath = table.Column<string>(nullable: true),
                    UpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avatar_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuperAdmin",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperAdmin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuperAdmin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_UserId",
                table: "Admin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Avatar_UserId",
                table: "Avatar",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuperAdmin_UserId",
                table: "SuperAdmin",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Avatar");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "SuperAdmin");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
