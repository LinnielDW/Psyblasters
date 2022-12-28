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

        public float PsyDamageMulti;
        public float PsyPenMulti;
        public float PsyCost;
        public float EntropyCost;

        public override void Initialize(CompProperties compProps)
        {
            base.Initialize(compProps);
            PsyDamageMulti = Props.psyDamageMulti;
            PsyPenMulti = Props.psyPenMulti;
            PsyCost = Props.psyCost;
            EntropyCost = Props.entropyCost;
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref PsyDamageMulti, "PsyDamageMulti");
            Scribe_Values.Look(ref PsyPenMulti, "PsyDamageMulti");
            Scribe_Values.Look(ref PsyCost, "PsyDamageMulti");
            Scribe_Values.Look(ref EntropyCost, "PsyDamageMulti");
        }
    }
}