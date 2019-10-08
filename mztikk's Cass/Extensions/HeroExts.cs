using EnsoulSharp;
using EnsoulSharp.Common;
using System.Linq;

namespace mztikksCassiopeia.Extensions
{
    public static class HeroExts
    {
        #region Public Methods and Operators

        public static double GetAlliesDamageNearEnemy(this AIHeroClient enemy, float percent = 0.8f, float range = 750)
        {
            var fullDmg = 0.0;
            var slots = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
            foreach (var ally in HeroManager.Allies.Where(al => al.Distance(enemy) <= range))
            {
                fullDmg +=
                    ally.Spellbook.Spells.Where(spell => spell.IsReady() && slots.Contains(spell.Slot))
                        .Sum(spell => ally.GetSpellDamage(enemy, spell.Slot));
                fullDmg += ally.GetAutoAttackDamage(enemy);
            }

            return fullDmg * percent;
        }

        public static double GetEnemiesDamageNearAlly(this AIHeroClient ally, float percent = 0.8f, float range = 750)
        {
            var fullDmg = 0.0;
            var slots = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
            foreach (var enemy in HeroManager.Enemies.Where(en => en.Distance(ally) <= range))
            {
                fullDmg +=
                    enemy.Spellbook.Spells.Where(spell => spell.IsReady() && slots.Contains(spell.Slot))
                        .Sum(spell => enemy.GetSpellDamage(ally, spell.Slot));
                fullDmg += enemy.GetAutoAttackDamage(ally);
            }

            return fullDmg * percent;
        }

        public static double GetTotalDamageFromLib(this AIHeroClient attacker, AIBaseClient defender)
        {
            var slots = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
            var spellDmg =
                attacker.Spellbook.Spells.Where(spell => spell.IsReady() && slots.Contains(spell.Slot))
                    .Sum(spell => attacker.GetSpellDamage(defender, spell.Slot));
            var aaDmg = attacker.CanAttack ? attacker.GetAutoAttackDamage(defender) : 0f;
            var fulldmg = spellDmg + aaDmg;
            return fulldmg;
        }

        #endregion Public Methods and Operators
    }
}