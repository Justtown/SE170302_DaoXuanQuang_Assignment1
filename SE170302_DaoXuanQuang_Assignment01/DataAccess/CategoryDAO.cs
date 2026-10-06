using Microsoft.EntityFrameworkCore;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;

namespace SE170302_DaoXuanQuang_Assignment01_BackEnd.DataAccess
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();

        private CategoryDAO() { }

        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            using var context = new FunewsManagementContext();
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(short categoryId)
        {
            using var context = new FunewsManagementContext();
            return await context.Categories.FindAsync(categoryId);
        }

        public async Task AddCategoryAsync(Category category)
        {
            using var context = new FunewsManagementContext();
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            using var context = new FunewsManagementContext();
            context.Entry(category).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(short categoryId)
        {
            using var context = new FunewsManagementContext();
            // Đảm bảo dùng CategoryId (chữ d viết thường)
            bool hasNews = await context.NewsArticles.AnyAsync(n => n.CategoryId == categoryId);
            if (hasNews)
            {
                throw new InvalidOperationException("Cannot delete category associated with active news articles.");
            }

            var category = await context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
            }
        }
    }
}