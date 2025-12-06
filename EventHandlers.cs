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

        if (args.Player.Role == PlayerRoles.RoleTypeId.Tutorial && Plugin.Main.Config.Tutorial == true)
            args.IsAllowed = false;

        if (args.Player.Role == PlayerRoles.RoleTypeId.ClassD && Plugin.Main.Config.ClassD == true)
            args.IsAllowed = false;

        if (args.Player.Role == PlayerRoles.RoleTypeId.Scientist && Plugin.Main.Config.Scientist == true)
            args.IsAllowed = false;

        if (args.Player.Role == PlayerRoles.RoleTypeId.Scp049 && Plugin.Main.Config.Scp049 == true)
            args.IsAllowed = false;

        if (args.Player.Role == PlayerRoles.RoleTypeId.Scp0492 && Plugin.Main.Config.Scp0492 == true)
            args.IsAllowed = false;

        if (args.Player.Role == PlayerRoles.RoleTypeId.Scp096 && Plugin.Main.Config.Scp096 == true)
            args.IsAllowed = false;

        if (args.Player.Role == PlayerRoles.RoleTypeId.Scp106 && Plugin.Main.Config.Scp106 == true)
            args.IsAllowed = false;

        if (args.Player.Role == PlayerRoles.RoleTypeId.Scp173 && Plugin.Main.Config.Scp173 == true)
            args.IsAllowed = false;

        if (args.Player.Role == PlayerRoles.RoleTypeId.Scp939 && Plugin.Main.Config.Scp939 == true)
            args.IsAllowed = false;

        if (args.Player.Role == PlayerRoles.RoleTypeId.Scp3114 && Plugin.Main.Config.Scp3114 == true)
            args.IsAllowed = false;

        if ((args.Player.Role == PlayerRoles.RoleTypeId.Flamingo || args.Player.Role == PlayerRoles.RoleTypeId.AlphaFlamingo || args.Player.Role == PlayerRoles.RoleTypeId.ZombieFlamingo) && Plugin.Main.Config.Flamingos == true)
            args.IsAllowed = false;

        if ((args.Player.Role == PlayerRoles.RoleTypeId.ChaosConscript || args.Player.Role == PlayerRoles.RoleTypeId.ChaosMarauder || args.Player.Role == PlayerRoles.RoleTypeId.ChaosRepressor || args.Player.Role == PlayerRoles.RoleTypeId.ChaosRifleman) && Plugin.Main.Config.ChaosInsurgency == true)
            args.IsAllowed = false;

        if ((args.Player.Role == PlayerRoles.RoleTypeId.NtfPrivate || args.Player.Role == PlayerRoles.RoleTypeId.NtfSpecialist || args.Player.Role == PlayerRoles.RoleTypeId.NtfSergeant || args.Player.Role == PlayerRoles.RoleTypeId.NtfCaptain) && Plugin.Main.Config.MobileTaskForces == true)
            args.IsAllowed = false;

        if (args.Player.Role == PlayerRoles.RoleTypeId.FacilityGuard && Plugin.Main.Config.FacilityGuard == true)
            args.IsAllowed = false;
    }
}