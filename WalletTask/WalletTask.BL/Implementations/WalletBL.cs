﻿using System.Linq;
using System.Threading.Tasks;
using WalletTask.BL.Interfaces;
using WalletTask.Common.Helpers;
using WalletTask.Common.Helpers.CurrencyHelper;
using WalletTask.DAL;
using WalletTask.DAL.Models;

namespace WalletTask.BL.Implementations
{
    public class WalletBL : BaseBL, IWalletBL
    {
        public WalletBL(WalletTaskDAL dal) : base(dal)
        {
        }

        public async Task TopUp(int userId, string currency, decimal amount)
        {
            var wallet = await GetWallet(userId, currency);
            if(wallet is null)
                return;

            wallet.Amount += amount;

            await _dal.SaveChangesAsync();
        }

        public async Task Withdraw(int userId, string currency, decimal amount)
        {
            var wallet = await GetWallet(userId, currency);
            if (wallet is null)
                return;

            wallet.Amount -= amount;

            await _dal.SaveChangesAsync();
        }

        public async Task Transfer(int userId, string fromCurrency, string toCurrency, decimal amount)
        {
            var user = await _dal.UserDAL.Get(userId);
            if(user is null)
                return;

            var fromWallet = user.Wallets.FirstOrDefault(x => x.Currency == fromCurrency);
            if(fromWallet is null)
                return;

            var toWallet = user.Wallets.FirstOrDefault(x => x.Currency == toCurrency);
            if(toWallet is null)
                return;

            fromWallet.Amount -= amount;
            var toCurrencyAmount = CurrencyHelper.Convert(fromCurrency, toCurrency, amount);
            toWallet.Amount += toCurrencyAmount;

            await _dal.SaveChangesAsync();
        }

        private async Task<Wallet> GetWallet(int userId, string currency)
        {
            var user = await _dal.UserDAL.Get(userId);
            var wallet = user?.Wallets.FirstOrDefault(x => x.Currency == currency);
            return wallet;
        }
    }
}