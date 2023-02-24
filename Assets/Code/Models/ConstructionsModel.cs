using System;

namespace Code.Models
{
    public class ConstructionsModel
    {
        public int HitCounter
        {
            get => _hitCounter;
            set
            {
                _hitCounter = value < 0 ? 0 : value;
                HitCounterHandler?.Invoke();
            }
        }

        public event Action HitCounterHandler;

        private int _hitCounter = 0;
    }
}