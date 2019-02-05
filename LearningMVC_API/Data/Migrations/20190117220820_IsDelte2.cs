using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LearningMVC_API.Data.Migrations
{
    public partial class IsDelte2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "reservedModel");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "classModel",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "classModel");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "reservedModel",
                nullable: false,
                defaultValue: false);
        }
    }
}
