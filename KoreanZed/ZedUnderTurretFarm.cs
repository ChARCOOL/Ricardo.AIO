using EnsoulSharp;
using EnsoulSharp.Common;
using System;

namespace KoreanZed
{
    internal class ZedUnderTurretFarm
    {
        private readonly ZedSpell e;

        private readonly ZedSpell q;
        private readonly Orbwalking.Orbwalker zedOrbwalker;

        private AIMinionClient targetUnderTurret;

        private AIBaseClient turrent;

        public ZedUnderTurretFarm(ZedSpells zedSpells, Orbwalking.Orbwalker zedOrbwalker)
        {
            q = zedSpells.Q;
            e = zedSpells.E;

            this.zedOrbwalker = zedOrbwalker;

            Game.OnUpdate += Game_OnUpdate;
        }

        private void Game_OnUpdate(EventArgs args)
        {
            if (targetUnderTurret != null && targetUnderTurret.IsDead)
            {
                targetUnderTurret = null;
                turrent = null;
            }

            if (zedOrbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo
                && zedOrbwalker.ActiveMode != Orbwalking.OrbwalkingMode.None && turrent != null
                && targetUnderTurret != null && !targetUnderTurret.IsDead && targetUnderTurret.IsValid)
                if (targetUnderTurret.IsValid)
                {
                    if (ObjectManager.Player.Distance(targetUnderTurret)
                        < Orbwalking.GetRealAutoAttackRange(targetUnderTurret) + 20F && targetUnderTurret.Health
                        < ObjectManager.Player.GetAutoAttackDamage(targetUnderTurret) * 2
                        + turrent.GetAutoAttackDamage(targetUnderTurret) && targetUnderTurret.Health
                        > turrent.GetAutoAttackDamage(targetUnderTurret)
                        + ObjectManager.Player.GetAutoAttackDamage(targetUnderTurret))
                        ObjectManager.Player.IssueOrder(GameObjectOrder.AttackUnit, targetUnderTurret);

                    if (q.IsReady() && q.CanCast(targetUnderTurret)
                                    && ObjectManager.Player.Distance(targetUnderTurret)
                                    < Orbwalking.GetRealAutoAttackRange(targetUnderTurret) + 20F
                                    && targetUnderTurret.Health
                                    < q.GetDamage(targetUnderTurret)
                                    + ObjectManager.Player.GetAutoAttackDamage(targetUnderTurret, true))
                    {
                        q.Cast(targetUnderTurret);
                        return;
                    }

                    if (e.IsReady() && e.CanCast(targetUnderTurret) && !q.IsReady()
                        && ObjectManager.Player.Distance(targetUnderTurret)
                        < Orbwalking.GetRealAutoAttackRange(targetUnderTurret) + 20F
                        && targetUnderTurret.Health
                        < e.GetDamage(targetUnderTurret)
                        + ObjectManager.Player.GetAutoAttackDamage(targetUnderTurret, true))
                        e.Cast(targetUnderTurret);
                }
        }
    }
}