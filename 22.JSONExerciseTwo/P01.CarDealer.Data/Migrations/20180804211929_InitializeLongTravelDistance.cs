﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace P01.CarDealer.Data.Migrations
{
    public partial class InitializeLongTravelDistance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "TravelledDistance",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TravelledDistance",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
