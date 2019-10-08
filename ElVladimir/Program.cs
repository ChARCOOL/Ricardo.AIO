using EnsoulSharp.Common;

namespace ElVladimirReborn
{
    internal class Program
    {
        public static void VladimirMain()
        {
            CustomEvents.Game.OnGameLoad += ElVladimirReborn.Vladimir.OnLoad;
        }
    }
}