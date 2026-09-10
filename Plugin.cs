#pragma warning disable CS0162 
// ReSharper disable HeuristicUnreachableCode
// ReSharper disable ConditionIsAlwaysTrueOrFalse
using JetBrains.Annotations;

namespace ConfigurableTeslaGates;

using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using System;
using LabApi.Features.Console;

[UsedImplicitly]
public class Plugin : Plugin<Config>
{
	public static Plugin Main { get; private set; }

	public override string Name { get; } = "Configurable Tesla Gates";

	public override string Author { get; } = "NameDuckling770";

	public override string Description { get; } = "Config options for Tesla Gates";

	public override Version Version { get; } = new Version(2, 2,0);

	public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);

	public override LoadPriority Priority { get; } = LoadPriority.High;

	private EventHandlers Events { get; } = new();

	public static string[] ImmunePlayers = Array.Empty<string>();

	public override void Enable()
	{
		Main = this;
		if (this.Config.AprilFoolsModeEnabled)
			Logger.Debug("ConfigurableTeslaGates - April Fools mode is enabled.");
		if (this.Config.ResetConfig)
			ResetConfig();
		CustomHandlersManager.RegisterEventsHandler(Events);
	}

	public override void Disable()
	{
		Main = null;
		CustomHandlersManager.UnregisterEventsHandler(Events);
    }

	internal void ReloadConfig()
	{
		LoadConfigs();
    }

	internal void ResetConfig()
	{
		// throw new NotImplementedException("This feature is WIP.");
		if (this.Config is null)
		{
			this.Disable();
			throw new System.NullReferenceException("Can't find config for reset.");
		}

		Logger.Warn("[TGI] Resetting config.");
		this.Config.AprilFoolsModeEnabled = false;
		this.Config.AllowConfigEditing = true;
		this.Config.ClearImmunityOnRestart = false;
		this.Config.GatesEnabled = true;
		this.Config.RoleList = "Tutorial NtfPrivate NtfSpecialist NtfSergeant NtfCaptain";
		this.Config.TgiCommandEnabled = true;
		this.Config.ResetConfig = false;
		Logger.Warn("[TGI] Config reset complete.");
		this.ReloadConfig();
	}
}