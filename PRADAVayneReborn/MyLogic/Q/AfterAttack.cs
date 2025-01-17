using EnsoulSharp;
using EnsoulSharp.Common;
using PRADA_Vayne.MyUtils;
using System;
using System.Linq;

namespace PRADA_Vayne.MyLogic.Q
{
    public static partial class Events
    {
        public static void AfterAttack(AttackableUnit sender, AttackableUnit target)
        {
            if (!Program.Q.IsReady()) return;
            if (sender.IsMe && target.IsValid<AIHeroClient>() &&
                (Program.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo ||
                 !Program.ComboMenu.Item("OnlyQinCombo").GetValue<bool>()))
            {
                var tg = target as AIHeroClient;
                if (tg == null) return;
                var mode = Program.ComboMenu.Item("QMode").GetValue<StringList>().SelectedValue;
                var tumblePosition = Game.CursorPosCenter;
                switch (mode)
                {
                    case "PRADA":
                        tumblePosition = tg.GetTumblePos();
                        break;

                    default:
                        tumblePosition = Game.CursorPosCenter;
                        break;
                }

                Tumble.Cast(tumblePosition);
            }

            var m = target as AIMinionClient;
            if (m != null && Program.LaneClearMenu.Item("QLastHit").GetValue<bool>() &&
                ObjectManager.Player.ManaPercent >=
                Program.LaneClearMenu.Item("QLastHitMana").GetValue<Slider>().Value &&
                Program.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LastHit ||
                Program.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear)
            {
                var dashPosition = Game.CursorPosCenter;
                var mode = Program.ComboMenu.Item("QMode").GetValue<StringList>().SelectedValue;
                switch (mode)
                {
                    case "PRADA":
                        dashPosition = m.GetTumblePos();
                        break;

                    default:
                        dashPosition = Game.CursorPosCenter;
                        break;
                }

                if (m.Team == GameObjectTeam.Neutral) Program.Q.Cast(dashPosition);
                foreach (var minion in ObjectManager.Get<AIMinionClient>().Where(minion =>
                    m.NetworkId != minion.NetworkId && minion.IsEnemy && minion.IsValidTarget(615)))
                {
                    if (minion == null)
                        break;
                    var time = (int)(ObjectManager.Player.AttackCastDelay * 1000) + Game.Ping / 2 + 1000 *
                               (int)Math.Max(0,
                                   ObjectManager.Player.Distance(minion) - ObjectManager.Player.BoundingRadius) /
                               (int)ObjectManager.Player.BasicAttack.MissileSpeed;
                    var predHealth = HealthPrediction.GetHealthPrediction(minion, time);
                    if (predHealth < ObjectManager.Player.GetAutoAttackDamage(minion) + Program.Q.GetDamage(minion) &&
                        predHealth > 0)
                        Program.Q.Cast(dashPosition, true);
                }
            }
        }
    }
}