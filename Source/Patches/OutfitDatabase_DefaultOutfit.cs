using HarmonyLib;
using RimWorld;

namespace SetDefaultPolicy.Patches
{
    [HarmonyPatch(typeof(OutfitDatabase), nameof(OutfitDatabase.DefaultOutfit))]
    public class OutfitDatabase_DefaultOutfit
    {
        public static void Postfix(ref ApparelPolicy __result, OutfitDatabase __instance)
        {
            ApparelPolicy defaultPolicy = __instance.AllOutfits.Find((ApparelPolicy compare) => compare.id == DataStorage.DefaultPolicyIdOutfit);
            if (defaultPolicy != null) { __result = defaultPolicy; }
        }
    }
}
