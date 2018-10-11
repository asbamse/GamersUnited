using GamersUnited.Core.DomainService;
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
            mockProductCategoryRepo.Setup(x => x.Add(It.IsAny<ProductCategory>())).Returns<ProductCategory>((pc) => { pc.ProductCategoryId = nextId++; productCategories.Add(pc.ProductCategoryId, pc); return productCategories[pc.ProductCategoryId]; });
            mockProductCategoryRepo.Setup(x => x.Remove(It.IsAny<ProductCategory>())).Returns<ProductCategory>((pc) => { ProductCategory tmp = productCategories[pc.ProductCategoryId]; productCategories.Remove(pc.ProductCategoryId); return tmp; });
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
            mockProductCategoryRepo.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<ProductCategory>())).Returns<int, ProductCategory>((id, pc) =>
            {
                if (productCategories.ContainsKey(id))
                {
                    productCategories[id] = pc;
                    return productCategories[id];
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Element not found!");
                }
            });
        }

        #region Add
        [Fact]
        public void CreateValidProductRepositoryTest()
        {
            var p1 = new Product() {
                ProductId = 1,
                Name = "Testing product",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1,
                    Name = "Testing category"
                },
                Price = 200.0,
                ImageUrl = "Test URL",
                Description = "This is a description"
            };
            var p2 = new Product()
            {
                ProductId = 2,
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
                ProductId = 3,
                Name = "product",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1
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
                Assert.Equal(p3.Category.ProductCategoryId, np3.Category.ProductCategoryId);
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
                ProductId = -10,
                Name = "Testing product",
                Category = new ProductCategory()
                { 
                    ProductCategoryId = 1,
                    Name = "Testing category"
                },
                Price = 200.0,
                ImageUrl = "Test URL",
                Description = "This is a description"
            };
            var p2 = new Product()
            {
                ProductId = -23,
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
                Assert.NotEqual(p1.ProductId, np1.ProductId);

                var np2 = repo.Add(p2);
                Assert.Equal(2, context.Product.Count());
                Assert.NotEqual(p2.ProductId, np2.ProductId);
            }
        }

        [Fact]
        public void CreateInvalidProductRepositoryTestExpectArgumentNullException()
        {
            var p1 = new Product()
            {
                ProductId = 1,
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1,
                    Name = "Testing category"
                },
                Price = 200.0,
                ImageUrl = "Test URL",
                Description = "This is a description"
            };
            var p2 = new Product()
            {
                ProductId = 2,
                Name = "Testing",
                Price = 400.0,
                ImageUrl = "Est URL",
                Description = "This a description"
            };
            var p3 = new Product()
            {
                ProductId = 3,
                Name = "product",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1
                },
                Price = 200.1,
                Description = "This is description"
            };
            var p4 = new Product()
            {
                ProductId = 4,
                Name = "prodeuct",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 2
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
        public void CountOneProductRepositoryTest()
        {
            var p = new Product()
            {
                ProductId = 1,
                Name = "Testing product",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1,
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
        public void CountNoProductRepositoryTest()
        {
            using (var context = new GamersUnitedContext(GetOption(System.Reflection.MethodBase.GetCurrentMethod().Name)))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductRepository(context, mockProductCategoryRepo.Object);
                Assert.Equal(0, repo.Count());
            }
        }
        #endregion

        #region GetAll
        [Fact]
        public void GetAllProductRepositoryTest()
        {
            var pl = new List<Product>{
                new Product()
            {
                ProductId = 1,
                Name = "1",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1,
                    Name = "Testing category"
                },
                Price = 200.0,
                ImageUrl = "Test URL",
                Description = "This is a description"
            }, new Product()
            {
                ProductId = 2,
                Name = "2",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1,
                    Name = "Testing category"
                },
                Price = 400.0,
                ImageUrl = "Est URL",
                Description = "This a description"
            }, new Product()
            {
                ProductId = 3,
                Name = "3",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1
                },
                Price = 200.1,
                ImageUrl = "Est URL",
                Description = "This is description"
            }, new Product()
            {
                ProductId = 4,
                Name = "4",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 2
                },
                Price = 200.1,
                ImageUrl = "Test UR",
                Description = "This is description"
            }
        };

            using (var context = new GamersUnitedContext(GetOption(System.Reflection.MethodBase.GetCurrentMethod().Name)))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductRepository(context, mockProductCategoryRepo.Object);
                for (int i = 0; i < pl.Count; i++)
                {
                    repo.Add(pl[i]);
                }

                var npl = repo.GetAll();
                for (int i = 0; i < pl.Count; i++)
                {
                    Assert.Equal(pl[i].Name, npl[i].Name);
                }

                Assert.Equal(4, context.Product.Count());
            }
        }

        [Fact]
        public void GetAllEmptyProductRepositoryTest()
        {
            using (var context = new GamersUnitedContext(GetOption(System.Reflection.MethodBase.GetCurrentMethod().Name)))
            {
                context.Database.EnsureDeleted();

                var repo = new ProductRepository(context, mockProductCategoryRepo.Object);
                Assert.Equal(0, context.Product.Count());
            }
        }
        #endregion

        #region GetById
        [Fact]
        public void GetValidIdProductRepositoryTest()
        {
            var p = new Product()
            {
                ProductId = 1,
                Name = "1",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1,
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
                var np = repo.Add(p);

                var get = repo.GetById(np.ProductId);

                Assert.Equal(np.ProductId, get.ProductId);
                Assert.Equal(p.Name, get.Name);
            }
        }

        [Fact]
        public void GetInvalidIdProductRepositoryTest()
        {
            var p = new Product()
            {
                ProductId = 1,
                Name = "1",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1,
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
                var np = repo.Add(p);

                Assert.Throws<ArgumentOutOfRangeException>(() => {
                    var get = repo.GetById(context.Product.Count());
                });
            }
        }
        #endregion

        #region Remove
        [Fact]
        public void RemoveValidIdProductRepositoryTest()
        {
            var p = new Product()
            {
                ProductId = 1,
                Name = "1",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1,
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
                var np = repo.Add(p);
                
                var get = repo.Remove(np);

                Assert.Equal(np.ProductId, get.ProductId);
                Assert.Equal(p.Name, get.Name);
                Assert.Equal(0, context.Product.Count());
            }
        }

        [Fact]
        public void RemoveInvalidIdProductCategoryRepositoryTest()
        {
            var p = new Product()
            {
                ProductId = 1,
                Name = "1",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1,
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
                var np = repo.Add(p);
                p.ProductId = np.ProductId++;

                Assert.Throws<ArgumentOutOfRangeException>(() => {
                    var get = repo.Remove(p);
                });
            }
        }
        #endregion

        #region Update
        [Fact]
        public void UpdateValidProductCategoryRepositoryTest()
        {
            var p = new Product()
            {
                ProductId = 1,
                Name = "Testing product",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1,
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

                var ip = repo.Add(p);
                Assert.Equal(1, context.Product.Count());
                var tmp = new Product()
                {
                    ProductId = 1,
                    Name = "Testing monkey",
                    Category = new ProductCategory()
                    {
                        Name = "monkey category"
                    },
                    Price = 100.0,
                    ImageUrl = "Test monkey",
                    Description = "This is a monkey"
                };
                var np = repo.Update(ip.ProductId, tmp);

                Assert.Equal(ip.ProductId, np.ProductId);

                Assert.Equal(tmp.Name, np.Name);
                Assert.Equal(tmp.Category, np.Category);
                Assert.Equal(tmp.Price, np.Price);
                Assert.Equal(tmp.ImageUrl, np.ImageUrl);
                Assert.Equal(tmp.Description, np.Description);

                Assert.NotEqual(p.Name, np.Name);
                Assert.NotEqual(p.Category, np.Category);
                Assert.NotEqual(p.Price, np.Price);
                Assert.NotEqual(p.ImageUrl, np.ImageUrl);
                Assert.NotEqual(p.Description, np.Description);
            }
        }

        [Fact]
        public void UpdateInvalidIdProductCategoryRepository()
        {
            var p = new Product()
            {
                ProductId = 1,
                Name = "Testing product",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1,
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

                var np = repo.Add(p);
                Assert.Equal(1, context.Product.Count());
                var tmp = new Product()
                {
                    ProductId = 1,
                    Name = "Testing monkey",
                    Category = new ProductCategory()
                    {
                        Name = "monkey category"
                    },
                    Price = 100.0,
                    ImageUrl = "Test monkey",
                    Description = "This is a monkey"
                };

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    repo.Update(np.ProductId++, tmp);
                });
            }
        }

        [Fact]
        public void UpdateInvalidProductCategoryRepositoryExpectArgumentNullException()
        {
            var p = new Product()
            {
                ProductId = 1,
                Name = "Testing product",
                Category = new ProductCategory()
                {
                    ProductCategoryId = 1,
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

                var np1 = repo.Add(p);
                Assert.Equal(1, context.ProductCategory.Count());
                var tmp = new Product()
                {
                    ProductId = 1,
                    Name = "Testing monkey",
                    Category = new ProductCategory()
                    {
                        Name = "monkey category"
                    },
                    Price = 100.0,
                    ImageUrl = "Test monkey"
                };

                Assert.Throws<ArgumentNullException>(() =>
                {
                    var np = repo.Update(np1.ProductId, tmp);
                });
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
