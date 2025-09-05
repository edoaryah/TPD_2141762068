using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspnetCoreMvcFull.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KategoriSurats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NamaKategori = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Keterangan = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategoriSurats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArsipSurats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomorSurat = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Judul = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    WaktuPengarsipan = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    KategoriSuratId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArsipSurats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArsipSurats_KategoriSurats_KategoriSuratId",
                        column: x => x.KategoriSuratId,
                        principalTable: "KategoriSurats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "KategoriSurats",
                columns: new[] { "Id", "Keterangan", "NamaKategori" },
                values: new object[,]
                {
                    { 1, "Surat bersifat undangan resmi.", "Undangan" },
                    { 2, "Surat bersifat pengumuman.", "Pengumuman" },
                    { 3, "Surat internal antar dinas.", "Nota Dinas" },
                    { 4, "Surat bersifat pemberitahuan informasi.", "Pemberitahuan" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArsipSurats_KategoriSuratId",
                table: "ArsipSurats",
                column: "KategoriSuratId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArsipSurats");

            migrationBuilder.DropTable(
                name: "KategoriSurats");
        }
    }
}
