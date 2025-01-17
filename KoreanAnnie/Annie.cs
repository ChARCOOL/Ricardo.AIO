using EnsoulSharp;
using EnsoulSharp.Common;
using KoreanAnnie.Common;
using System;

namespace KoreanAnnie
{
    internal class Annie : ICommonChampion
    {
        #region Constants

        private const string MenuDisplay = "Korean Annie";

        #endregion Constants

        #region Constructors and Destructors

        public Annie()
        {
            Player = ObjectManager.Player;
            MainMenu = new CommonMenu(MenuDisplay, true);
            Orbwalker = MainMenu.Orbwalker;
            AnnieCustomMenu.Load(MainMenu);

            LoadLambdaExpressions();

            Spells = new CommonSpells(this);
            AnnieSpells.Load(this);

            Buttons = new AnnieButtons(this);
            AnnieOrbwalker = new AnnieCore(this);

            Draws = new AnnieDrawings(this);
            DrawDamage = new CommonDamageDrawing(this);
            DisableAA = new CommonDisableAA(this);
            ForceUltimate = new CommonForceUltimate(this);
            UltimateRange = Spells.R.Range;
            ForceUltimate.ForceUltimate = AnnieOrbwalker.Ultimate;
            //DrawDamage.AmountOfDamage = Spells.MaxComboDamage;
            //DrawDamage.Active = true;

            Tibbers = new AnnieTibbers(this);

            AIBaseClient.OnProcessSpellCast += EAgainstEnemyAA;
            Interrupters.OnInterrupter += InterruptOnPossibleToInterruptDangerousSpells;
            Gapclosers.OnGapcloser += OnGapcloser;
            Game.OnUpdate += StackE;
            AIHeroClient.OnLevelUp += EvolveUltimate;
        }

        #endregion Constructors and Destructors

        #region Fields

        public Func<bool> CanFarm;

        public Func<bool> CheckStun;

        public Func<string, bool> GetParamBool;

        public Func<string, bool> GetParamKeyBind;

        public Func<string, int> GetParamSlider;

        public Func<string, string> ParamName;

        public Func<bool> SaveStun;

        public Action<string, bool> SetValueBool;

        #endregion Fields

        #region Public Properties

        public AnnieCore AnnieOrbwalker { get; set; }
        public AnnieButtons Buttons { get; set; }
        public CommonDisableAA DisableAA { get; set; }
        public CommonDamageDrawing DrawDamage { get; set; }
        public AnnieDrawings Draws { get; set; }
        public CommonForceUltimate ForceUltimate { get; set; }
        public CommonMenu MainMenu { get; set; }
        public Orbwalking.Orbwalker Orbwalker { get; set; }
        public AIHeroClient Player { get; set; }
        public CommonSpells Spells { get; set; }
        public AnnieTibbers Tibbers { get; set; }
        public float UltimateRange { get; set; }

        #endregion Public Properties

        #region Methods

        private void LoadLambdaExpressions()
        {
            ParamName = s => KoreanUtils.ParamName(MainMenu, s);
            GetParamBool = s => KoreanUtils.GetParamBool(MainMenu, s);
            SetValueBool = (s, b) => KoreanUtils.SetValueBool(MainMenu, s, b);
            GetParamSlider = s => KoreanUtils.GetParamSlider(MainMenu, s);
            GetParamKeyBind = s => KoreanUtils.GetParamKeyBind(MainMenu, s);
            CanFarm = () =>
                !GetParamBool("supportmode") || GetParamBool("supportmode") && Player.CountAlliesInRange(1500f) == 1;
            CheckStun = () => Player.HasBuff("anniepassiveprimed");
            SaveStun = () => CheckStun() && GetParamBool("savestunforcombo");
        }

        private void EvolveUltimate(AIBaseClient sender, EventArgs args)
        {
            if (sender.IsMe) sender.Spellbook.EvolveSpell(SpellSlot.R);
        }

        private void EAgainstEnemyAA(AIBaseClient sender, AIBaseClientProcessSpellCastEventArgs args)
        {
            if (GetParamBool("useeagainstaa") && !sender.IsMe && sender.IsEnemy && sender is AIHeroClient &&
                args.Target != null && args.Target.IsMe && Player.Distance(args.End) < 440 &&
                args.SData.Name.ToLowerInvariant().Contains("attack")) Spells.E.Cast();
        }

        private void InterruptOnPossibleToInterruptDangerousSpells(ActiveInterrupter interrupter)
        {
            if (interrupter.Sender == null) return;
            if (GetParamBool("interruptspells") && CheckStun() && interrupter.Sender.IsEnemy &&
                interrupter.DangerLevel > InterrupterDangerLevel.Medium)
            {
                if (Spells.Q.IsReady() && interrupter.Sender.IsValidTarget(Spells.Q.Range))
                    Spells.Q.Cast(interrupter.Sender);
                if (Spells.W.IsReady() && interrupter.Sender.IsValidTarget(Spells.W.Range))
                    Spells.W.Cast(interrupter.Sender);
            }
        }

        private void StackE(EventArgs args)
        {
            if (!Player.IsRecalling() && !CheckStun() && GetParamBool("useetostack") &&
                Player.ManaPercent > GetParamSlider("manalimitforstacking") &&
                Spells.E.IsReady()) Utility.DelayAction.Add(100, () => Spells.E.Cast());
        }

        private void OnGapcloser(ActiveGapcloser gapcloser)
        {
            if (gapcloser.Sender == null) return;
            if (GetParamBool("antigapcloser") && CheckStun())
            {
                if (Spells.Q.IsReady() && gapcloser.Sender.IsValidTarget(Spells.Q.Range))
                    Spells.Q.Cast(gapcloser.Sender);
                else if (Spells.W.IsReady() && gapcloser.Sender.IsValidTarget(Spells.W.Range))
                    Spells.W.Cast(gapcloser.Sender);
            }
        }

        #endregion Methods
    }
}