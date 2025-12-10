namespace ConfigurableTeslaGates;

using CommandSystem;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using System;

public class Plugin : Plugin<Config>
{
	public static Plugin Main { get; set; } = null;

	public override string Name { get; } = "Configurable Tesla Gates";

	public override string Author { get; } = "DylanJ3191";

	public override string Description { get; } = "Config options for Tesla Gates";

	public override Version Version { get; } = new Version(1, 2, 2);

	public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);

	public override LoadPriority Priority { get; } = LoadPriority.High;

	public EventHandlers Events { get; } = new();

	public static string[] ImmunePlayers = Array.Empty<string>();

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