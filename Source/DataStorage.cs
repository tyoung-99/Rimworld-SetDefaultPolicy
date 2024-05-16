using RimWorld.Planet;
using Verse;

namespace SetDefaultPolicy
{
    public class DataStorage : WorldComponent
    {
        private static int defaultPolicyIdOutfit, defaultPolicyIdFood, defaultPolicyIdDrug, defaultPolicyIdReading;

        public static int DefaultPolicyIdOutfit { get { return defaultPolicyIdOutfit; } set { defaultPolicyIdOutfit = value; } }
        public static int DefaultPolicyIdFood { get { return defaultPolicyIdFood; } set { defaultPolicyIdFood = value; } }
        public static int DefaultPolicyIdDrug { get { return defaultPolicyIdDrug; } set { defaultPolicyIdDrug = value; } }
        public static int DefaultPolicyIdReading { get { return defaultPolicyIdReading; } set { defaultPolicyIdReading = value; } }

        public DataStorage(World world) : base(world)
        {
            DefaultPolicyIdOutfit = 1;
            DefaultPolicyIdFood = 1;
            DefaultPolicyIdDrug = 1;
            DefaultPolicyIdReading = 1;
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref defaultPolicyIdOutfit, "defaultPolicyIdOutfit");
            Scribe_Values.Look(ref defaultPolicyIdFood, "defaultPolicyIdFood");
            Scribe_Values.Look(ref defaultPolicyIdDrug, "defaultPolicyIdDrug");
            Scribe_Values.Look(ref defaultPolicyIdReading, "defaultPolicyIdReading");
        }
    }
}
