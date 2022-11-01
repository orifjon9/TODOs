using Microsoft.EntityFrameworkCore.Migrations;

namespace TODOs.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Todos_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Lists",
                columns: new[] { "Id", "Label" },
                values: new object[] { 1, "Morning" });

            migrationBuilder.InsertData(
                table: "Lists",
                columns: new[] { "Id", "Label" },
                values: new object[] { 2, "Evening" });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "Label", "ListId", "Status" },
                values: new object[,]
                {
                    { 1, "Brush teeth", 1, false },
                    { 2, "Breakfast", 1, false },
                    { 3, "Dress up", 1, false },
                    { 4, "Spend time with family", 2, false },
                    { 5, "Have a dinner", 2, false },
                    { 6, "Watch TV", 2, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_ListId",
                table: "Todos",
                column: "ListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "Lists");
        }
    }
}
