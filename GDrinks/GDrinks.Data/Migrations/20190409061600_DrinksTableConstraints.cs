namespace GDrinks.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DrinksTableConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPreferredDrink",
                table: "Drinks",
                newName: "IsPreferred");

            migrationBuilder.RenameColumn(
                name: "InStock",
                table: "Drinks",
                newName: "IsInStock");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Drinks",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Drinks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageThumbnailUrl",
                table: "Drinks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullDescription",
                table: "Drinks",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Drinks",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPreferred",
                table: "Drinks",
                newName: "IsPreferredDrink");

            migrationBuilder.RenameColumn(
                name: "IsInStock",
                table: "Drinks",
                newName: "InStock");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Drinks",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Drinks",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ImageThumbnailUrl",
                table: "Drinks",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FullDescription",
                table: "Drinks",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Drinks",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 300);
        }
    }
}
