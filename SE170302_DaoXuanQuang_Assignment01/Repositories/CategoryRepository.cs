using SE170302_DaoXuanQuang_Assignment01_BackEnd.DataAccess;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;

namespace SE170302_DaoXuanQuang_Assignment01_BackEnd.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task<List<Category>> GetAllCategoriesAsync()
            => CategoryDAO.Instance.GetCategoriesAsync();

        public Task<Category?> GetCategoryByIdAsync(short id)
            => CategoryDAO.Instance.GetCategoryByIdAsync(id);

        public Task AddCategoryAsync(Category category)
            => CategoryDAO.Instance.AddCategoryAsync(category);

        public Task UpdateCategoryAsync(Category category)
            => CategoryDAO.Instance.UpdateCategoryAsync(category);

        public Task DeleteCategoryAsync(short id)
            => CategoryDAO.Instance.DeleteCategoryAsync(id);
    }
}