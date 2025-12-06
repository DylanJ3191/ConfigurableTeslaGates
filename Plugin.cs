namespace ConfigurableTeslaGates;

using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;

public class Plugin : Plugin<Config>
{
	public static Plugin Main { get; set; } = null;

	public override string Name { get; } = "Configurable Tesla Gates";

	public override string Author { get; } = "DylanJ3191";

	public override string Description { get; } = "Config options for Tesla Gates";

	public override Version Version { get; } = new Version(1,0,1);

	public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);

	public override LoadPriority Priority { get; } = LoadPriority.High;

	public EventHandlers Events { get; } = new();

	public override void Enable()
	{
		Main = this;
        CustomHandlersManager.RegisterEventsHandler(Events);
    }

    public override void Disable()
	{
		Main = null;
		CustomHandlersManager.UnregisterEventsHandler(Events);
	}

}
