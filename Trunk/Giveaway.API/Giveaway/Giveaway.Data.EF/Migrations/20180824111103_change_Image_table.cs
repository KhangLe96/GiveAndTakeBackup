using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Giveaway.Data.EF.Migrations
{
    public partial class change_Image_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Image",
                newName: "ResizedImage");

            migrationBuilder.AddColumn<string>(
                name: "OriginalImage",
                table: "Image",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalImage",
                table: "Image");

            migrationBuilder.RenameColumn(
                name: "ResizedImage",
                table: "Image",
                newName: "ImageUrl");
        }
    }
}
