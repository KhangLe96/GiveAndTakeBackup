using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Giveaway.Data.EF.Migrations
{
    public partial class ChangeNotificationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Notification");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Notification",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSeen",
                table: "Notification",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "IsSeen",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundColor",
                table: "Notification",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Notification",
                nullable: false,
                defaultValue: 0);
        }
    }
}
