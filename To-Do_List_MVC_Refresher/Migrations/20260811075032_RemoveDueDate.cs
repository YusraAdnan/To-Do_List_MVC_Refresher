using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_Do_List_MVC_Refresher.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDueDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "TaskItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "TaskItems",
                type: "datetime2",
                nullable: true);
        }
    }
}
