using System.ComponentModel;
using System.Data;

namespace ConfigurableTeslaGates;

public class Config
{
    [Description("Are Tesla Gates enabled?")]
    public bool GatesEnabled { get; set; } = true;

    [Description("Set roles immune to triggering Tesla Gates. ")]
    public string RoleList { get; set; } = "Tutorial NtfCadet NtfSpecialist NtfSergeant NtfCaptain";
}