using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryDemo.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    creater_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    updater_id = table.Column<long>(nullable: false),
                    category_name = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IdTest",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    requestType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdTest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "passage",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    creater_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    updater_id = table.Column<long>(nullable: false),
                    title = table.Column<string>(maxLength: 30, nullable: true),
                    last_edit_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passage", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    creater_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    updater_id = table.Column<long>(nullable: false),
                    student_name = table.Column<string>(maxLength: 30, nullable: true),
                    test = table.Column<string>(maxLength: 30, nullable: true),
                    a_a2 = table.Column<string>(maxLength: 30, nullable: true),
                    score = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "passage_category",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    create_time = table.Column<DateTime>(nullable: false),
                    creater_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    updater_id = table.Column<long>(nullable: false),
                    category_id = table.Column<long>(nullable: false),
                    passage_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passage_category", x => new { x.passage_id, x.category_id });
                    table.ForeignKey(
                        name: "FK_passage_category_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_passage_category_passage_passage_id",
                        column: x => x.passage_id,
                        principalTable: "passage",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student_address",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    creater_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    updater_id = table.Column<long>(nullable: false),
                    address1 = table.Column<string>(maxLength: 30, nullable: true),
                    zipcode = table.Column<int>(nullable: false),
                    state = table.Column<string>(maxLength: 30, nullable: true),
                    country = table.Column<string>(maxLength: 30, nullable: true),
                    student_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_address", x => x.id);
                    table.ForeignKey(
                        name: "FK_student_address_student_student_id",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_passage_category_category_id",
                table: "passage_category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_address_student_id",
                table: "student_address",
                column: "student_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdTest");

            migrationBuilder.DropTable(
                name: "passage_category");

            migrationBuilder.DropTable(
                name: "student_address");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "passage");

            migrationBuilder.DropTable(
                name: "student");
        }
    }
}
