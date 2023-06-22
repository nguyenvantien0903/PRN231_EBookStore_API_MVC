using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    First_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email_address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Publisher_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.PublisherId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_desc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Advance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Royalty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Ytd_sales = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Published_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email_adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    First_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Middle_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Hire_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BooksAuthors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Author_order = table.Column<int>(type: "int", nullable: false),
                    Royalty_percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksAuthors", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BooksAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Address", "City", "Email_address", "First_name", "Last_name", "Phone", "State", "Zip" },
                values: new object[,]
                {
                    { 1, "Address 1", "City 1", "Email1@gmail.com", "Lmao", "Lmao", "1234567890", "State 1", "Zip 1" },
                    { 2, "Address 2", "City 2", "Email2@gmail.com", "Lmao2", "Lmao2", "12345678902", "State 2", "Zip 2" },
                    { 3, "Address 3", "City 3", "Email3@gmail.com", "Lmao3", "Lmao3", "12345678903", "State 3", "Zip 3" },
                    { 4, "Address 4", "City 4", "Email4@gmail.com", "Lmao4", "Lmao4", "12345678904", "State 4", "Zip 4" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "PublisherId", "City", "Country", "Publisher_name", "State" },
                values: new object[,]
                {
                    { 1, "City 1", "Country 1", "Publish 1", "State 1" },
                    { 2, "City 2", "Country 2", "Publish 2", "State 2" },
                    { 3, "City 3", "Country 3", "Publish 3", "State 3" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Role_desc" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Advance", "Notes", "Price", "Published_date", "PublisherId", "Royalty", "Title", "Type", "Ytd_sales" },
                values: new object[,]
                {
                    { 1, 5.1m, "Notes 1", 10.1m, new DateTime(2022, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1.1m, "Title 1", "Type 1", 100m },
                    { 2, 50.1m, "Notes 2", 100.1m, new DateTime(2022, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 10.1m, "Title 2", "Type 2", 1000m },
                    { 3, 500.1m, "Notes 3", 1000.1m, new DateTime(2022, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 100.1m, "Title 3", "Type 3", 10000m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email_adress", "First_name", "Hire_date", "Last_name", "Middle_name", "Password", "PublisherId", "RoleId", "Source" },
                values: new object[,]
                {
                    { 1, "Email1User@gmail.com", "First1", new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Last1", "Middle1", "123456", 1, 1, "Source1" },
                    { 2, "Email2User@gmail.com", "First2", new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Last2", "Middle2", "123456", 2, 2, "Source2" },
                    { 3, "Email3User@gmail.com", "First3", new DateTime(2022, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Last2", "Middle3", "123456", 3, 2, "Source3" }
                });

            migrationBuilder.InsertData(
                table: "BooksAuthors",
                columns: new[] { "AuthorId", "BookId", "Author_order", "Royalty_percentage" },
                values: new object[,]
                {
                    { 1, 1, 100, 0.5m },
                    { 2, 2, 1000, 0.55m },
                    { 3, 3, 10000, 0.555m },
                    { 4, 1, 100000, 0.5555m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksAuthors_BookId",
                table: "BooksAuthors",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PublisherId",
                table: "Users",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksAuthors");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
