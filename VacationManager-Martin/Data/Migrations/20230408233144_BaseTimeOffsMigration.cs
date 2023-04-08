using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationManager_Martin.Data.Migrations
{
    /// <inheritdoc />
    public partial class BaseTimeOffsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnpaidTimeOffs_AspNetUsers_RequestorId",
                table: "UnpaidTimeOffs");

            migrationBuilder.DropTable(
                name: "PaidTimeOffs");

            migrationBuilder.DropTable(
                name: "SickTimeOffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnpaidTimeOffs",
                table: "UnpaidTimeOffs");

            migrationBuilder.RenameTable(
                name: "UnpaidTimeOffs",
                newName: "BaseTimeOff");

            migrationBuilder.RenameIndex(
                name: "IX_UnpaidTimeOffs_RequestorId",
                table: "BaseTimeOff",
                newName: "IX_BaseTimeOff_RequestorId");

            migrationBuilder.AddColumn<string>(
                name: "AttachmentPath",
                table: "BaseTimeOff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseTimeOff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SickTimeOff_UserId",
                table: "BaseTimeOff",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnpaidTimeOff_UserId",
                table: "BaseTimeOff",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BaseTimeOff",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseTimeOff",
                table: "BaseTimeOff",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BaseTimeOff_SickTimeOff_UserId",
                table: "BaseTimeOff",
                column: "SickTimeOff_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseTimeOff_UnpaidTimeOff_UserId",
                table: "BaseTimeOff",
                column: "UnpaidTimeOff_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseTimeOff_UserId",
                table: "BaseTimeOff",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseTimeOff_AspNetUsers_RequestorId",
                table: "BaseTimeOff",
                column: "RequestorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseTimeOff_AspNetUsers_SickTimeOff_UserId",
                table: "BaseTimeOff",
                column: "SickTimeOff_UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseTimeOff_AspNetUsers_UnpaidTimeOff_UserId",
                table: "BaseTimeOff",
                column: "UnpaidTimeOff_UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseTimeOff_AspNetUsers_UserId",
                table: "BaseTimeOff",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseTimeOff_AspNetUsers_RequestorId",
                table: "BaseTimeOff");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseTimeOff_AspNetUsers_SickTimeOff_UserId",
                table: "BaseTimeOff");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseTimeOff_AspNetUsers_UnpaidTimeOff_UserId",
                table: "BaseTimeOff");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseTimeOff_AspNetUsers_UserId",
                table: "BaseTimeOff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseTimeOff",
                table: "BaseTimeOff");

            migrationBuilder.DropIndex(
                name: "IX_BaseTimeOff_SickTimeOff_UserId",
                table: "BaseTimeOff");

            migrationBuilder.DropIndex(
                name: "IX_BaseTimeOff_UnpaidTimeOff_UserId",
                table: "BaseTimeOff");

            migrationBuilder.DropIndex(
                name: "IX_BaseTimeOff_UserId",
                table: "BaseTimeOff");

            migrationBuilder.DropColumn(
                name: "AttachmentPath",
                table: "BaseTimeOff");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseTimeOff");

            migrationBuilder.DropColumn(
                name: "SickTimeOff_UserId",
                table: "BaseTimeOff");

            migrationBuilder.DropColumn(
                name: "UnpaidTimeOff_UserId",
                table: "BaseTimeOff");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BaseTimeOff");

            migrationBuilder.RenameTable(
                name: "BaseTimeOff",
                newName: "UnpaidTimeOffs");

            migrationBuilder.RenameIndex(
                name: "IX_BaseTimeOff_RequestorId",
                table: "UnpaidTimeOffs",
                newName: "IX_UnpaidTimeOffs_RequestorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnpaidTimeOffs",
                table: "UnpaidTimeOffs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PaidTimeOffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsHalfDay = table.Column<bool>(type: "bit", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaidTimeOffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaidTimeOffs_AspNetUsers_RequestorId",
                        column: x => x.RequestorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SickTimeOffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AttachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SickTimeOffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SickTimeOffs_AspNetUsers_RequestorId",
                        column: x => x.RequestorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaidTimeOffs_RequestorId",
                table: "PaidTimeOffs",
                column: "RequestorId");

            migrationBuilder.CreateIndex(
                name: "IX_SickTimeOffs_RequestorId",
                table: "SickTimeOffs",
                column: "RequestorId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnpaidTimeOffs_AspNetUsers_RequestorId",
                table: "UnpaidTimeOffs",
                column: "RequestorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
