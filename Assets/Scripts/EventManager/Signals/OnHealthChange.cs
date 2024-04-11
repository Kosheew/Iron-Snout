using UnityEngine;

namespace CustomEventBus.Signals
{ 
    public class OnHealthChange
    {
        public readonly int Value;

        public OnHealthChange(int value)
        {
            Value = value;
        }
    }
}