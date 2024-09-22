using Dex.ProductCrud.Core.DataTransferObjects;
using Dex.ProductCrud.Core.Exceptions;
using Dex.ProductCrud.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dex.ProductCrud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoryController>
        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>List of categories.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<CategoryController>/5
        /// <summary>
        /// Retrieves a specific category by its ID.
        /// </summary>
        /// <param name="id">Category ID.</param>
        /// <returns>Category data.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<CategoryController>
        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryInfo">Category information.</param>
        /// <returns>ID of the newly created category.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CategoryInfo categoryInfo)
        {
            if (categoryInfo == null)
            {
                return BadRequest("Category cannot be null.");
            }

            try
            {
                var categoryId = await _categoryService.AddAsync(categoryInfo);
                return Ok(categoryId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<CategoryController>/5
        /// <summary>
        /// Updates an existing category by its ID.
        /// </summary>
        /// <param name="id">Category ID.</param>
        /// <param name="categoryInfo">Updated category information.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryInfo categoryInfo)
        {
            if (categoryInfo == null)
            {
                return BadRequest("Invalid category data.");
            }

            try
            {
                await _categoryService.UpdateAsync(id, categoryInfo);
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

        // DELETE api/<CategoryController>/5
        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">Category ID.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
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
