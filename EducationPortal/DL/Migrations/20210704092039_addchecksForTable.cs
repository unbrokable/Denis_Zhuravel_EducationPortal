using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addchecksForTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skills",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Materials",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Materials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Format",
                table: "BookMaterial",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "BookMaterial",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "videomaterial_length_check",
                table: "VideoMaterial",
                sql: "[Length] > 0 ");

            migrationBuilder.AddCheckConstraint(
                name: "videomaterial_height_check",
                table: "VideoMaterial",
                sql: "[Height] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "videomaterial_width_check",
                table: "VideoMaterial",
                sql: "[Width] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "user_email_check",
                table: "Users",
                sql: "len([Email]) >=  10");

            migrationBuilder.AddCheckConstraint(
                name: "user_name_check",
                table: "Users",
                sql: "len([Name]) >= 5");

            migrationBuilder.AddCheckConstraint(
                name: "user_password_check",
                table: "Users",
                sql: "len([Password]) >= 5");

            migrationBuilder.AddCheckConstraint(
                name: "skill_name_check",
                table: "Skills",
                sql: "len([Name]) >= 3");

            migrationBuilder.AddCheckConstraint(
                name: "material_location_check",
                table: "Materials",
                sql: "[Location] <> '' ");

            migrationBuilder.AddCheckConstraint(
                name: "material_name_check",
                table: "Materials",
                sql: "len([Name]) >= 5");

            migrationBuilder.AddCheckConstraint(
                name: "bookmaterial_author_check",
                table: "BookMaterial",
                sql: "LEN([Author]) >= 5");

            migrationBuilder.AddCheckConstraint(
                name: "bookmaterial_format_check",
                table: "BookMaterial",
                sql: "LEN([Format]) >= 3");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "videomaterial_length_check",
                table: "VideoMaterial");

            migrationBuilder.DropCheckConstraint(
                name: "videomaterial_height_check",
                table: "VideoMaterial");

            migrationBuilder.DropCheckConstraint(
                name: "videomaterial_width_check",
                table: "VideoMaterial");

            migrationBuilder.DropCheckConstraint(
                name: "user_email_check",
                table: "Users");

            migrationBuilder.DropCheckConstraint(
                name: "user_name_check",
                table: "Users");

            migrationBuilder.DropCheckConstraint(
                name: "user_password_check",
                table: "Users");

            migrationBuilder.DropCheckConstraint(
                name: "skill_name_check",
                table: "Skills");

            migrationBuilder.DropCheckConstraint(
                name: "material_location_check",
                table: "Materials");

            migrationBuilder.DropCheckConstraint(
                name: "material_name_check",
                table: "Materials");

            migrationBuilder.DropCheckConstraint(
                name: "bookmaterial_author_check",
                table: "BookMaterial");

            migrationBuilder.DropCheckConstraint(
                name: "bookmaterial_format_check",
                table: "BookMaterial");

            migrationBuilder.DropCheckConstraint(
                name: "videomaterial_length_check",
                table: "VideoMaterial");

            migrationBuilder.DropCheckConstraint(
                name: "videomaterial_height_check",
                table: "VideoMaterial");

            migrationBuilder.DropCheckConstraint(
                name: "videomaterial_width_check",
                table: "VideoMaterial");

            migrationBuilder.DropCheckConstraint(
                name: "course_name_check",
                table: "Courses");

            migrationBuilder.DropCheckConstraint(
                name: "bookmaterial_author_check",
                table: "BookMaterial");

            migrationBuilder.DropCheckConstraint(
                name: "bookmaterial_format_check",
                table: "BookMaterial");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skills",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Materials",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Materials",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Format",
                table: "BookMaterial",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "BookMaterial",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
