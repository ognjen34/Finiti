using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finiti.DATA.Migrations
{
    /// <inheritdoc />
    public partial class forbiddenwords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "ForbiddenWords",
            columns: new[] { "Id", "Word" },
            values: new object[,]
            {
                { 0, "lorem" },
                { 1, "test" },
                { 2, "sample" }
            });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
