using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Replacing.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncryptedMessages",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncryptedMessageText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncryptedMessages", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "ReplaceWords",
                columns: table => new
                {
                    OldSymbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NewSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplaceWords", x => x.OldSymbol);
                });

            migrationBuilder.InsertData(
                table: "EncryptedMessages",
                columns: new[] { "MessageId", "EncryptedMessageText", "OriginalMessage", "ReceivedTime" },
                values: new object[] { new Guid("f0bb13c3-8e78-4020-8cc5-b98e4547a62f"), " ", " ", new DateTime(2023, 11, 20, 18, 2, 9, 461, DateTimeKind.Local).AddTicks(129) });

            migrationBuilder.InsertData(
                table: "ReplaceWords",
                columns: new[] { "OldSymbol", "NewSymbol" },
                values: new object[] { "а", "н" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncryptedMessages");

            migrationBuilder.DropTable(
                name: "ReplaceWords");
        }
    }
}
