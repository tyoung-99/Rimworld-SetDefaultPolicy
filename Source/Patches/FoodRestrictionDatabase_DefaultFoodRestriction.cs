using HarmonyLib;
using RimWorld;

namespace SetDefaultPolicy.Patches
{
    [HarmonyPatch(typeof(FoodRestrictionDatabase), nameof(FoodRestrictionDatabase.DefaultFoodRestriction))]
    public class FoodRestrictionDatabase_DefaultFoodRestriction
    {
        public static void Postfix(ref FoodPolicy __result, FoodRestrictionDatabase __instance)
        {
            FoodPolicy defaultPolicy = __instance.AllFoodRestrictions.Find((FoodPolicy compare) => compare.id == DataStorage.DefaultPolicyIdFood);
            if (defaultPolicy != null) { __result = defaultPolicy; }
        }
    }
}
