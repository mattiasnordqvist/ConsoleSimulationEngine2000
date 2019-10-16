using System;
using System.Text;
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
            Input = new Input();
            Console.WindowWidth = width;
            Console.WindowHeight = height;
            Console.CursorVisible = false;
        }

        private bool started;
        public int TargetRenderTime { get; set; } = 20;
        public int TargetUpdateTime { get; set; } = 1000;

        public bool ForceFullNextRender { get; set; } = false;
        public bool OptimizedForPerformance { get; set; } = false;

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

            await Task.Run(async () =>
            {
                int delta = 0;
                while (true)
                {
                    var s1 = DateTime.UtcNow;
                    await Task.Delay(TargetRenderTime);
                    var s2 = DateTime.UtcNow;
                    delta += (s2 - s1).Milliseconds;
                    if (delta > TargetUpdateTime)
                    {
                        var u1 = DateTime.UtcNow;
                        simulation.PassTime(delta);
                        LastUpdateTime = (u1 - DateTime.UtcNow);
                        delta = 0;
                    }

                    while (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        Input.KeyInputted(key);
                    }

                    Render(simulation);
                }
            });
        }
        string lastRendered = null;

        public TimeSpan BackBufferRenderTime { get; private set; }
        public TimeSpan ScreenRenderTime { get; private set; }
        public TimeSpan LastUpdateTime { get; private set; }
        public Input Input { get; }

        private void Render(Simulation simulation)
        {
            var ms1 = DateTime.UtcNow;
            var ms2 = DateTime.UtcNow;
            DateTime ms3;
            if (OptimizedForPerformance)
            {
                char[][] c = new char[Console.WindowHeight][];
                for (int i = 0; i < c.Length; i++)
                {
                    c[i] = new char[Console.WindowWidth];
                }
                var displays = simulation.Displays;
                CharMatrixStack cms = new CharMatrixStack(displays.Count + 1);
                CharMatrix cm = new CharMatrix(c, 0, 0, Console.WindowWidth, Console.WindowHeight);
                cms.Add(cm);
                foreach (var display in displays)
                {
                    cms.Add(display.GetCharMatrix());
                }
                var backBuffer = cms.ToString(Console.WindowWidth, Console.WindowHeight);
                ms2 = DateTime.Now;
                Console.SetCursorPosition(0, 0);
                if (!OptimizedForPerformance || ForceFullNextRender || lastRendered == null || lastRendered.Length != backBuffer.Length)
                {
                    Console.CursorVisible = false;
                    Console.Write(backBuffer);
                    ForceFullNextRender = false;
                }
                else
                {
                    var backLines = backBuffer.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    var lastRenderedLines = lastRendered.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    for (int y = 0; y < backLines.Length; y++)
                    {
                        if (backLines[y] != lastRenderedLines[y])
                        {
                            for (int x = 0; x < backLines[y].Length; x++)
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
                lastRendered = backBuffer;

            }
            else
            {
                Console.SetCursorPosition(0, 0);
                for (int i = 0; i < Console.WindowHeight; i++)
                {
                    Console.WriteLine("".PadRight(Console.WindowWidth));
                }
                foreach (var display in simulation.Displays)
                {
                    var t = display.GetStringToDisplay(false).Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    int row = 0;
                    for (int y = display.GetY(); y < display.GetY() + display.GetHeight(); y++)
                    {
                        if (row < t.Length)
                        {
                            Console.SetCursorPosition(display.GetX(), y);
                            Console.Write(t[row]);
                        }
                        row++;
                    }
                }
            
            }

            ms3 = DateTime.UtcNow;
            BackBufferRenderTime = ms2 - ms1;
            ScreenRenderTime = ms3 - ms2;
        }
    }
}
