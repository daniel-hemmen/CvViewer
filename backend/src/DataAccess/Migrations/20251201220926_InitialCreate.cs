using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CvViewer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auteurs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Voornaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tussenvoegsel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Achternaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres_Straat = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Adres_Huisnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres_Plaats = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Adres_Postcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Adres_Land = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contactgegevens_Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Contactgegevens_Telefoonnummer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Contactgegevens_LinkedInUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CertificaatInstances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uitgever = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumAfgifte = table.Column<DateOnly>(type: "date", nullable: false),
                    Verloopdatum = table.Column<DateOnly>(type: "date", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificaatInstances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpleidingInstances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instituut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Startdatum = table.Column<DateOnly>(type: "date", nullable: true),
                    Einddatum = table.Column<DateOnly>(type: "date", nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpleidingInstances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WerkervaringInstances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Organisatie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Startdatum = table.Column<DateOnly>(type: "date", nullable: false),
                    Einddatum = table.Column<DateOnly>(type: "date", nullable: true),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WerkervaringInstances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cvs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuteurId = table.Column<long>(type: "bigint", nullable: false),
                    ContactgegevensSnapshot_Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ContactgegevensSnapshot_Telefoonnummer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactgegevensSnapshot_LinkedInUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AdresSnapshot_Straat = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AdresSnapshot_Huisnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdresSnapshot_Postcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AdresSnapshot_Stad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AdresSnapshot_Land = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VaardigheidInstances = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inleiding = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsFavorite = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cvs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cvs_Auteurs_AuteurId",
                        column: x => x.AuteurId,
                        principalTable: "Auteurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CertificaatInstanceSnapshot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uitgever = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumAfgifte = table.Column<DateOnly>(type: "date", nullable: true),
                    Verloopdatum = table.Column<DateOnly>(type: "date", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CvId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificaatInstanceSnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificaatInstanceSnapshot_Cvs_CvId",
                        column: x => x.CvId,
                        principalTable: "Cvs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpleidingInstanceSnapshot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instituut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Startdatum = table.Column<DateOnly>(type: "date", nullable: true),
                    Einddatum = table.Column<DateOnly>(type: "date", nullable: true),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CvId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpleidingInstanceSnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpleidingInstanceSnapshot_Cvs_CvId",
                        column: x => x.CvId,
                        principalTable: "Cvs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WerkervaringInstanceSnapshot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bedrijfsnaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Functietitel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Startdatum = table.Column<DateOnly>(type: "date", nullable: false),
                    Einddatum = table.Column<DateOnly>(type: "date", nullable: true),
                    Locatie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CvId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WerkervaringInstanceSnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WerkervaringInstanceSnapshot_Cvs_CvId",
                        column: x => x.CvId,
                        principalTable: "Cvs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CertificaatInstanceSnapshot_CvId",
                table: "CertificaatInstanceSnapshot",
                column: "CvId");

            migrationBuilder.CreateIndex(
                name: "IX_Cvs_AuteurId",
                table: "Cvs",
                column: "AuteurId");

            migrationBuilder.CreateIndex(
                name: "IX_OpleidingInstanceSnapshot_CvId",
                table: "OpleidingInstanceSnapshot",
                column: "CvId");

            migrationBuilder.CreateIndex(
                name: "IX_WerkervaringInstanceSnapshot_CvId",
                table: "WerkervaringInstanceSnapshot",
                column: "CvId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CertificaatInstances");

            migrationBuilder.DropTable(
                name: "CertificaatInstanceSnapshot");

            migrationBuilder.DropTable(
                name: "OpleidingInstances");

            migrationBuilder.DropTable(
                name: "OpleidingInstanceSnapshot");

            migrationBuilder.DropTable(
                name: "WerkervaringInstances");

            migrationBuilder.DropTable(
                name: "WerkervaringInstanceSnapshot");

            migrationBuilder.DropTable(
                name: "Cvs");

            migrationBuilder.DropTable(
                name: "Auteurs");
        }
    }
}
