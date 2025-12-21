using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEventsAndNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_AspNetUsers_UploadedById",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_UploadedById",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "UploadedById",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "UploadedAt",
                table: "Notes",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Notes",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Notes",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "StartAt",
                table: "Events",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndAt",
                table: "Events",
                newName: "EndDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Notes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Events",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notes",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Notes",
                newName: "UploadedAt");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Notes",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Events",
                newName: "StartAt");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Events",
                newName: "EndAt");

            migrationBuilder.AddColumn<string>(
                name: "UploadedById",
                table: "Notes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UploadedById",
                table: "Notes",
                column: "UploadedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_AspNetUsers_UploadedById",
                table: "Notes",
                column: "UploadedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
