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
        private const float BUTTON_WIDTH = 128f;
        private const float BUTTON_HEIGHT = 32f;

        public static void Postfix(Rect inRect, Policy ___policyInt, Dialog_ManagePolicies<Policy> __instance)
        {
            if (___policyInt == null) { return; }

            MethodInfo GetDefaultPolicy = __instance.GetType().GetMethod("GetDefaultPolicy", BindingFlags.NonPublic | BindingFlags.Instance);
            Policy defaultPolicy = (Policy)GetDefaultPolicy.Invoke(__instance, null);

            if (___policyInt.id == defaultPolicy.id) { return; }

            // x accounts for button being left of 3 other buttons 32f wide w/ 10f gaps
            // y accounts for button being below 2 lines of text 32f tall, plus a 10f gap
            if (Widgets.ButtonText(new Rect(inRect.xMax - (32f * 3 + 10f * 3 + BUTTON_WIDTH), 32f * 2 + 10f, BUTTON_WIDTH, BUTTON_HEIGHT), "SetDefaultPolicy.SetDefaultButton".Translate()))
            {
                SetDefault(___policyInt);
            }
        }

        private static void SetDefault(Policy newDefault)
        {
            switch (newDefault.GetType().ToString())
            {
                case "RimWorld.ApparelPolicy":
                    DataStorage.DefaultPolicyIdOutfit = newDefault.id;
                    break;
                case "RimWorld.FoodPolicy":
                    DataStorage.DefaultPolicyIdFood = newDefault.id;
                    break;
                case "RimWorld.DrugPolicy":
                    DataStorage.DefaultPolicyIdDrug = newDefault.id;
                    break;
                case "RimWorld.ReadingPolicy":
                    DataStorage.DefaultPolicyIdReading = newDefault.id;
                    break;
                default:
                    break;
            }
        }
    }
}
