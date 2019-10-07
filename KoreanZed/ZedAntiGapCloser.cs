using EnsoulSharp;
using EnsoulSharp.Common;
using KoreanZed.QueueActions;
using SharpDX;
using System;
using System.Linq;

namespace KoreanZed
{
    internal class ZedAntiGapCloser
    {
        private readonly ActionQueue actionQueue;

        private readonly ActionQueueList antiGapCloserList;

        private readonly ZedSpell e;

        private readonly AIHeroClient player;

        private readonly ZedShadows shadows;

        private readonly ZedSpell w;
        private readonly ZedMenu zedMenu;

        public ZedAntiGapCloser(ZedMenu menu, ZedSpells spells, ZedShadows shadows)
        {
            zedMenu = menu;
            w = spells.W;
            e = spells.E;
            this.shadows = shadows;
            player = ObjectManager.Player;

            actionQueue = new ActionQueue();
            antiGapCloserList = new ActionQueueList();

            Gapclosers.OnGapcloser += AntiGapcloser_OnEnemyGapcloser;
        }

        private void Game_OnUpdate(EventArgs args)
        {
            actionQueue.ExecuteNextAction(antiGapCloserList);
            if (antiGapCloserList.Count == 0) Game.OnUpdate -= Game_OnUpdate;
        }

        private void AntiGapcloser_OnEnemyGapcloser(ActiveGapcloser gapcloser)
        {
            if (ObjectManager.Player.Distance(gapcloser.Sender.Position) > e.Range) return;

            if (zedMenu.GetParamBool("koreanzed.miscmenu.useeantigc") && e.IsReady())
                e.Cast();
            //Console.WriteLine("antig1");

            if (zedMenu.GetParamBool("koreanzed.miscmenu.usewantigc") && w.IsReady() && antiGapCloserList.Count == 0)
            {
                if (shadows.CanCast)
                {
                    actionQueue.EnqueueAction(
                        antiGapCloserList,
                        () => player.Mana > w.ManaCost && player.HealthPercent - 10 < gapcloser.Sender.HealthPercent,
                        () => shadows.Cast(Vector3.Negate(gapcloser.Sender.Position)),
                        () => true);
                    actionQueue.EnqueueAction(
                        antiGapCloserList,
                        () => w.Instance.ToggleState != 0,
                        () => shadows.Switch(),
                        () => !w.IsReady());
                    Game.OnUpdate += Game_OnUpdate;
                }
                else if (!shadows.CanCast && shadows.CanSwitch)
                {
                    var champCount =
                        HeroManager.Enemies.Count(enemy => enemy.Distance((Vector2)shadows.Instance.Position) < 1500F);

                    if (player.HealthPercent > 80 && champCount <= 3
                        || player.HealthPercent > 40 && champCount <= 2
                    )
                        shadows.Switch();
                    //Console.WriteLine("antig2");
                }
            }
        }
    }
}