﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wowthing.Lib.Migrations
{
    public partial class Add_PlayerCharacterShadowlands : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "player_character_shadowlands",
                columns: table => new
                {
                    character_id = table.Column<long>(nullable: false),
                    covenant_id = table.Column<int>(nullable: false),
                    renown_level = table.Column<int>(nullable: false),
                    soulbind_id = table.Column<int>(nullable: false),
                    conduit_ids = table.Column<List<int>>(nullable: true),
                    conduit_ranks = table.Column<List<int>>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_player_character_shadowlands", x => x.character_id);
                    table.ForeignKey(
                        name: "fk_player_character_shadowlands_player_character_character_id",
                        column: x => x.character_id,
                        principalTable: "player_character",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "player_character_shadowlands");
        }
    }
}