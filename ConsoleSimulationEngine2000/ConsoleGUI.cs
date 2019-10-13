using System;
using System.Threading;
using System.Threading.Tasks;

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
                Console.WindowWidth = width;
                Console.WindowHeight = height;
                Console.CursorVisible = false;
            }
        }

        public static object cursorLock = new object();
        private bool started;

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

            var input = new Input(cursorLock);
            simulation.Input = input;
            await Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000 / simulation.FPS);
                    simulation.PassTime(1000 / simulation.FPS);

                    while (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        input.KeyInputted(key);
                    }

                    Render(simulation);
                }
            });
        }
        string lastRendered = null;

        public TimeSpan RenderTime { get; private set; }
        public TimeSpan PrintTime { get; private set; }

        private void Render(Simulation simulation)
        {
            var ms1 = DateTime.UtcNow;
            var backBuffer = "";

            char[][] c = new char[Console.WindowHeight][];
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = new char[Console.WindowWidth];
            }
            CharMatrixStack cms = new CharMatrixStack();
            CharMatrix cm = new CharMatrix(c, 0, 0, Console.WindowWidth, Console.WindowHeight);
            cms.Add(cm);
            foreach (var display in simulation.Displays)
            {
                //backBuffer = backBuffer.Blend(display.FullDisplay()).ToString();
                cms.Add(display.GetCharMatrix());
            }
            backBuffer = cms.ToString(Console.WindowWidth, Console.WindowHeight);
            var ms2 = DateTime.UtcNow;

            lock (cursorLock)
            {
                Console.SetCursorPosition(0, 0);
                if (lastRendered == null || lastRendered.Length != backBuffer.Length)
                {
                    Console.Write(backBuffer);
                }
                else
                {
                    var backLines = backBuffer.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    var lastRenderedLines = lastRendered.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    for (int y = 0; y < backLines.Length; y++)
                    {
                        if (backLines[y] != lastRenderedLines[y])
                        {
                            for (int x = 0; x < backLines.Length; x++)
                            {

                                if (backLines[y][x] != lastRenderedLines[y][x])
                                {
                                    Console.SetCursorPosition(x, y);
                                    Console.Write(backLines[y][x]);
                                }
                            }
                        }
                    }

                }
                var ms3 = DateTime.UtcNow;

                RenderTime = ms2 - ms1;
                PrintTime = ms3 - ms2;
            }
            lastRendered = backBuffer;
        }
    }
}
