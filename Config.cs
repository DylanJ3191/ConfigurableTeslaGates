using System.ComponentModel;

namespace ConfigurableTeslaGates;

public class Config
{
    [Description("Are Tesla Gates enabled? Default: true")]
    public bool GatesEnabled { get; set; } = true;

    [Description("Set roles immune to triggering Tesla Gates. Default: \"Tutorial NtfCadet NtfSpecialist NtfSergeant NtfCaptain\"")]
    public string RoleList { get; set; } = "Tutorial NtfCadet NtfSpecialist NtfSergeant NtfCaptain";

    [Description("Enable the teslagateimmunity command? Default: true")]
    public bool tgiCommandEnabled { get; set; } = true;
}