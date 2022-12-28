using HarmonyLib;
using Verse;

namespace PsyBlasters
{
    [HotSwappable]
    public class PsychicBlasterRocket : Projectile_Explosive
    {
        PsyBlasterBulletComp _psyBlasterBulletComp => GetComp<PsyBlasterBulletComp>();

        private bool CanConsumeResources(Pawn launcherPawn)
        {
            return _psyBlasterBulletComp != null && launcherPawn is { HasPsylink: true };
        }

        public override int DamageAmount
        {
            get
            {
                if (CanConsumeResources(Launcher as Pawn))
                {
                    return def.projectile.GetDamageAmount(weaponDamageMultiplier) +
                           (int)((((Pawn)Launcher).psychicEntropy.MaxPotentialEntropy - ((Pawn)Launcher).psychicEntropy.EntropyValue) * _psyBlasterBulletComp.PsyDamageMulti);
                }

                return 0;
            }
        }

        //Do damage. If hits pawn, use resources, else, don't use resources most of the time.
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            base.Impact(hitThing, blockedByShield);

            if (!CanConsumeResources(Launcher as Pawn)) return;

            var launcherPawn = (Pawn)Launcher;
            Traverse.Create(launcherPawn).Field("psychicEntropy").Field("currentEntropy").SetValue(launcherPawn.psychicEntropy.MaxPotentialEntropy * 5f);
        }
    }
}