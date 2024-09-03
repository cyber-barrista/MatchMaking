using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMaking.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    game_name = table.Column<string>(type: "text", nullable: false),
                    player_count = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_games", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "game_sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    state = table.Column<string>(type: "text", nullable: true),
                    latency_ms = table.Column<int>(type: "integer", nullable: true),
                    game_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game_sessions", x => x.id);
                    table.ForeignKey(
                        name: "fk_game_sessions_games_game_id",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    game_session_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_players", x => x.id);
                    table.ForeignKey(
                        name: "fk_players_game_sessions_game_session_id",
                        column: x => x.game_session_id,
                        principalTable: "game_sessions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "player_requests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    request_type = table.Column<string>(type: "text", nullable: false),
                    game_session_id = table.Column<Guid>(type: "uuid", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    latency = table.Column<int>(type: "integer", nullable: false),
                    game_id = table.Column<Guid>(type: "uuid", nullable: false),
                    player_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_player_requests", x => x.id);
                    table.ForeignKey(
                        name: "fk_player_requests_games_game_id",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_player_requests_players_player_id",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_game_sessions_game_id",
                table: "game_sessions",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "ix_games_game_name",
                table: "games",
                column: "game_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_player_requests_game_id",
                table: "player_requests",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "ix_player_requests_player_id",
                table: "player_requests",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "ix_players_game_session_id",
                table: "players",
                column: "game_session_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "player_requests");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "game_sessions");

            migrationBuilder.DropTable(
                name: "games");
        }
    }
}
