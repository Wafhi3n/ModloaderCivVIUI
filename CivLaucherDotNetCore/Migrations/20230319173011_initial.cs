using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModLoader.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "config",
                columns: table => new
                {
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_config", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "gitHub",
                columns: table => new
                {
                    call = table.Column<string>(type: "TEXT", nullable: false),
                    value = table.Column<string>(type: "TEXT", nullable: true),
                    date = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gitHub", x => x.call);
                });

            migrationBuilder.CreateTable(
                name: "mod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    path = table.Column<string>(type: "TEXT", nullable: true),
                    modID = table.Column<string>(type: "TEXT", nullable: true),
                    owner = table.Column<string>(type: "TEXT", nullable: true),
                    depot = table.Column<string>(type: "TEXT", nullable: true),
                    tag = table.Column<string>(type: "TEXT", nullable: true),
                    lastag = table.Column<string>(type: "TEXT", nullable: true),
                    ModRowId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modSqlite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    path = table.Column<string>(type: "TEXT", nullable: true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    modID = table.Column<string>(type: "TEXT", nullable: true),
                    isSteam = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModRowId = table.Column<int>(type: "INTEGER", nullable: true),
                    ScannedFileRowId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modSqlite", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "config");

            migrationBuilder.DropTable(
                name: "gitHub");

            migrationBuilder.DropTable(
                name: "mod");

            migrationBuilder.DropTable(
                name: "modSqlite");
        }
    }
}
