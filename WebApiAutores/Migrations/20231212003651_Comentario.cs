using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAutores.Migrations
{
    /// <inheritdoc />
    public partial class Comentario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Lbros_LibroId",
                table: "Comentarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lbros",
                table: "Lbros");

            migrationBuilder.RenameTable(
                name: "Lbros",
                newName: "Libros");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Libros",
                table: "Libros",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Libros_LibroId",
                table: "Comentarios",
                column: "LibroId",
                principalTable: "Libros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Libros_LibroId",
                table: "Comentarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Libros",
                table: "Libros");

            migrationBuilder.RenameTable(
                name: "Libros",
                newName: "Lbros");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lbros",
                table: "Lbros",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Lbros_LibroId",
                table: "Comentarios",
                column: "LibroId",
                principalTable: "Lbros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
