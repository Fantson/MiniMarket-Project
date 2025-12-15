using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMarket.API.Migrations
{
    public partial class FixSqlProcedure : Migration
    {
        // Metoda wykonywana przy aktualizacji bazy
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Dla bezpieczeństwa usuwamy starą procedurę (jeśli jest)
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS \"ApplyDiscount\";");

            // 2. Tworzymy nową procedurę
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

        // Metoda cofająca zmiany
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS \"ApplyDiscount\";");
        }
    }
}