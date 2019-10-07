using EnsoulSharp;
using EnsoulSharp.Common;
using System;

namespace KoreanZed
{
    internal class Program
    {
        public static void ZedMain()
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            if (ObjectManager.Player.CharacterName.ToLowerInvariant() == "zed")
            {
                var ZedZeppelin = new Zed();
            }
        }
    }
}