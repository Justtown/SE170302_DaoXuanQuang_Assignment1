using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Repositories;

namespace SE170302_DaoXuanQuang_Assignment01_BackEnd.Controllers
{
    public class NewsArticlesController : ODataController
    {
        private readonly INewsArticleRepository _repo;
        public NewsArticlesController(INewsArticleRepository repo) => _repo = repo;

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _repo.GetAllArticlesAsync());

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] string key)
        {
            var article = await _repo.GetArticleByIdAsync(key);
            if (article == null) return NotFound();
            return Ok(article);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewsArticleDto dto)
        {
            if (dto == null) return BadRequest("Article is null");

            var article = new NewsArticle
            {
                NewsArticleId = dto.NewsArticleId,
                NewsTitle = dto.NewsTitle,
                Headline = dto.Headline,
                CreatedDate = DateTime.Now,
                NewsContent = dto.NewsContent,
                NewsSource = dto.NewsSource,
                CategoryId = dto.CategoryId,
                NewsStatus = dto.NewsStatus,
                CreatedById = dto.CreatedById
            };

            await _repo.AddArticleAsync(article, dto.TagIds ?? new List<int>());
            return Created(article);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromRoute] string key, [FromBody] NewsArticleDto dto)
        {
            if (dto == null) return BadRequest("Article is null");
            var article = new NewsArticle
            {
                NewsArticleId = key,
                NewsTitle = dto.NewsTitle,
                Headline = dto.Headline,
                NewsContent = dto.NewsContent,
                NewsSource = dto.NewsSource,
                CategoryId = dto.CategoryId,
                NewsStatus = dto.NewsStatus,
                UpdatedById = dto.UpdatedById
            };
            await _repo.UpdateArticleAsync(article, dto.TagIds ?? new List<int>());
            return Updated(article);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] string key)
        {
            await _repo.DeleteArticleAsync(key);
            return NoContent();
        }
    }

    public class NewsArticleDto
    {
        public string NewsArticleId { get; set; } = null!;
        public string? NewsTitle { get; set; }
        public string Headline { get; set; } = null!;
        public string? NewsContent { get; set; }
        public string? NewsSource { get; set; }
        public short? CategoryId { get; set; }
        public bool? NewsStatus { get; set; }
        public short? CreatedById { get; set; }
        public short? UpdatedById { get; set; }
        public List<int>? TagIds { get; set; }
    }
}