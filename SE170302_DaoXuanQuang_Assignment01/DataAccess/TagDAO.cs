using Microsoft.EntityFrameworkCore;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;

namespace SE170302_DaoXuanQuang_Assignment01_BackEnd.DataAccess
{
    public class TagDAO
    {
        private static TagDAO instance = null;
        private static readonly object instanceLock = new object();
        private TagDAO() { }

        public static TagDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null) instance = new TagDAO();
                    return instance;
                }
            }
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            using var context = new FunewsManagementContext();
            return await context.Tags.ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(int tagId)
        {
            using var context = new FunewsManagementContext();
            return await context.Tags.FindAsync(tagId);
        }

        public async Task AddTagAsync(Tag tag)
        {
            using var context = new FunewsManagementContext();
            context.Tags.Add(tag);
            await context.SaveChangesAsync();
        }

        public async Task UpdateTagAsync(Tag tag)
        {
            using var context = new FunewsManagementContext();
            context.Entry(tag).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(int tagId)
        {
            using var context = new FunewsManagementContext();
            var tag = await context.Tags.FindAsync(tagId);
            if (tag != null)
            {
                context.Tags.Remove(tag);
                await context.SaveChangesAsync();
            }
        }
    }
}