using EnsoulSharp;
using EnsoulSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KoreanZed
{
    internal class ZedAutoE
    {
        private readonly ZedSpell e;
        private readonly ZedMenu zedMenu;

        private readonly ZedShadows zedShadows;

        public ZedAutoE(ZedMenu zedMenu, ZedShadows zedShadows, ZedSpells zedSpells)
        {
            this.zedMenu = zedMenu;
            this.zedShadows = zedShadows;
            e = zedSpells.E;

            Game.OnUpdate += Game_OnUpdate;
        }

        public List<AIBaseClient> GetShadows2()
        {
            var resultList = new List<AIBaseClient>();

            foreach (var objAiBase in
                ObjectManager.Get<AIBaseClient>()
                    .Where(obj => obj.Name.ToLowerInvariant().Contains("Shadow") && !obj.IsDead))
                resultList.Add(objAiBase);
            return resultList;
        }

        private void Game_OnUpdate(EventArgs args)
        {
            if (!e.IsReady() || ObjectManager.Player.Mana < e.ManaCost ||
                !zedMenu.GetParamBool("koreanzed.miscmenu.autoe")) return;

            var shadows = GetShadows2();

            if (
                HeroManager.Enemies.Any(
                    enemy =>
                        !enemy.IsDead && !enemy.IsZombie && enemy.Distance(ObjectManager.Player) < e.Range
                        && enemy.IsValidTarget())
                || GetShadows2()
                    .Any(
                        shadow =>
                            HeroManager.Enemies.Any(
                                enemy =>
                                    !enemy.IsDead && !enemy.IsZombie && enemy.Distance(shadow) < e.Range
                                    && enemy.IsValidTarget())))
                e.Cast();
            //Console.WriteLine("auto e1");
        }
    }
}