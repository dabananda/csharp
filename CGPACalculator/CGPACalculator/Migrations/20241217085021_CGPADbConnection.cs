using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CGPACalculator.Migrations
{
    /// <inheritdoc />
    public partial class CGPADbConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SemesterGPAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Semister = table.Column<int>(type: "int", nullable: false),
                    GPA = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterGPAs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemesterGPAs");
        }
    }
}
