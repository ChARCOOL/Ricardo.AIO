using EnsoulSharp;
using System;

namespace KoreanZed
{
    internal class ZedFlee
    {
        private readonly ZedMenu zedMenu;

        private readonly ZedShadows zedShadows;

        public ZedFlee(ZedMenu zedMenu, ZedShadows zedShadows)
        {
            this.zedMenu = zedMenu;
            this.zedShadows = zedShadows;

            Game.OnUpdate += Game_OnUpdate;
        }

        private void Game_OnUpdate(EventArgs args)
        {
            if (zedMenu.GetParamKeyBind("koreanzed.miscmenu.flee"))
            {
                ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPosCenter);
                //Console.WriteLine("flee 1");
                zedShadows.Cast(Game.CursorPosCenter);
                //Console.WriteLine("flee 2");
                zedShadows.Switch();
                //Console.WriteLine("flee 3");
            }
        }
    }
}