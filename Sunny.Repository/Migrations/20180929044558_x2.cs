using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunny.Repository.Migrations
{
    public partial class x2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_StudentAddress_StudentAddressId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Student_StudentId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_StudentAddressId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_StudentId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "StudentAddressId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Category");

            migrationBuilder.AlterColumn<DateTime>(
                name: "row_version",
                table: "StudentAddress",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "row_version",
                table: "Student",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "row_version",
                table: "PassageCategory",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "row_version",
                table: "Passage",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "row_version",
                table: "Category",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "row_version",
                table: "StudentAddress",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "row_version",
                table: "Student",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "row_version",
                table: "PassageCategory",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "row_version",
                table: "Passage",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "row_version",
                table: "Category",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StudentAddressId",
                table: "Category",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "Category",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_StudentAddressId",
                table: "Category",
                column: "StudentAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_StudentId",
                table: "Category",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_StudentAddress_StudentAddressId",
                table: "Category",
                column: "StudentAddressId",
                principalTable: "StudentAddress",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Student_StudentId",
                table: "Category",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
