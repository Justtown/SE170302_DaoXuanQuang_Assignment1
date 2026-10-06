using SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;

namespace SE170302_DaoXuanQuang_Assignment01_BackEnd.Repositories
{
    public interface INewsArticleRepository
    {
        Task<List<NewsArticle>> GetAllArticlesAsync();
        Task<NewsArticle?> GetArticleByIdAsync(string id);
        Task<List<NewsArticle>> GetActiveArticlesAsync();
        Task<List<NewsArticle>> GetArticlesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task AddArticleAsync(NewsArticle article, List<int> tagIds);      
        Task UpdateArticleAsync(NewsArticle article, List<int> tagIds);   
        Task DeleteArticleAsync(string id);                                
    }
}