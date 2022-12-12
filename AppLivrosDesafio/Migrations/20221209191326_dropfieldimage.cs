using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLivrosDesafio.Migrations
{
    public partial class dropfieldimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Books",
                type: "longblob",
                nullable: false);
        }
    }
}
