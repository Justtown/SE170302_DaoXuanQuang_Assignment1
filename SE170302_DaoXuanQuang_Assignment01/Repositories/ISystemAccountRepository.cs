using SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;

namespace SE170302_DaoXuanQuang_Assignment01_BackEnd.Repositories
{
    public interface ISystemAccountRepository
    {
        Task<List<SystemAccount>> GetAllAccountsAsync();
        Task<SystemAccount?> GetAccountByIdAsync(short id);
        Task<SystemAccount?> GetAccountByEmailAsync(string email);
        Task AddAccountAsync(SystemAccount account);
        Task UpdateAccountAsync(SystemAccount account);
        Task DeleteAccountAsync(short id);
    }
}