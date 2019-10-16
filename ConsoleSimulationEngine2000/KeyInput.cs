using System;
using System.Collections.Generic;

namespace ConsoleSimulationEngine2000
{
    public class KeyInput : IInput<ConsoleKeyInfo>
    {
        internal Queue<ConsoleKeyInfo> LastInput { get; } = new Queue<ConsoleKeyInfo>();
        public bool HasInput => LastInput.TryPeek(out _);

        public ConsoleKeyInfo Consume()
        {
            if (HasInput)
            {
                var returnValue = LastInput.Dequeue();
                return returnValue;
            }
            else
            {
                throw new InvalidOperationException("There's no input to consume");
            }
        }


        void IInput.KeyInputted(ConsoleKeyInfo consoleKeyInfo)
        {
            LastInput.Enqueue(consoleKeyInfo);
        }
    }
}
