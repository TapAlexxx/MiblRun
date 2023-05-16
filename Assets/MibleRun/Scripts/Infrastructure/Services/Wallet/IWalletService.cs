namespace Scripts.Infrastructure.Services.Wallet
{
    public interface IWalletService
    {
        void AddCoins(int amount);
        void WithdrawCoins(int amount);
    }

}