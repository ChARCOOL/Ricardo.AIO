using EnsoulSharp;
using EnsoulSharp.Common;
using System;
using System.Linq;

namespace KoreanZed
{
    internal class ZedSpellDodge
    {
        private readonly AIHeroClient player;
        private readonly ZedSpell r;

        private readonly ZedMenu zedMenu;

        public ZedSpellDodge(ZedSpells spells, ZedMenu mainMenu)
        {
            r = spells.R;
            zedMenu = mainMenu;
            player = ObjectManager.Player;

            AIBaseClient.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        private void Obj_AI_Base_OnProcessSpellCast(AIBaseClient sender, AIBaseClientProcessSpellCastEventArgs args)
        {
            if (sender == null || args == null || !r.IsReady() || r.Instance.ToggleState != 0
                || !player.GetEnemiesInRange(r.Range).Any() || args.Slot != SpellSlot.R || !sender.IsChampion()
                || !sender.IsEnemy || !zedMenu.GetParamBool("koreanzed.miscmenu.rdodge.user") ||
                !zedMenu.CheckMenuItem("koreanzed.miscmenu.rdodge." + args.SData.Name.ToLowerInvariant()))
                return;

            if ((args.Target != null && args.Target.IsMe ||
                 player.Distance(args.End) < Math.Max(args.SData.CastRadius, args.SData.LineWidth)) &&
                zedMenu.GetParamBool("koreanzed.miscmenu.rdodge." + args.SData.Name.ToLowerInvariant()))
            {
                var delay = (int)Math.Truncate(player.Distance(sender) / args.SData.MissileSpeed) - 1;
                Utility.DelayAction.Add(delay, () => { r.Cast(TargetSelector.GetTarget(r.Range, r.DamageType)); });
            }
        }
    }
}