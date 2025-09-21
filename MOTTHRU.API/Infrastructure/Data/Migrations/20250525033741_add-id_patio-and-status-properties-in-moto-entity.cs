using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOTTHRU.API.Migrations
{
    /// <inheritdoc />
    public partial class addid_patioandstatuspropertiesinmotoentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id_patio",
                table: "moto");

            migrationBuilder.DropColumn(
                name: "status",
                table: "moto");
        }
    }
}
