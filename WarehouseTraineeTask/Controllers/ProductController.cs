using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarehouseTraineeTask.Application.DTOs.RequestDTO;
using WarehouseTraineeTask.Application.DTOs.ResponseDTO;
using WarehouseTraineeTask.Domain.Entity;
using WarehouseTraineeTask.Domain.Interfaces.Services;

namespace WarehouseTraineeTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetProducts()
        {
            var products = await _productService.GetAllAsync();
            var productDTOs = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
            return Ok(productDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> GetProduct(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            var productDTO = _mapper.Map<ProductResponseDTO>(product);
            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDTO>> CreateProduct(ProductRequestDTO productRequestDTO)
        {
            var product = _mapper.Map<Product>(productRequestDTO);
            await _productService.AddAsync(product);

            var productResponseDTO = _mapper.Map<ProductResponseDTO>(product);
            return CreatedAtAction(nameof(GetProduct), new { id = productResponseDTO.Id }, productResponseDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductRequestDTO productRequestDTO)
        {
            if (id == Guid.Empty) return BadRequest();

            var productToUpdate = await _productService.GetByIdAsync(id);
            if (productToUpdate == null) return NotFound();

            _mapper.Map(productRequestDTO, productToUpdate);
            await _productService.UpdateAsync(id, productToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            await _productService.DeleteAsync(product.Id);
            return NoContent();
        }
    }
}