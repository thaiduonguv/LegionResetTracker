using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;

public class LegionResetTrackerSettings : ISettings
{
    public ToggleNode Enable { get; set; } = new ToggleNode(true);
}
