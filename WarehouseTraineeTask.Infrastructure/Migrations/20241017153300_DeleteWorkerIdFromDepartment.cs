using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseTraineeTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteWorkerIdFromDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Departments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "Departments",
                type: "uuid",
                nullable: true);
        }
    }
}
