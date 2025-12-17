using System;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace ConfigurableTeslaGates;

public class EventHandlers : CustomEventsHandler
{
    public override void OnPlayerIdlingTesla(PlayerIdlingTeslaEventArgs args)
    {
        if (Plugin.Main.Config is null)
            return;

        if (Plugin.Main.Config.GatesEnabled == false)
        {
            args.IsAllowed = false;
            return;
        }

        if (Array.Exists(Plugin.ImmunePlayers, element => element == args.Player.UserId))
        {
            args.IsAllowed = false;
            return;
        }

        var immuneRoles = Plugin.Main.Config.RoleList.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (Array.Exists(immuneRoles, r => r.Equals(args.Player.Role.ToString(), StringComparison.OrdinalIgnoreCase)))
            args.IsAllowed = false;
    }

    public override void OnPlayerTriggeringTesla(PlayerTriggeringTeslaEventArgs args)
    {
        if (Plugin.Main.Config is null)
            return;

        if (Plugin.Main.Config.GatesEnabled == false)
        {
            args.IsAllowed = false;
            return;
        }

        if (Array.Exists(Plugin.ImmunePlayers, element => element == args.Player.UserId))
        {
            args.IsAllowed = false;
            return;
        }

        var immuneRoles = Plugin.Main.Config.RoleList.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (Array.Exists(immuneRoles, r => r.Equals(args.Player.Role.ToString(), StringComparison.OrdinalIgnoreCase)))
            args.IsAllowed = false;
    }

    public override void OnServerWaitingForPlayers()
    {
        if (Plugin.Main.Config is null)
            return;

        if (Plugin.Main.Config.clearImmunityOnRestart)
        {
            Plugin.ImmunePlayers = Array.Empty<string>();
        }
    }
}