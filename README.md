# ConsoleSimulationEngine2000
"Live" update of console window

## Install

Available at Nuget: https://www.nuget.org/packages/ConsoleSimulationEngine2000

## Use

Create a .Net Core 3 Console App. Smack this into your Main method:

```csharp
var input = new TextInput();
var gui = new ConsoleGUI() { Input = input };
var sim = new MySimulation(input, gui.CreateDisplay(4, 4));
await gui.Start(sim);
```

`MySimulation` is a type inheriting from `ConsoleSimulationEngine2000.Simulation`. It could look like this:

```csharp
    public class MySimulation : Simulation
    {
        private RollingDisplay log = new RollingDisplay(0, 0, -1, 13);
        private BorderedDisplay clockDisplay = new BorderedDisplay(0, 12, 20, 3) { };
        private readonly TextInput input;
        private readonly DebugDisplay debug;

        public override List<IDisplay> Displays => new List<IDisplay>() { log, debug, input.CreateDisplay(0, -3, -1) };
        public MySimulation(TextInput input, DebugDisplay debug)
        {
            this.input = input;
            this.debug = debug;
        }
        public override void Update(int deltaTime)
        {
            log.LogSingleLine($"{deltaTime} milliseconds has passed and everything is fine!".Pastel(Color.MediumVioletRed));
            clockDisplay.Value = DateTime.Now.ToString("HH:mm:ss");
            while (input.HasInput)
            {
                log.LogSingleLine("Input: " + input.Consume());
            }
        }
    }
```
`Display`s are used to render things on screen. All `Display`s listed in the `Displays` property will be rendered every second.
`Input` (from base class) can be used to query input. It also has a list of words available for auto-completion.
