using SE170302_DaoXuanQuang_Assignment01_BackEnd.DataAccess;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;

namespace SE170302_DaoXuanQuang_Assignment01_BackEnd.Repositories
{
    public class TagRepository : ITagRepository
    {
        public Task<List<Tag>> GetAllTagsAsync() => TagDAO.Instance.GetAllTagsAsync();
        public Task<Tag?> GetTagByIdAsync(int id) => TagDAO.Instance.GetTagByIdAsync(id);
        public Task AddTagAsync(Tag tag) => TagDAO.Instance.AddTagAsync(tag);
        public Task UpdateTagAsync(Tag tag) => TagDAO.Instance.UpdateTagAsync(tag);
        public Task DeleteTagAsync(int id) => TagDAO.Instance.DeleteTagAsync(id);
    }
}