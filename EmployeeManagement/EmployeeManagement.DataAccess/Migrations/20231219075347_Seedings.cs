using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.DataAccess.Migrations
{
    public partial class Seedings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 1, "Seeding", new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeding", new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "IT" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 2, "Seeding", new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeding", new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "HR" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 3, "Seeding", new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeding", new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marketing" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Age", "BirthDate", "CreatedBy", "CreatedOn", "DepartmentId", "LastModifiedBy", "LastModifiedOn", "MonthlyPayment", "Name", "Surname" },
                values: new object[] { 1, 22, new DateTime(2001, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeding", new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Seeding", new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 10000m, "Mahmood", "Garibov" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Age", "BirthDate", "CreatedBy", "CreatedOn", "DepartmentId", "LastModifiedBy", "LastModifiedOn", "MonthlyPayment", "Name", "Surname" },
                values: new object[] { 2, 26, new DateTime(1996, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeding", new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Seeding", new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 3000m, "Elchin", "Garibov" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Age", "BirthDate", "CreatedBy", "CreatedOn", "DepartmentId", "LastModifiedBy", "LastModifiedOn", "MonthlyPayment", "Name", "Surname" },
                values: new object[] { 3, 32, new DateTime(1998, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeding", new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Seeding", new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000m, "Eldar", "Rasulov" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
