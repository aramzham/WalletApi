using WalletTask.DAL.Interfaces;
using WalletTask.DAL.Models;

namespace WalletTask.DAL.Implementations
{
    public class WalletDAL : BaseDAL, IWalletDAL
    {
        public WalletDAL(WalletTaskContext dbContext) : base(dbContext)
        {
        }
    }
}