using EnsoulSharp;
using EnsoulSharp.Common;
using System;

namespace mztikksCassiopeia
{
    internal static class Program
    {
        public static void CassiopeiaMain()
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            if (ObjectManager.Player.CharacterName.ToLower() != "cassiopeia") return;

            Spells.LoadSpells();
            Config.BuildMenu();
            Mainframe.Init();
        }
    }
}