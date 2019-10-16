# ConsoleSimulationEngine2000

## Install

Available at Nuget: https://www.nuget.org/packages/ConsoleSimulationEngine2000

## Use

Create a .Net Core 3 Console App. Smack this into your Main method:

```csharp
var gui = new ConsoleGUI();
var sim = new MySimulation(gui);
await gui.Start(sim);
```

`MySimulation` is a type inheriting from `ConsoleSimulationEngine2000.Simulation`. It could look like this:

```csharp
public class MySimulation : Simulation
{
    private RollingDisplay log = new RollingDisplay(0, 0, -1, 12);
    private BorderedDisplay clockDisplay = new BorderedDisplay(0, 11, 20, 3) { };
    private BorderedDisplay updateDisplay = new BorderedDisplay(0, 14, 20, 3) { };
    private BorderedDisplay renderDisplay = new BorderedDisplay(0, 17, 20, 3) { };
    private BorderedDisplay printDisplay = new BorderedDisplay(0, 20, 20, 3) { };
    private readonly ConsoleGUI gui;
    private readonly Input input;

    public override List<BaseDisplay> Displays => new List<BaseDisplay>() {
        log, 
        clockDisplay,
        updateDisplay,
        renderDisplay,
        printDisplay,
        input.CreateDisplay(0, -3, -1) };

    public MySimulation(ConsoleGUI gui)
    {
        this.gui = gui;
        this.input = gui.Input;
    }
    public override void PassTime(int deltaTime)
    {
        log.Log($"{deltaTime} milliseconds has passed");
        clockDisplay.Value = DateTime.Now.ToString("HH:mm:ss");
        updateDisplay.Value = "Update: "+gui.LastUpdateTime.Milliseconds;
        renderDisplay.Value = "Render: "+gui.BackBufferRenderTime.Milliseconds;
        printDisplay.Value = "Print: "+gui.ScreenRenderTime.Milliseconds;
        while (input.HasInput)
        {
            log.Log("Input: " + input.Consume());
        }
    }
}
```
`Display`s are used to render things on screen. All `Display`s listed in the `Displays` property will be rendered every second.
`Input` (from base class) can be used to query input. It also has a list of words available for auto-completion.
