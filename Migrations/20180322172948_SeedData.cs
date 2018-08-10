using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vega.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          migrationBuilder.Sql("Insert into Makes (Name) values ('Make1')");
          migrationBuilder.Sql("Insert into Makes (Name) values ('Make2')");
          migrationBuilder.Sql("Insert into Makes (Name) values ('Make3')");

          migrationBuilder.Sql("Insert into Models (Name, MakeId) values ('Make1-ModelA',('Select Id from Makes where Name = 'Make1'))");
          migrationBuilder.Sql("Insert into Models (Name, MakeId) values ('Make1-ModelB',('Select Id from Makes where Name = 'Make1'))");
          migrationBuilder.Sql("Insert into Models (Name, MakeId) values ('Make1-ModelC',('Select Id from Makes where Name = 'Make1'))");

          migrationBuilder.Sql("Insert into Models (Name, MakeId) values ('Make2-ModelA',('Select Id from Makes where Name = 'Make2'))");
          migrationBuilder.Sql("Insert into Models (Name, MakeId) values ('Make2-ModelB',('Select Id from Makes where Name = 'Make2'))");
          migrationBuilder.Sql("Insert into Models (Name, MakeId) values ('Make2-ModelC',('Select Id from Makes where Name = 'Make2'))");

          migrationBuilder.Sql("Insert into Models (Name, MakeId) values ('Make3-ModelA',('Select Id from Makes where Name = 'Make3'))");
          migrationBuilder.Sql("Insert into Models (Name, MakeId) values ('Make3-ModelB',('Select Id from Makes where Name = 'Make3'))");
          migrationBuilder.Sql("Insert into Models (Name, MakeId) values ('Make3-ModelC',('Select Id from Makes where Name = 'Make3'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          migrationBuilder.Sql("Delete from Makes where name in ('Make1','Make2','Make3')");
        }
    }
}
