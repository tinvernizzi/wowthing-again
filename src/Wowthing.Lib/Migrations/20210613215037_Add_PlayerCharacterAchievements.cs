﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wowthing.Lib.Migrations
{
    public partial class Add_PlayerCharacterAchievements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "player_character_achievements",
                columns: table => new
                {
                    character_id = table.Column<int>(type: "integer", nullable: false),
                    achievement_timestamps = table.Column<Dictionary<int, int>>(type: "jsonb", nullable: true),
                    criteria_amounts = table.Column<Dictionary<int, long>>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_player_character_achievements", x => x.character_id);
                    table.ForeignKey(
                        name: "fk_player_character_achievements_player_character_character_id",
                        column: x => x.character_id,
                        principalTable: "player_character",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "player_character_achievements");
        }
    }
}