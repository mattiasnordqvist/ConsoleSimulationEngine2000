# ConsoleSimulationEngine2000

## Install

Available at Nuget: https://www.nuget.org/packages/ConsoleSimulationEngine2000

## Use

Create a .Net Core 3 Console App. Smack this into your Main method:

```csharp
var gui = new ConsoleGUI()
{
    TargetUpdateTime = 100
};
var sim = new MySimulation();
await gui.Start(sim);
```

`MySimulation` is a type inheriting from `ConsoleSimulationEngine2000.Simulation`. It could look like this:

```csharp
public class MySimulation : Simulation
{
    private RollingDisplay log = new RollingDisplay(0, 0, -1, 12);
    private BorderedDisplay clockDisplay = new BorderedDisplay(0, 11, 20, 3) { };
    public override List<BaseDisplay> Displays => new List<BaseDisplay>() { log, clockDisplay, Input.CreateDisplay(0, -3, -1, 3) };

    public override void PassTime(int deltaTime)
    {
        log.Log($"{deltaTime} milliseconds has passed");
        clockDisplay.Value = DateTime.Now.ToString("HH:mm:ss");
        while (Input.HasInput)
        {
            log.Log("Input: " + Input.Consume());
        }
    }
}
```
`Display`s are used to render things on screen. All `Display`s listed in the `Displays` property will be rendered every second.
`Input` (from base class) can be used to query input. It also has a list of words available for auto-completion.
