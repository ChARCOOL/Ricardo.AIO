using EnsoulSharp;
using System.Collections.Generic;

namespace KoreanZed
{
    internal class ZedEnergyChecker
    {
        private readonly ZedMenu zedMenu;

        public ZedEnergyChecker(ZedMenu menu)
        {
            zedMenu = menu;
        }

        public bool ReadyToHaras =>
            ObjectManager.Player.ManaPercent
            > zedMenu.GetParamSlider("koreanzed.harasmenu.saveenergy");

        public bool ReadyToLaneClear =>
            ObjectManager.Player.ManaPercent
            > zedMenu.GetParamSlider("koreanzed.laneclearmenu.saveenergy");

        public bool ReadyToLastHit =>
            ObjectManager.Player.ManaPercent
            > zedMenu.GetParamSlider("koreanzed.lasthitmenu.saveenergy");

        public bool CanHarass(List<ZedSpell> spellList)
        {
            var total = 0F;

            foreach (var zedSpell in spellList) total += zedSpell.ManaCost;

            return (ObjectManager.Player.Mana - total) / ObjectManager.Player.MaxMana * 100
                   > zedMenu.GetParamSlider("koreanzed.harasmenu.saveenergy");
        }
    }
}