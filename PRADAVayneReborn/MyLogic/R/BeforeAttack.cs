using EnsoulSharp;
using EnsoulSharp.Common;
using PRADA_Vayne.MyUtils;
using System.Linq;

namespace PRADA_Vayne.MyLogic.R
{
    public static partial class Events
    {
        public static void BeforeAttack(Orbwalking.BeforeAttackEventArgs args)
        {
            if (args.Unit.IsMe || Program.Q.IsReady() || Program.ComboMenu.Item("QCombo").GetValue<bool>())
                if (ObjectManager.Player.HasBuff("vaynetumblefade") &&
                    Program.EscapeMenu.Item("QUlt").GetValue<bool>() && Heroes.EnemyHeroes.Any(h =>
                        h.IsMelee && h.Distance(Heroes.Player) < h.AttackRange + h.BoundingRadius))
                    args.Process = false;
        }
    }
}