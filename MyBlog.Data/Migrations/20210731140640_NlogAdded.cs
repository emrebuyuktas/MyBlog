using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlog.Data.Migrations
{
    public partial class NlogAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Logged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Logger = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Callsite = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    Exception = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(1381), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(273), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(2025) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3319), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3317), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3321) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3328), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3326), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3329) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3336), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3334), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3337) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3343), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3341), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3344) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3350), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3349), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3351) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3357), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3356), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3358) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3367), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3365), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3368) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3375), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3372), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3376) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3402), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3381), new DateTime(2021, 7, 31, 17, 6, 39, 79, DateTimeKind.Local).AddTicks(3404) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6164), new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6179) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6194), new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6196) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6201), new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6202) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6206), new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6208) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6403), new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6404) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6409), new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6415), new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6416) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6421), new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6422) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6426), new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6428) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6432), new DateTime(2021, 7, 31, 17, 6, 39, 85, DateTimeKind.Local).AddTicks(6434) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6001), new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6018) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6032), new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6033) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6038), new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6039) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6044), new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6046) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6050), new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6052) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6056), new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6057) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6062), new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6063) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6068), new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6069) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6074), new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6075) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6080), new DateTime(2021, 7, 31, 17, 6, 39, 89, DateTimeKind.Local).AddTicks(6081) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "bc92d1f0-c55d-47fa-9e66-2f07b3c15c0b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "175de537-1f82-4ce4-99ea-c2528a6dd93c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6ea6ccf0-91a9-45db-8b1a-326840a5b58a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "9407d198-21ad-4eb2-a32d-11d4a226402e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "09a27e2b-ca7b-4708-a56c-9b9c18c7504e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "7feeddab-8241-459c-b99a-c349af8b4fcc");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "e7632cde-c501-4258-9438-307bea565cc0");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConcurrencyStamp",
                value: "42cd5bb8-1787-4815-88b7-2e7cd2f3d73c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConcurrencyStamp",
                value: "d5a7d772-707d-4074-b2cc-16911db3548b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "fc454f76-77c0-4ac4-83a8-d74560ac34a7");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 11,
                column: "ConcurrencyStamp",
                value: "f688a118-9f37-4383-8241-a923e61b293a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 12,
                column: "ConcurrencyStamp",
                value: "d043792a-d17f-44d3-b110-14ece6644dbc");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 13,
                column: "ConcurrencyStamp",
                value: "587323f8-3d80-4061-84a2-424c40677516");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 14,
                column: "ConcurrencyStamp",
                value: "77dcc44a-5033-465f-a647-3fdedd2cb6d9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 15,
                column: "ConcurrencyStamp",
                value: "e6a46b61-62ee-47e0-8bf0-c509b3a46d48");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 16,
                column: "ConcurrencyStamp",
                value: "353df2ce-8c71-4705-b782-bde61020addf");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 17,
                column: "ConcurrencyStamp",
                value: "c7c61cd8-ec73-4064-99c9-7dd03e0ef0c0");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 18,
                column: "ConcurrencyStamp",
                value: "cef76e06-7a80-48e6-94af-9bac95f6d86f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 19,
                column: "ConcurrencyStamp",
                value: "3060d987-7bc8-4ffe-bb45-2adb993edfd2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 20,
                column: "ConcurrencyStamp",
                value: "59ad6b12-3c90-485b-9caf-e4cd41ec549f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 21,
                column: "ConcurrencyStamp",
                value: "bae0bfe2-90cb-4195-81a5-bad46b7e95c0");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 22,
                column: "ConcurrencyStamp",
                value: "c411792e-6107-4005-a035-500759876081");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad077693-2000-48b9-85ef-afd3f0080e72", "AQAAAAEAACcQAAAAEP8QzAk6JFVrNOpworZedlAK8UNNL6CzLpDx3cRBRu46rTDu40QUugcP9yhKGoe2xA==", "b9b4c373-f53d-4d29-a903-2e2dad42f935" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "574b343c-7e6b-4340-8858-de491141c980", "AQAAAAEAACcQAAAAEGJTe3VNfFcimRqmWqqU5XntiVGO/8HIDjmaRXayAhxNFMRmCFuXMG9D6vTUw5D0AA==", "37a0c544-cd83-44c3-ae1c-c6a792d2f3ff" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(2685), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(1665), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(3211) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4401), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4399), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4402) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4410), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4408), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4411) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4417), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4415), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4418) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4424), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4423), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4425) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4431), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4429), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4432) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4438), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4437), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4439) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4446), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4444), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4447) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4452), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4451), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4454) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreateDate", "Date", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4460), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4458), new DateTime(2021, 7, 17, 15, 40, 36, 615, DateTimeKind.Local).AddTicks(4461) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2319), new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2345) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2491), new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2492) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2497), new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2499) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2504), new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2505) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2510), new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2512) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2516), new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2518) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2522), new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2524) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2528), new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2530) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2534), new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2535) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2540), new DateTime(2021, 7, 17, 15, 40, 36, 619, DateTimeKind.Local).AddTicks(2541) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3851), new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3865) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3878), new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3879) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3884), new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3885) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3889), new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3891) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3895), new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3896) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3900), new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3901) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3905), new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3907) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3911), new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3912) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3916), new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3917) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreateDate", "ModiefiedDate" },
                values: new object[] { new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3922), new DateTime(2021, 7, 17, 15, 40, 36, 622, DateTimeKind.Local).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3519ccad-e3dd-4c87-9870-3add200cb3ef");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2f89eb6a-ee45-41ac-9ec9-2ea85b522c58");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "c618d7da-4d9c-4f93-adc5-d136de8fdf4e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "61696903-62d4-45cb-b3e7-f83089c1bfad");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "fc0795b0-0302-4a13-bec3-7f81732a6c28");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "8bae8aaa-e72d-40a3-ac9e-2a99b75f7964");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "ad6d4be5-4131-44d3-932e-4f333a6703b7");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConcurrencyStamp",
                value: "289d157f-5b10-4f90-a09d-27db7504970b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConcurrencyStamp",
                value: "bff36c4c-9e50-4637-a1a0-25431f461564");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "1469d4e0-9432-4590-87d4-8d43e61a03bb");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 11,
                column: "ConcurrencyStamp",
                value: "29fd69bf-3d11-4ffd-888d-4696ad3fc732");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 12,
                column: "ConcurrencyStamp",
                value: "dc7a7471-d857-485d-a29d-cd1261a5bee8");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 13,
                column: "ConcurrencyStamp",
                value: "06d1002e-33d3-4b2b-a56d-b9f189d94856");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 14,
                column: "ConcurrencyStamp",
                value: "27c2f329-ca77-4754-8bee-46391ffcd516");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 15,
                column: "ConcurrencyStamp",
                value: "7c8e793d-ae59-4ba9-9416-55da6d5a5c40");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 16,
                column: "ConcurrencyStamp",
                value: "27186c62-be2e-4374-aedc-a628aa442cb3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 17,
                column: "ConcurrencyStamp",
                value: "de26b23b-5dd6-4533-9272-b2a3b66bfb07");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 18,
                column: "ConcurrencyStamp",
                value: "ba5fd12f-4e46-4ed2-a844-461953d11418");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 19,
                column: "ConcurrencyStamp",
                value: "e30d737b-7a67-4b0d-b6a5-c6cd7ca06ea4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 20,
                column: "ConcurrencyStamp",
                value: "18e59d60-e83c-432d-b5e9-b832506c28ab");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 21,
                column: "ConcurrencyStamp",
                value: "4fe0c5e8-b3e5-4290-b180-46d4e4bc8256");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 22,
                column: "ConcurrencyStamp",
                value: "e6d14d20-ffba-4762-9bd0-2fb905bd115e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd5d547d-7c82-4c68-a2f6-7bf78ae97ed7", "AQAAAAEAACcQAAAAEIZBgMkXJRmxceeQ0e0klTYl4pV9GHOeZlf2bEIU96aYwrjPY6rtJyndqaGLtWuYQw==", "cf5113eb-9258-4b39-a348-f3de802eb6be" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbebbdfb-339b-4bb4-a021-1e628e620cf2", "AQAAAAEAACcQAAAAEB1xdmYLvYhhwlmo7TlXVB2TyH09NultC9Few6rsJCGfasapNhMHIJBKcJd2k5BMkg==", "185b18f8-c250-4e6d-94d7-ae65447a3ec2" });
        }
    }
}
