using System.Threading.Tasks;

namespace WalletTask.BL.Interfaces
{
    public interface IWalletBL : IBaseBL
    {
        Task TopUp(int userId, string currency, decimal amount);
        Task Withdraw(int userId, string currency, decimal amount);
        Task Transfer(int userId, string fromCurrency, string toCurrency, decimal amount);
    }
}