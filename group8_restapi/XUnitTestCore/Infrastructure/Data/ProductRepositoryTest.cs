﻿using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;
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
    public class ProductRepositoryTest
    {
        // Mocking out the repository
        private Mock<IRepository<ProductCategory>> mockProductCategoryRepo = new Mock<IRepository<ProductCategory>>();

        // Data structure for the mock object
        // All the methods of the Mock object operates on this instance.
        private Dictionary<int, ProductCategory> productCategories = new Dictionary<int, ProductCategory>();
        private int nextId = 1;

        public ProductRepositoryTest()
        {
            mockProductCategoryRepo.Setup(x => x.Count()).Returns(() => productCategories.Count);
            mockProductCategoryRepo.Setup(x => x.Add(It.IsAny<ProductCategory>())).Returns<ProductCategory>((pc) => { pc.Id = nextId++; productCategories.Add(pc.Id, pc); return productCategories[pc.Id]; });
            mockProductCategoryRepo.Setup(x => x.Remove(It.IsAny<ProductCategory>())).Returns<ProductCategory>((pc) => { ProductCategory tmp = productCategories[pc.Id]; productCategories.Remove(pc.Id); return tmp; });
            mockProductCategoryRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns<int>((pcNumb) => 
            {
                if(productCategories.ContainsKey(pcNumb))
                {
                    return productCategories[pcNumb];
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Element not found!");
                }
            });
            mockProductCategoryRepo.Setup(x => x.GetAll()).Returns(() => new List<ProductCategory>(productCategories.Values));
        }

        #region Add
        [Fact]
        public void CreateValidProductRepositoryTest()
        {
            var p1 = new Product() {
                Id = 1,
                Name = "Testing product",
                Category = new ProductCategory()
                {
                    Id = 1,
                    Name = "Testing category"
                },
                Price = 200.0,
                ImageUrl = "Test URL",
                Description = "This is a description"
            };
            var p2 = new Product()
            {
                Id = 2,
                Name = "Testing",
                Category = new ProductCategory()
                {
                    Name = "Testing category"
                },
                Price = 400.0,
                ImageUrl = "Est URL",
                Description = "This a description"
            };
            var p3 = new Product()
            {
                Id = 3,
                Name = "product",
                Category = new ProductCategory()
                {
                    Id = 1
                },
                Price = 200.1,
                ImageUrl = "Test UR",
                Description = "This is description"
            };

            // Run the test against one instance of the context
            using (var context = new GamersUnitedContext(GetOption(System.Reflection.MethodBase.GetCurrentMethod().Name)))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductRepository(context, mockProductCategoryRepo.Object);

                var np1 = repo.Add(p1);
                Assert.Equal(1, context.Product.Count());

                var np2 = repo.Add(p2);
                Assert.Equal(2, context.Product.Count());

                var np3 = repo.Add(p3);
                Assert.Equal(3, context.Product.Count());

                Assert.Equal(p1.Name, np1.Name);
                Assert.Equal(p1.Category, np1.Category);
                Assert.Equal(p1.Price, np1.Price);
                Assert.Equal(p1.ImageUrl, np1.ImageUrl);
                Assert.Equal(p1.Description, np1.Description);

                Assert.Equal(p2.Name, np2.Name);
                Assert.Equal(p2.Category.Name, np2.Category.Name);
                Assert.Equal(p2.Price, np2.Price);
                Assert.Equal(p2.ImageUrl, np2.ImageUrl);
                Assert.Equal(p2.Description, np2.Description);

                Assert.Equal(p3.Name, np3.Name);
                Assert.Equal(p3.Category.Id, np3.Category.Id);
                Assert.Equal(p3.Price, np3.Price);
                Assert.Equal(p3.ImageUrl, np3.ImageUrl);
                Assert.Equal(p3.Description, np3.Description);
            }
        }

        [Fact]
        public void CreateValidProductRepositoryTestAutoincrement()
        {
            var p1 = new Product()
            {
                Id = -10,
                Name = "Testing product",
                Category = new ProductCategory()
                { 
                    Id = 1,
                    Name = "Testing category"
                },
                Price = 200.0,
                ImageUrl = "Test URL",
                Description = "This is a description"
            };
            var p2 = new Product()
            {
                Id = -23,
                Name = "Testing",
                Category = new ProductCategory()
                {
                    Name = "Testing category"
                },
                Price = 400.0,
                ImageUrl = "Est URL",
                Description = "This a description"
            };

            // Run the test against one instance of the context
            using (var context = new GamersUnitedContext(GetOption(System.Reflection.MethodBase.GetCurrentMethod().Name)))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductRepository(context, mockProductCategoryRepo.Object);

                var np1 = repo.Add(p1);
                Assert.Equal(1, context.Product.Count());
                Assert.NotEqual(p1.Id, np1.Id);

                var np2 = repo.Add(p2);
                Assert.Equal(2, context.Product.Count());
                Assert.NotEqual(p2.Id, np2.Id);
            }
        }

        [Fact]
        public void CreateInvalidProductRepositoryTestExpectArgumentNullException()
        {
            var p1 = new Product()
            {
                Id = 1,
                Category = new ProductCategory()
                {
                    Id = 1,
                    Name = "Testing category"
                },
                Price = 200.0,
                ImageUrl = "Test URL",
                Description = "This is a description"
            };
            var p2 = new Product()
            {
                Id = 2,
                Name = "Testing",
                Price = 400.0,
                ImageUrl = "Est URL",
                Description = "This a description"
            };
            var p3 = new Product()
            {
                Id = 3,
                Name = "product",
                Category = new ProductCategory()
                {
                    Id = 1
                },
                Price = 200.1,
                Description = "This is description"
            };
            var p4 = new Product()
            {
                Id = 4,
                Name = "prodeuct",
                Category = new ProductCategory()
                {
                    Id = 2
                },
                Price = 200.1,
                ImageUrl = "Test UR"
            };

            // Run the test against one instance of the context
            using (var context = new GamersUnitedContext(GetOption(System.Reflection.MethodBase.GetCurrentMethod().Name)))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductRepository(context, mockProductCategoryRepo.Object);

                Assert.Throws<ArgumentNullException>(() =>
                {
                    repo.Add(p1);
                });
                Assert.Throws<ArgumentNullException>(() =>
                {
                    repo.Add(p2);
                });
                Assert.Throws<ArgumentNullException>(() =>
                {
                    repo.Add(p3);
                });
                Assert.Throws<ArgumentNullException>(() =>
                {
                    repo.Add(p4);
                });
            }
        }
        #endregion
        
        #region Count
        [Fact]
        public void CountOneProductCategoryRepositoryTest()
        {
            var p = new Product()
            {
                Id = 1,
                Name = "Testing product",
                Category = new ProductCategory()
                {
                    Id = 1,
                    Name = "Testing category"
                },
                Price = 200.0,
                ImageUrl = "Test URL",
                Description = "This is a description"
            };

            using (var context = new GamersUnitedContext(GetOption(System.Reflection.MethodBase.GetCurrentMethod().Name)))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductRepository(context, mockProductCategoryRepo.Object);
                repo.Add(p);
                Assert.Equal(1, repo.Count());
            }
        }

        [Fact]
        public void CountNoProductCategoryRepositoryTest()
        {
            using (var context = new GamersUnitedContext(GetOption(System.Reflection.MethodBase.GetCurrentMethod().Name)))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductRepository(context, mockProductCategoryRepo.Object);
                Assert.Equal(0, repo.Count());
            }
        }
        #endregion

        private DbContextOptions<GamersUnitedContext> GetOption(string databasename)
        {
            return new DbContextOptionsBuilder<GamersUnitedContext>()
                .UseInMemoryDatabase(databaseName: databasename)
                .Options;
        }
    }
}
