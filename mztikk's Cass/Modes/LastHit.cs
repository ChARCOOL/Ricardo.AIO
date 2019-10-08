using EnsoulSharp;
using EnsoulSharp.Common;
using System.Linq;

namespace mztikksCassiopeia.Modes
{
    internal static class LastHit
    {
        #region Methods

        internal static void Execute()
        {
            if (ObjectManager.Player.Spellbook.IsAutoAttack) return;

            if (Config.IsChecked("useEInLH") && Spells.E.IsReady())
            {
                var minToE =
                    MinionManager.GetMinions(ObjectManager.Player.Position, Spells.E.Range)
                        .FirstOrDefault(m => m.Health < Spells.GetEDamage(m) && m.IsValidTarget(Spells.E.Range));
                if (minToE != null)
                    if (!Config.IsChecked("lastEonP") || minToE.HasBuffOfType(BuffType.Poison))
                        Spells.E.Cast(minToE);
            }
        }

        #endregion Methods
    }
}