using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Koeman_INTDEV_SE_1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMetAfbeeldingUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AfbeeldingUrl",
                table: "Producten",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfbeeldingUrl",
                table: "Producten");
        }
    }
}
