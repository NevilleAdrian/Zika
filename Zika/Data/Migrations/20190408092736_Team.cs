using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Zika.Data.Migrations
{
    public partial class Team : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Skype",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Mass",
                table: "Freights",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Skype",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Mass",
                table: "Freights");
        }
    }
}
