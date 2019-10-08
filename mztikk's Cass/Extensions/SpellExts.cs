using EnsoulSharp;
using EnsoulSharp.Common;

namespace mztikksCassiopeia.Extensions
{
    public static class SpellExts
    {
        #region Public Methods and Operators

        public static bool CanCast(this Spell spell)
        {
            return spell.Level > 0 && spell.IsReady() && spell.ManaCost < ObjectManager.Player.Mana;
        }

        #endregion Public Methods and Operators
    }
}