using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMarket.API.Migrations
{
    public partial class FixSqlProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS \"ApplyDiscount\";");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE PROCEDURE ""ApplyDiscount""(categoryName TEXT, discountPercentage DECIMAL)
                LANGUAGE plpgsql
                AS $$
                BEGIN
                    UPDATE ""Products""
                    SET ""Price"" = ""Price"" - (""Price"" * (discountPercentage / 100.0))
                    WHERE ""Category"" = categoryName;
                END;
                $$;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS \"ApplyDiscount\";");
        }
    }
}