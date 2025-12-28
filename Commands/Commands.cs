namespace ConfigurableTeslaGates.Commands;

using System;
using System.Linq;
using LabApi.Features.Wrappers;
using LabApi.Features.Console;
using CommandSystem;
using NorthwoodLib.Pools;

[CommandHandler(typeof(ClientCommandHandler))]
[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class ImmunityCommand : ICommand
{
    public string Command => "tgimmunity";
    public string[] Aliases => new[] { "tgi", "teslagateimmunity", "donttriggertg" };
    public string Description => "Toggles Tesla Gate immunity for yourself or another player.";

    public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response)
    {
        if (!Plugin.Main.Config.TgiCommandEnabled)
        {
            response = "This command is disabled.";
            return false;
        }

        if (!sender.CheckPermission(PlayerPermissions.FacilityManagement))
        {
            response = "You do not have the permission required to use this command. (FacilityManagement)";
            return false;
        }

        Player target;

        // No args → toggle on yourself
        if (args.Count == 0)
        {
            target = Player.Get(sender);

            if (target is null)
            {
                response = "This command can only be used by players.";
                return false;
            }
        }
        else
        {
            // Given in-game ID
            if (!int.TryParse(args.At(0), out int targetId))
            {
                response = "Invalid player ID. Use a number like: tgi 14";
                return false;
            }

            target = Player.Get(targetId);

            if (target is null)
            {
                response = $"No player exists with ID {targetId}.";
                return false;
            }
        }

        var plr = target.UserId;

        if (Array.Exists(Plugin.ImmunePlayers, element => element == plr))
        {
            Plugin.ImmunePlayers = Array.FindAll(Plugin.ImmunePlayers, element => element != plr);
            response = (target == Player.Get(sender))
                ? "You are no longer immune to triggering Tesla Gates."
                : $"{target.Nickname} is no longer immune to triggering Tesla Gates.";
            return true;
        }

        Array.Resize(ref Plugin.ImmunePlayers, Plugin.ImmunePlayers.Length + 1);
        Plugin.ImmunePlayers[^1] = plr;

        response = (target == Player.Get(sender))
            ? "You are now immune to triggering Tesla Gates."
            : $"{target.Nickname} is now immune to triggering Tesla Gates.";
        return true;
    }
}

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class ConfigurableTeslaGatesParentCmd : ParentCommand
{
    public override string Command { get; } = "ctg";

    public override string[] Aliases { get; } = Array.Empty<string>();

    public override string Description { get; } = "Parent command for Configurable Tesla Gates.";

    public override void LoadGeneratedCommands()
    {
        RegisterCommand(new ReloadConfig());
        RegisterCommand(new ClearImmunePlayers());
        RegisterCommand(new GetConfig());
    }
    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = "Valid subcommands:\n - reloadcfg\n - clearimmuneplayers\n - getcfg\n - editcfg";
        return false;
    }
}

[CommandHandler(typeof(ConfigurableTeslaGatesParentCmd))]
public class ReloadConfig : ICommand
{
    public string Command { get; } = "reloadcfg";

    public string[] Aliases { get; } = new[] { "reloadconfig", "rc" };

    public string Description { get; } = "Reloads the config for Configurable Tesla Gates.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission(PlayerPermissions.ServerConfigs))
        {
            response = "You do not have the permission required to use this command. (ServerConfigs)";
            return false;
        }

        Logger.Info("[Configurable Tesla Gates]: Reloading config.");
        try
        {
            Plugin.Main.ReloadConfig();
            Logger.Info("[Configurable Tesla Gates]: Config reloaded successfully.");
            response = "Config reloaded successfully.";
            return true;
        }
        catch (Exception ex)
        {
            Logger.Error($"[Configurable Tesla Gates]: Failed to reload config: {ex.Message}");
            response = $"Failed to reload config: {ex.Message}";
            return false;
        }
    }
}

[CommandHandler(typeof(ConfigurableTeslaGatesParentCmd))]
public class ClearImmunePlayers : ICommand
{
    public string Command { get; } = "clearimmuneplayers";

    public string Description { get; } = "Clears the list of players immune to Tesla Gates.\n Note: This does not affect role immunity.";

    public string[] Aliases { get; } = new[] { "cip" };

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!Plugin.Main.Config.TgiCommandEnabled)
        {
            response = "This command is disabled.";
            return false;
        }

        if (!sender.CheckPermission(PlayerPermissions.FacilityManagement))
        {
            response = "You do not have the permission required to use this command. (FacilityManagement)";
            return false;
        }
        Plugin.ImmunePlayers = Array.Empty<string>();
        Logger.Info("[Configurable Tesla Gates]: Cleared the list of players immune to Tesla Gates.");
        response = "Cleared the list of players immune to Tesla Gates.";
        return true;
    }
}

[CommandHandler(typeof(ConfigurableTeslaGatesParentCmd))]
public class GetConfig : ICommand
{
    public string Command { get; } = "getcfg";

    public string Description { get; } = "Displays the current configuration.";

    public string[] Aliases { get; } = new[] { "cfg" };

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission(PlayerPermissions.ServerConfigs))
        {
            response = "You do not have the permission required to use this command. (ServerConfigs)";
            return false;
        }

        if (Plugin.Main.Config is null)
        {
            Logger.Error("Config not found.");
            response = "Config not found.";
            return false;
        }
        var config = Plugin.Main.Config;
        response = $"Current config for Configurable Tesla Gates:\n" +
                   $"- GatesEnabled: {config.GatesEnabled}\n" +
                   $"- RoleList: {string.Join(" ", config.RoleList)}\n" +
                   $"- tgiCommandEnabled: {config.TgiCommandEnabled}\n" +
                   $"- clearImmunityOnRestart: {config.ClearImmunityOnRestart}\n" +
                   $"- allowConfigEditing: {config.AllowConfigEditing}";
        return true;
    }
}

[CommandHandler(typeof(ConfigurableTeslaGatesParentCmd))]
public class EditConfig : ICommand
{
    public string Command { get; } = "editcfg";

    public string Description { get; } = "Edits a configuration option. Usage: editcfg <option> <value>";

    public string[] Aliases { get; } = new[] { "editconfig" };

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!Plugin.Main.Config.AllowConfigEditing)
        {
            response = "Editing the config via command is disabled.";
            return false;
        }
        if (!sender.CheckPermission(PlayerPermissions.ServerConfigs) || !sender.CheckPermission(PlayerPermissions.ServerConsoleCommands))
        {
            response = "You do not have the permission(s) required to use this command. (ServerConfigs, ServerConsoleCommands)";
            return false;
        }
        if (arguments.Count < 2)
        {
            response = "Usage: editcfg <option> <value>\n Run \"ctg cfg\" to get the list of options.";
            return false;
        }
        var option = arguments.At(0).ToLower();
        // For rolelist allow the value to contain spaces by joining all remaining arguments.
        string value;
        if (option == "rolelist")
        {
            if (arguments.Count < 2)
            {
                response = "Usage: ctg editcfg rolelist <roles...> \n Warning: Config is CASE SENSITIVE!";
                return false;
            }
            var parts = new string[arguments.Count - 1];
            for (int i = 1; i < arguments.Count; i++)
                parts[i - 1] = arguments.At(i);
            value = string.Join(' ', parts);
        }
        else
        {
            value = arguments.At(1);
        }
        try
        {  
            if (Plugin.Main.Config is null)
            {
                Logger.Error("Config not found.");
                response = "Config not found.";
                return false;
            }
            var config = Plugin.Main.Config;
            switch (option)
            {
                case "gatesenabled":
                    config.GatesEnabled = bool.Parse(value);
                    break;
                case "rolelist":
                    config.RoleList = value;
                    break;
                case "tgicommandenabled":
                    config.TgiCommandEnabled = bool.Parse(value);
                    break;
                case "clearimmunityonrestart":
                    config.ClearImmunityOnRestart = bool.Parse(value);
                    break;
                case "allowconfigediting":
                    config.AllowConfigEditing = bool.Parse(value);
                    break;
                default:
                    response = $"Unknown configuration option: {option}";
                    return false;
            }
            Plugin.Main.SaveConfig();
            response = $"Configuration option '{option}' updated to '{value}'.";
            return true;
        }
        catch (Exception ex)
        {
            response = $"Error updating configuration: {ex.Message}";
            return false;
        }
    }
}