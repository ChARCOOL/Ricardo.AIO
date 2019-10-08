using EnsoulSharp;
using EnsoulSharp.Common;
using SharpDX;
using System.Linq;

namespace mztikksCassiopeia.Extensions
{
    public static class VectorExts
    {
        #region Public Methods and Operators

        public static bool UnderAllyTurret(this Vector3 position)
        {
            return ObjectManager.Get<AITurretClient>()
                .Where(x => !x.IsDead && x.Team == ObjectManager.Player.Team)
                .Any(x => x.Distance(position) < 775);
        }

        public static bool UnderEnemyTurret(this Vector3 position)
        {
            return ObjectManager.Get<AITurretClient>()
                .Where(x => !x.IsDead && x.Team != ObjectManager.Player.Team)
                .Any(x => x.Distance(position) < 775);
        }

        public static Vector2 CenterPoint(this Vector2[] v2Arr)
        {
            float totalX = 0, totalY = 0;
            foreach (var p in v2Arr)
            {
                totalX += p.X;
                totalY += p.Y;
            }

            var centerX = totalX / v2Arr.Length;
            var centerY = totalY / v2Arr.Length;
            return new Vector2(centerX, centerY);
        }

        #endregion Public Methods and Operators
    }
}