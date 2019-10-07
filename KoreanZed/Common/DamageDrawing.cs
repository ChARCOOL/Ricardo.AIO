using EnsoulSharp;
using EnsoulSharp.Common;
using SharpDX;
using System;
using System.Linq;
using Color = System.Drawing.Color;

namespace KoreanZed.Common
{
    internal class CommonDamageDrawing
    {
        public delegate float DrawDamageDelegate(AIHeroClient hero);

        private const int Width = 103;

        private const int Height = 8;

        private readonly Render.Text
            killableText = new Render.Text(0, 0, "KILLABLE", 20, new ColorBGRA(255, 0, 0, 255));

        private readonly ZedMenu zedMenu;

        public bool Active = true;

        private DrawDamageDelegate amountOfDamage;

        public CommonDamageDrawing(ZedMenu zedMenu)
        {
            this.zedMenu = zedMenu;
        }

        public DrawDamageDelegate AmountOfDamage
        {
            get { return amountOfDamage; }

            set
            {
                if (amountOfDamage == null) Drawing.OnDraw += DrawDamage;
                amountOfDamage = value;
            }
        }

        private bool Enabled()
        {
            return Active && amountOfDamage != null
                          && (zedMenu.GetParamBool("koreanzed.drawing.damageindicator")
                              || zedMenu.GetParamBool("koreanzed.drawing.killableindicator"));
        }

        private void DrawDamage(EventArgs args)
        {
            var color = Color.Gray;
            var barColor = Color.White;

            if (zedMenu.GetParamStringList("koreanzed.drawing.damageindicatorcolor") == 1)
            {
                color = Color.Gold;
                barColor = Color.Olive;
            }
            else if (zedMenu.GetParamStringList("koreanzed.drawing.damageindicatorcolor") == 2)
            {
                color = Color.FromArgb(100, Color.Black);
                barColor = Color.Lime;
            }

            if (Enabled())
                foreach (
                    var champ in
                    ObjectManager.Get<AIHeroClient>()
                        .Where(h => h.IsVisible && h.IsEnemy && h.IsValid && h.IsHPBarRendered))
                {
                    var damage = amountOfDamage(champ);

                    if (damage > 0)
                    {
                        var pos = champ.HPBarPosition;

                        if (zedMenu.GetParamBool("koreanzed.drawing.killableindicator")
                            && damage > champ.Health + 50f)
                        {
                            Render.Circle.DrawCircle(champ.Position, 100, Color.Red);
                            Render.Circle.DrawCircle(champ.Position, 75, Color.Red);
                            Render.Circle.DrawCircle(champ.Position, 50, Color.Red);
                            killableText.X = (int)pos.X + 40;
                            killableText.Y = (int)pos.Y - 20;
                            killableText.OnEndScene();
                        }

                        if (zedMenu.GetParamBool("koreanzed.drawing.damageindicator"))
                        {
                            var healthAfterDamage = Math.Max(0, champ.Health - damage) / champ.MaxHealth;
                            var posY = pos.Y + 20f;
                            var posDamageX = pos.X + 12f + Width * healthAfterDamage;
                            var posCurrHealthX = pos.X + 12f + Width * champ.Health / champ.MaxHealth;

                            var diff = posCurrHealthX - posDamageX + 3;

                            var pos1 = pos.X + 8 + 107 * healthAfterDamage;

                            for (var i = 0; i < diff - 3; i++)
                                Drawing.DrawLine(pos1 + i, posY, pos1 + i, posY + Height, 1, color);

                            Drawing.DrawLine(posDamageX, posY, posDamageX, posY + Height, 2, barColor);
                        }
                    }
                }
        }
    }
}