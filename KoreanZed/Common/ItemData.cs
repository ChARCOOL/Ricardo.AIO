﻿using System.Diagnostics.CodeAnalysis;

namespace EnsoulSharp.Common.Data
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ItemData
    {
        public struct Item
        {
            #region Fields

            /// <summary>
            ///     Item Consumed
            /// </summary>
            public bool Consumed;

            /// <summary>
            ///     Consume Item On Full
            /// </summary>
            public bool ConsumeOnFull;

            /// <summary>
            ///     Item Depth
            /// </summary>
            public int Depth;

            /// <summary>
            ///     Item Disabled Maps
            /// </summary>
            public int[] DisabledMaps;

            public float FlatArmorMod;

            public float FlatAttackSpeedMod;

            public float FlatBlockMod;

            public float FlatCritChanceMod;

            public float FlatCritDamageMod;

            public float FlatEnergyPoolMod;

            public float FlatEnergyRegenMod;

            public float FlatEXPBonus;

            public float FlatHPPoolMod;

            public float FlatHPRegenMod;

            public float FlatMagicDamageMod;

            public float FlatMovementSpeedMod;

            public float FlatMPPoolMod;

            public float FlatMPRegenMod;

            public float FlatPhysicalDamageMod;

            public float FlatSpellBlockMod;

            /// <summary>
            ///     Item From Item Ids
            /// </summary>
            public int[] From;

            /// <summary>
            ///     Gold Base Price
            /// </summary>
            public int GoldBase;

            /// <summary>
            ///     Gold Price
            /// </summary>
            public int GoldPrice;

            /// <summary>
            ///     Gold Sell Price
            /// </summary>
            public int GoldSell;

            /// <summary>
            ///     Item Group
            /// </summary>
            public string Group;

            /// <summary>
            ///     Item Hide from All
            /// </summary>
            public bool HideFromAll;

            /// <summary>
            ///     Item Id
            /// </summary>
            public int Id;

            /// <summary>
            ///     Item In Store
            /// </summary>
            public bool InStore;

            /// <summary>
            ///     Item into Items
            /// </summary>
            public int[] Into;

            /// <summary>
            ///     Item Name
            /// </summary>
            public string Name;

            public float PercentArmorMod;

            public float PercentAttackSpeedMod;

            public float PercentBlockMod;

            public float PercentCritChanceMod;

            public float PercentCritDamageMod;

            public float PercentDodgeMod;

            public float PercentEXPBonus;

            public float PercentHPPoolMod;

            public float PercentHPRegenMod;

            public float PercentLifeStealMod;

            public float PercentMagicDamageMod;

            public float PercentMovementSpeedMod;

            public float PercentMPPoolMod;

            public float PercentMPRegenMod;

            public float PercentPhysicalDamageMod;

            public float PercentSpellBlockMod;

            public float PercentSpellVampMod;

            /// <summary>
            ///     Item Purchasable
            /// </summary>
            public bool Purchasable;

            /// <summary>
            ///     Item Range
            /// </summary>
            public float Range;

            /// <summary>
            ///     Required Champion for Item
            /// </summary>
            public string RequiredChampion;

            public float rFlatArmorModPerLevel;

            public float rFlatArmorPenetrationMod;

            public float rFlatArmorPenetrationModPerLevel;

            public float rFlatCritChanceModPerLevel;

            public float rFlatCritDamageModPerLevel;

            public float rFlatDodgeMod;

            public float rFlatDodgeModPerLevel;

            public float rFlatEnergyModPerLevel;

            public float rFlatEnergyRegenModPerLevel;

            public float rFlatGoldPer10Mod;

            public float rFlatHPModPerLevel;

            public float rFlatHPRegenModPerLevel;

            public float rFlatMagicDamageModPerLevel;

            public float rFlatMagicPenetrationMod;

            public float rFlatMagicPenetrationModPerLevel;

            public float rFlatMovementSpeedModPerLevel;

            public float rFlatMPModPerLevel;

            public float rFlatMPRegenModPerLevel;

            public float rFlatPhysicalDamageModPerLevel;

            public float rFlatSpellBlockModPerLevel;

            public float rFlatTimeDeadMod;

            public float rFlatTimeDeadModPerLevel;

            public float rPercentArmorPenetrationMod;

            public float rPercentArmorPenetrationModPerLevel;

            public float rPercentAttackSpeedModPerLevel;

            public float rPercentCooldownMod;

            public float rPercentCooldownModPerLevel;

            public float rPercentMagicPenetrationMod;

            public float rPercentMagicPenetrationModPerLevel;

            public float rPercentMovementSpeedModPerLevel;

            public float rPercentTimeDeadMod;

            public float rPercentTimeDeadModPerLevel;

            /// <summary>
            ///     Special Recipe
            /// </summary>
            public int SpecialRecipe;

            /// <summary>
            ///     Item Stacks
            /// </summary>
            public int Stacks;

            /// <summary>
            ///     Item Tags
            /// </summary>
            public string[] Tags;

            #endregion Fields

            #region Public Methods and Operators

            public Items.Item GetItem()
            {
                return new Items.Item(Id, Range);
            }

            #endregion Public Methods and Operators
        }

        #region Static Fields

        public static Item Bilgewater_Cutlass = new Item
        {
            Name = "Bilgewater Cutlass",
            Range = 550f,
            GoldBase = 240,
            GoldPrice = 1400,
            GoldSell = 980,
            Purchasable = true,
            Stacks = 1,
            Depth = 3,
            From = new[] { 1036, 1053 },
            Into = new[] { 3146, 3153 },
            InStore = true,
            FlatPhysicalDamageMod = 25f,
            PercentLifeStealMod = 0.08f,
            Tags = new[] { "Active", "Damage", "LifeSteal", "Slow" },
            Id = 3144
        };

        public static Item Blade_of_the_Ruined_King = new Item
        {
            Name = "Blade of the Ruined King",
            Range = 550f,
            GoldBase = 750,
            GoldPrice = 3400,
            GoldSell = 2380,
            Purchasable = true,
            Stacks = 1,
            Depth = 4,
            From = new[] { 3144, 1043 },
            InStore = true,
            FlatPhysicalDamageMod = 25f,
            PercentAttackSpeedMod = 0.4f,
            PercentLifeStealMod = 0.1f,
            Tags = new[] { "Active", "AttackSpeed", "Damage", "LifeSteal", "NonbootsMovement", "OnHit" },
            Id = 3153
        };

        public static Item Youmuus_Ghostblade = new Item
        {
            Name = "Youmuu's Ghostblade",
            GoldBase = 563,
            GoldPrice = 2700,
            GoldSell = 1890,
            Purchasable = true,
            Stacks = 1,
            Depth = 3,
            From = new[] { 3093, 3134 },
            InStore = true,
            FlatPhysicalDamageMod = 30f,
            FlatCritChanceMod = 0.15f,
            Tags = new[]
            {
                "Active", "ArmorPenetration", "AttackSpeed", "CooldownReduction", "CriticalStrike", "Damage",
                "NonbootsMovement"
            },
            Id = 3142
        };

        public static Item Ravenous_Hydra_Melee_Only = new Item
        {
            Name = "Ravenous Hydra (Melee Only)",
            Range = 400f,
            GoldBase = 600,
            GoldPrice = 3300,
            GoldSell = 2310,
            Purchasable = true,
            Stacks = 1,
            Depth = 3,
            From = new[] { 3077, 1053 },
            InStore = true,
            FlatPhysicalDamageMod = 75f,
            PercentLifeStealMod = 0.12f,
            Tags = new[] { "Active", "Damage", "HealthRegen", "LifeSteal", "OnHit" },
            Id = 3074
        };

        public static Item Titanic_Hydra_Melee_Only = new Item
        {
            Name = "Titanic Hydra (Melee Only)",
            Range = 385f,
            GoldBase = 750,
            GoldPrice = 3600,
            GoldSell = 2520,
            Purchasable = true,
            Stacks = 1,
            Depth = 3,
            From = new[] { 3077, 1028, 3052 },
            InStore = true,
            FlatHPPoolMod = 450f,
            FlatPhysicalDamageMod = 50f,
            Tags =
                new[]
                    {"Active", "Damage", "Health", "HealthRegen", "OnHit"},
            Id = 3748
        };

        public static Item Tiamat_Melee_Only = new Item
        {
            Name = "Tiamat (Melee Only)",
            Range = 400f,
            GoldBase = 305,
            GoldPrice = 1900,
            GoldSell = 1330,
            Purchasable = true,
            Stacks = 1,
            Depth = 2,
            From = new[] { 1037, 1036, 1006, 1006 },
            Into = new[] { 3074 },
            InStore = true,
            FlatPhysicalDamageMod = 40f,
            Tags = new[] { "Active", "Damage", "HealthRegen", "OnHit" },
            Id = 3077
        };

        public static Item Archangels_Staff = new Item
        {
            Name = "Archangel's Staff",
            GoldBase = 1100,
            GoldPrice = 3100,
            GoldSell = 2170,
            Purchasable = true,
            Stacks = 1,
            Depth = 3,
            From = new[] { 3070, 1058 },
            Into = new[] { 3040 },
            InStore = true,
            FlatMPPoolMod = 250f,
            FlatMPRegenMod = 0.5f,
            FlatMagicDamageMod = 80f,
            Tags = new[] { "Mana", "ManaRegen", "SpellDamage" },
            Id = 3003
        };

        public static Item Archangels_Staff_Crystal_Scar = new Item
        {
            Name = "Archangel's Staff (Crystal Scar)",
            GoldBase = 1100,
            GoldPrice = 3100,
            GoldSell = 2170,
            Purchasable = true,
            Stacks = 1,
            Depth = 3,
            From = new[] { 3070, 1058 },
            Into = new[] { 3040 },
            InStore = true,
            FlatMPPoolMod = 250f,
            FlatMPRegenMod = 0.5f,
            FlatMagicDamageMod = 80f,
            Tags = new[] { "Mana", "ManaRegen", "SpellDamage" },
            Id = 3007
        };

        public static Item Manamune = new Item
        {
            Name = "Manamune",
            GoldBase = 775,
            GoldPrice = 2400,
            GoldSell = 1680,
            Purchasable = true,
            Stacks = 1,
            Depth = 3,
            From = new[] { 3070, 1037 },
            Into = new[] { 3042 },
            InStore = true,
            FlatMPPoolMod = 250f,
            FlatMPRegenMod = 0.25f,
            FlatPhysicalDamageMod = 25f,
            Tags = new[] { "Damage", "Mana", "ManaRegen", "OnHit" },
            Id = 3004
        };

        public static Item Manamune_Crystal_Scar = new Item
        {
            Name = "Manamune (Crystal Scar)",
            GoldBase = 775,
            GoldPrice = 2400,
            GoldSell = 1680,
            Purchasable = true,
            Stacks = 1,
            Depth = 3,
            From = new[] { 3070, 1037 },
            Into = new[] { 3042 },
            InStore = true,
            FlatMPPoolMod = 250f,
            FlatMPRegenMod = 0.25f,
            FlatPhysicalDamageMod = 25f,
            Tags = new[] { "Damage", "Mana", "ManaRegen", "OnHit" },
            Id = 3008
        };

        public static Item Muramana = new Item
        {
            Name = "Muramana",
            GoldBase = 2200,
            GoldPrice = 2200,
            GoldSell = 3080,
            Stacks = 1,
            Depth = 4,
            From = new[] { 3004 },
            SpecialRecipe = 3004,
            InStore = false,
            FlatMPPoolMod = 1000f,
            FlatPhysicalDamageMod = 25f,
            Tags = new[] { "OnHit" },
            Id = 3042
        };

        public static Item Muramana2 = new Item
        {
            Name = "Muramana",
            GoldBase = 2200,
            GoldPrice = 2200,
            GoldSell = 3080,
            Stacks = 1,
            Depth = 4,
            From = new[] { 3008 },
            SpecialRecipe = 3008,
            InStore = false,
            FlatMPPoolMod = 1000f,
            FlatPhysicalDamageMod = 25f,
            Tags = new[] { "OnHit" },
            Id = 3043
        };

        public static Item Seraphs_Embrace = new Item
        {
            Name = "Seraph's Embrace",
            GoldBase = 2700,
            GoldPrice = 2700,
            GoldSell = 3780,
            Stacks = 1,
            Depth = 4,
            From = new[] { 3003 },
            SpecialRecipe = 3003,
            InStore = false,
            FlatMPPoolMod = 1000f,
            FlatMagicDamageMod = 80f,
            Tags = new[] { "Active" },
            Id = 3040
        };

        public static Item Seraphs_Embrace2 = new Item
        {
            Name = "Seraph's Embrace",
            GoldBase = 2700,
            GoldPrice = 2700,
            GoldSell = 3780,
            Stacks = 1,
            Depth = 4,
            From = new[] { 3007 },
            SpecialRecipe = 3007,
            InStore = false,
            FlatMPPoolMod = 1000f,
            FlatMagicDamageMod = 60f,
            Tags = new[] { "Active" },
            Id = 3048
        };

        public static Item Tear_of_the_Goddess = new Item
        {
            Name = "Tear of the Goddess",
            GoldBase = 140,
            GoldPrice = 720,
            GoldSell = 504,
            Purchasable = true,
            Stacks = 1,
            Depth = 2,
            From = new[] { 1027, 1004 },
            Into = new[] { 3003, 3004 },
            InStore = true,
            FlatMPPoolMod = 250f,
            Tags = new[] { "Mana", "ManaRegen" },
            Id = 3070
        };

        public static Item Tear_of_the_Goddess_Crystal_Scar = new Item
        {
            Name = "Tear of the Goddess (Crystal Scar)",
            GoldBase = 140,
            GoldPrice = 720,
            GoldSell = 504,
            Purchasable = true,
            Stacks = 1,
            Depth = 2,
            From = new[] { 1027, 1004 },
            Into = new[] { 3007, 3008 },
            InStore = true,
            FlatMPPoolMod = 250f,
            Tags = new[] { "Mana", "ManaRegen" },
            Id = 3073
        };

        #endregion Static Fields
    }
}