using Verse;

namespace PsyBlasters
{
    [HotSwappable]
    public class CompProperties_PsyBlaster : CompProperties
    {
        public CompProperties_PsyBlaster()
        {
            compClass = typeof(PsyBlasterComp);
        }

        public int addedDamage = 0;
        public float psyCost = 0f;
        public float entropyCost = 0f;
    }

    [HotSwappable]
    public class PsyBlasterComp : ThingComp
    {
    }
}