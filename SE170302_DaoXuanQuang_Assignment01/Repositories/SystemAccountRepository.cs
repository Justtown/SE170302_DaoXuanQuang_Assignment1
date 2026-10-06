using Microsoft.EntityFrameworkCore;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;

namespace SE170302_DaoXuanQuang_Assignment01_BackEnd.Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        public async Task<List<SystemAccount>> GetAllAccountsAsync()
        {
            using var context = new FunewsManagementContext();
            return await context.SystemAccounts.ToListAsync();
        }

        public async Task<SystemAccount?> GetAccountByIdAsync(short id)
        {
            using var context = new FunewsManagementContext();
            return await context.SystemAccounts.FindAsync(id);
        }

        public async Task<SystemAccount?> GetAccountByEmailAsync(string email)
        {
            using var context = new FunewsManagementContext();
            return await context.SystemAccounts.FirstOrDefaultAsync(a => a.AccountEmail == email);
        }

        public async Task AddAccountAsync(SystemAccount account)
        {
            using var context = new FunewsManagementContext();
            context.SystemAccounts.Add(account);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(SystemAccount account)
        {
            using var context = new FunewsManagementContext();
            context.Entry(account).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(short id)
        {
            using var context = new FunewsManagementContext();
            var account = await context.SystemAccounts.FindAsync(id);
            if (account != null)
            {
                context.SystemAccounts.Remove(account);
                await context.SaveChangesAsync();
            }
        }
    }
}