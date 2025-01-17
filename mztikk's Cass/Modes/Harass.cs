using EnsoulSharp;
using EnsoulSharp.Common;

namespace mztikksCassiopeia.Modes
{
    internal static class Harass
    {
        #region Methods

        internal static void Execute()
        {
            var target = TargetSelector.GetTarget(Spells.R.Range, TargetSelector.DamageType.Magical);
            if (target == null || !target.IsValidTarget(Spells.Q.Range)
                               || ObjectManager.Player.ManaPercent < Config.GetSliderValue("manaToHarass"))
                return;

            if (Config.IsChecked("useQInHarass") && Spells.Q.IsReady() && !target.HasBuffOfType(BuffType.Poison)
                && !ObjectManager.Player.Spellbook.IsAutoAttack)
            {
                var qPred = Spells.Q.GetPrediction(target);
                if (qPred.Hitchance >= HitChance.Medium) Spells.Q.Cast(qPred.CastPosition);
            }

            if (Config.IsChecked("useWInHarass") && Spells.W.IsReady() && target.IsValidTarget(Spells.W.Range)
                && !ObjectManager.Player.Spellbook.IsAutoAttack)
            {
                if (Config.IsChecked("harassWonlyCD"))
                {
                    if (!Spells.Q.IsReady() && Spells.QCasted - Game.Time < -0.5f
                                            && !target.HasBuffOfType(BuffType.Poison))
                    {
                        var wPred = Spells.W.GetPrediction(target);
                        if (wPred.CastPosition.Distance(ObjectManager.Player.Position) >= Spells.WMinRange
                            && wPred.Hitchance >= HitChance.Medium)
                            Spells.W.Cast(wPred.CastPosition);
                    }
                }
                else
                {
                    var wPred = Spells.W.GetPrediction(target);
                    if (wPred.Hitchance >= HitChance.Medium) Spells.W.Cast(wPred.CastPosition);
                }
            }

            if (Config.IsChecked("useEInHarass") && Spells.E.IsReady() && target.IsValidTarget(Spells.E.Range)
                && (!Config.IsChecked("harassEonP") || target.HasBuffOfType(BuffType.Poison)))
            {
                if (Config.IsChecked("humanEInHarass"))
                {
                    var delay = Computed.RandomDelay(Config.GetSliderValue("humanDelay"));
                    Utility.DelayAction.Add(delay, () => Spells.E.Cast(target));
                }
                else
                {
                    Spells.E.Cast(target);
                }
            }
        }

        #endregion Methods
    }
}