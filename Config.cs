using System.ComponentModel;

namespace ConfigurableTeslaGates;

public class Config
{
    [Description("Are Tesla Gates enabled?")]
    public bool GatesEnabled { get; set; } = true;

    [Description("Should Tutorial be ignored by Tesla Gates?")]
    public bool Tutorial { get; set; } = true;

    [Description("Should Class-Ds be ignored by Tesla Gates?")]
    public bool ClassD { get; set; } = false;

    [Description("Should Scientists be ignored by Tesla Gates?")]
    public bool Scientist { get; set; } = false;

    [Description("Should SCP-049 be ignored by Tesla Gates?")]
    public bool Scp049 { get; set; } = false;

    [Description("Should SCP-049-2 be ignored by Tesla Gates?")]
    public bool Scp0492 { get; set; } = false;

    [Description("Should SCP-096 be ignored by Tesla Gates?")]
    public bool Scp096 { get; set; } = false;

    [Description("Should SCP-106 be ignored by Tesla Gates?")]
    public bool Scp106 { get; set; } = false;

    [Description("Should SCP-173 be ignored by Tesla Gates?")]
    public bool Scp173 { get; set; } = false;

    [Description("Should SCP-939 be ignored by Tesla Gates?")]
    public bool Scp939 { get; set; } = false;

    [Description("Should SCP-3114 be ignored by Tesla Gates?")]
    public bool Scp3114 { get; set; } = false;

    [Description("Should the flamingos be ignored by Tesla Gates?")]
    public bool Flamingos { get; set; } = false;

    [Description("Should the Chaos Insurgency be ignored by Tesla Gates?")]
    public bool ChaosInsurgency { get; set; } = false;

    [Description("Should the Mobile Task Forces be ignored by Tesla Gates?")]
    public bool MobileTaskForces { get; set; } = true;

    [Description("Should Guards be ignored by Tesla Gates?")]
    public bool FacilityGuard { get; set; } = false;
}