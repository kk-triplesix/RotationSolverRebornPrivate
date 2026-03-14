using ECommons.DalamudServices;
using Newtonsoft.Json;

namespace RotationSolver.RotationPlanner;

/// <summary>
/// Handles persistence of rotation plans as JSON files
/// </summary>
internal static class RotationPlanStorage
{
    private static string PlanDirectory => Path.Combine(
        Svc.PluginInterface.ConfigDirectory.FullName, "RotationPlans");

    private static string GetFileName(uint territoryId, string job, string planName)
    {
        var safeName = string.Join("_", planName.Split(Path.GetInvalidFileNameChars()));
        return $"{territoryId}_{job}_{safeName}.json";
    }

    private static string GetFilePath(uint territoryId, string job, string planName)
        => Path.Combine(PlanDirectory, GetFileName(territoryId, job, planName));

    public static void Save(RotationPlan plan)
    {
        plan.ModifiedAt = DateTime.Now;
        var dir = PlanDirectory;
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        var json = JsonConvert.SerializeObject(plan, Formatting.Indented);
        File.WriteAllText(GetFilePath(plan.TerritoryId, plan.Job, plan.Name), json);
    }

    public static RotationPlan? Load(uint territoryId, string job, string planName)
    {
        var path = GetFilePath(territoryId, job, planName);
        if (!File.Exists(path))
            return null;

        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<RotationPlan>(json);
    }

    public static List<string> ListPlans(uint? territoryId = null, string? job = null)
    {
        var dir = PlanDirectory;
        if (!Directory.Exists(dir))
            return [];

        var plans = new List<string>();
        foreach (var file in Directory.GetFiles(dir, "*.json"))
        {
            try
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var parts = fileName.Split('_', 3);
                if (parts.Length < 3) continue;

                if (territoryId.HasValue && parts[0] != territoryId.Value.ToString()) continue;
                if (job != null && parts[1] != job) continue;

                // Load just to get the plan name
                var json = File.ReadAllText(file);
                var plan = JsonConvert.DeserializeObject<RotationPlan>(json);
                if (plan != null)
                    plans.Add(plan.Name);
            }
            catch
            {
                // Skip invalid files
            }
        }
        return plans;
    }

    public static List<RotationPlan> LoadAll(uint? territoryId = null, string? job = null)
    {
        var dir = PlanDirectory;
        if (!Directory.Exists(dir))
            return [];

        var plans = new List<RotationPlan>();
        foreach (var file in Directory.GetFiles(dir, "*.json"))
        {
            try
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var parts = fileName.Split('_', 3);
                if (parts.Length < 3) continue;

                if (territoryId.HasValue && parts[0] != territoryId.Value.ToString()) continue;
                if (job != null && parts[1] != job) continue;

                var json = File.ReadAllText(file);
                var plan = JsonConvert.DeserializeObject<RotationPlan>(json);
                if (plan != null)
                    plans.Add(plan);
            }
            catch
            {
                // Skip invalid files
            }
        }
        return plans;
    }

    public static bool Delete(uint territoryId, string job, string planName)
    {
        var path = GetFilePath(territoryId, job, planName);
        if (!File.Exists(path))
            return false;

        File.Delete(path);
        return true;
    }
}
