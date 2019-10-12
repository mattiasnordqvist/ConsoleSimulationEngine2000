# ConsoleSimulationEngine2000

## Install

Available at Nuget: https://www.nuget.org/packages/ConsoleSimulationEngine2000

## Use

Create a .Net Core 3 Console App. Smack this into your Main method:

```csharp
    var gui = new ConsoleGUI();
    var sim = new MySimulation();
    await gui.Start(sim);
```

`MySimulation` is a type inheriting from `ConsoleSimulationEngine2000.Simulation`. It could look like this:

```csharp
    public class MySimulation : Simulation
    {
        private RollingDisplay log = new RollingDisplay(0, 0, -1, 12); 
        private Display clockDisplay = new Display(0, 11, 20, 3) { TextColor = Color.DarkOrange };
        private DateTime dateTime = DateTime.Now;
        public override IEnumerable<Display> Displays => new List<Display>() {  log, clockDisplay };

        public override void PassTime(int deltaTime)
        {
            dateTime.AddMilliseconds(deltaTime);
            log.Log("A second has passed");
            clockDisplay.Value = dateTime.ToString("HH:mm");
        }
    }
```
