using EnsoulSharp;
using EnsoulSharp.Common;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KoreanZed
{
    internal class ZedKS
    {
        private readonly ZedSpell e;

        private readonly AIHeroClient player;
        private readonly ZedSpell q;

        private readonly ZedSpell w;

        private readonly Orbwalking.Orbwalker zedOrbwalker;

        private readonly ZedShadows zedShadows;

        public ZedKS(ZedSpells spells, Orbwalking.Orbwalker orbwalker, ZedShadows zedShadows)
        {
            q = spells.Q;
            w = spells.W;
            e = spells.E;

            player = ObjectManager.Player;

            zedOrbwalker = orbwalker;
            this.zedShadows = zedShadows;

            Game.OnUpdate += Game_OnUpdate;
        }

        private void Game_OnUpdate(EventArgs args)
        {
            if (q.IsReady() && player.Mana > q.ManaCost)
                foreach (var objAiHero in player.GetEnemiesInRange(q.Range)
                    .Where(hero => !hero.IsDead && !hero.IsZombie && q.IsKillable(hero)))
                {
                    var predictionOutput = q.GetPrediction(objAiHero);

                    if (predictionOutput.Hitchance >= HitChance.Medium &&
                        (!q.GetCollision(player.Position.To2D(),
                             new List<Vector2> { predictionOutput.CastPosition.To2D() }).Any()
                         || q.GetDamage(objAiHero) / 2 > objAiHero.Health))
                        q.Cast(predictionOutput.CastPosition);
                    //Console.WriteLine("ks q");
                }

            if (e.IsReady() && player.Mana > e.ManaCost)
                if (player.GetEnemiesInRange(e.Range).Any(hero => !hero.IsDead && !hero.IsZombie && e.IsKillable(hero)))
                    e.Cast();
            //Console.WriteLine("ks e");

            if (zedOrbwalker.ActiveMode != Orbwalking.OrbwalkingMode.Combo || !zedShadows.CanCast) return;

            var heroList = ObjectManager.Player.GetEnemiesInRange(2000F);
            if (heroList.Count() == 1)
            {
                var target = heroList.FirstOrDefault();

                if (target != null && zedShadows.CanCast &&
                    player.Distance(target) > Orbwalking.GetRealAutoAttackRange(target)
                    && player.Distance(target) < w.Range + Orbwalking.GetRealAutoAttackRange(target)
                    && player.GetAutoAttackDamage(target) > target.Health && player.Mana > w.ManaCost)
                {
                    zedShadows.Cast(target.Position);
                    //Console.WriteLine("ks w 1");
                    zedShadows.Switch();
                    //Console.WriteLine("ks w 2");
                }
            }
        }
    }
}