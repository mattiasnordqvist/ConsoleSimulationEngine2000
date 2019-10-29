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
        private (CharMatrix clean, int w, int h)? _cleanCache;

        public int TargetRenderTime { get; set; } = 50;
        public int TargetUpdateTime { get; set; } = 200;

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
                int delta = 0, updateDelta = 0;
                var s1 = DateTime.UtcNow;
                simulation.Update(delta);

                while (true)
                {
                    delta = (DateTime.UtcNow - s1).Milliseconds;
                    updateDelta += delta;
                    s1 = DateTime.UtcNow;
                    while (Input != null && Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        Input.KeyInputted(key);
                    }
                    if (updateDelta >= TargetUpdateTime)
                    {
                        var u1 = DateTime.UtcNow;
                        LastUpdateDelta = updateDelta;
                        simulation.Update(updateDelta);
                        LastUpdateTime = u1 - DateTime.UtcNow;
                        updateDelta -= TargetUpdateTime;
                    }
                    Render(simulation);
                    await Task.Delay(Math.Max(0, TargetRenderTime - delta));
                }
            });
        }

        public TimeSpan BackBufferRenderTime { get; private set; }
        public TimeSpan ScreenRenderTime { get; private set; }
        public TimeSpan LastUpdateTime { get; private set; }
        public int LastUpdateDelta { get; private set; }
        public IInput Input { get; set; }

        private void Render(Simulation simulation)
        {
            var ms1 = DateTime.UtcNow;

            CharMatrix cm = Clean(Console.WindowWidth, Console.WindowHeight);


            var displays = simulation.Displays;
            CharMatrixStack cms = new CharMatrixStack(displays.Count + 1);
            cms.Add(cm);
            foreach (var display in displays)
            {
                cms.Add(display.GetCharMatrix());
            }

            var s = cms.ToString(Console.WindowWidth, Console.WindowHeight);
            var ms2 = DateTime.UtcNow;

            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            Console.Out.Write(s);
            var ms3 = DateTime.UtcNow;

            BackBufferRenderTime = ms2 - ms1;
            ScreenRenderTime = ms3 - ms2;
        }

        private CharMatrix Clean(int w, int h)
        {
            if (_cleanCache == null || _cleanCache.Value.w != w || _cleanCache.Value.h != h)
            {
                (char c, string pre)[][] c = new (char, string)[h][];
                for (int i = 0; i < c.Length; i++)
                {
                    c[i] = new (char, string)[w];
                }
                _cleanCache = (new CharMatrix(c, 0, 0, w, h), w, h);
            }
            return _cleanCache.Value.clean;
        }

        public DebugDisplay CreateDisplay(int x, int y)
        {
            return new DebugDisplay(this, x, y);
        }
    }
}
