using System.ComponentModel;

namespace ConfigurableTeslaGates;

public class Config
{
    [Description("Are Tesla Gates enabled? Default: true")]
    public bool GatesEnabled { get; set; } = true;

    [Description("Set roles immune to triggering Tesla Gates. THIS IS CASE SENSITIVE! Default: \"Tutorial NtfPrivate NtfSpecialist NtfSergeant NtfCaptain\"")]
    public string RoleList { get; set; } = "Tutorial NtfPrivate NtfSpecialist NtfSergeant NtfCaptain";

    [Description("Enable the tgimmunity command? Default: true")]
    public bool TgiCommandEnabled { get; internal set; } = true;

    [Description("Clear Tesla Gate immunity on round restart? Default: false")]
    public bool ClearImmunityOnRestart { get; set; } = false;

    [Description("Allow editing config from RA/LA?")]
    public bool AllowConfigEditing { get; set; } = true;
}