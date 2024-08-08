using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFandango.Flamenco.Data.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantFieldDefinitions2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StudyId",
                table: "ParticipantFieldDefinitions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SurveyId",
                table: "ParticipantFieldDefinitions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantFieldDefinitions_StudyId",
                table: "ParticipantFieldDefinitions",
                column: "StudyId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantFieldDefinitions_SurveyId",
                table: "ParticipantFieldDefinitions",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantFieldDefinitions_Studies_StudyId",
                table: "ParticipantFieldDefinitions",
                column: "StudyId",
                principalTable: "Studies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantFieldDefinitions_Surveys_SurveyId",
                table: "ParticipantFieldDefinitions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantFieldDefinitions_Studies_StudyId",
                table: "ParticipantFieldDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantFieldDefinitions_Surveys_SurveyId",
                table: "ParticipantFieldDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantFieldDefinitions_StudyId",
                table: "ParticipantFieldDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantFieldDefinitions_SurveyId",
                table: "ParticipantFieldDefinitions");

            migrationBuilder.DropColumn(
                name: "StudyId",
                table: "ParticipantFieldDefinitions");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "ParticipantFieldDefinitions");
        }
    }
}
