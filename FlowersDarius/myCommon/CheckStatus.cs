﻿using EnsoulSharp;
using EnsoulSharp.Common;

namespace myCommon
{
    public static class CheckStatus
    {
        public static bool Check(this AIBaseClient target, float range = float.MaxValue)
        {
            if (target == null) return false;

            if (target.Distance(ObjectManager.Player) > range) return false;

            if (target.HasBuff("KindredRNoDeathBuff")) return false;

            if (target.HasBuff("UndyingRage") && target.GetBuff("UndyingRage").EndTime - Game.Time > 0.3) return false;

            if (target.HasBuff("JudicatorIntervention")) return false;

            if (target.HasBuff("ChronoShift") && target.GetBuff("ChronoShift").EndTime - Game.Time > 0.3) return false;

            if (target.HasBuff("ShroudofDarkness")) return false;

            if (target.HasBuff("SivirShield")) return false;

            return !target.HasBuff("FioraW");
        }

        public static bool IsUnKillable(this AIBaseClient target)
        {
            if (target == null) return true;

            if (target.HasBuff("KindredRNoDeathBuff")) return true;

            if (target.HasBuff("UndyingRage") && target.GetBuff("UndyingRage").EndTime - Game.Time > 0.3) return true;

            if (target.HasBuff("JudicatorIntervention")) return true;

            if (target.HasBuff("ChronoShift") && target.GetBuff("ChronoShift").EndTime - Game.Time > 0.3) return true;

            if (target.HasBuff("ShroudofDarkness")) return true;

            if (target.HasBuff("SivirShield")) return true;

            if (target.HasBuff("itemmagekillerveil")) return true;

            return target.HasBuff("FioraW");
        }

        public static bool HaveShiled(this AIHeroClient target)
        {
            if (target.HasBuff("BlackShield")) return true;

            if (target.HasBuff("bansheesveil")) return true;

            if (target.HasBuff("SivirE")) return true;

            if (target.HasBuff("NocturneShroudofDarkness")) return true;

            if (target.HasBuff("itemmagekillerveil")) return true;

            return false;
        }

        public static bool CanMoveMent(this AIHeroClient target)
        {
            return !(target.MoveSpeed < 50) && !target.IsStunned && !target.HasBuffOfType(BuffType.Stun) &&
                   !target.HasBuffOfType(BuffType.Fear) && !target.HasBuffOfType(BuffType.Snare) &&
                   !target.HasBuffOfType(BuffType.Knockup) && !target.HasBuff("Recall") &&
                   !target.HasBuffOfType(BuffType.Knockback)
                   && !target.HasBuffOfType(BuffType.Charm) && !target.HasBuffOfType(BuffType.Taunt) &&
                   !target.HasBuffOfType(BuffType.Suppression) && (!target.IsCastingInterruptableSpell()
                                                                   || target.IsMoving) &&
                   !target.HasBuff("zhonyasringshield") && !target.HasBuff("bardrstasis");
        }

        public static bool Compare(this GameObject gameObject, GameObject @object)
        {
            return gameObject != null && gameObject.IsValid && @object != null && @object.IsValid &&
                   gameObject.NetworkId == @object.NetworkId;
        }
    }
}