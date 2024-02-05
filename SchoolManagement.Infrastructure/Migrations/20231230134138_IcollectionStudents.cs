using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IcollectionStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("01f7ccef-ebb3-4966-92f5-47011d4d6a1a"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("0ac83339-c5e6-4820-a644-3e54d1966405"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("10ee1c40-2da0-44d1-8a5a-6efe86044c8d"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("462a4ed1-3670-4f1d-b9b7-d95baded242e"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("7a9228f7-442f-483d-9b04-72b1164393b0"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("84517902-8ccb-45da-b2e0-65e6a7c758f5"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("ab25484b-9464-45d0-89e9-c574552364cf"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("bcf4e89c-9340-449d-a357-0bfafcb8bdd5"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("d9614cbe-034a-4c01-8b5f-78b95806a7fd"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("f1425d7f-5b0b-4232-96c0-853a836ff7f6"));

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "DeleteName", "IsActive", "Title" },
                values: new object[,]
                {
                    { new Guid("1b38f27c-4aee-453d-8907-1c372caadbe3"), null, true, "Arabic" },
                    { new Guid("1d50aae7-d0f1-44ac-be88-b6dc8befe228"), null, true, "English" },
                    { new Guid("26f92029-4774-484a-a6f8-bd9c4ee25bb2"), null, true, "Chemistry" },
                    { new Guid("4bccabea-578c-4a6a-b433-556481143418"), null, true, "Physics" },
                    { new Guid("4bf5b580-05ff-44d1-a3a3-7d476cc330c4"), null, true, "Sport" },
                    { new Guid("5b95900e-0ea7-4fde-a852-1daaa93f7894"), null, true, "History" },
                    { new Guid("767ebcb8-0275-47e9-bd97-55ec7c6e33ae"), null, true, "Science" },
                    { new Guid("8648082a-df52-4f4b-85bd-a74496dce80f"), null, true, "Mathematics" },
                    { new Guid("8e37aa04-e23b-4de2-8c99-c15c199302b4"), null, true, "Religion" },
                    { new Guid("a5d1d77d-354d-4faf-818a-2a19039099bc"), null, true, "Geography" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("1b38f27c-4aee-453d-8907-1c372caadbe3"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("1d50aae7-d0f1-44ac-be88-b6dc8befe228"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("26f92029-4774-484a-a6f8-bd9c4ee25bb2"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("4bccabea-578c-4a6a-b433-556481143418"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("4bf5b580-05ff-44d1-a3a3-7d476cc330c4"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("5b95900e-0ea7-4fde-a852-1daaa93f7894"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("767ebcb8-0275-47e9-bd97-55ec7c6e33ae"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("8648082a-df52-4f4b-85bd-a74496dce80f"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("8e37aa04-e23b-4de2-8c99-c15c199302b4"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("a5d1d77d-354d-4faf-818a-2a19039099bc"));

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "DeleteName", "IsActive", "Title" },
                values: new object[,]
                {
                    { new Guid("01f7ccef-ebb3-4966-92f5-47011d4d6a1a"), null, true, "Arabic" },
                    { new Guid("0ac83339-c5e6-4820-a644-3e54d1966405"), null, true, "Mathematics" },
                    { new Guid("10ee1c40-2da0-44d1-8a5a-6efe86044c8d"), null, true, "Physics" },
                    { new Guid("462a4ed1-3670-4f1d-b9b7-d95baded242e"), null, true, "Geography" },
                    { new Guid("7a9228f7-442f-483d-9b04-72b1164393b0"), null, true, "Chemistry" },
                    { new Guid("84517902-8ccb-45da-b2e0-65e6a7c758f5"), null, true, "English" },
                    { new Guid("ab25484b-9464-45d0-89e9-c574552364cf"), null, true, "Sport" },
                    { new Guid("bcf4e89c-9340-449d-a357-0bfafcb8bdd5"), null, true, "History" },
                    { new Guid("d9614cbe-034a-4c01-8b5f-78b95806a7fd"), null, true, "Science" },
                    { new Guid("f1425d7f-5b0b-4232-96c0-853a836ff7f6"), null, true, "Religion" }
                });
        }
    }
}
