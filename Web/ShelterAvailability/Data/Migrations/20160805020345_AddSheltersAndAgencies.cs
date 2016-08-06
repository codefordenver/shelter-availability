using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShelterAvailability.Data.Migrations
{
    public partial class AddSheltersAndAgencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    AgencyID = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.AgencyID);
                });

            migrationBuilder.CreateTable(
                name: "Shelters",
                columns: table => new
                {
                    ShelterID = table.Column<string>(nullable: false),
                    AgencyID = table.Column<int>(nullable: true),
                    AvailabilityLastUpdated = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    CurrentPopulation = table.Column<int>(nullable: false),
                    CurrentTotalSpaces = table.Column<int>(nullable: false),
                    FamilySpacesAvailable = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    SingleSpacesAvailable = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelters", x => x.ShelterID);
                    table.ForeignKey(
                        name: "FK_Shelters_Agencies_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agencies",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shelters_AgencyID",
                table: "Shelters",
                column: "AgencyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shelters");

            migrationBuilder.DropTable(
                name: "Agencies");
        }
    }
}
