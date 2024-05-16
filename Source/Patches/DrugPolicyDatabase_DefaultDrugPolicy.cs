using HarmonyLib;
using RimWorld;

namespace SetDefaultPolicy.Patches
{
    [HarmonyPatch(typeof(DrugPolicyDatabase), nameof(DrugPolicyDatabase.DefaultDrugPolicy))]
    public class DrugPolicyDatabase_DefaultDrugPolicy
    {
        public static void Postfix(ref DrugPolicy __result, DrugPolicyDatabase __instance)
        {
            DrugPolicy defaultPolicy = __instance.AllPolicies.Find((DrugPolicy compare) => compare.id == DataStorage.DefaultPolicyIdDrug);
            if (defaultPolicy != null) { __result = defaultPolicy; }
        }
    }
}
