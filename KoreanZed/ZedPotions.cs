using EnsoulSharp;
using EnsoulSharp.Common;
using System;

namespace KoreanZed
{
    internal class ZedPotions
    {
        private readonly ZedMenu zedMenu;

        public ZedPotions(ZedMenu zedMenu)
        {
            this.zedMenu = zedMenu;

            Game.OnUpdate += Game_OnUpdate;
        }

        private void Game_OnUpdate(EventArgs args)
        {
            if (zedMenu.GetParamBool("koreanzed.miscmenu.pot.active")
                && ObjectManager.Player.HealthPercent
                < zedMenu.GetParamSlider("koreanzed.miscmenu.pot.when")
                && !ObjectManager.Player.HasBuff("RegenerationPotion")
                && !ObjectManager.Player.InShop())
                Items.UseItem(2003);
        }
    }
}