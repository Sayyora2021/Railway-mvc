using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Railway.Data.Migrations
{
    public partial class Railway_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buillets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Titre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buillets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aller = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Retour = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cotisation = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exemplaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuilletId = table.Column<int>(type: "int", nullable: false),
                    NumeroInventaire = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiseEnService = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exemplaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exemplaires_Buillets_BuilletId",
                        column: x => x.BuilletId,
                        principalTable: "Buillets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuilletDestination",
                columns: table => new
                {
                    BuilletsId = table.Column<int>(type: "int", nullable: false),
                    DestinationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuilletDestination", x => new { x.BuilletsId, x.DestinationsId });
                    table.ForeignKey(
                        name: "FK_BuilletDestination_Buillets_BuilletsId",
                        column: x => x.BuilletsId,
                        principalTable: "Buillets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuilletDestination_Destinations_DestinationsId",
                        column: x => x.DestinationsId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuilletTrain",
                columns: table => new
                {
                    BuilletsId = table.Column<int>(type: "int", nullable: false),
                    TrainsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuilletTrain", x => new { x.BuilletsId, x.TrainsId });
                    table.ForeignKey(
                        name: "FK_BuilletTrain_Buillets_BuilletsId",
                        column: x => x.BuilletsId,
                        principalTable: "Buillets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuilletTrain_Trains_TrainsId",
                        column: x => x.TrainsId,
                        principalTable: "Trains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExemplaireId = table.Column<int>(type: "int", nullable: false),
                    PassagerId = table.Column<int>(type: "int", nullable: false),
                    DateAller = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateRetour = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Exemplaires_ExemplaireId",
                        column: x => x.ExemplaireId,
                        principalTable: "Exemplaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Passagers_PassagerId",
                        column: x => x.PassagerId,
                        principalTable: "Passagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuilletDestination_DestinationsId",
                table: "BuilletDestination",
                column: "DestinationsId");

            migrationBuilder.CreateIndex(
                name: "IX_BuilletTrain_TrainsId",
                table: "BuilletTrain",
                column: "TrainsId");

            migrationBuilder.CreateIndex(
                name: "IX_Exemplaires_BuilletId",
                table: "Exemplaires",
                column: "BuilletId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ExemplaireId",
                table: "Reservations",
                column: "ExemplaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PassagerId",
                table: "Reservations",
                column: "PassagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuilletDestination");

            migrationBuilder.DropTable(
                name: "BuilletTrain");

            migrationBuilder.DropTable(
                name: "Gares");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "Trains");

            migrationBuilder.DropTable(
                name: "Exemplaires");

            migrationBuilder.DropTable(
                name: "Passagers");

            migrationBuilder.DropTable(
                name: "Buillets");
        }
    }
}
