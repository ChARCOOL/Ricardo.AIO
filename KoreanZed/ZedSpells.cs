using EnsoulSharp;
using EnsoulSharp.Common;

namespace KoreanZed
{
    internal class ZedSpells
    {
        public ZedSpells()
        {
            Q = new ZedSpell(SpellSlot.Q, 900F, TargetSelector.DamageType.Physical);
            Q.SetSkillshot(0.25F, 50F, 900F, false, SkillshotType.SkillshotLine);

            var wRange = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).SData.CastRange;
            W = new ZedSpell(SpellSlot.W, wRange, TargetSelector.DamageType.Physical);
            W.SetSkillshot(0.75F, 75F, 1750F, false, SkillshotType.SkillshotLine);

            var eRange = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).SData.CastRange - 10;
            E = new ZedSpell(SpellSlot.E, eRange, TargetSelector.DamageType.Physical);

            var rRange = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).SData.CastRange;
            R = new ZedSpell(SpellSlot.R, rRange, TargetSelector.DamageType.Physical);
        }

        public ZedSpell Q { get; set; }
        public ZedSpell W { get; set; }
        public ZedSpell E { get; set; }
        public ZedSpell R { get; set; }
    }
}