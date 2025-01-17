using EnsoulSharp;
using EnsoulSharp.Common;
using System.Linq;

namespace mztikksCassiopeia
{
    internal static class Events
    {
        #region Public Methods and Operators

        public static void OnGapCloser(ActiveGapcloser gapcloser)
        {
            if (gapcloser.Sender.IsMe || gapcloser.Sender.IsAlly) return;

            if (Config.IsChecked("qGapclose"))
                if (Spells.Q.IsInRange(gapcloser.End) && Spells.Q.IsReady())
                {
                    var delay = Mainframe.RDelay.Next(45, 95);
                    Utility.DelayAction.Add(delay, () => Spells.Q.Cast(gapcloser.End));
                }
        }

        public static void OnInterruptableSpell(ActiveInterrupter interrupter)
        {
            if (interrupter.Sender.IsMe || interrupter.Sender.IsAlly || !Config.IsChecked("bInterrupt")) return;

            var wanteDangerLevel = InterrupterDangerLevel.High;
            switch (Config.GetStringListValue("dangerL"))
            {
                case 0:
                    wanteDangerLevel = InterrupterDangerLevel.Low;
                    break;

                case 1:
                    wanteDangerLevel = InterrupterDangerLevel.Medium;
                    break;

                case 2:
                    wanteDangerLevel = InterrupterDangerLevel.High;
                    break;

                default:
                    wanteDangerLevel = InterrupterDangerLevel.High;
                    break;
            }

            if (Spells.R.IsReady() && interrupter.Sender.IsValidTarget(Spells.R.Range) &&
                interrupter.DangerLevel == wanteDangerLevel)
            {
                var rPred = Spells.R.GetPrediction(interrupter.Sender);
                if (rPred.Hitchance >= HitChance.Medium)
                {
                    var delay = Mainframe.RDelay.Next(100, 120);
                    Utility.DelayAction.Add(delay, () => Spells.R.Cast(rPred.CastPosition));
                }
            }
        }

        public static void OnProcessSpellCast(AIBaseClient sender, AIBaseClientProcessSpellCastEventArgs args)
        {
            if (sender.IsMe)
                if (args.SData.Name == "SummonerFlash" && Spells.FlashR)
                    Spells.FlashR = false;
        }

        public static void OnSpellbookCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args)
        {
            if (sender.Owner.IsMe && args.Slot == SpellSlot.R && Config.IsChecked("antiMissR") && !Spells.FlashR)
            {
                var facingEns =
                    HeroManager.Enemies.Where(h =>
                        h.IsValidTarget(Spells.R.Range) && h.IsFacing(ObjectManager.Player) &&
                        Spells.R.WillHit(h, args.StartPosition));
                if (!facingEns.Any()) args.Process = false;
            }

            if (sender.Owner.IsMe)
                switch (args.Slot)
                {
                    case SpellSlot.Q:
                        Spells.QCasted = Game.Time;
                        Spells.LastQPos = args.EndPosition;
                        break;

                    case SpellSlot.W:
                        Spells.WCasted = Game.Time;
                        Spells.LastWPos = args.EndPosition;
                        break;
                }
        }

        public static void OnUnkillableMinion(AttackableUnit minion)
        {
            var target = minion as AIMinionClient;
            if (target == null) return;

            var eTravelTime = target.Distance(ObjectManager.Player) / Spells.E.Instance.SData.MissileSpeed
                              + Spells.E.Delay + Game.Ping / 2f / 1000;
            if (Config.IsChecked("eLastHit") && Spells.GetEDamage(target) > target.Health
                                             && HealthPrediction.GetHealthPrediction(target, (int)eTravelTime * 1000) >
                                             0)
                Spells.E.Cast(target);
        }

        #endregion Public Methods and Operators
    }
}