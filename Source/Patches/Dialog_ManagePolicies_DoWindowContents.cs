using HarmonyLib;
using RimWorld;
using System.Reflection;
using UnityEngine;
using Verse;

namespace SetDefaultPolicy
{
    [HarmonyPatch(typeof(Dialog_ManagePolicies<Policy>), nameof(Dialog_ManagePolicies<Policy>.DoWindowContents))]
    public class Dialog_ManagePolicies_DoWindowContents
    {
        public static void Postfix(Rect inRect, Policy ___policyInt, Dialog_ManagePolicies<Policy> __instance)
        {
            if (___policyInt == null) { return; }

            // x accounts for button being left of 3 other identically-sized buttons w/ 10f gaps
            // y accounts for button being below 2 lines of text 32f tall, plus a 10f gap
            if (Widgets.ButtonImage(new Rect(inRect.xMax - (32f * 4 + 10f * 3), 32f * 2 + 10f, 32f, 32f), TexUI.DismissTex))
            {
                MethodInfo GetDefaultPolicy = __instance.GetType().GetMethod("GetDefaultPolicy", BindingFlags.NonPublic | BindingFlags.Instance);
                Policy defaultPolicy = (Policy)GetDefaultPolicy.Invoke(__instance, null);

                if (___policyInt.id == defaultPolicy.id) { Log.Message($"{___policyInt.label} already default"); return; }

                Log.Message($"Setting {___policyInt.label} as default");
                Log.Message($"Old: {defaultPolicy.label}");

                SetDefault(___policyInt);

                defaultPolicy = (Policy)GetDefaultPolicy.Invoke(__instance, null);
                Log.Message($"New: {defaultPolicy.label}");
            }
        }

        private static void SetDefault(Policy newDefault)
        {
            Log.Message(newDefault.GetType().ToString());
            switch (newDefault.GetType().ToString())
            {
                case "RimWorld.ApparelPolicy":
                    DataStorage.DefaultPolicyIdOutfit = newDefault.id;
                    Log.Message("Apparel");
                    break;
                case "RimWorld.FoodPolicy":
                    DataStorage.DefaultPolicyIdFood = newDefault.id;
                    Log.Message("Food");
                    break;
                case "RimWorld.DrugPolicy":
                    DataStorage.DefaultPolicyIdDrug = newDefault.id;
                    Log.Message("Drug");
                    break;
                case "RimWorld.ReadingPolicy":
                    DataStorage.DefaultPolicyIdReading = newDefault.id;
                    Log.Message("Reading");
                    break;
                default:
                    Log.Warning("Unknown policy type");
                    break;
            }
        }
    }
}
