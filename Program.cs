using EnsoulSharp;
using EnsoulSharp.Common;
using System;
using Notification = EnsoulSharp.SDK.MenuUI.Notification;
using Notifications = EnsoulSharp.SDK.MenuUI.Notifications;

namespace Ricardo.AIO
{
    internal class Program
    {
        private static AIHeroClient Player => ObjectManager.Player;

        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += GameOnOnGameLoad;
        }

        private static void GameOnOnGameLoad(EventArgs eventArgs)
        {
            var notify = new Notification("Ricardo.AIO",
                "Ricardo.AIO Loaded. \n \nThanks xDreamms for helping me <3");
            Notifications.Add(notify);
            switch (Player.CharacterName)
            {
                case "Shen":
                    BadaoShen.Program.ShenMain();
                    break;

                case "Darius":
                    Flowers_Darius.Program.DariusMain();
                    break;

                case "Illaoi":
                    Illaoi___Tentacle_Kitty.Program.IllaoiMain();
                    break;

                case "Annie":
                    KoreanAnnie.Program.AnnieMain();
                    break;

                case "Zed":
                    KoreanZed.Program.ZedMain();
                    break;

                case "Vayne":
                    PRADA_Vayne.Program.VayneMain();
                    break;

                case "Camille":
                    hCamille.Program.CamilleMain();
                    break;

                case "LeeSin":
                    ElLeeSin.Program.LeeSinMain();
                    break;

                case "Twitch":
                    Flowers_Twitch.Program.TwitchMain();
                    break;

                case "Vladimir":
                    ElVladimirReborn.Program.VladimirMain();
                    break;

                case "Riven":
                    EloFactory_Riven.Program.RivenMain();
                    break;

                case "Cassiopeia":
                    mztikksCassiopeia.Program.CassiopeiaMain();
                    break;
            }
        }
    }
}
