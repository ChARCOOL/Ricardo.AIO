using EnsoulSharp;
using EnsoulSharp.Common;
using System;

namespace KoreanZed.QueueActions
{
    internal class ActionQueueCheckAutoAttack
    {
        private bool status;

        public ActionQueueCheckAutoAttack()
        {
            status = false;
            Game.OnUpdate += Game_OnUpdate;
            Orbwalking.AfterAttack += Orbwalking_AfterAttack;
        }

        public bool Status
        {
            get
            {
                if (status)
                {
                    status = false;
                    return true;
                }

                return false;
            }
        }

        private void Orbwalking_AfterAttack(AttackableUnit unit, AttackableUnit target)
        {
            if (!unit.IsMe) return;

            status = true;
            Utility.DelayAction.Add(100, () => status = false);
        }

        private void Game_OnUpdate(EventArgs args)
        {
        }
    }
}