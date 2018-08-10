using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vega.Migrations
{
    public partial class photoChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Vehicles_VehicleId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_VehicleId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Photos");

            migrationBuilder.AddColumn<int>(
                name: "VehichleId",
                table: "Photos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_VehichleId",
                table: "Photos",
                column: "VehichleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Vehicles_VehichleId",
                table: "Photos",
                column: "VehichleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Vehicles_VehichleId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_VehichleId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "VehichleId",
                table: "Photos");

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Photos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_VehicleId",
                table: "Photos",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Vehicles_VehicleId",
                table: "Photos",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
