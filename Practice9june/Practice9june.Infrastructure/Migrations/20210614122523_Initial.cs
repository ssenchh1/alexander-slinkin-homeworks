using Microsoft.EntityFrameworkCore.Migrations;

namespace Practice9june.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Directories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentDirectoryId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Directories_Directories_ParentDirectoryId",
                        column: x => x.ParentDirectoryId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DirectoryId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Extention = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bitrate = table.Column<int>(type: "int", nullable: true),
                    SampleRate = table.Column<int>(type: "int", nullable: true),
                    ChannelCount = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoFile_Height = table.Column<int>(type: "int", nullable: true),
                    VideoFile_Width = table.Column<int>(type: "int", nullable: true),
                    VideoFile_Duration = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Directories_DirectoryId",
                        column: x => x.DirectoryId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectoryPermissions",
                columns: table => new
                {
                    DirectoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CanRead = table.Column<bool>(type: "bit", nullable: false),
                    CanWrite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectoryPermissions", x => new { x.UserId, x.DirectoryId });
                    table.ForeignKey(
                        name: "FK_DirectoryPermissions_Directories_DirectoryId",
                        column: x => x.DirectoryId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectoryPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FilePermissions",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CanRead = table.Column<bool>(type: "bit", nullable: false),
                    CanWrite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilePermissions", x => new { x.UserId, x.FileId });
                    table.ForeignKey(
                        name: "FK_FilePermissions_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FilePermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Directories",
                columns: new[] { "Id", "ParentDirectoryId", "Title" },
                values: new object[] { 1, null, "FirstDirectory" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "UserName" },
                values: new object[,]
                {
                    { 1, "email@gmail.com", "sdf3gh4j56432bnm,c.hgf", "ForstUser" },
                    { 2, "email@gmail.com", "fm,/", "SecondUser" },
                    { 3, "email@gmail.com", "fjzkvjdjkrbf", "ThirdUser" }
                });

            migrationBuilder.InsertData(
                table: "Directories",
                columns: new[] { "Id", "ParentDirectoryId", "Title" },
                values: new object[,]
                {
                    { 2, 1, "SecondDir" },
                    { 3, 1, "ThirdDir" }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Bitrate", "ChannelCount", "DirectoryId", "Discriminator", "Duration", "Extention", "SampleRate", "Size", "Title" },
                values: new object[] { 9, 12, 3, 1, "AudioFile", 3, "mp3", 5, 5, "Lection3" });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "DirectoryId", "Discriminator", "Extention", "Height", "Size", "Title", "Width" },
                values: new object[] { 10, 1, "ImageFile", "jpg", 400, 5, "Image1", 400 });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "DirectoryId", "Discriminator", "VideoFile_Duration", "Extention", "VideoFile_Height", "Size", "Title", "VideoFile_Width" },
                values: new object[] { 4, 1, "VideoFile", 20, "mp4", 720, 0, "Me at the zoo", 1080 });

            migrationBuilder.InsertData(
                table: "DirectoryPermissions",
                columns: new[] { "DirectoryId", "UserId", "CanRead", "CanWrite" },
                values: new object[,]
                {
                    { 2, 1, true, false },
                    { 3, 1, false, false },
                    { 3, 2, true, true }
                });

            migrationBuilder.InsertData(
                table: "FilePermissions",
                columns: new[] { "FileId", "UserId", "CanRead", "CanWrite" },
                values: new object[,]
                {
                    { 10, 3, true, true },
                    { 4, 3, true, false }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Bitrate", "ChannelCount", "DirectoryId", "Discriminator", "Duration", "Extention", "SampleRate", "Size", "Title" },
                values: new object[] { 7, 12, 3, 2, "AudioFile", 3, "mp3", 5, 3, "Lection1" });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "DirectoryId", "Discriminator", "Extention", "Height", "Size", "Title", "Width" },
                values: new object[] { 11, 2, "ImageFile", "jpg", 400, 5, "Image2", 400 });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Content", "DirectoryId", "Discriminator", "Extention", "Size", "Title" },
                values: new object[] { 1, "using System...", 2, "TextFile", "txt", 5, "C# file" });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "DirectoryId", "Discriminator", "VideoFile_Duration", "Extention", "VideoFile_Height", "Size", "Title", "VideoFile_Width" },
                values: new object[] { 5, 2, "VideoFile", 50, "mp4", 720, 0, "C# Crash course", 1080 });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Bitrate", "ChannelCount", "DirectoryId", "Discriminator", "Duration", "Extention", "SampleRate", "Size", "Title" },
                values: new object[] { 8, 12, 3, 3, "AudioFile", 3, "mp3", 5, 4, "Lection2" });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "DirectoryId", "Discriminator", "Extention", "Height", "Size", "Title", "Width" },
                values: new object[] { 12, 3, "ImageFile", "jpg", 400, 5, "Image3", 400 });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Content", "DirectoryId", "Discriminator", "Extention", "Size", "Title" },
                values: new object[,]
                {
                    { 2, "Entity Framework", 3, "TextFile", "txt", 5, "EF tutorial" },
                    { 3, "Dapper is ORM", 3, "TextFile", "txt", 5, "Dapper tutorial" }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "DirectoryId", "Discriminator", "VideoFile_Duration", "Extention", "VideoFile_Height", "Size", "Title", "VideoFile_Width" },
                values: new object[] { 6, 3, "VideoFile", 5, "mp4", 720, 0, "Despacito", 1080 });

            migrationBuilder.InsertData(
                table: "FilePermissions",
                columns: new[] { "FileId", "UserId", "CanRead", "CanWrite" },
                values: new object[,]
                {
                    { 7, 1, true, true },
                    { 7, 2, false, false },
                    { 7, 3, true, false },
                    { 11, 2, true, false },
                    { 1, 1, true, false },
                    { 5, 2, false, true },
                    { 12, 1, false, true },
                    { 2, 1, false, false },
                    { 3, 2, true, true },
                    { 6, 3, true, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Directories_ParentDirectoryId",
                table: "Directories",
                column: "ParentDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectoryPermissions_DirectoryId",
                table: "DirectoryPermissions",
                column: "DirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FilePermissions_FileId",
                table: "FilePermissions",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_DirectoryId",
                table: "Files",
                column: "DirectoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DirectoryPermissions");

            migrationBuilder.DropTable(
                name: "FilePermissions");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Directories");
        }
    }
}
