using EnsoulSharp.Common;
using KoreanZed.Common;

namespace KoreanZed
{
    internal class Zed
    {
        private readonly CommonDamageDrawing damageDrawing;

        private readonly ZedEnergyChecker energy;

        private readonly CommonForceUltimate forceUltimate;

        private readonly ZedAntiGapCloser zedAntiGapCloser;

        private readonly ZedAutoE zedAutoE;

        private readonly ZedCore zedCore;

        private readonly ZedDrawingSpells zedDrawingSpells;

        private readonly ZedFlee zedFlee;

        private readonly ZedKS zedKs;
        private readonly ZedMenu zedMenu;

        private readonly Orbwalking.Orbwalker zedOrbwalker;

        private readonly ZedPotions zedPotions;

        private readonly ZedShadows zedShadows;

        private readonly ZedSpellDodge zedSpellDodge;

        private readonly ZedSpells zedSpells;

        private readonly ZedUnderTurretFarm zedUnderTurretFarm;

        public Zed()
        {
            zedSpells = new ZedSpells();
            zedMenu = new ZedMenu(zedSpells, out zedOrbwalker);
            energy = new ZedEnergyChecker(zedMenu);
            zedShadows = new ZedShadows(zedMenu, zedSpells, energy);
            zedCore = new ZedCore(zedSpells, zedOrbwalker, zedMenu, zedShadows, energy);
            zedAntiGapCloser = new ZedAntiGapCloser(zedMenu, zedSpells, zedShadows);
            zedPotions = new ZedPotions(zedMenu);
            zedKs = new ZedKS(zedSpells, zedOrbwalker, zedShadows);
            zedSpellDodge = new ZedSpellDodge(zedSpells, zedMenu);
            zedDrawingSpells = new ZedDrawingSpells(zedMenu, zedSpells);
            zedUnderTurretFarm = new ZedUnderTurretFarm(zedSpells, zedOrbwalker);
            damageDrawing = new CommonDamageDrawing(zedMenu) { AmountOfDamage = zedCore.ComboDamage };
            forceUltimate = new CommonForceUltimate(zedMenu, zedSpells, zedOrbwalker)
            { ForceUltimate = zedCore.ForceUltimate };
            zedAutoE = new ZedAutoE(zedMenu, zedShadows, zedSpells);
            zedFlee = new ZedFlee(zedMenu, zedShadows);
        }
    }
}