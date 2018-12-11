//using Microsoft.EntityFrameworkCore.Migrations;
//using System;
//using System.Collections.Generic;

//namespace Giveaway.Data.EF.Migrations
//{
//    public partial class ChangeRelationshipBetweenRequestAndResponse : Migration
//    {
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropIndex(
//                name: "IX_Response_RequestId",
//                table: "Response");

//            migrationBuilder.CreateIndex(
//                name: "IX_Response_RequestId",
//                table: "Response",
//                column: "RequestId");
//        }

//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropIndex(
//                name: "IX_Response_RequestId",
//                table: "Response");

//            migrationBuilder.CreateIndex(
//                name: "IX_Response_RequestId",
//                table: "Response",
//                column: "RequestId",
//                unique: true);
//        }
//    }
//}
