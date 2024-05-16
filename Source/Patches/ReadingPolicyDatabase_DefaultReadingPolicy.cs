using HarmonyLib;
using RimWorld;

namespace SetDefaultPolicy.Patches
{
    [HarmonyPatch(typeof(ReadingPolicyDatabase), nameof(ReadingPolicyDatabase.DefaultReadingPolicy))]
    public class ReadingPolicyDatabase_DefaultReadingPolicy
    {
        public static void Postfix(ref ReadingPolicy __result, ReadingPolicyDatabase __instance)
        {
            ReadingPolicy defaultPolicy = __instance.AllReadingPolicies.Find((ReadingPolicy compare) => compare.id == DataStorage.DefaultPolicyIdReading);
            if (defaultPolicy != null) { __result = defaultPolicy; }
        }
    }
}
