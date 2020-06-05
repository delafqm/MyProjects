using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

namespace FullSearchApp.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(maxLength: 128, nullable: false),
                    abst = table.Column<string>(maxLength: 512, nullable: true),
                    title_vector = table.Column<NpgsqlTsVector>(nullable: true),
                    abst_vector = table.Column<NpgsqlTsVector>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_articles", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_articles_abst_vector",
                table: "articles",
                column: "abst_vector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "ix_articles_title_vector",
                table: "articles",
                column: "title_vector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            #region 英文词库触发
            migrationBuilder.Sql(
            @"CREATE TRIGGER article_title_search_vector_update BEFORE INSERT OR UPDATE
              ON articles FOR EACH ROW EXECUTE PROCEDURE
              tsvector_update_trigger(title_vector, 'pg_catalog.english', title);");

            migrationBuilder.Sql(
                        @"CREATE TRIGGER article_abst_search_vector_update BEFORE INSERT OR UPDATE
              ON articles FOR EACH ROW EXECUTE PROCEDURE
              tsvector_update_trigger(abst_vector, 'pg_catalog.english', abst);");
            #endregion

            #region 中文词库触发
            //migrationBuilder.Sql(@"CREATE EXTENSION zhparser;");
            //migrationBuilder.Sql(@"CREATE TEXT SEARCH CONFIGURATION chinese_zh (PARSER = zhparser);");
            //migrationBuilder.Sql(@"ALTER TEXT SEARCH CONFIGURATION chinese_zh ADD MAPPING FOR n,v,a,i,e,l WITH simple;");

            //migrationBuilder.Sql(
            //            @"CREATE TRIGGER article_title_search_vector_update BEFORE INSERT OR UPDATE
            //  ON articles FOR EACH ROW EXECUTE PROCEDURE
            //  tsvector_update_trigger(title_vector, 'public.chinese_zh', title);");
            //migrationBuilder.Sql(
            //            @"CREATE TRIGGER article_abst_search_vector_update BEFORE INSERT OR UPDATE
            //  ON articles FOR EACH ROW EXECUTE PROCEDURE
            //  tsvector_update_trigger(abst_vector, 'public.chinese_zh', abst);");
            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articles");
        }
    }
}
