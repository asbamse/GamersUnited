﻿using GamersUnited.Core.Entities;
using GamersUnited.Infrastructure.Data;
using GamersUnited.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestCore.Infrastructure.Data
{
    public class ProductCategoryRepositoryTest
    {
        private readonly DbContextOptions<GamersUnitedContext> _options = new DbContextOptionsBuilder<GamersUnitedContext>()
            .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
            .Options;

        [Fact]
        public void CreateValidProductCategoryRepositoryTest()
        {
            var pc1 = new ProductCategory() { Id = 1, Name = "Testing category" };
            var pc2 = new ProductCategory() { Id = 2, Name = "category" };
            var pc3 = new ProductCategory() { Id = 3, Name = "Testing" };

            // Run the test against one instance of the context
            using (var context = new GamersUnitedContext(_options))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductCategoryRepository(context);

                var npc1 = repo.Add(pc1);
                Assert.Equal(1, context.ProductCategory.Count());

                var npc2 = repo.Add(pc2);
                Assert.Equal(2, context.ProductCategory.Count());

                var npc3 = repo.Add(pc3);
                Assert.Equal(3, context.ProductCategory.Count());
                
                Assert.Equal(pc1.Name, npc1.Name);
                Assert.Equal(pc2.Name, npc2.Name);
                Assert.Equal(pc3.Name, npc3.Name);
            }
        }

        [Fact]
        public void CreateInvalidProductCategoryRepositoryTestAutoincrement()
        {
            var pc = new ProductCategory() { Id = 9999, Name = "" };

            // Run the test against one instance of the context
            using (var context = new GamersUnitedContext(_options))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductCategoryRepository(context);
                var npc = repo.Add(pc);
                
                Assert.NotEqual(pc.Id, npc.Id);
            }
        }

        [Fact]
        public void CreateInvalidProductCategoryRepositoryTestExpectArgumentNullException()
        {
            var pc = new ProductCategory() { Id = 1 };

            using (var context = new GamersUnitedContext(_options))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductCategoryRepository(context);
                Assert.Throws<ArgumentNullException>(() =>
                {
                    repo.Add(pc);
                });
            }
        }

        [Fact]
        public void CountOneProductCategoryRepositoryTest()
        {
            var pc = new ProductCategory() { Name="" };

            using (var context = new GamersUnitedContext(_options))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductCategoryRepository(context);
                repo.Add(pc);
                Assert.Equal(1, repo.Count());
            }
        }

        [Fact]
        public void CountNoProductCategoryRepositoryTest()
        {
            using (var context = new GamersUnitedContext(_options))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductCategoryRepository(context);
                Assert.Equal(0, repo.Count());
            }
        }

        [Fact]
        public void GetAllProductCategoryRepositoryTest()
        {
            var pcl = new List<ProductCategory>{
                new ProductCategory() { Name = "1" },
                new ProductCategory() { Name = "2" },
                new ProductCategory() { Name = "3" },
                new ProductCategory() { Name = "4" },
                new ProductCategory() { Name = "5" }
            };

            using (var context = new GamersUnitedContext(_options))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductCategoryRepository(context);
                for (int i = 0; i < pcl.Count; i++)
                {
                    repo.Add(pcl[i]);
                }

                var npcl = repo.GetAll();
                for (int i = 0; i < pcl.Count; i++)
                {
                    Assert.Equal(pcl[i].Name, npcl[i].Name);
                }

                Assert.Equal(5, context.ProductCategory.Count());
            }
        }

        [Fact]
        public void GetAllEmptyProductCategoryRepositoryTest()
        {
            using (var context = new GamersUnitedContext(_options))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductCategoryRepository(context);
                Assert.Equal(0, context.ProductCategory.Count());
            }
        }
    }
}
