using System.Threading.Tasks;

namespace WalletTask.BL.Interfaces
{
    public interface IUserBL : IBaseBL
    {
        Task Add(string name);
        Task<string> GetBalance(int userId);
    }
}