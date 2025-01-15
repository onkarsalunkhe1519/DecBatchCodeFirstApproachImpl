using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecBatchCodeFirstApproachImpl.Migrations
{
    /// <inheritdoc />
    public partial class emp12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "emp",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "emp");
        }
    }
}
