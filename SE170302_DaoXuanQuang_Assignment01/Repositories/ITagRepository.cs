using SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;

namespace SE170302_DaoXuanQuang_Assignment01_BackEnd.Repositories
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAllTagsAsync();
        Task<Tag?> GetTagByIdAsync(int tagId);
        Task AddTagAsync(Tag tag);
        Task UpdateTagAsync(Tag tag);
        Task DeleteTagAsync(int tagId);
    }
}