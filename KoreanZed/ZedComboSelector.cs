using EnsoulSharp;
using EnsoulSharp.Common;
using KoreanZed.Enumerators;
using Ricardo.AIO.Properties;
using SharpDX;
using System;

namespace KoreanZed
{
    internal class ZedComboSelector
    {
        private readonly Render.Sprite allStarImage;

        private readonly Render.Sprite theLineImage;
        private readonly ZedMenu zedMenu;

        private bool animating;

        private int i;

        public ZedComboSelector(ZedMenu zedMenu)
        {
            this.zedMenu = zedMenu;

            theLineImage = new Render.Sprite(Resources.ZedTheLine, new Vector2(1F, 1F));
            theLineImage.Scale = new Vector2(0.9f, 0.9f);
            theLineImage.Add();
            theLineImage.Visible = false;

            allStarImage = new Render.Sprite(Resources.ZedStar, new Vector2(1F, 1F));
            allStarImage.Scale = new Vector2(0.9f, 0.9f);
            allStarImage.Add();
            allStarImage.Visible = false;

            Game.OnWndProc += Game_OnWndProc;
        }

        public ComboType ComboStyle
        {
            get { return zedMenu.GetCombo(); }

            set { zedMenu.SetCombo(value); }
        }

        private void Game_OnWndProc(WndEventArgs args)
        {
            if (animating) return;

            if (args.WParam == 4 && MouseOnChampion() && args.Msg == (uint)WindowsMessages.WM_LBUTTONUP)
            {
                if (ComboStyle == ComboType.AllStar)
                {
                    TheLineAnimation();
                    ComboStyle = ComboType.TheLine;
                }
                else
                {
                    AllStarAnimation();
                    ComboStyle = ComboType.AllStar;
                }
            }
        }

        public void TheLineAnimation()
        {
            Game.OnUpdate -= ShowAllStart;
            Game.OnUpdate -= HideAllStar;
            Game.OnUpdate -= HideTheLine;
            i = -213;
            Game.OnUpdate += ShowTheLine;
        }

        public void AllStarAnimation()
        {
            Game.OnUpdate -= ShowTheLine;
            Game.OnUpdate -= HideAllStar;
            Game.OnUpdate -= HideTheLine;
            i = -213;
            Game.OnUpdate += ShowAllStart;
        }

        private void ShowTheLine(EventArgs args)
        {
            animating = true;
            i += 5;

            theLineImage.Position = new Vector2(1, i);
            theLineImage.Visible = true;

            if (i >= 1)
            {
                Game.OnUpdate -= ShowTheLine;
                Utility.DelayAction.Add(2000, () => Game.OnUpdate += HideTheLine);
            }
        }

        private void HideTheLine(EventArgs args)
        {
            i -= 5;

            theLineImage.Position = new Vector2(1, i);
            theLineImage.Visible = true;

            if (i <= -210)
            {
                Game.OnUpdate -= HideTheLine;
                theLineImage.Visible = false;
                animating = false;
            }
        }

        private void ShowAllStart(EventArgs args)
        {
            animating = true;
            i += 5;

            allStarImage.Position = new Vector2(1, i);
            allStarImage.Visible = true;

            if (i >= 1)
            {
                Game.OnUpdate -= ShowAllStart;
                Utility.DelayAction.Add(2000, () => Game.OnUpdate += HideAllStar);
            }
        }

        private void HideAllStar(EventArgs args)
        {
            i -= 5;

            allStarImage.Position = new Vector2(1, i);
            allStarImage.Visible = true;

            if (i <= -210)
            {
                Game.OnUpdate -= HideAllStar;
                allStarImage.Visible = false;
                animating = false;
            }
        }

        private static bool MouseOnChampion()
        {
            var pos = Utils.GetCursorPos();
            var champPos = ObjectManager.Player.HPBarPosition;

            return pos.X >= champPos.X && pos.X <= champPos.X + 140F
                                       && pos.Y >= champPos.Y && pos.Y <= champPos.Y + 140F;
        }
    }
}