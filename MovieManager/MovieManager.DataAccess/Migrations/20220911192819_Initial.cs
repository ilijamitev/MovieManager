using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieManager.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersMovies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersMovies", x => new { x.MovieId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UsersMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersMovies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Genre", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "Greg Focker and his fiancee Pam decide to make their respective parents meet before their wedding. However, the Fockers' relaxed attitude does not go down well with Pam's family.", 2, "Meet the Fockers", 2004 },
                    { 2, "James takes Ben along to pull the plug on a drug racket involving an influential businessman, Antonio Pope. However, with Ben's wedding day approaching, the two have little time to expose the crime.", 2, "Ride Along 2", 2016 },
                    { 3, "In the world of international crime, an Interpol agent attempts to hunt down and capture the world's most wanted art thief.", 1, "Red Notice", 2021 },
                    { 4, "Victor Sullivan recruits Nathan Drake to help him find the lost fortune of Ferdinand Magellan. However, they face competition from Santiago Moncada, who believes that the treasure belongs to him.", 33, "Uncharted", 2022 },
                    { 5, "The revival of Emperor Palpatine resurrects the battle between the Resistance and the First Order while the Jedi's legendary conflict with the Sith Lord comes to a head.", 48, "Star Wars: The Rise of Skywalker (Episode IX)", 2022 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Role", "Username" },
                values: new object[] { 1, "Ilija", "Mitev", "ile123", 2, "ile123" });

            migrationBuilder.InsertData(
                table: "UsersMovies",
                columns: new[] { "MovieId", "UserId", "Id" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "UsersMovies",
                columns: new[] { "MovieId", "UserId", "Id" },
                values: new object[] { 3, 1, 3 });

            migrationBuilder.InsertData(
                table: "UsersMovies",
                columns: new[] { "MovieId", "UserId", "Id" },
                values: new object[] { 5, 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_UsersMovies_UserId",
                table: "UsersMovies",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersMovies");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
