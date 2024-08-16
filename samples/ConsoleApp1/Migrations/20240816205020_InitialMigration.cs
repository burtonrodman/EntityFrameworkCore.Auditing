using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp1.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    BlogPostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "BlogPostsHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "BlogPostsHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime"),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "BlogPostsHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime"),
                    SysEndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "BlogPostsHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime"),
                    SysStartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "BlogPostsHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "BlogPostsHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.BlogPostId);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "BlogPostsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime");

            migrationBuilder.CreateTable(
                name: "MyWeirdlyNamedUsersTable",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "MyWeirdlyNamedUsersTableHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "MyWeirdlyNamedUsersTableHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime"),
                    SysEndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "MyWeirdlyNamedUsersTableHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime"),
                    SysStartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "MyWeirdlyNamedUsersTableHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "MyWeirdlyNamedUsersTableHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyWeirdlyNamedUsersTable", x => x.UserId);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MyWeirdlyNamedUsersTableHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPosts")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "BlogPostsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime");

            migrationBuilder.DropTable(
                name: "MyWeirdlyNamedUsersTable")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MyWeirdlyNamedUsersTableHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime");
        }
    }
}
