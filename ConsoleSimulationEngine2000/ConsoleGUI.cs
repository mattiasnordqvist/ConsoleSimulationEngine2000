using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArtisticPastelPainter;

namespace ConsoleSimulationEngine2000
{
    /// <summary>
    /// A Console GUI for game loop renderings.
    /// </summary>
    public class ConsoleGUI
    {
        public ConsoleGUI(int height = 35, int width = 150)
        {
            lock (cursorLock)
            {
                Console.SetCursorPosition(0, InputStartLine);
                Console.WindowWidth = width;
                Console.WindowHeight = height;
                Console.CursorVisible = false;
            }
        }
        public static object cursorLock = new object();
        private bool started;

        public int InputStartLine { get { return Console.WindowHeight - 2; } }

        /// <summary>
        /// Starts a simulation
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        public async Task Start(Simulation simulation)
        {
            if (started)
            {
                throw new InvalidOperationException("Can only run one simulation at a time");
            }
            started = true;

            var input = new Input();
            simulation.Input = input;
            Task.Run(() => CaptureInput(input));

            await Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(simulation.UpdatePeriod);
                    simulation.PassTime(simulation.UpdatePeriod);
                    Render(simulation);
                }
            });
        }

        private void Render(Simulation game)
        {
            foreach (var display in game.Displays)
            {
                display.Print(cursorLock);
            }
        }

        private void CaptureInput(Input input)
        {
            Console.SetCursorPosition(0, InputStartLine);
            Console.Write("> _" + "".PadRight(Console.WindowWidth));
            while (true)
            {
                Thread.Sleep(30);

                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    input.KeyInputted(key);
                    lock (cursorLock)
                    {
                        Console.SetCursorPosition(0, InputStartLine);
                        ArtisticString text = input.CurrentInput;
                        if (input.Suggestion != null)
                        {
                            text = input.CurrentInput + new ArtisticString(input.Suggestion.Substring(input.CurrentInput.Length), Color.Gray);
                        }

                        Console.Write("> " + text + "_" + "".PadRight(Console.WindowWidth));
                    }
                }
            }
        }
    }
}
