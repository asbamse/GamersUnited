using GamersUnited.Core.Entities;
using GamersUnited.Infrastructure.Data;
using GamersUnited.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace XUnitTestCore.Infrastructure.Data
{
    public class ProductCategoryTest
    {
        [Theory]
        [InlineData(1, "Testing category")]
        [InlineData(2, "category")]
        [InlineData(3, "Testing")]
        public void CreateValidProductCategoryTest(int id, string name)
        {
            var options = new DbContextOptionsBuilder<GamersUnitedContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            var pc = new ProductCategory() { Id = id, Name = name };

            // Run the test against one instance of the context
            using (var context = new GamersUnitedContext(options))
            {
                var repo = new ProductCategoryRepository(context);
                repo.Add(pc);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new GamersUnitedContext(options))
            {
                Assert.Equal(1, context.ProductCategory.Count());
                Assert.Equal(id, context.ProductCategory.Single().Id);
                Assert.Equal(name, context.ProductCategory.Single().Name);
                context.Database.EnsureDeleted();
            }
        }
    }
}
