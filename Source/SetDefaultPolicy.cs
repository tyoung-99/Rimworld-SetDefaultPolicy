using HarmonyLib;
using Verse;

namespace SetDefaultPolicy
{
    [StaticConstructorOnStartup]
    public static class SetDefaultPolicy
    {
        static SetDefaultPolicy()
        {
            new Harmony("BobBobson99.SetDefaultPolicy").PatchAll();
        }
    }
}
