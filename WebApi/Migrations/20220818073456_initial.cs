using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(maxLength: 200, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    KodeBuku = table.Column<string>(maxLength: 5, nullable: false),
                    NamaBuku = table.Column<string>(maxLength: 50, nullable: false),
                    NamaPengarang = table.Column<string>(maxLength: 50, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    Initial = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileCollections",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    FileName = table.Column<string>(maxLength: 200, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileCollections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupJobs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Job = table.Column<string>(maxLength: 100, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderHeaders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    Reference = table.Column<string>(maxLength: 15, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    CategoryId = table.Column<long>(nullable: false),
                    Initial = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variants_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransRoles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupJobId = table.Column<long>(nullable: false),
                    Role = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransRoles_GroupJobs_GroupJobId",
                        column: x => x.GroupJobId,
                        principalTable: "GroupJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    GroupJobId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_GroupJobs_GroupJobId",
                        column: x => x.GroupJobId,
                        principalTable: "GroupJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Accounts_UserName",
                        column: x => x.UserName,
                        principalTable: "Accounts",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    VariantId = table.Column<long>(nullable: false),
                    Initial = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    HeaderId = table.Column<long>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderHeaders_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "OrderHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "UserName", "Active", "FirstName", "Id", "LastName", "Password" },
                values: new object[,]
                {
                    { "admin", true, "Super", 1L, "User", "admin1234" },
                    { "user1", true, "User", 2L, "Satu", "user1234" },
                    { "user2", true, "User", 3L, "Dua", "user1234" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Active", "CreateBy", "CreateDate", "Initial", "ModifyBy", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1L, true, "Admin", new DateTime(2022, 8, 18, 14, 34, 56, 490, DateTimeKind.Local).AddTicks(5785), "MainCo", null, null, "Main Course" },
                    { 2L, true, "Admin", new DateTime(2022, 8, 18, 14, 34, 56, 491, DateTimeKind.Local).AddTicks(2880), "Drink", null, null, "Drink" },
                    { 3L, true, "Admin", new DateTime(2022, 8, 18, 14, 34, 56, 491, DateTimeKind.Local).AddTicks(2940), "Dessert", null, null, "Dessert" }
                });

            migrationBuilder.InsertData(
                table: "GroupJobs",
                columns: new[] { "Id", "Active", "Job" },
                values: new object[,]
                {
                    { 1L, true, "Administrator" },
                    { 2L, true, "Supervisor" },
                    { 3L, true, "Staff" },
                    { 4L, true, "Cashier" }
                });

            migrationBuilder.InsertData(
                table: "TransRoles",
                columns: new[] { "Id", "GroupJobId", "Role" },
                values: new object[,]
                {
                    { 1L, 2L, "Category" },
                    { 2L, 2L, "Variant" },
                    { 3L, 2L, "Product" },
                    { 4L, 2L, "Order" },
                    { 5L, 3L, "Category" },
                    { 6L, 3L, "Variant" },
                    { 7L, 3L, "Product" },
                    { 8L, 4L, "Order" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "GroupJobId", "UserName" },
                values: new object[,]
                {
                    { 1L, 1L, "admin" },
                    { 2L, 3L, "user1" },
                    { 3L, 4L, "user2" }
                });

            migrationBuilder.InsertData(
                table: "Variants",
                columns: new[] { "Id", "Active", "CategoryId", "CreateBy", "CreateDate", "Initial", "ModifyBy", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1L, true, 1L, "Admin", new DateTime(2022, 8, 18, 14, 34, 56, 492, DateTimeKind.Local).AddTicks(3383), "Paket Nasi", null, null, "Paket Nasi" },
                    { 2L, true, 1L, "Admin", new DateTime(2022, 8, 18, 14, 34, 56, 492, DateTimeKind.Local).AddTicks(3453), "Ala Carte", null, null, "Ala Carte" },
                    { 3L, true, 1L, "Admin", new DateTime(2022, 8, 18, 14, 34, 56, 492, DateTimeKind.Local).AddTicks(3456), "Favorite", null, null, "Favorite" },
                    { 4L, true, 2L, "Admin", new DateTime(2022, 8, 18, 14, 34, 56, 492, DateTimeKind.Local).AddTicks(3458), "Iced", null, null, "Iced" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Active", "CreateBy", "CreateDate", "Description", "Initial", "ModifyBy", "ModifyDate", "Name", "Price", "Stock", "VariantId" },
                values: new object[,]
                {
                    { 1L, true, "Admin", new DateTime(2022, 8, 18, 14, 34, 56, 492, DateTimeKind.Local).AddTicks(5350), "Capcay seafood", "NasCap", null, null, "Nasi Capcay", 25000m, 10m, 1L },
                    { 4L, true, "Admin", new DateTime(2022, 8, 18, 14, 34, 56, 492, DateTimeKind.Local).AddTicks(5441), "1/2 ekor", "AyGor", null, null, "Ayam Goreng", 18000m, 10m, 2L },
                    { 2L, true, "Admin", new DateTime(2022, 8, 18, 14, 34, 56, 492, DateTimeKind.Local).AddTicks(5436), "Dengan bumbu rempah", "AyKal", null, null, "Ayam Kalasan", 26000m, 10m, 3L },
                    { 3L, true, "Admin", new DateTime(2022, 8, 18, 14, 34, 56, 492, DateTimeKind.Local).AddTicks(5439), "Dengan gula aren", "KaMer", null, null, "Iced Kacang Merah", 18000m, 10m, 4L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserName",
                table: "Accounts",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_KodeBuku",
                table: "Books",
                column: "KodeBuku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Initial",
                table: "Categories",
                column: "Initial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileCollections_FileName",
                table: "FileCollections",
                column: "FileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileCollections_Title",
                table: "FileCollections",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupJobs_Job",
                table: "GroupJobs",
                column: "Job",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_HeaderId",
                table: "OrderDetails",
                column: "HeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Initial",
                table: "Products",
                column: "Initial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_VariantId",
                table: "Products",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_TransRoles_GroupJobId_Role",
                table: "TransRoles",
                columns: new[] { "GroupJobId", "Role" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_GroupJobId",
                table: "UserRoles",
                column: "GroupJobId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserName_GroupJobId",
                table: "UserRoles",
                columns: new[] { "UserName", "GroupJobId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Variants_CategoryId",
                table: "Variants",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_Initial",
                table: "Variants",
                column: "Initial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Variants_Name",
                table: "Variants",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "FileCollections");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "TransRoles");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "OrderHeaders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "GroupJobs");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
