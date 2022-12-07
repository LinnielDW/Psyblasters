using Verse;

namespace PsyBlasters
{
    [HotSwappable]
    public class CompProperties_PsyBlasterBullet : CompProperties
    {
        public CompProperties_PsyBlasterBullet()
        {
            compClass = typeof(PsyBlasterBulletComp);
        }

        public float psyDamageMulti = 0;
        public float psyPenMulti = 0f;
        
        public float psyCost = 0f;
        public float entropyCost = 0f;
    }

    [HotSwappable]
    public class PsyBlasterBulletComp : ThingComp
    {
        public CompProperties_PsyBlasterBullet Props => (CompProperties_PsyBlasterBullet)props;

        public float PsyDamageMultiMulti => Props.psyDamageMulti;
        public float PsyPenMultiMulti => Props.psyPenMulti;
        public float PsyCost => Props.psyCost;
        public float EntropyCost => Props.entropyCost;
    }
}