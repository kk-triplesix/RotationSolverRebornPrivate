using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RotationSolver.ActionTimeline;

namespace RotationSolver.RotationPlanner;

/// <summary>
/// A single planned action at a specific fight timestamp
/// </summary>
public class PlannedAction
{
    [JsonProperty("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [JsonProperty("actionId")]
    public uint ActionId { get; set; }

    [JsonProperty("actionName")]
    public string ActionName { get; set; } = "";

    [JsonProperty("iconId")]
    public uint IconId { get; set; }

    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public TimelineItemType Type { get; set; }

    [JsonProperty("combatTime")]
    public float CombatTime { get; set; }

    [JsonProperty("duration")]
    public float Duration { get; set; }

    /// <summary>
    /// Cast time of this action in seconds (0 = instant).
    /// Used to show when to START pressing the button.
    /// </summary>
    [JsonProperty("castTime")]
    public float CastTime { get; set; }

    [JsonProperty("comment")]
    public string? Comment { get; set; }

    /// <summary>
    /// The combat time at which this action's effect resolves.
    /// For instant casts: same as CombatTime.
    /// For casted spells: CombatTime + CastTime.
    /// </summary>
    [JsonIgnore]
    public float EffectTime => CastTime > 0 ? CombatTime + CastTime : CombatTime;
}

/// <summary>
/// A boss mechanic marker on the timeline
/// </summary>
public class TimelineMechanic
{
    [JsonProperty("combatTime")]
    public float CombatTime { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = "";

    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public MechanicType Type { get; set; }

    [JsonProperty("duration")]
    public float Duration { get; set; }
}

/// <summary>
/// Type of boss mechanic
/// </summary>
public enum MechanicType
{
    Raidwide,
    Tankbuster,
    SharedStack,
    Knockback,
    Downtime,
    Positioning,
    Vulnerable,
    BossCast,
    Custom
}

/// <summary>
/// A phase within the fight
/// </summary>
public class PlanPhase
{
    [JsonProperty("name")]
    public string Name { get; set; } = "";

    [JsonProperty("startTime")]
    public float StartTime { get; set; }

    [JsonProperty("duration")]
    public float Duration { get; set; }
}

/// <summary>
/// Complete rotation plan for one encounter + one job
/// </summary>
public class RotationPlan
{
    [JsonProperty("name")]
    public string Name { get; set; } = "";

    [JsonProperty("territoryId")]
    public uint TerritoryId { get; set; }

    [JsonProperty("encounterOID")]
    public uint EncounterOID { get; set; }

    [JsonProperty("encounterName")]
    public string EncounterName { get; set; } = "";

    [JsonProperty("job")]
    public string Job { get; set; } = "";

    [JsonProperty("isActive")]
    public bool IsActive { get; set; }

    [JsonProperty("totalDuration")]
    public float TotalDuration { get; set; }

    /// <summary>
    /// Seconds before pull shown on timeline (default 5s for prepull actions)
    /// </summary>
    [JsonProperty("precastTime")]
    public float PrecastTime { get; set; } = 5f;

    [JsonProperty("actions")]
    public List<PlannedAction> Actions { get; set; } = [];

    [JsonProperty("mechanics")]
    public List<TimelineMechanic> Mechanics { get; set; } = [];

    [JsonProperty("phases")]
    public List<PlanPhase> Phases { get; set; } = [];

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonProperty("modifiedAt")]
    public DateTime ModifiedAt { get; set; } = DateTime.Now;
}
