using EnsoulSharp;
using EnsoulSharp.Common;
using Ricardo.AIO.Properties;
using SharpDX;
using System;

namespace KoreanAnnie.Common
{
    internal class CommonForceUltimate
    {
        #region Delegates

        public delegate void ForceUltimateDelegate();

        #endregion Delegates

        #region Constructors and Destructors

        public CommonForceUltimate(ICommonChampion champion)
        {
            this.champion = champion;

            MouseImage1 = new Render.Sprite(Resources.Mouse1, new Vector2(0, 0));
            MouseImage1.Scale = new Vector2(0.50f, 0.50f);
            MouseImage1.Add();

            MouseImage2 = new Render.Sprite(Resources.Mouse2, new Vector2(0, 0));
            MouseImage2.Scale = new Vector2(0.50f, 0.50f);
            MouseImage2.Add();

            DenyMouseImage = new Render.Sprite(Resources.DenyMouse, new Vector2(0, 0));
            DenyMouseImage.Scale = new Vector2(0.50f, 0.50f);
            DenyMouseImage.Add();
            DenyMouseImage.Visible = false;

            text.Add();
            text.Visible = false;

            Game.OnWndProc += CheckMouseButtons;
            Game.OnUpdate += ShowAnimation;
        }

        #endregion Constructors and Destructors

        #region Public Properties

        public ForceUltimateDelegate ForceUltimate { get; set; }

        #endregion Public Properties

        #region Fields

        private readonly ICommonChampion champion;

        private readonly Render.Text
            text = new Render.Text(0, 0, "No enemies found", 20, new ColorBGRA(255, 0, 0, 255));

        private int j, k;

        private bool leftButtonDown;

        private bool rightButtonDown;

        #endregion Fields

        #region Properties

        private Render.Sprite DenyMouseImage { get; }
        private Render.Sprite MouseImage1 { get; }
        private Render.Sprite MouseImage2 { get; }

        #endregion Properties

        #region Methods

        private void CheckMouseButtons(WndEventArgs args)
        {
            if (UltimateUp())
            {
                switch (args.Msg)
                {
                    case (uint)WindowsMessages.WM_LBUTTONDOWN:
                        leftButtonDown = true;
                        break;

                    case (uint)WindowsMessages.WM_RBUTTONDOWN:
                        rightButtonDown = true;
                        break;

                    case (uint)WindowsMessages.WM_LBUTTONUP:
                        leftButtonDown = false;
                        break;

                    case (uint)WindowsMessages.WM_RBUTTONUP:
                        rightButtonDown = false;
                        break;
                }

                if (leftButtonDown && rightButtonDown && ForceUltimate != null)
                {
                    if (TargetSelector.GetTarget(champion.UltimateRange, TargetSelector.DamageType.Magical) == null)
                    {
                        DenyMouseImage.Visible = true;
                        text.Visible = true;
                        text.OnEndScene();
                        k = 0;
                    }
                    else
                    {
                        ForceUltimate();
                    }
                }
            }
            else
            {
                leftButtonDown = false;
                rightButtonDown = false;
            }
        }

        private void ShowAnimation(EventArgs args)
        {
            if (UltimateUp())
            {
                j++;

                var pos = Utils.GetCursorPos();
                pos.X -= MouseImage1.Width * 1.2f;

                MouseImage1.Position = pos;

                MouseImage2.Position = pos;

                var pos2 = Utils.GetCursorPos();
                pos2.X -= DenyMouseImage.Width;

                if (DenyMouseImage.Visible) k++;

                DenyMouseImage.Position = pos2;

                text.X = (int)(pos2.X - 32);
                text.Y = (int)(pos2.Y + 50);
                text.OnEndScene();

                if (j == 30 && MouseImage1.Visible)
                {
                    j = 0;
                    MouseImage1.Visible = false;
                    MouseImage2.Visible = true;
                }
                else if (j == 30 && !MouseImage1.Visible)
                {
                    j = 0;
                    MouseImage1.Visible = true;
                    MouseImage2.Visible = false;
                }

                if (k == 70)
                {
                    text.Visible = false;
                    DenyMouseImage.Visible = false;
                    k = 0;
                }
            }
            else
            {
                MouseImage1.PositionUpdate = null;
                MouseImage2.PositionUpdate = null;
                DenyMouseImage.Visible = false;
                DenyMouseImage.PositionUpdate = null;
                MouseImage1.Visible = false;
                MouseImage2.Visible = false;
                text.Visible = false;
                k = 0;
            }
        }

        private bool UltimateUp()
        {
            var b = ObjectManager.Player.GetSpell(SpellSlot.R).IsReady() &&
                    champion.MainMenu.Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo;
            b = b && KoreanUtils.GetParamBool(champion.MainMenu, "forceultusingmouse");

            if (b && champion is Annie && ((Annie)champion).Tibbers.Tibbers != null) b = false;

            return b;
        }

        #endregion Methods
    }
}