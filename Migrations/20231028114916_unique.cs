using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
        name: "Username",
        table: "Users",
        type: "nvarchar(max)",
        nullable: true,  // Change this from false to true
        oldClrType: typeof(string),
        oldType: "nvarchar(max)");
    }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
