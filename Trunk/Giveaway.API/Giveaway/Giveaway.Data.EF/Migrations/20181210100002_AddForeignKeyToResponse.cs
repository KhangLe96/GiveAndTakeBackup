using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Giveaway.Data.EF.Migrations
{
    public partial class AddForeignKeyToResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.AddForeignKey(
		        name: "FK_Response_Request_RequestId",
		        table: "Response",
		        column: "RequestId",
		        principalTable: "Request",
		        principalColumn: "Id",
		        onDelete: ReferentialAction.Cascade);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
