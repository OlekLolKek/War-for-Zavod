using System;


namespace Abstractions
{
    public interface ICoinModel
    {
        IObservable<int> Coins { get; }
        bool TryBuyItem(int price);
        void AddCoins(int coins);
    }
}