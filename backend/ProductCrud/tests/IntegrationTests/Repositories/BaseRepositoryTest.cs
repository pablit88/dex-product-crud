using Dex.ProductCrud.Core.Entities;
using Dex.ProductCrud.Core.Interfaces.Repositories;
using Dex.ProductCrud.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dex.ProductCrud.IntegrationTests.Repositories
{
    public abstract class BaseRepositoryTest<T> : IDisposable where T : BaseEntity
    {
        protected readonly ProductCrudDbContext _context;

        protected IAsyncRepository<T> _asyncRepository;

        protected BaseRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ProductCrudDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ProductCrudDbContext(options);

            _context.Database.EnsureCreated();
        }

        protected abstract T GetEntity();

        protected abstract void ModifyEntity(T entity);

        [Fact]
        public async Task GetByIdAsync()
        {
            T entity = GetEntity();

            _context.Set<T>().Add(entity);

            _context.SaveChanges();

            var addedEntity = await _asyncRepository.GetByIdAsync(entity.Id);

            Assert.NotNull(addedEntity);
        }

        [Fact]
        public async Task ListAllAsync()
        {
            int entitiesCount = 5;

            for (int i = 0; i < entitiesCount; i++)
            {
                _context.Set<T>().Add(GetEntity());
            }

            _context.SaveChanges();

            var addedEntities = await _asyncRepository.ListAllAsync();

            Assert.Equal(entitiesCount, addedEntities.Count());
        }

        [Fact]
        public async Task AddAsync()
        {
            T entity = GetEntity();

            await _asyncRepository.AddAsync(entity);

            _context.SaveChanges();

            var addedEntity = await _context.Set<T>().FindAsync(entity.Id);

            Assert.NotNull(addedEntity);
        }

        [Fact]
        public async Task Update()
        {
            T entity = GetEntity();

            await _context.Set<T>().AddAsync(entity);

            _context.SaveChanges();

            ModifyEntity(entity);

            await _asyncRepository.UpdateAsync(entity);

            var exception = Record.Exception(() => _context.SaveChanges());

            Assert.Null(exception);
        }

        [Fact]
        public async Task Delete()
        {
            T entity = GetEntity();

            await _context.Set<T>().AddAsync(entity);

            _context.SaveChanges();

            await _asyncRepository.DeleteAsync(entity);

            _context.SaveChanges();

            var addedEntity = await _context.Set<T>().FindAsync(entity.Id);

            Assert.Null(addedEntity);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
