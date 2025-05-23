﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NEWZEAL_LAND_WORK_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Difficulty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "Regions",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        RegionImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Regions", x => x.Id);
            //    });

        //    migrationBuilder.CreateTable(
        //        name: "Walks",
        //        columns: table => new
        //        {
        //            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
        //            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            LengthInkm = table.Column<double>(type: "float", nullable: false),
        //            WalkImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            DifficultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
        //            RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Walks", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_Walks_Difficulty_DifficultyId",
        //                column: x => x.DifficultyId,
        //                principalTable: "Difficulty",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "FK_Walks_Regions_RegionId",
        //                column: x => x.RegionId,
        //                principalTable: "Regions",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Walks_DifficultyId",
        //        table: "Walks",
        //        column: "DifficultyId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Walks_RegionId",
        //        table: "Walks",
        //        column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "Walks");

            migrationBuilder.DropTable(
                name: "Difficulty");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
