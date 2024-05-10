using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BIsleriumBlog.Migrations
{
    /// <inheritdoc />
    public partial class inni3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Blogs",
                newName: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Blogs",
                newName: "UserId");
        }
    }
}
