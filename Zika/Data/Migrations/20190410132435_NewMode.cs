using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Zika.Data.Migrations
{
    public partial class NewMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FreightTo",
                table: "Freights",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FreightFrom",
                table: "Freights",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FreightSummary",
                table: "Freights",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreightSummary",
                table: "Freights");

            migrationBuilder.AlterColumn<string>(
                name: "FreightTo",
                table: "Freights",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FreightFrom",
                table: "Freights",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
