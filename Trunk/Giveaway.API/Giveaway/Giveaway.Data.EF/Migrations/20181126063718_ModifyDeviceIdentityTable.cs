using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Giveaway.Data.EF.Migrations
{
    public partial class ModifyDeviceIdentityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MobilePlatform",
                table: "DeviceIdentity",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MobilePlatform",
                table: "DeviceIdentity",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
