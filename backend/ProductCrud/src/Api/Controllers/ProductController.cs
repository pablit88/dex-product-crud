using Dex.ProductCrud.Core.DataTransferObjects;
using Dex.ProductCrud.Core.Exceptions;
using Dex.ProductCrud.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dex.ProductCrud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        /// <summary>
        /// Retrieves a list of all products.
        /// </summary>
        /// <returns>A list of products.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<ProductController>/5
        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<ProductController>
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productInfo">The information of the product to create.</param>
        /// <returns>The ID of the created product.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] ProductInfo productInfo)
        {
            if (productInfo == null)
            {
                return BadRequest("product cannot be null.");
            }

            try
            {
                var productId = await _productService.AddAsync(productInfo);
                return Ok(productId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<ProductController>/5
        /// <summary>
        /// Updates an existing product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productInfo">The updated product information.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] ProductInfo productInfo)
        {
            if (productInfo == null)
            {
                return BadRequest("Invalid product data.");
            }

            try
            {
                await _productService.UpdateAsync(id, productInfo);
                return NoContent();
            }
            catch (NotFoundException nfEx)
            {
                return NotFound(nfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<ProductController>/5
        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException nfEx)
            {
                return NotFound(nfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<ProductController>/5/categories/3
        /// <summary>
        /// Adds a category to a product.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <param name="categoryId">The ID of the category to add.</param>
        [HttpPost("{productId}/categories/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCategoryToProduct(int productId, int categoryId)
        {
            try
            {
                await _productService.AddCategoryToProductAsync(productId, categoryId);
                return NoContent();
            }
            catch (NotFoundException nfEx)
            {
                return NotFound(nfEx.Message);
            }
            catch (ConflictException cEx)
            {
                return Conflict(cEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<ProductController>/5/categories/3
        /// <summary>
        /// Removes a category from a product.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <param name="categoryId">The ID of the category to remove.</param>
        [HttpDelete("{productId}/categories/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveCategoryFromProduct(int productId, int categoryId)
        {
            try
            {
                await _productService.RemoveCategoryFromProductAsync(productId, categoryId);
                return NoContent();
            }
            catch (NotFoundException nfEx)
            {
                return NotFound(nfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
