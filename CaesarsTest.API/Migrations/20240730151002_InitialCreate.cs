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
                name: "HotelLocations",
                columns: table => new
                {
                    HotelLocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelLocations", x => x.HotelLocationId);
                });

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
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HotelLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guests_HotelLocations_HotelLocationId",
                        column: x => x.HotelLocationId,
                        principalTable: "HotelLocations",
                        principalColumn: "HotelLocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HotelLocations",
                columns: new[] { "HotelLocationId", "LocationName" },
                values: new object[,]
                {
                    { 1, "Atlantic City" },
                    { 2, "Las Vegas" }
                });

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "Id", "Address1", "Address2", "ArrivalDate", "City", "DateOfBirth", "DepartureDate", "EmailAddress", "FirstName", "HotelLocationId", "LastName", "PhoneNumber", "PostalCode", "StateCode" },
                values: new object[,]
                {
                    { new Guid("2d73aa9a-4c2c-4011-98da-a0abe80556b9"), "789 Bridge St", null, new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Las Vegas", new DateTime(1990, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "mrichards@kmail.com", "Mary", 2, "Richards", "3456789012", "88901", "NV" },
                    { new Guid("63e951ff-ccba-4efa-ad95-ad9b167eac37"), "123 Main St", "Apt 101", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Atlantic City", new DateTime(1962, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "jwilliams@kmail.com", "Jim", 1, "Williams", "2345678901", "08201", "NJ" },
                    { new Guid("8fcd2a88-95e1-44c2-8d7e-fe500292880e"), "345 Front St", null, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Atlantic City", new DateTime(1980, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "fjohnson@kmail.com", "Frank", 1, "Johnson", "1234567890", "08201", "NJ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guests_HotelLocationId",
                table: "Guests",
                column: "HotelLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "HotelLocations");
        }
    }
}
