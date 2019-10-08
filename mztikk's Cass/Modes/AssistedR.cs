using EnsoulSharp;
using EnsoulSharp.Common;
using System.Linq;

namespace mztikksCassiopeia.Modes
{
    internal static class AssistedR
    {
        #region Methods

        internal static void Execute()
        {
            if (!Config.IsKeyPressed("assistedR") || !Spells.R.IsReady()) return;
            var target =
                HeroManager.Enemies.Where(
                        x =>
                            !x.IsDead && x.IsValid && x.Distance(ObjectManager.Player) < Spells.R.Range
                            && x.IsFacing(ObjectManager.Player)).OrderBy(x => x.Distance(Game.CursorPosRaw))
                    .FirstOrDefault();
            if (target == null) return;
            var rPred = Spells.R.GetPrediction(target, true);
            if (rPred.Hitchance >= HitChance.High) Spells.R.Cast(rPred.CastPosition);
        }

        #endregion Methods
    }
}