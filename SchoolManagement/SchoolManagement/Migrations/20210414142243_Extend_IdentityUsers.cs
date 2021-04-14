﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Migrations
{
    public partial class Extend_IdentityUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(name: "City", table: "AspNetUsers", nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "City", table: "AspNetUsers");
        }
    }
}
