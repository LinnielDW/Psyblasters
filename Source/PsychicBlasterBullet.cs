using RimWorld;
using Verse;

namespace PsyBlasters
{
    [HotSwappable]
    public class PsychicBlasterBullet : Bullet
    {
        public override int DamageAmount
        {
            get
            {
                var damage = base.DamageAmount;
                var launcherPawn = Launcher as Pawn;
                var compProperties = EquipmentDef.GetCompProperties<CompProperties_PsyBlaster>();
                if (compProperties != null && launcherPawn is { HasPsylink: true, psychicEntropy.CurrentPsyfocus: > 0 })
                {
                    damage += compProperties.addedDamage;
                }

                return damage;
            }
        }

        public override float ArmorPenetration
        {
            get
            {
                var armPen = base.ArmorPenetration;
                
                var launcherPawn = Launcher as Pawn;
                var compProperties = EquipmentDef.GetCompProperties<CompProperties_PsyBlaster>();
                if (compProperties != null && launcherPawn is { HasPsylink: true, psychicEntropy.CurrentPsyfocus: > 0 })
                {
                    armPen += compProperties.addedArmPen;
                }

                return armPen;
            }
        }

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            base.Impact(hitThing, blockedByShield);

            //TODO: randchance to prop
            if (hitThing == null && Rand.Chance(0.75f)) return;

            var launcherPawn = Launcher as Pawn;
            var compProperties = EquipmentDef.GetCompProperties<CompProperties_PsyBlaster>();
            if (compProperties != null
                && launcherPawn != null
                && launcherPawn.HasPsylink
                && launcherPawn.psychicEntropy.CurrentPsyfocus > 0
                && !launcherPawn.psychicEntropy.WouldOverflowEntropy(compProperties.entropyCost))
            {
                launcherPawn.psychicEntropy.OffsetPsyfocusDirectly(-compProperties.psyCost);
                launcherPawn.psychicEntropy.TryAddEntropy(compProperties.entropyCost);
            }
        }
    }
}