using HarmonyLib;
using RimWorld;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                MethodInfo GetPolicies = __instance.GetType().GetMethod("GetPolicies", BindingFlags.NonPublic | BindingFlags.Instance);
                List<Policy> allPolicies = ((IList)GetPolicies.Invoke(__instance, null)).Cast<Policy>().ToList();

                // 1st policy in list is default
                if (___policyInt.id == allPolicies[0].id) { Log.Message("Policy is already default"); return; }

                Log.Message($"Swapping {allPolicies[0].label} with {___policyInt.label}");
                Log.Message($"Before: {allPolicies[0].label}");

                int swapIndex = allPolicies.FindIndex((Policy check) => check.id == ___policyInt.id);

                Policy temp = allPolicies[0];
                allPolicies[0] = ___policyInt;
                allPolicies[swapIndex] = temp;

                allPolicies = ((IList)GetPolicies.Invoke(__instance, null)).Cast<Policy>().ToList();
                Log.Message($"After: {allPolicies[0].label}");
            }
        }
    }




}
