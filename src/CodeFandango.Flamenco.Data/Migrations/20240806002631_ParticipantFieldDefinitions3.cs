using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFandango.Flamenco.Data.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantFieldDefinitions3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ParticipantFieldDefinitions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "ParticipantFieldDefinitions");
        }
    }
}
