using UnityEngine;

namespace CustomEventBus.Signals
{
    public class OnScoreChange
    {
        public readonly int Value;

        public OnScoreChange(int value)
        {
            Value = value;
        }
    }
}

