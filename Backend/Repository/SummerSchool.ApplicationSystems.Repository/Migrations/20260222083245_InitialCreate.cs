using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SummerSchool.ApplicationSystems.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COURSE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DEPARTMENT = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FACULTY = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    QUOTA = table.Column<int>(type: "int", nullable: false),
                    ADDED_USER = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ADDED_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_USER = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_ACTIVE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COURSE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OTP_VERIFICATION",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    ADDED_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IS_ACTIVE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTP_VERIFICATION", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "STUDENT",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IDENTITY_NUMBER = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    SCHOOL_NUMBER = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DEPARTMENT = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FACULTY = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    COUNTRY_CODE = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    IS_ACTIVE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "COURSE_APPLICATION",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    STUDENT_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COURSE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    APPLICATION_STATUS = table.Column<int>(type: "int", nullable: false),
                    UPDATED_USER = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_ACTIVE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COURSE_APPLICATION", x => x.ID);
                    table.ForeignKey(
                        name: "FK_COURSE_APPLICATION_COURSE_COURSE_ID",
                        column: x => x.COURSE_ID,
                        principalTable: "COURSE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_COURSE_APPLICATION_STUDENT_STUDENT_ID",
                        column: x => x.STUDENT_ID,
                        principalTable: "STUDENT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_COURSE_APPLICATION_COURSE_ID",
                table: "COURSE_APPLICATION",
                column: "COURSE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COURSE_APPLICATION_STUDENT_ID_COURSE_ID",
                table: "COURSE_APPLICATION",
                columns: new[] { "STUDENT_ID", "COURSE_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OTP_VERIFICATION_PHONE_NUMBER",
                table: "OTP_VERIFICATION",
                column: "PHONE_NUMBER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COURSE_APPLICATION");

            migrationBuilder.DropTable(
                name: "OTP_VERIFICATION");

            migrationBuilder.DropTable(
                name: "COURSE");

            migrationBuilder.DropTable(
                name: "STUDENT");
        }
    }
}
