namespace ConfigurableTeslaGates.Commands;

using System;
using LabApi.Features.Wrappers;
using CommandSystem;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(ClientCommandHandler))]
public class ImmunityCommand : ICommand
{
    public string Command => "tgimmunity";
    public string[] Aliases => new[] { "tgi", "teslagateimmunity", "donttriggertg" };
    public string Description => "Toggles Tesla Gate immunity for yourself or another player.";

    public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response)
    {
        // Config check
        if (Plugin.Main.Config.tgiCommandEnabled == false)
        {
            response = "This command is disabled.";
            return false;
        }

        if (!sender.CheckPermission(PlayerPermissions.FacilityManagement))
        {
            response = "You do not have permission to use this command.";
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