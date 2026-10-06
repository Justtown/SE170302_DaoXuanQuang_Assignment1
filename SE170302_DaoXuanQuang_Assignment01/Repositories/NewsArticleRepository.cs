using Microsoft.EntityFrameworkCore;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;

namespace SE170302_DaoXuanQuang_Assignment01_BackEnd.Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        public async Task<List<NewsArticle>> GetAllArticlesAsync()
        {
            using var context = new FunewsManagementContext();
            return await context.NewsArticles
                .Include(n => n.Category).Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        public async Task<NewsArticle?> GetArticleByIdAsync(string id)
        {
            using var context = new FunewsManagementContext();
            return await context.NewsArticles
                .Include(n => n.Category).Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .FirstOrDefaultAsync(n => n.NewsArticleId == id);
        }

        public async Task<List<NewsArticle>> GetActiveArticlesAsync()
        {
            using var context = new FunewsManagementContext();
            return await context.NewsArticles.Where(n => n.NewsStatus == true)
                .OrderByDescending(n => n.CreatedDate).ToListAsync();
        }

        public async Task<List<NewsArticle>> GetArticlesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            using var context = new FunewsManagementContext();
            return await context.NewsArticles
                .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                .OrderByDescending(n => n.CreatedDate).ToListAsync();
        }

        public async Task AddArticleAsync(NewsArticle article, List<int> tagIds)
        {
            using var context = new FunewsManagementContext();
            if (tagIds != null && tagIds.Any())
            {
                var tags = await context.Tags.Where(t => tagIds.Contains(t.TagId)).ToListAsync();
                article.Tags = tags;
            }
            context.NewsArticles.Add(article);
            await context.SaveChangesAsync();
        }

        public async Task UpdateArticleAsync(NewsArticle article, List<int> tagIds)
        {
            using var context = new FunewsManagementContext();
            var existing = await context.NewsArticles
                .Include(n => n.Tags)
                .FirstOrDefaultAsync(n => n.NewsArticleId == article.NewsArticleId);
            if (existing == null) throw new InvalidOperationException("Article not found");

            existing.NewsTitle = article.NewsTitle;
            existing.Headline = article.Headline;
            existing.NewsContent = article.NewsContent;
            existing.NewsSource = article.NewsSource;
            existing.CategoryId = article.CategoryId;
            existing.NewsStatus = article.NewsStatus;
            existing.UpdatedById = article.UpdatedById;
            existing.ModifiedDate = DateTime.Now;

            existing.Tags.Clear();
            if (tagIds != null && tagIds.Any())
            {
                var tags = await context.Tags.Where(t => tagIds.Contains(t.TagId)).ToListAsync();
                foreach (var tag in tags) existing.Tags.Add(tag);
            }
            await context.SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(string id)
        {
            using var context = new FunewsManagementContext();
            var article = await context.NewsArticles.Include(n => n.Tags)
                .FirstOrDefaultAsync(n => n.NewsArticleId == id);
            if (article == null) throw new InvalidOperationException("Article not found");
            article.Tags.Clear();
            context.NewsArticles.Remove(article);
            await context.SaveChangesAsync();
        }
    }
}