using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaarvensBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobNo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ArchitectName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    SiteLocation = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrawingsDetails",
                columns: table => new
                {
                    DrawingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrawingName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DrawingStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Revision = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ProjectDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrawingsDetails", x => x.DrawingId);
                    table.ForeignKey(
                        name: "FK_DrawingsDetails_ProjectDetails_ProjectDetailsId",
                        column: x => x.ProjectDetailsId,
                        principalTable: "ProjectDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrawingsDetails_ProjectDetailsId",
                table: "DrawingsDetails",
                column: "ProjectDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrawingsDetails");

            migrationBuilder.DropTable(
                name: "ProjectDetails");
        }
    }
}
