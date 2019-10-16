using System;

namespace ConsoleSimulationEngine2000
{
    public interface IInput<T> : IInput
    {
        bool HasInput { get; }
        T Consume();
    }

    public interface IInput
    {
        protected internal void KeyInputted(ConsoleKeyInfo consoleKeyInfo);
    }
}
