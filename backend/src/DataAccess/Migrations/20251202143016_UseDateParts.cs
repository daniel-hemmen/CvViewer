using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CvViewer.DataAccess.Migrations;

/// <inheritdoc />
public partial class UseDateParts : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Title",
            table: "OpleidingInstanceSnapshot");

        migrationBuilder.RenameColumn(
            name: "Locatie",
            table: "WerkervaringInstanceSnapshot",
            newName: "Plaats");

        migrationBuilder.RenameColumn(
            name: "Functietitel",
            table: "WerkervaringInstanceSnapshot",
            newName: "Rol");

        migrationBuilder.RenameColumn(
            name: "Bedrijfsnaam",
            table: "WerkervaringInstanceSnapshot",
            newName: "Organisatie");

        migrationBuilder.AddColumn<string>(
            name: "Plaats",
            table: "WerkervaringInstances",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Instituut",
            table: "OpleidingInstanceSnapshot",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateOnly>(
            name: "Einddatum",
            table: "OpleidingInstanceSnapshot",
            type: "date",
            nullable: false,
            defaultValue: new DateOnly(1, 1, 1),
            oldClrType: typeof(DateOnly),
            oldType: "date",
            oldNullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Naam",
            table: "OpleidingInstanceSnapshot",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AlterColumn<string>(
            name: "Uitgever",
            table: "CertificaatInstanceSnapshot",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Naam",
            table: "CertificaatInstanceSnapshot",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateOnly>(
            name: "DatumAfgifte",
            table: "CertificaatInstanceSnapshot",
            type: "date",
            nullable: false,
            defaultValue: new DateOnly(1, 1, 1),
            oldClrType: typeof(DateOnly),
            oldType: "date",
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Plaats",
            table: "WerkervaringInstances");

        migrationBuilder.DropColumn(
            name: "Naam",
            table: "OpleidingInstanceSnapshot");

        migrationBuilder.RenameColumn(
            name: "Rol",
            table: "WerkervaringInstanceSnapshot",
            newName: "Functietitel");

        migrationBuilder.RenameColumn(
            name: "Plaats",
            table: "WerkervaringInstanceSnapshot",
            newName: "Locatie");

        migrationBuilder.RenameColumn(
            name: "Organisatie",
            table: "WerkervaringInstanceSnapshot",
            newName: "Bedrijfsnaam");

        migrationBuilder.AlterColumn<string>(
            name: "Instituut",
            table: "OpleidingInstanceSnapshot",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<DateOnly>(
            name: "Einddatum",
            table: "OpleidingInstanceSnapshot",
            type: "date",
            nullable: true,
            oldClrType: typeof(DateOnly),
            oldType: "date");

        migrationBuilder.AddColumn<string>(
            name: "Title",
            table: "OpleidingInstanceSnapshot",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Uitgever",
            table: "CertificaatInstanceSnapshot",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "Naam",
            table: "CertificaatInstanceSnapshot",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<DateOnly>(
            name: "DatumAfgifte",
            table: "CertificaatInstanceSnapshot",
            type: "date",
            nullable: true,
            oldClrType: typeof(DateOnly),
            oldType: "date");
    }
}
