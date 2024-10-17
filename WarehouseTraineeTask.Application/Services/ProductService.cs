using WarehouseTraineeTask.Domain.Entity;
using WarehouseTraineeTask.Domain.Interfaces.Interfaces;
using WarehouseTraineeTask.Domain.Interfaces.Services;

namespace WarehouseTraineeTask.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            product.Id = Guid.NewGuid();
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(Guid id, Product product)
        {
            product.Id = id;
            _productRepository.Update(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = _productRepository.GetByIdAsync(id).Result;
            _productRepository.Delete(product);
        }
    }
}