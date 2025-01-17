using EnsoulSharp.Common;
using PRADA_Vayne.MyLogic.Others;
using PRADA_Vayne.MyUtils;
using System.Drawing;
using System.Linq;
using Color = SharpDX.Color;

namespace PRADA_Vayne.MyInitializer
{
    public static partial class PRADALoader
    {
        public static void LoadMenu()
        {
            ConstructMenu();
            InitOrbwalker();
            FinishMenuInit();
        }

        public static void ConstructMenu()
        {
            Program.MainMenu = new Menu("PRADA Vayne", "pradamenu", true).SetFontStyle(FontStyle.Bold, Color.Gold);
            Program.ComboMenu = new Menu("Combo Settings", "combomenu").SetFontStyle(FontStyle.Bold, Color.Red);
            Program.LaneClearMenu =
                new Menu("Laneclear Settings", "laneclearmenu").SetFontStyle(FontStyle.Bold, Color.Red);
            Program.EscapeMenu = new Menu("Escape Settings", "escapemenu").SetFontStyle(FontStyle.Bold, Color.Red);

            Program.DrawingsMenu =
                new Menu("Drawing Settings", "drawingsmenu").SetFontStyle(FontStyle.Regular, Color.Turquoise);
            Program.DrawingsMenu.AddItem(new MenuItem("streamingmode", "Disable All Drawings").SetValue(false));
            Program.DrawingsMenu.AddItem(new MenuItem("drawenemywaypoints", "Draw Enemy Waypoints").SetValue(true));
            Program.OrbwalkerMenu =
                new Menu("Orbwalker", "orbwalkermenu").SetFontStyle(FontStyle.Regular, Color.Turquoise);
            Program.ComboMenu.AddItem(new MenuItem("QCombo", "Auto Tumble").SetValue(true));
            Program.ComboMenu
                .AddItem(new MenuItem("QMode", "Q Mode: ").SetValue(new StringList(new[] { "PRADA", "TO MOUSE" })))
                .SetFontStyle(FontStyle.Bold, Color.Red);
            Program.ComboMenu.AddItem(
                new MenuItem("QMinDist", "Min dist from enemies").SetValue(new Slider(375, 325, 525)));
            Program.ComboMenu.AddItem(
                new MenuItem("QOrderBy", "Q to position").SetValue(new StringList(new[]
                    {"CLOSETOMOUSE", "CLOSETOTARGET"})));
            Program.ComboMenu.AddItem(new MenuItem("QChecks", "Q Safety Checks").SetValue(true));
            Program.ComboMenu.AddItem(new MenuItem("EQ", "Q After E").SetValue(false));
            Program.ComboMenu.AddItem(new MenuItem("QR", "Q after Ult").SetValue(true));
            Program.ComboMenu.AddItem(new MenuItem("OnlyQinCombo", "Only Q in COMBO").SetValue(false));
            Program.ComboMenu.AddItem(new MenuItem("FocusTwoW", "Focus 2 W Stacks").SetValue(true));
            Program.ComboMenu.AddItem(new MenuItem("ECombo", "Auto Condemn").SetValue(true));
            Program.ComboMenu
                .AddItem(new MenuItem("ManualE", "Semi-Manual Condemn").SetValue(new KeyBind('E', KeyBindType.Press)))
                .SetFontStyle(FontStyle.Bold, Color.Gold);
            Program.ComboMenu.AddItem(new MenuItem("EMode", "E Mode").SetValue(new StringList(new[]
            {
                "PRADASMART", "PRADAPERFECT", "MARKSMAN", "SHARPSHOOTER", "GOSU", "VHR", "PRADALEGACY", "FASTEST",
                "OLDPRADA"
            }))).SetFontStyle(FontStyle.Bold, Color.Red);
            Program.ComboMenu.AddItem(new MenuItem("EPushDist", "E Push Distance").SetValue(new Slider(475, 300, 475)));
            Program.ComboMenu.AddItem(new MenuItem("EHitchance", "E % Hitchance").SetValue(new Slider(50)));
            Program.ComboMenu.AddItem(new MenuItem("RCombo", "Auto Ult").SetValue(false));
            Program.EscapeMenu.AddItem(new MenuItem("QUlt", "Smart Q-Ult").SetValue(true));
            Program.EscapeMenu.AddItem(new MenuItem("EInterrupt", "Use E to Interrupt").SetValue(true));

            var antigcmenu = Program.EscapeMenu.AddSubMenu(new Menu("Anti-Gapcloser", "antigapcloser"))
                .SetFontStyle(FontStyle.Bold, Color.Red);
            foreach (var hero in Heroes.EnemyHeroes)
            {
                var championName = hero.CharacterName;
                antigcmenu.AddItem(
                    new MenuItem("antigc" + championName, championName).SetValue(
                        Lists.CancerChamps.Any(entry => championName == entry)));
            }

            Program.LaneClearMenu.AddItem(new MenuItem("QLastHit", "Use Q to Lasthit").SetValue(true))
                .SetFontStyle(FontStyle.Bold, Color.Red);
            Program.LaneClearMenu.AddItem(
                new MenuItem("QLastHitMana", "Min Mana% for Q Lasthit").SetValue(new Slider(45)));
            Program.LaneClearMenu.AddItem(new MenuItem("EJungleMobs", "Use E on Jungle Mobs").SetValue(true))
                .SetFontStyle(FontStyle.Bold, Color.Red);
        }

        public static void InitOrbwalker()
        {
            Program.Orbwalker = new Orbwalking.Orbwalker(Program.OrbwalkerMenu);
        }

        public static void FinishMenuInit()
        {
            Program.MainMenu.AddSubMenu(Program.ComboMenu);
            Program.MainMenu.AddSubMenu(Program.LaneClearMenu);
            Program.MainMenu.AddSubMenu(Program.EscapeMenu);
            Program.MainMenu.AddSubMenu(Program.DrawingsMenu);
            Program.MainMenu.AddSubMenu(Program.OrbwalkerMenu);
        }
    }
}