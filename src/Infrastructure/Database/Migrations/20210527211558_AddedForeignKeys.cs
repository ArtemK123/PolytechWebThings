using Microsoft.EntityFrameworkCore.Migrations;

namespace PolytechWebThings.Infrastructure.Database.Migrations
{
    public partial class AddedForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeThingStateStepDatabaseModel_Rules_RuleDatabaseModelId",
                table: "ChangeThingStateStepDatabaseModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ExecuteRuleStepDatabaseModel_Rules_RuleDatabaseModelId",
                table: "ExecuteRuleStepDatabaseModel");

            migrationBuilder.DropIndex(
                name: "IX_ExecuteRuleStepDatabaseModel_RuleDatabaseModelId",
                table: "ExecuteRuleStepDatabaseModel");

            migrationBuilder.DropIndex(
                name: "IX_ChangeThingStateStepDatabaseModel_RuleDatabaseModelId",
                table: "ChangeThingStateStepDatabaseModel");

            migrationBuilder.DropColumn(
                name: "RuleDatabaseModelId",
                table: "ExecuteRuleStepDatabaseModel");

            migrationBuilder.DropColumn(
                name: "RuleDatabaseModelId",
                table: "ChangeThingStateStepDatabaseModel");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Workspaces",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_Email",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_UserEmail",
                table: "Workspaces",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_WorkspaceId",
                table: "Rules",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecuteRuleStepDatabaseModel_RuleId",
                table: "ExecuteRuleStepDatabaseModel",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeThingStateStepDatabaseModel_RuleId",
                table: "ChangeThingStateStepDatabaseModel",
                column: "RuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeThingStateStepDatabaseModel_Rules_RuleId",
                table: "ChangeThingStateStepDatabaseModel",
                column: "RuleId",
                principalTable: "Rules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExecuteRuleStepDatabaseModel_Rules_RuleId",
                table: "ExecuteRuleStepDatabaseModel",
                column: "RuleId",
                principalTable: "Rules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rules_Workspaces_WorkspaceId",
                table: "Rules",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workspaces_Users_UserEmail",
                table: "Workspaces",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeThingStateStepDatabaseModel_Rules_RuleId",
                table: "ChangeThingStateStepDatabaseModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ExecuteRuleStepDatabaseModel_Rules_RuleId",
                table: "ExecuteRuleStepDatabaseModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Rules_Workspaces_WorkspaceId",
                table: "Rules");

            migrationBuilder.DropForeignKey(
                name: "FK_Workspaces_Users_UserEmail",
                table: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Workspaces_UserEmail",
                table: "Workspaces");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Rules_WorkspaceId",
                table: "Rules");

            migrationBuilder.DropIndex(
                name: "IX_ExecuteRuleStepDatabaseModel_RuleId",
                table: "ExecuteRuleStepDatabaseModel");

            migrationBuilder.DropIndex(
                name: "IX_ChangeThingStateStepDatabaseModel_RuleId",
                table: "ChangeThingStateStepDatabaseModel");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Workspaces",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "RuleDatabaseModelId",
                table: "ExecuteRuleStepDatabaseModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RuleDatabaseModelId",
                table: "ChangeThingStateStepDatabaseModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExecuteRuleStepDatabaseModel_RuleDatabaseModelId",
                table: "ExecuteRuleStepDatabaseModel",
                column: "RuleDatabaseModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeThingStateStepDatabaseModel_RuleDatabaseModelId",
                table: "ChangeThingStateStepDatabaseModel",
                column: "RuleDatabaseModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeThingStateStepDatabaseModel_Rules_RuleDatabaseModelId",
                table: "ChangeThingStateStepDatabaseModel",
                column: "RuleDatabaseModelId",
                principalTable: "Rules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExecuteRuleStepDatabaseModel_Rules_RuleDatabaseModelId",
                table: "ExecuteRuleStepDatabaseModel",
                column: "RuleDatabaseModelId",
                principalTable: "Rules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}