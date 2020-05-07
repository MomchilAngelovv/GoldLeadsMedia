using Microsoft.EntityFrameworkCore.Migrations;

namespace GoldLeadsMedia.Database.Migrations
{
    public partial class Add_Column_IdInPartner_Leads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdInPartner",
                table: "Leads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdInPartner",
                table: "Leads");
        }
    }
}
