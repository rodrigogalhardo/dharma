using Dharma.Common.Helpers;
using Dharma.Common.Tests;
using Dharma.Data.MongoDB.Exceptions;
using Dharma.Data.MongoDB.Providers;
using Dharma.Data.MongoDB.Repositories;
using Dharma.Data.MongoDB.Tests.Repositories.Shared;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dharma.Data.MongoDB.Tests.Repositories
{
    [TestClass]
    public class MongoRepositoryTest : BaseTest
    {
        protected readonly MongoDatabaseProvider Provider;
        protected MongoRepository<MongoTestEntity> Repository;
        protected static IMongoDatabase Client;
        protected static MongoOptions MongoOptions;

        static  MongoRepositoryTest()
        {
            MongoOptions = OptionsHelper.GetConfiguration<MongoOptions>(Configuration);

            MongoOptions.DatabaseName = GetUniqueName(MongoOptions.DatabaseName);
        }

        public MongoRepositoryTest()
        {
            Provider = new MongoDatabaseProvider(Options.Create(MongoOptions));
            Client = Provider.Create();
        }

        [TestInitialize]
        public void BeforeEveryUnitTest()
        {
            Repository = new MongoRepository<MongoTestEntity>(Provider);

            Client.DropCollection(Repository.TableName());
        }

        [ClassCleanup]
        public static void AfterAll()
        {
            Client.Client.DropDatabase(MongoOptions.DatabaseName);
        }

        [TestMethod]
        public void Create_Success()
        {
            var entity = new MongoTestEntity()
            {
                Bool = false,
                Number = 1,
                String = "String"
            };

            entity = Repository.Create(entity);

            Assert.IsNotNull(entity.Id);
            Assert.IsTrue(entity.Created >= DateTime.UtcNow.AddSeconds(-5));
            Assert.IsTrue(entity.Updated >= DateTime.UtcNow.AddSeconds(-5));
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNullException))]
        public void Create_Throw_Exception_Entity_Null()
        {
            MongoTestEntity entity = null;

            Repository.Create(entity);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityIdNullArgumentException))]
        public void Create_Throw_Exception_Valid_Id()
        {
            var entity = new MongoTestEntity()
            {
                Id = "7zulgmp4pmyb9ofbp2zr1z99",
                Bool = false,
                Number = 1,
                String = "String"
            };

            Repository.Create(entity);
        }

        [TestMethod]
        public async Task CreateAsync_Success()
        {
            var entity = new MongoTestEntity()
            {
                Bool = false,
                Number = 1,
                String = "String"
            };

            entity = await Repository.CreateAsync(entity);

            Assert.IsNotNull(entity.Id);
            Assert.IsTrue(entity.Created >= DateTime.UtcNow.AddSeconds(-5));
            Assert.IsTrue(entity.Updated >= DateTime.UtcNow.AddSeconds(-5));
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNullException))]
        public async Task CreateAsync_Throw_Exception_Entity_Null()
        {
            MongoTestEntity entity = null;

            await Repository.CreateAsync(entity);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityIdNullArgumentException))]
        public async Task CreateAsync_Throw_Exception_Valid_Id()
        {
            var entity = new MongoTestEntity()
            {
                Id = "7zulgmp4pmyb9ofbp2zr1z99",
                Bool = false,
                Number = 1,
                String = "String"
            };

            await Repository.CreateAsync(entity);
        }

        [TestMethod]
        public void Create_Batch_Success()
        {
            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 1000; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            Repository.Create(batch);

            var result = Repository.Read(1, 1000);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1000);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNullException))]
        public void Create_Batch_Throw_Exception_Entity_Null()
        {
            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 10; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            batch.Add(null);

            Repository.Create(batch);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityIdNullArgumentException))]
        public void Create_Batch_Throw_Exception_Valid_Id()
        {
            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 10; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            batch.Add(new MongoTestEntity()
            {
                Id = "7zulgmp4pmyb9ofbp2zr1z99",
                Bool = false,
                Number = 1,
                String = "String"
            });

            Repository.Create(batch);
        }

        [TestMethod]
        public async Task CreateAsync_Batch_Success()
        {
            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 1000; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            await Repository.CreateAsync(batch);

            var result = Repository.Read(1, 1000);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1000);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNullException))]
        public async Task CreateAsync_Batch_Throw_Exception_Entity_Null()
        {
            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 10; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            batch.Add(null);

            await Repository.CreateAsync(batch);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityIdNullArgumentException))]
        public async Task CreateAsync_Batch_Throw_Exception_Valid_Id()
        {
            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 10; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            batch.Add(new MongoTestEntity()
            {
                Id = "7zulgmp4pmyb9ofbp2zr1z99",
                Bool = false,
                Number = 1,
                String = "String"
            });

            await Repository.CreateAsync(batch);
        }

        [TestMethod]
        public void Delete_Success()
        {
            var entity = new MongoTestEntity()
            {
                Bool = false,
                Number = 1,
                String = "String"
            };

            entity = Repository.Create(entity);

            Repository.Delete(entity);

            entity = Repository.Read(entity.Id);

            Assert.IsNull(entity);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNullException))]
        public void Delete_Throw_Exception_Entity_Null()
        {
            MongoTestEntity entity = null;

            Repository.Delete(entity);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityIdInvalidArgumentException))]
        public void Delete_Throw_Exception_Invalid_Id()
        {
            var entity = new MongoTestEntity()
            {
                Id = null
            };

            Repository.Delete(entity);
        }

        [TestMethod]
        public async Task DeleteAsync_Success()
        {
            var entity = new MongoTestEntity()
            {
                Bool = false,
                Number = 1,
                String = "String"
            };

            entity = Repository.Create(entity);

            await Repository.DeleteAsync(entity);

            entity = Repository.Read(entity.Id);

            Assert.IsNull(entity);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNullException))]
        public async Task DeleteAsync_Throw_Exception_Entity_Null()
        {
            MongoTestEntity entity = null;

            await Repository.DeleteAsync(entity);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityIdInvalidArgumentException))]
        public async Task DeleteAsync_Throw_Exception_Invalid_Id()
        {
            var entity = new MongoTestEntity()
            {
                Id = null
            };

            await Repository.DeleteAsync(entity);
        }

        [TestMethod]
        public void Delete_By_Id_Success()
        {
            var entity = new MongoTestEntity()
            {
                Bool = false,
                Number = 1,
                String = "String"
            };

            entity = Repository.Create(entity);

            Repository.Delete(entity.Id);

            entity = Repository.Read(entity.Id);

            Assert.IsNull(entity);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityIdInvalidArgumentException))]
        public void Delete_By_Id_Throw_Exception_Invalid_Id()
        {
            string id = null;

            Repository.Delete(id);
        }

        [TestMethod]
        public async Task DeleteAsync_By_Id_Success()
        {
            var entity = new MongoTestEntity()
            {
                Bool = false,
                Number = 1,
                String = "String"
            };

            entity = Repository.Create(entity);

            await Repository.DeleteAsync(entity.Id);

            entity = Repository.Read(entity.Id);

            Assert.IsNull(entity);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityIdInvalidArgumentException))]
        public async Task DeleteAsync_By_Id_Throw_Exception_Invalid_Id()
        {
            string id = null;

            await Repository.DeleteAsync(id);
        }

        [TestMethod]
        public void Read_Success()
        {
            var entity = new MongoTestEntity()
            {
                Bool = false,
                Number = 1,
                String = "String"
            };

            entity = Repository.Create(entity);

            var result = Repository.Read(entity.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, entity.Id);
            Assert.AreEqual(result.String, entity.String);
            Assert.AreEqual(result.Number, entity.Number);
            Assert.AreEqual(result.Bool, entity.Bool);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityIdInvalidArgumentException))]
        public void Read_Throw_Exception_Invalid_Id()
        {
            string id = null;

            Repository.Read(id);
        }

        [TestMethod]
        public async Task ReadAsync_Success()
        {
            var entity = new MongoTestEntity()
            {
                Bool = false,
                Number = 1,
                String = "String"
            };

            entity = Repository.Create(entity);

            var result = await Repository.ReadAsync(entity.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, entity.Id);
            Assert.AreEqual(result.String, entity.String);
            Assert.AreEqual(result.Number, entity.Number);
            Assert.AreEqual(result.Bool, entity.Bool);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityIdInvalidArgumentException))]
        public async Task ReadAsync_Throw_Exception_Invalid_Id()
        {
            string id = null;

            await Repository.ReadAsync(id);
        }

        [TestMethod]
        public void Read_Pagination_Success()
        {
            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 1000; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            Repository.Create(batch);

            var result = Repository.Read(1, 100);

            Assert.AreEqual(result.Count, 100);

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(result[i].Number, batch[i].Number);
                Assert.AreEqual(result[i].String, batch[i].String);
            }

            result = Repository.Read(3, 100);

            Assert.AreEqual(result.Count, 100);

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(result[i].Number, batch[i + 200].Number);
                Assert.AreEqual(result[i].String, batch[i + 200].String);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageException))]
        public void Read_Pagination_Page_0_Throw_Exception_Invalid_Page()
        {
            Repository.Read(0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageException))]
        public void Read_Pagination_Page_Less_Than_0_Throw_Exception_Invalid_Page()
        {
            Repository.Read(-1, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageSizeException))]
        public void Read_Pagination_PageSize_0_Throw_Exception_Invalid_Page()
        {
            Repository.Read(1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageSizeException))]
        public void Read_Pagination_PageSize_Less_Than_0_Throw_Exception_Invalid_Page()
        {
            Repository.Read(1, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageSizeException))]
        public void Read_Pagination_PageSize_More_Than_1000_Throw_Exception_Invalid_Page()
        {
            Repository.Read(1, 1001);
        }

        [TestMethod]
        public async Task ReadAsync_Pagination_Success()
        {
            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 1000; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            Repository.Create(batch);

            var result = await Repository.ReadAsync(1, 100);

            Assert.AreEqual(result.Count, 100);

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(result[i].Number, batch[i].Number);
                Assert.AreEqual(result[i].String, batch[i].String);
            }

            result = await Repository.ReadAsync(3, 100);

            Assert.AreEqual(result.Count, 100);

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(result[i].Number, batch[i + 200].Number);
                Assert.AreEqual(result[i].String, batch[i + 200].String);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageException))]
        public async Task ReadAsync_Pagination_Page_0_Throw_Exception_Invalid_Page()
        {
            await Repository.ReadAsync(0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageException))]
        public async Task ReadAsync_Pagination_Page_Less_Than_0_Throw_Exception_Invalid_Page()
        {
            await Repository.ReadAsync(-1, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageSizeException))]
        public async Task ReadAsync_Pagination_PageSize_0_Throw_Exception_Invalid_Page()
        {
            await Repository.ReadAsync(1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageSizeException))]
        public async Task ReadAsync_Pagination_PageSize_Less_Than_0_Throw_Exception_Invalid_Page()
        {
            await Repository.ReadAsync(1, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageSizeException))]
        public async Task ReadAsync_Pagination_PageSize_More_Than_1000_Throw_Exception_Invalid_Page()
        {
            await Repository.ReadAsync(1, 1001);
        }

        [TestMethod]
        public void Read_Pagination_With_Query_Success()
        {
            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 1000; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            Repository.Create(batch);

            var result = Repository.Read(1, 100, x => x.Number > 500);

            Assert.AreEqual(result.Count, 100);

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(result[i].Number, batch[i + 500].Number);
                Assert.AreEqual(result[i].String, batch[i + 500].String);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageException))]
        public void Read_Pagination_With_Query_Page_0_Throw_Exception_Invalid_Page()
        {
            Repository.Read(0, 100, x => x.Number > 500);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageException))]
        public void Read_Pagination_With_Query_Page_Less_Than_0_Throw_Exception_Invalid_Page()
        {
            Repository.Read(-1, 100, x => x.Number > 500);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageSizeException))]
        public void Read_Pagination_With_Query_PageSize_0_Throw_Exception_Invalid_Page()
        {
            Repository.Read(1, 0, x => x.Number > 500);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageSizeException))]
        public void Read_Pagination_With_Query_PageSize_Less_Than_0_Throw_Exception_Invalid_Page()
        {
            Repository.Read(1, -1, x => x.Number > 500);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageSizeException))]
        public void Read_Pagination_With_Query_PageSize_More_Than_1000_Throw_Exception_Invalid_Page()
        {
            Repository.Read(1, 1001, x => x.Number > 500);
        }

        [TestMethod]
        [ExpectedException(typeof(QueryException))]
        public void Read_Pagination_With_Query_Throw_Exception_Query_Null()
        {
            Repository.Read(1, 100, null);
        }

        [TestMethod]
        public async Task ReadAsync_Pagination_With_Query_Success()
        {
            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 1000; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            Repository.Create(batch);

            var result = await Repository.ReadAsync(1, 100, x => x.Number > 500);

            Assert.AreEqual(result.Count, 100);

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(result[i].Number, batch[i + 500].Number);
                Assert.AreEqual(result[i].String, batch[i + 500].String);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageException))]
        public async Task ReadAsync_Pagination_With_Query_Page_0_Throw_Exception_Invalid_Page()
        {
            await Repository.ReadAsync(0, 100, x => x.Number > 500);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageException))]
        public async Task ReadAsync_Pagination_With_Query_Page_Less_Than_0_Throw_Exception_Invalid_Page()
        {
            await Repository.ReadAsync(-1, 100, x => x.Number > 500);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageSizeException))]
        public async Task ReadAsync_Pagination_With_Query_PageSize_0_Throw_Exception_Invalid_Page()
        {
            await Repository.ReadAsync(1, 0, x => x.Number > 500);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageSizeException))]
        public async Task ReadAsync_Pagination_With_Query_PageSize_Less_Than_0_Throw_Exception_Invalid_Page()
        {
            await Repository.ReadAsync(1, -1, x => x.Number > 500);
        }

        [TestMethod]
        [ExpectedException(typeof(PaginationPageSizeException))]
        public async Task ReadAsync_Pagination_With_Query_PageSize_More_Than_1000_Throw_Exception_Invalid_Page()
        {
            await Repository.ReadAsync(1, 1001, x => x.Number > 500);
        }

        [TestMethod]
        [ExpectedException(typeof(QueryException))]
        public async Task ReadAsync_Pagination_With_Query_Throw_Exception_Query_Null()
        {
            await Repository.ReadAsync(1, 100, null);
        }

        [TestMethod]
        public void Read_With_Query_Success()
        {
            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 1000; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            Repository.Create(batch);

            var result = Repository.Read(x => x.Number > 500);

            Assert.AreEqual(result.Count, 500);

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(result[i].Number, batch[i + 500].Number);
                Assert.AreEqual(result[i].String, batch[i + 500].String);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(QueryException))]
        public void Read_With_Query_Throw_Exception_Query_Null()
        {
            Expression<Func<MongoTestEntity, bool>> query = null;

            Repository.Read(query);
        }

        [TestMethod]
        public async Task ReadAsync_With_Query_Success()
        {
            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 1000; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            Repository.Create(batch);

            var result = await Repository.ReadAsync(x => x.Number > 500);

            Assert.AreEqual(result.Count, 500);

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(result[i].Number, batch[i + 500].Number);
                Assert.AreEqual(result[i].String, batch[i + 500].String);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(QueryException))]
        public async Task ReadAsync_With_Query_Throw_Exception_Query_Null()
        {
            Expression<Func<MongoTestEntity, bool>> query = null;

            await Repository.ReadAsync(query);
        }

        [TestMethod]
        public void ReadFirstOrDefault_With_Query_Success()
        {
            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 1000; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            Repository.Create(batch);

            var result = Repository.ReadFirstOrDefault(x => x.Number > 500);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Number > 500);
        }

        [TestMethod]
        [ExpectedException(typeof(QueryException))]
        public void ReadFirstOrDefault_With_Query_Throw_Exception_Query_Null()
        {
            Expression<Func<MongoTestEntity, bool>> query = null;

            Repository.ReadFirstOrDefault(query);
        }

        [TestMethod]
        public async Task ReadFirstOrDefaultAsync_With_Query_Success()
        {
            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 1000; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            Repository.Create(batch);

            var result = await Repository.ReadFirstOrDefaultAsync(x => x.Number > 500);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Number > 500);
        }

        [TestMethod]
        [ExpectedException(typeof(QueryException))]
        public async Task ReadFirstOrDefaultAsync_With_Query_Throw_Exception_Query_Null()
        {
            Expression<Func<MongoTestEntity, bool>> query = null;

            await Repository.ReadFirstOrDefaultAsync(query);
        }

        [TestMethod]
        public void Count_Success()
        {
            var result = Repository.Count();

            Assert.AreEqual(result, 0);

            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 1000; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            Repository.Create(batch);

            result = Repository.Count();

            Assert.AreEqual(result, batch.Count);
        }

        [TestMethod]
        public async Task CountAsync_Success()
        {
            var result = await Repository.CountAsync();

            Assert.AreEqual(result, 0);

            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 1000; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            await Repository.CreateAsync(batch);

            result = await Repository.CountAsync();

            Assert.AreEqual(result, batch.Count);
        }

        [TestMethod]
        public void Count_With_Query_Success()
        {
            var result = Repository.Count();

            Assert.AreEqual(result, 0);

            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 1000; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            Repository.Create(batch);

            result = Repository.Count(x => x.Number > 500);

            Assert.AreEqual(result, 500);
        }

        [TestMethod]
        [ExpectedException(typeof(QueryException))]
        public void Count_With_Query_Throw_Exception_Query_Null()
        {
            Repository.Count(null);
        }

        [TestMethod]
        public async Task CountAsync_With_Query_Success()
        {
            var result = await Repository.CountAsync();

            Assert.AreEqual(result, 0);

            var batch = new List<MongoTestEntity>();

            for (int i = 1; i <= 1000; i++)
            {
                batch.Add(new MongoTestEntity()
                {
                    Number = i,
                    Bool = false,
                    String = "String " + i
                });
            }

            await Repository.CreateAsync(batch);

            result = await Repository.CountAsync(x => x.Number > 500);

            Assert.AreEqual(result, 500);
        }

        [TestMethod]
        [ExpectedException(typeof(QueryException))]
        public async Task CountAsync_With_Query_Throw_Exception_Query_Null()
        {
            await Repository.CountAsync(null);
        }

        [TestMethod]
        public void Update_Success()
        {
            var entity = new MongoTestEntity()
            {
                Number = 1,
                Bool = false,
                String = "String 1"
            };

            entity = Repository.Create(entity);

            entity.String = "String 2";

            Repository.Update(entity);

            var result = Repository.Read(entity.Id);

            Assert.AreEqual(result.String, entity.String);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNullException))]
        public void Update_Throw_Exception_Entity_Null()
        {
            MongoTestEntity entity = null;

            Repository.Update(entity);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityIdInvalidArgumentException))]
        public void Update_Throw_Exception_Id_Null()
        {
            var entity = new MongoTestEntity()
            {
                Id = null
            };

            Repository.Update(entity);
        }

        [TestMethod]
        public async Task UpdateAsync_Success()
        {
            var entity = new MongoTestEntity()
            {
                Number = 1,
                Bool = false,
                String = "String 1"
            };

            entity = await Repository.CreateAsync(entity);

            entity.String = "String 2";

            await Repository.UpdateAsync(entity);

            var result = await Repository.ReadAsync(entity.Id);

            Assert.AreEqual(result.String, entity.String);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNullException))]
        public async Task UpdateAsync_Throw_Exception_Entity_Null()
        {
            MongoTestEntity entity = null;

            await Repository.UpdateAsync(entity);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityIdInvalidArgumentException))]
        public async Task UpdateAsync_Throw_Exception_Id_Null()
        {
            var entity = new MongoTestEntity()
            {
                Id = null
            };

            await Repository.UpdateAsync(entity);
        }
    }
}
