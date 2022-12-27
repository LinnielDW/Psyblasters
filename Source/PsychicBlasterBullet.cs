using RimWorld;
using Verse;

namespace PsyBlasters
{
    [HotSwappable]
    public class PsychicBlasterBullet : Bullet
    {
        PsyBlasterBulletComp _psyBlasterBulletComp => GetComp<PsyBlasterBulletComp>();

        private bool CanConsumeResources(Pawn launcherPawn)
        {
            return _psyBlasterBulletComp != null && launcherPawn is { HasPsylink: true, psychicEntropy.CurrentPsyfocus: > 0 };
        }

        public override int DamageAmount
        {
            get
            {
                var damMulti = weaponDamageMultiplier;
                if (CanConsumeResources(Launcher as Pawn))
                {
                    damMulti += _psyBlasterBulletComp.PsyDamageMulti;
                }

                return def.projectile.GetDamageAmount(damMulti);
            }
        }

        public override float ArmorPenetration
        {
            get
            {
                var armorPenMulti = weaponDamageMultiplier;
                if (CanConsumeResources(Launcher as Pawn))
                {
                    armorPenMulti += _psyBlasterBulletComp.PsyPenMulti;
                }

                return def.projectile.GetArmorPenetration(armorPenMulti);
            }
        }

        //Do damage. If hits pawn, use resources, else, don't use resources most of the time.
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            base.Impact(hitThing, blockedByShield);

            //TODO: randchance to prop
            if (hitThing is not Pawn && Rand.Chance(0.66f)
                || Launcher is not Pawn launcherPawn
                || !CanConsumeResources(launcherPawn)
                || launcherPawn.psychicEntropy.limitEntropyAmount && launcherPawn.psychicEntropy.WouldOverflowEntropy(_psyBlasterBulletComp.EntropyCost)) return;
            launcherPawn.psychicEntropy.OffsetPsyfocusDirectly(-_psyBlasterBulletComp.PsyCost);
            launcherPawn.psychicEntropy.TryAddEntropy(_psyBlasterBulletComp.EntropyCost);
        }
    }
}