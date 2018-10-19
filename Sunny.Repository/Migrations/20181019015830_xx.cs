using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunny.Repository.Migrations
{
    public partial class xx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
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
                    table.PrimaryKey("PK_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IdTest",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdTest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passage",
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
                    table.PrimaryKey("PK_Passage", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
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
                    a_a_a = table.Column<string>(maxLength: 30, nullable: true),
                    score = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Uesr2s",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PuaId = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uesr2s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uesrs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PuaId = table.Column<decimal>(nullable: true),
                    test = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uesrs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PassageCategory",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                    table.PrimaryKey("PK_PassageCategory", x => x.id);
                    table.ForeignKey(
                        name: "FK_PassageCategory_Category_category_id",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassageCategory_Passage_passage_id",
                        column: x => x.passage_id,
                        principalTable: "Passage",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAddress",
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
                    table.PrimaryKey("PK_StudentAddress", x => x.id);
                    table.ForeignKey(
                        name: "FK_StudentAddress_Student_student_id",
                        column: x => x.student_id,
                        principalTable: "Student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassageCategory_category_id",
                table: "PassageCategory",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_PassageCategory_passage_id",
                table: "PassageCategory",
                column: "passage_id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAddress_student_id",
                table: "StudentAddress",
                column: "student_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdTest");

            migrationBuilder.DropTable(
                name: "PassageCategory");

            migrationBuilder.DropTable(
                name: "StudentAddress");

            migrationBuilder.DropTable(
                name: "Uesr2s");

            migrationBuilder.DropTable(
                name: "Uesrs");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Passage");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
