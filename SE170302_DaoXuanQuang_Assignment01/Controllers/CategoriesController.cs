using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Repositories;

namespace SE170302_DaoXuanQuang_Assignment01_BackEnd.Controllers
{
    public class CategoriesController : ODataController
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] short key)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(key);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            // XÓA validation tự động của OData vì nó không cần thiết cho create mới
            if (category == null) return BadRequest("Category is null");

            await _categoryRepository.AddCategoryAsync(category);
            return Created(category);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromRoute] short key, [FromBody] Category category)
        {
            if (category == null) return BadRequest("Category is null");
            if (key != category.CategoryId) return BadRequest("Key mismatch");

            var existing = await _categoryRepository.GetCategoryByIdAsync(key);
            if (existing == null) return NotFound();

            await _categoryRepository.UpdateCategoryAsync(category);
            return Updated(category);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] short key)
        {
            try
            {
                await _categoryRepository.DeleteCategoryAsync(key);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}