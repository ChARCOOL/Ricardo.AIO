using EnsoulSharp;
using EnsoulSharp.Common;
using myCommon;

namespace Flowers_Darius.Manager.Spells
{
    internal class SpellManager : Logic
    {
        internal static int RMana => R.Level == 0 || R.Level == 3 ? 0 : 100;

        internal static void Init()
        {
            Q = new Spell(SpellSlot.Q, 420);
            W = new Spell(SpellSlot.W);
            E = new Spell(SpellSlot.E, 530);
            R = new Spell(SpellSlot.R, 450);
            E.SetSkillshot(0.25f, 80, int.MaxValue, false, SkillshotType.SkillshotCone);
            Q.SetSkillshot(0.75f, 42.5f, int.MaxValue, false, SkillshotType.SkillshotCircle);

            Ignite = Me.GetSpellSlot("SummonerDot");
        }

        internal static bool CanQHit(AIHeroClient target)
        {
            if (target == null) return false;

            if (target.DistanceToPlayer() > Q.Range) return false;

            if (target.DistanceToPlayer() <= 240) return false;

            if (target.Health < DamageCalculate.GetRDamage(target) && R.IsReady() &&
                target.IsValidTarget(R.Range)) return false;

            return true;
        }

        internal static void CastItem()
        {
            if (Items.HasItem(3077) && Items.CanUseItem(3077)) Items.UseItem(3077);

            if (Items.HasItem(3074) && Items.CanUseItem(3074)) Items.UseItem(3074);

            if (Items.HasItem(3053) && Items.CanUseItem(3053)) Items.UseItem(3053);
        }
    }
}