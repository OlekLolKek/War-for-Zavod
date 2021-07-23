using System;
using Abstractions;
using UniRx;


namespace Core
{
    public class CoinModel : ICoinModel
    {
        public IObservable<int> Coins => _coins;

        private readonly ReactiveProperty<int> _coins
            = new ReactiveProperty<int>();

        public bool TryBuyItem(int price)
        {
            if (_coins.Value - price >= 0)
            {
                _coins.Value -= price;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddCoins(int coins)
        {
            _coins.Value += coins;
        }
    }
}