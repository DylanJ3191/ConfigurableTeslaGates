using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace ConfigurableTeslaGates;

public class EventHandlers : CustomEventsHandler
{
    public override void OnPlayerTriggeringTesla(PlayerTriggeringTeslaEventArgs args)
    {
        if (Plugin.Main.Config is null)
            return;

        if (Plugin.Main.Config.GatesEnabled == false)
            args.IsAllowed = false;

        if (Plugin.Main.Config.RoleList.Contains(args.Player.Role.ToString()))
            args.IsAllowed = false;
    }
}