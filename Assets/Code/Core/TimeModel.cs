using System;
using Abstractions;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Zenject;


namespace Core
{
    [UsedImplicitly]
    public class TimeModel : ITimeModel, ITickable
    {
        public IObservable<int> GameTime =>
            _gameTime.Select(value => (int)value);

        private readonly ReactiveProperty<float> _gameTime
            = new ReactiveProperty<float>();
        
        private readonly float _normalTimeScale = 1.0f;
        private readonly float _pausedTimeScale = 0.0f;
        
        public void Tick()
        {
            _gameTime.Value += Time.deltaTime;
        }

        public void Pause()
        {
            Time.timeScale = _pausedTimeScale;
        }

        public void Unpause()
        {
            Time.timeScale = _normalTimeScale;
        }
    }
}