using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Context.Migrations
{
    /// <inheritdoc />
    public partial class Add_Image_columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Projects",
                newName: "ImageContent");

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Projects",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageType",
                table: "Projects",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ImageContent",
                table: "Projects",
                newName: "Content");
        }
    }
}
