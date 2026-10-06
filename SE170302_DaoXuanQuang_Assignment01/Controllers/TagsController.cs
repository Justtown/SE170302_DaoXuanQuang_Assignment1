using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Repositories;

namespace SE170302_DaoXuanQuang_Assignment01_BackEnd.Controllers
{
    public class TagsController : ODataController
    {
        private readonly ITagRepository _tagRepository;

        public TagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tags = await _tagRepository.GetAllTagsAsync();
            return Ok(tags);
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] int key)
        {
            var tag = await _tagRepository.GetTagByIdAsync(key);
            if (tag == null) return NotFound();
            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Tag tag)
        {
            if (tag == null) return BadRequest("Tag is null");
            await _tagRepository.AddTagAsync(tag);
            return Created(tag);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromRoute] int key, [FromBody] Tag tag)
        {
            if (tag == null) return BadRequest("Tag is null");
            if (key != tag.TagId) return BadRequest("Key mismatch");
            var existing = await _tagRepository.GetTagByIdAsync(key);
            if (existing == null) return NotFound();
            await _tagRepository.UpdateTagAsync(tag);
            return Updated(tag);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int key)
        {
            await _tagRepository.DeleteTagAsync(key);
            return NoContent();
        }
    }
}