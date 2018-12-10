using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Giveaway.Data.EF.Migrations
{
    public partial class ChangeRelationshipRequestResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Response_RequestId",
                table: "Response",
                column: "RequestId",
                unique: true);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Response_RequestId",
                table: "Response",
                column: "RequestId");
        }
    }
}
