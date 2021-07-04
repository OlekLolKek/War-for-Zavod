using System;
using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;


namespace Core
{
    public class TimeModel : ITimeModel, ITickable
    {
        public IObservable<int> GameTime => _gameTime.Select(value => (int)value);

        private ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();

        private bool _isPaused;
        
        public void Tick()
        {
            if (!_isPaused)
            {
                _gameTime.Value += Time.deltaTime;
            }
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Unpause()
        {
            _isPaused = false;
        }
    }
}