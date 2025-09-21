using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOTTHRU.API.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addpatioandrfidtablesandupdatemototable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id_patio",
                table: "moto");

            migrationBuilder.DropColumn(
                name: "status",
                table: "moto");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "moto",
                newName: "id_moto");

            migrationBuilder.AlterColumn<string>(
                name: "placa",
                table: "moto",
                type: "NVARCHAR2(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "num_motor",
                table: "moto",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "chassi",
                table: "moto",
                type: "NVARCHAR2(17)",
                maxLength: 17,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AddColumn<int>(
                name: "patio_id_patio",
                table: "moto",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "patio",
                columns: table => new
                {
                    id_patio = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome_patio = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patio", x => x.id_patio);
                });

            migrationBuilder.CreateTable(
                name: "rfid",
                columns: table => new
                {
                    id_rfid = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    sinal = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false),
                    moto_id_moto = table.Column<int>(type: "NUMBER(10)", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_moto_patio_patio_id_patio",
                table: "moto",
                column: "patio_id_patio",
                principalTable: "patio",
                principalColumn: "id_patio",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_moto_patio_patio_id_patio",
                table: "moto");

            migrationBuilder.DropTable(
                name: "patio");

            migrationBuilder.DropTable(
                name: "rfid");

            migrationBuilder.DropIndex(
                name: "IX_moto_patio_id_patio",
                table: "moto");

            migrationBuilder.DropColumn(
                name: "patio_id_patio",
                table: "moto");

            migrationBuilder.RenameColumn(
                name: "id_moto",
                table: "moto",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "placa",
                table: "moto",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(7)",
                oldMaxLength: 7);

            migrationBuilder.AlterColumn<string>(
                name: "num_motor",
                table: "moto",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "chassi",
                table: "moto",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(17)",
                oldMaxLength: 17);

            migrationBuilder.AddColumn<string>(
                name: "id_patio",
                table: "moto",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "moto",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");
        }
    }
}
