using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinControl.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class Accountsmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BankBranch = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
