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
            Console.WindowWidth = width;
            Console.WindowHeight = height;
            Console.CursorVisible = false;
        }

        private bool started;
        public int TargetRenderTime { get; set; } = 20;
        public int TargetUpdateTime { get; set; } = 1000;

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
                        simulation.Update(delta);
                        LastUpdateTime = (u1 - DateTime.UtcNow);
                        delta = 0;
                    }
                    
                    while (Input != null && Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        Input.KeyInputted(key);
                    }

                    Render(simulation);
                }
            });
        }

        public TimeSpan BackBufferRenderTime { get; private set; }
        public TimeSpan ScreenRenderTime { get; private set; }
        public TimeSpan LastUpdateTime { get; private set; }
        public IInput Input { get; set; }

        private void Render(Simulation simulation)
        {
            var ms1 = DateTime.UtcNow;
            (char c, string pre)[][] c = new (char, string)[Console.WindowHeight][];
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = new (char, string)[Console.WindowWidth];
            }
            var displays = simulation.Displays;
            CharMatrixStack cms = new CharMatrixStack(displays.Count + 1);
            CharMatrix cm = new CharMatrix(c, 0, 0, Console.WindowWidth, Console.WindowHeight);
            cms.Add(cm);
            foreach (var display in displays)
            {
                cms.Add(display.GetCharMatrix());
            }

            var ms2 = DateTime.UtcNow;

            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            Console.Write(cms.ToString(Console.WindowWidth, Console.WindowHeight));
            var ms3 = DateTime.UtcNow;

            BackBufferRenderTime = ms2 - ms1;
            ScreenRenderTime = ms3 - ms2;
        }

        public DebugDisplay CreateDisplay(int x, int y)
        {
            return new DebugDisplay(this, x, y, 20, 5);
        }
    }
}
