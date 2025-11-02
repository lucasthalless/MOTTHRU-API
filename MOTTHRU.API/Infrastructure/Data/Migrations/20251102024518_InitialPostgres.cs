using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MOTTHRU.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "patio",
                columns: table => new
                {
                    id_patio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome_patio = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patio", x => x.id_patio);
                });

            migrationBuilder.CreateTable(
                name: "moto",
                columns: table => new
                {
                    id_moto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    placa = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    chassi = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    num_motor = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    patio_id_patio = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moto", x => x.id_moto);
                    table.ForeignKey(
                        name: "FK_moto_patio_patio_id_patio",
                        column: x => x.patio_id_patio,
                        principalTable: "patio",
                        principalColumn: "id_patio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rfid",
                columns: table => new
                {
                    id_rfid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sinal = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    moto_id_moto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rfid", x => x.id_rfid);
                    table.ForeignKey(
                        name: "FK_rfid_moto_moto_id_moto",
                        column: x => x.moto_id_moto,
                        principalTable: "moto",
                        principalColumn: "id_moto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_moto_patio_id_patio",
                table: "moto",
                column: "patio_id_patio");

            migrationBuilder.CreateIndex(
                name: "IX_rfid_moto_id_moto",
                table: "rfid",
                column: "moto_id_moto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rfid");

            migrationBuilder.DropTable(
                name: "moto");

            migrationBuilder.DropTable(
                name: "patio");
        }
    }
}
