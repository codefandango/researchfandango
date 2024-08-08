using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFandango.Flamenco.Data.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantFieldDefinitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParticipantFieldDefinitions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowInList = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantFieldDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantFieldDefinitionCustomers",
                columns: table => new
                {
                    ParticipantFieldDefinitionId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantFieldDefinitionCustomers", x => new { x.ParticipantFieldDefinitionId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_ParticipantFieldDefinitionCustomers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantFieldDefinitionCustomers_ParticipantFieldDefinitions_ParticipantFieldDefinitionId",
                        column: x => x.ParticipantFieldDefinitionId,
                        principalTable: "ParticipantFieldDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantFieldDefinitionCustomers_CustomerId",
                table: "ParticipantFieldDefinitionCustomers",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantFieldDefinitionCustomers");

            migrationBuilder.DropTable(
                name: "ParticipantFieldDefinitions");
        }
    }
}
