using EnsoulSharp;
using EnsoulSharp.Common;

namespace hCamille.Extensions
{
    public static class Spells
    {
        public static Spell Q { get; set; }
        public static Spell W { get; set; }
        public static Spell E { get; set; }
        public static Spell R { get; set; }

        public static void Initializer()
        {
            Q = new Spell(SpellSlot.Q, 325f);
            W = new Spell(SpellSlot.W, 610f);
            E = new Spell(SpellSlot.E, 800f);
            R = new Spell(SpellSlot.R, 475f);

            W.SetSkillshot(0.195f, 100, 1750, false, SkillshotType.SkillshotCone);
            E.SetSkillshot(0.3f, 30, 500, false, SkillshotType.SkillshotLine);
        }
    }
}