using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaesarsTest.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "Id", "Address1", "Address2", "City", "DateOfBirth", "EmailAddress", "FirstName", "LastName", "PhoneNumber", "PostalCode", "StateCode" },
                values: new object[,]
                {
                    { new Guid("c031dcab-f47f-4f27-b430-129ca4fdae3c"), "345 Front St", null, "Atlantic City", new DateTime(1980, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "fjohnson@kmail.com", "Frank", "Johnson", "1234567890", "08201", "NJ" },
                    { new Guid("d2adc02f-e50b-48eb-9278-b24e6fdc1518"), "123 Main St", "Apt 101", "Atlantic City", new DateTime(1962, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "jwilliams@kmail.com", "Jim", "Williams", "2345678901", "08201", "NJ" },
                    { new Guid("d9112bdb-cc4b-4c4d-bbfc-3c69334bc133"), "789 Bridge St", null, "Las Vegas", new DateTime(1990, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "mrichards@kmail.com", "Mary", "Richards", "3456789012", "88901", "NV" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guests");
        }
    }
}
