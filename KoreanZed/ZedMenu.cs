using EnsoulSharp;
using EnsoulSharp.Common;
using KoreanZed.Enumerators;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace KoreanZed
{
    internal class ZedMenu
    {
        private readonly ZedSpells zedSpells;

        public ZedMenu(ZedSpells zedSpells, out Orbwalking.Orbwalker orbwalker)
        {
            MainMenu = new Menu("Korean Zed", "mainmenu", true);
            this.zedSpells = zedSpells;

            AddTargetSelector();
            AddOrbwalker(out orbwalker);
            ComboMenu();
            HarassMenu();
            LaneClearMenu();
            LastHitMenu();
            MiscMenu();
            DrawingMenu();

            GetInitialSpellValues();
        }

        public Menu MainMenu { get; set; }

        private void AddTargetSelector()
        {
            var targetSelectorMenu = new Menu("Target Selector", "zedtargetselector");
            TargetSelector.AddToMenu(targetSelectorMenu);
            MainMenu.AddSubMenu(targetSelectorMenu);
        }

        private void AddOrbwalker(out Orbwalking.Orbwalker orbwalker)
        {
            var orbwalkerMenu = new Menu("Orbwalker", "zedorbwalker");
            orbwalker = new Orbwalking.Orbwalker(orbwalkerMenu);
            MainMenu.AddSubMenu(orbwalkerMenu);
        }

        private void ComboMenu()
        {
            var prefix = "koreanzed.combomenu";
            var comboMenu = new Menu("Combo", prefix);

            comboMenu.AddItem(new MenuItem(prefix + ".useq", "Use Q").SetValue(true)).ValueChanged += (sender, args) =>
            {
                zedSpells.Q.UseOnCombo = args.GetNewValue<bool>();
            };

            comboMenu.AddItem(new MenuItem(prefix + ".usew", "Use W").SetValue(true)).ValueChanged += (sender, args) =>
            {
                zedSpells.W.UseOnCombo = args.GetNewValue<bool>();
            };

            comboMenu.AddItem(new MenuItem(prefix + ".usee", "Use E").SetValue(true)).ValueChanged += (sender, args) =>
            {
                zedSpells.E.UseOnCombo = args.GetNewValue<bool>();
            };

            comboMenu.AddItem(new MenuItem(prefix + ".user", "Use R").SetValue(true)).ValueChanged += (sender, args) =>
            {
                zedSpells.R.UseOnCombo = args.GetNewValue<bool>();
            };

            var useItemsPrefix = prefix + ".items";
            var useItems = new Menu("Items", useItemsPrefix);

            useItems.AddItem(new MenuItem(useItemsPrefix + ".bilgewater", "Use Bilgewater Cutlass").SetValue(true));
            useItems.AddItem(new MenuItem(useItemsPrefix + ".botrk", "Use BotRK").SetValue(true));
            useItems.AddItem(new MenuItem(useItemsPrefix + ".yomuus", "Use Youmuu's Ghostblade").SetValue(true));
            useItems.AddItem(new MenuItem(useItemsPrefix + ".hydra", "Use Hydra").SetValue(true));
            useItems.AddItem(new MenuItem(useItemsPrefix + ".tiamat", "Use Tiamat").SetValue(true));

            var rBlockSettings = new Menu("Use R Against", prefix + ".neverultmenu");
            var blockUltPrefix = prefix + ".blockult";
            foreach (var objAiHero in HeroManager.Enemies)
                rBlockSettings.AddItem(
                    new MenuItem(
                        string.Format("{0}.{1}", blockUltPrefix, objAiHero.Name.ToLowerInvariant()),
                        objAiHero.Name).SetValue(true));

            comboMenu.AddItem(
                new MenuItem("koreanzed.combo.ronselected", "Use R ONLY on Selected Target").SetValue(false));

            comboMenu.AddItem(new MenuItem(prefix + ".labelcombo1", "To switch the combo style:"));
            comboMenu.AddItem(new MenuItem(prefix + ".labelcombo2", "1 - Hold SHIFT; 2 - LEFT Click on Zed;"));
            comboMenu.AddItem(
                new MenuItem(prefix + ".combostyle", "Combo Style").SetValue(
                    new StringList(new[] { "All Star", "The Line" })));

            comboMenu.AddSubMenu(useItems);
            comboMenu.AddSubMenu(rBlockSettings);
            MainMenu.AddSubMenu(comboMenu);
        }

        private void HarassMenu()
        {
            var prefix = "koreanzed.harasmenu";
            var harasMenu = new Menu("Harass", prefix);

            harasMenu.AddItem(new MenuItem(prefix + ".useq", "Use Q").SetValue(true)).ValueChanged += (sender, args) =>
            {
                zedSpells.Q.UseOnHarass = args.GetNewValue<bool>();
            };

            harasMenu.AddItem(
                new MenuItem(prefix + ".checkcollisiononq", "Check Collision Before Using Q").SetValue(false));

            harasMenu.AddItem(new MenuItem(prefix + ".usee", "Use E").SetValue(true)).ValueChanged += (sender, args) =>
            {
                zedSpells.E.UseOnHarass = args.GetNewValue<bool>();
            };

            var eUsagePrefix = prefix + ".wusage";
            var eHarasUsage = new Menu("W Settings", eUsagePrefix);

            eHarasUsage.AddItem(new MenuItem(prefix + ".usew", "Use W").SetValue(true)).ValueChanged +=
                (sender, args) => { zedSpells.W.UseOnHarass = args.GetNewValue<bool>(); };
            eHarasUsage.AddItem(
                new MenuItem(eUsagePrefix + ".trigger", "Trigger").SetValue(
                    new StringList(new[] { "Max Range", "Max Damage" })));
            eHarasUsage.AddItem(
                new MenuItem(eUsagePrefix + ".dontuseagainst", "Don't Use if Laning Against X Enemies").SetValue(
                    new Slider(6, 2, 6)));
            eHarasUsage.AddItem(
                new MenuItem(eUsagePrefix + ".dontuselowlife", "Don't Use if % HP Below").SetValue(
                    new Slider()));

            var blackListPrefix = prefix + ".blacklist";
            var blackListHaras = new Menu("Harass Target(s)", blackListPrefix + "");
            foreach (var objAiHero in HeroManager.Enemies)
                blackListHaras.AddItem(
                    new MenuItem(
                        string.Format("{0}.{1}", blackListPrefix, objAiHero.Name.ToLowerInvariant()),
                        objAiHero.Name).SetValue(true));

            var useItemsPrefix = prefix + ".items";
            var useItems = new Menu("Use Items", useItemsPrefix);
            useItems.AddItem(new MenuItem(useItemsPrefix + ".hydra", "Use Hydra").SetValue(true));
            useItems.AddItem(new MenuItem(useItemsPrefix + ".tiamat", "Use Tiamat").SetValue(true));

            harasMenu.AddItem(new MenuItem(prefix + ".saveenergy", "Save Energy (%)").SetValue(new Slider(50)));

            harasMenu.AddSubMenu(blackListHaras);
            harasMenu.AddSubMenu(useItems);
            harasMenu.AddSubMenu(eHarasUsage);
            MainMenu.AddSubMenu(harasMenu);
        }

        private void LaneClearMenu()
        {
            var prefix = "koreanzed.laneclearmenu";
            var laneClearMenu = new Menu("Lane / Jungle Clear", prefix);

            laneClearMenu.AddItem(new MenuItem(prefix + ".useq", "Use Q").SetValue(true)).ValueChanged +=
                (sender, args) => { zedSpells.Q.UseOnLaneClear = args.GetNewValue<bool>(); };

            laneClearMenu.AddItem(
                new MenuItem(prefix + ".useqif", "Min. Minions to Use Q").SetValue(new Slider(3, 1, 6)));

            laneClearMenu.AddItem(new MenuItem(prefix + ".usew", "Use W").SetValue(true)).ValueChanged +=
                (sender, args) => { zedSpells.W.UseOnLaneClear = args.GetNewValue<bool>(); };
            laneClearMenu.AddItem(
                new MenuItem(prefix + ".dontuseeif", "Don't Use if Laning Against X Enemies").SetValue(new Slider(6, 1,
                    6)));

            laneClearMenu.AddItem(new MenuItem(prefix + ".usee", "Use E").SetValue(true)).ValueChanged +=
                (sender, args) => { zedSpells.E.UseOnLaneClear = args.GetNewValue<bool>(); };

            laneClearMenu.AddItem(
                new MenuItem(prefix + ".useeif", "Min. Minions to Use E").SetValue(new Slider(3, 1, 6)));

            laneClearMenu.AddItem(new MenuItem(prefix + ".saveenergy", "Save Energy (%)").SetValue(new Slider(40)));

            var useItemsPrefix = prefix + ".items";
            var useItems = new Menu("Use Items", useItemsPrefix);

            useItems.AddItem(new MenuItem(useItemsPrefix + ".hydra", "Use Hydra").SetValue(true));
            useItems.AddItem(new MenuItem(useItemsPrefix + ".tiamat", "Use Tiamat").SetValue(true));
            useItems.AddItem(
                new MenuItem(useItemsPrefix + ".when", "When will hit X minions").SetValue(new Slider(3, 1, 10)));

            laneClearMenu.AddSubMenu(useItems);

            MainMenu.AddSubMenu(laneClearMenu);
        }

        private void LastHitMenu()
        {
            var prefix = "koreanzed.lasthitmenu";
            var lastHitMenu = new Menu("Last Hit", prefix);

            lastHitMenu.AddItem(new MenuItem(prefix + ".useq", "Use Q").SetValue(true)).ValueChanged +=
                (sender, args) => { zedSpells.Q.UseOnLastHit = args.GetNewValue<bool>(); };

            lastHitMenu.AddItem(new MenuItem(prefix + ".usee", "Use E").SetValue(true)).ValueChanged +=
                (sender, args) => { zedSpells.E.UseOnLastHit = args.GetNewValue<bool>(); };

            lastHitMenu.AddItem(
                new MenuItem(prefix + ".useeif", "Min. Minions to Use E").SetValue(new Slider(3, 1, 6)));

            lastHitMenu.AddItem(new MenuItem(prefix + ".saveenergy", "Save Energy (%)").SetValue(new Slider()));

            MainMenu.AddSubMenu(lastHitMenu);
        }

        private void GetInitialSpellValues()
        {
            zedSpells.Q.UseOnCombo = GetParamBool("koreanzed.combomenu.useq");
            zedSpells.W.UseOnCombo = GetParamBool("koreanzed.combomenu.usew");
            zedSpells.E.UseOnCombo = GetParamBool("koreanzed.combomenu.usee");
            zedSpells.R.UseOnCombo = GetParamBool("koreanzed.combomenu.user");

            zedSpells.Q.UseOnHarass = GetParamBool("koreanzed.harasmenu.useq");
            zedSpells.W.UseOnHarass = GetParamBool("koreanzed.harasmenu.usew");
            zedSpells.E.UseOnHarass = GetParamBool("koreanzed.harasmenu.usee");

            zedSpells.Q.UseOnLastHit = GetParamBool("koreanzed.lasthitmenu.useq");
            zedSpells.E.UseOnLastHit = GetParamBool("koreanzed.lasthitmenu.usee");

            zedSpells.Q.UseOnLaneClear = GetParamBool("koreanzed.laneclearmenu.useq");
            zedSpells.W.UseOnLaneClear = GetParamBool("koreanzed.laneclearmenu.usew");
            zedSpells.E.UseOnLaneClear = GetParamBool("koreanzed.laneclearmenu.usee");
        }

        private void MiscMenu()
        {
            var prefix = "koreanzed.miscmenu";
            var miscMenu = new Menu("Miscellaneous", prefix);

            var gcPrefix = prefix + ".gc";
            var antiGapCloserMenu = new Menu("Gapcloser Options", gcPrefix);
            antiGapCloserMenu.AddItem(new MenuItem(prefix + ".usewantigc", "Use W to Escape").SetValue(true));
            antiGapCloserMenu.AddItem(new MenuItem(prefix + ".useeantigc", "Use E to Slow").SetValue(true));

            var rDodgePrefix = prefix + ".rdodge";
            var rDodgeMenu = new Menu("Use R to Dodge", rDodgePrefix);
            rDodgeMenu.AddItem(new MenuItem(rDodgePrefix + ".user", "Active").SetValue(true));
            rDodgeMenu.AddItem(
                new MenuItem(rDodgePrefix + ".dodgeifhealf", "Only if % HP Below").SetValue(new Slider(90)));
            rDodgeMenu.AddItem(new MenuItem(rDodgePrefix + ".label", "Dangerous Spells to Dodge:"));

            string[] neverDodge =
            {
                "shen", "karma", "poppy", "soraka", "janna", "nidalee", "zilean", "yorick",
                "mordekaiser", "vayne", "tryndamere", "trundle", "nasus", "lulu", "masteryi",
                "kennen", "anivia", "heimerdinger", "drmundo", "elise", "fiora", "jax", "kassadin",
                "khazix", "maokai", "fiddlesticks", "poppy", "shaco", "olaf", "alistar", "aatrox",
                "taric", "nunu", "katarina", "rammus", "singed", "twistedfate", "teemo", "sivir",
                "udyr"
            };

            foreach (
                var objAiHero in
                HeroManager.Enemies.Where(hero => !neverDodge.Contains(hero.Name.ToLowerInvariant()))
                    .OrderBy(hero => hero.Name))
                foreach (
                    var spellDataInst in objAiHero.Spellbook.Spells.Where(spell => spell.Slot == SpellSlot.R))
                    rDodgeMenu.AddItem(
                        new MenuItem(
                            rDodgePrefix + "." + spellDataInst.Name.ToLowerInvariant(),
                            objAiHero.Name + " - " + spellDataInst.Name.Replace(objAiHero.Name, "")).SetValue(
                            true));

            var potPrefix = prefix + ".pot";
            var usePotionMenu = new Menu("Use Health Potion", potPrefix);
            usePotionMenu.AddItem(new MenuItem(potPrefix + ".active", "Active").SetValue(true));
            usePotionMenu.AddItem(new MenuItem(potPrefix + ".when", "Use Potion at % HP").SetValue(new Slider(65)));

            miscMenu.AddItem(new MenuItem(prefix + ".flee", "Flee").SetValue(new KeyBind('G', KeyBindType.Press)));

            miscMenu.AddItem(new MenuItem(prefix + ".autoe", "Auto E if any enemy is on range").SetValue(true));

            miscMenu.AddItem(new MenuItem(prefix + ".forceultimate", "Force R Using Mouse Buttons (Cursor Sprite)")
                .SetValue(true));

            miscMenu.AddSubMenu(antiGapCloserMenu);
            miscMenu.AddSubMenu(rDodgeMenu);
            miscMenu.AddSubMenu(usePotionMenu);
            MainMenu.AddSubMenu(miscMenu);
        }

        private void DrawingMenu()
        {
            var prefix = "koreanzed.drawing";
            var drawingMenu = new Menu("Drawings", prefix);

            drawingMenu.AddItem(new MenuItem(prefix + ".damageindicator", "Damage Indicator").SetValue(true));
            drawingMenu.AddItem(
                new MenuItem(prefix + ".damageindicatorcolor", "Color Scheme").SetValue(
                    new StringList(new[] { "Normal", "Colorblind", "Sexy (Beta)" })));
            drawingMenu.AddItem(new MenuItem(prefix + ".killableindicator", "Killable Indicator").SetValue(true));
            drawingMenu.AddItem(new MenuItem(prefix + ".skillranges", "Skill Ranges").SetValue(true));

            MainMenu.AddSubMenu(drawingMenu);
        }

        public List<AIHeroClient> GetBlockList(BlockListType blockListType)
        {
            var blackList = new List<AIHeroClient>();

            switch (blockListType)
            {
                case BlockListType.Harass:
                    foreach (var objAiHero in HeroManager.Enemies)
                        if (!GetParamBool("koreanzed.harasmenu.blacklist." + objAiHero.Name.ToLowerInvariant()))
                            blackList.Add(objAiHero);
                    break;

                case BlockListType.Ultimate:
                    foreach (var objAiHero in HeroManager.Enemies)
                        if (!GetParamBool("koreanzed.combomenu.blockult." + objAiHero.Name.ToLowerInvariant()))
                            blackList.Add(objAiHero);
                    break;
            }

            return blackList;
        }

        public bool GetParamKeyBind(string paramName)
        {
            return MainMenu.Item(paramName).GetValue<KeyBind>().Active;
        }

        public int GetParamSlider(string paramName)
        {
            return MainMenu.Item(paramName).GetValue<Slider>().Value;
        }

        public bool GetParamBool(string paramName)
        {
            return MainMenu.Item(paramName).GetValue<bool>();
        }

        public int GetParamStringList(string paramName)
        {
            return MainMenu.Item(paramName).GetValue<StringList>().SelectedIndex;
        }

        public ComboType GetCombo()
        {
            return (ComboType)GetParamStringList("koreanzed.combomenu.combostyle");
        }

        public void SetCombo(ComboType comboStyle)
        {
            var teste =
                MainMenu.Item("koreanzed.combomenu.combostyle")
                    .SetValue(
                        new StringList(new[] { "All Star", "The Line" }) { SelectedIndex = (int)comboStyle });
        }

        public Color GetParamColor(string paramName)
        {
            return MainMenu.Item(paramName).GetValue<Circle>().Color;
        }

        public bool CheckMenuItem(string paramName)
        {
            return MainMenu.Item(paramName) != null;
        }
    }
}