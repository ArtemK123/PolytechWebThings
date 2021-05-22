using Microsoft.EntityFrameworkCore.Migrations;

namespace PolytechWebThings.Infrastructure.Database.Migrations
{
    public partial class AddedRuleAndStepModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChangeThingStateStepDatabaseModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThingId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewPropertyState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleDatabaseModelId = table.Column<int>(type: "int", nullable: true),
                    RuleId = table.Column<int>(type: "int", nullable: false),
                    ExecutionOrderPosition = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeThingStateStepDatabaseModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeThingStateStepDatabaseModel_Rules_RuleDatabaseModelId",
                        column: x => x.RuleDatabaseModelId,
                        principalTable: "Rules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExecuteRuleStepDatabaseModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RuleDatabaseModelId = table.Column<int>(type: "int", nullable: true),
                    RuleId = table.Column<int>(type: "int", nullable: false),
                    ExecutionOrderPosition = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecuteRuleStepDatabaseModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecuteRuleStepDatabaseModel_Rules_RuleDatabaseModelId",
                        column: x => x.RuleDatabaseModelId,
                        principalTable: "Rules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangeThingStateStepDatabaseModel_RuleDatabaseModelId",
                table: "ChangeThingStateStepDatabaseModel",
                column: "RuleDatabaseModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecuteRuleStepDatabaseModel_RuleDatabaseModelId",
                table: "ExecuteRuleStepDatabaseModel",
                column: "RuleDatabaseModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeThingStateStepDatabaseModel");

            migrationBuilder.DropTable(
                name: "ExecuteRuleStepDatabaseModel");

            migrationBuilder.DropTable(
                name: "Rules");
        }
    }
}