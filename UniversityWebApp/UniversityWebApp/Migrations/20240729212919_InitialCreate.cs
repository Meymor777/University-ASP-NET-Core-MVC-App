using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COURSES",
                columns: table => new
                {
                    COURSE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__COURSES__71CB31DBCE873BC7", x => x.COURSE_ID);
                });

            migrationBuilder.CreateTable(
                name: "GROUPS",
                columns: table => new
                {
                    GROUP_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COURSE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GROUPS__3EFEA3DE50B89559", x => x.GROUP_ID);
                    table.ForeignKey(
                        name: "FK__GROUPS__COURSE_I__440B1D61",
                        column: x => x.COURSE_ID,
                        principalTable: "COURSES",
                        principalColumn: "COURSE_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STUDENTS",
                columns: table => new
                {
                    STUDENT_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GROUP_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    LAST_NAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__STUDENTS__E69FE77B602858DA", x => x.STUDENT_ID);
                    table.ForeignKey(
                        name: "FK__STUDENTS__GROUP___46E78A0C",
                        column: x => x.GROUP_ID,
                        principalTable: "GROUPS",
                        principalColumn: "GROUP_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GROUPS_COURSE_ID",
                table: "GROUPS",
                column: "COURSE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENTS_GROUP_ID",
                table: "STUDENTS",
                column: "GROUP_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STUDENTS");

            migrationBuilder.DropTable(
                name: "GROUPS");

            migrationBuilder.DropTable(
                name: "COURSES");
        }
    }
}
