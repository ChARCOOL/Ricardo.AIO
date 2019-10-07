using EnsoulSharp;
using EnsoulSharp.Common;
using KoreanZed.Enumerators;
using KoreanZed.QueueActions;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KoreanZed
{
    internal class ZedShadows
    {
        private readonly ZedSpell e;

        private readonly ZedEnergyChecker energy;

        private readonly ZedSpell q;

        private readonly ZedSpell w;
        private readonly ZedMenu zedMenu;

        private float buffTime;

        private float lastTimeCast;

        public ZedShadows(ZedMenu menu, ZedSpells spells, ZedEnergyChecker energy)
        {
            zedMenu = menu;
            q = spells.Q;
            w = spells.W;
            e = spells.E;
            this.energy = energy;

            Game.OnUpdate += Game_OnUpdate;
        }

        public bool CanCast
        {
            get
            {
                var currentShadows = GetShadows().Count();
                return !ObjectManager.Player.HasBuff("zedwhandler") && w.IsReady() && Game.Time > lastTimeCast + 0.3F
                       && Game.Time > buffTime + 1F && w.IsReady() && w.Instance.ToggleState == 0
                       && !ObjectManager.Player.HasBuff("zedwhandler")
                       && (ObjectManager.Player.HasBuff("zedr2") && currentShadows == 1 || currentShadows == 0);
            }
        }

        public bool CanSwitch
        {
            get
            {
                return !CanCast && w.Instance.ToggleState != 0 && w.IsReady()
                       && !ObjectManager.Get<AITurretClient>()
                           .Any(ob => ob.Distance((Vector2)Instance.Position) < 775F && ob.IsEnemy && !ob.IsDead);
            }
        }

        public AIBaseClient Instance
        {
            get
            {
                var shadow = GetShadows().FirstOrDefault();
                if (shadow != null)
                    return shadow;
                return ObjectManager.Player;
            }
        }

        private void Game_OnUpdate(EventArgs args)
        {
            if (ObjectManager.Player.HasBuff("zedwhandler")) buffTime = Game.Time;
        }

        public void Cast(Vector3 position)
        {
            if (CanCast)
            {
                w.Cast(position);
                //Console.WriteLine("public if CanCast W cast");
                lastTimeCast = Game.Time;
            }
        }

        public void Cast(AIHeroClient target)
        {
            if (target == null) return;

            Cast(target.Position);
            //Console.WriteLine("target null, cast");
        }

        public void Switch()
        {
            if (CanSwitch)
                w.Cast();
            //Console.WriteLine("if CanSwitch, cast");
        }

        public List<AIBaseClient> GetShadows()
        {
            var resultList = new List<AIBaseClient>();

            foreach (var objAiBase in
                    ObjectManager.Get<AIBaseClient>()
                        .Where(x => x.IsVisible && x.IsAlly && x.Name == "Shadow" && !x.IsDead))
                // ObjectManager.Get<Obj_AI_Base>().Where(obj => obj.SkinName.ToLowerInvariant().Equals("Shadow") && !obj.IsDead))
                resultList.Add(objAiBase);
            return resultList;
        }

        public void Combo()
        {
            var shadows = GetShadows();

            if (!shadows.Any()
                || !q.UseOnCombo && !e.UseOnCombo
                || !q.IsReady() && !e.IsReady())
                //Console.WriteLine("shadow combo 1");
                return;

            foreach (var objAiBase in shadows)
            {
                if ((q.UseOnCombo && !q.IsReady() || !q.UseOnCombo)
                    && (e.UseOnCombo && !e.IsReady() || !e.UseOnCombo))
                    break;

                if (q.UseOnCombo && q.IsReady())
                {
                    var target = TargetSelector.GetTarget(
                        q.Range,
                        q.DamageType,
                        true,
                        null,
                        objAiBase.Position);

                    if (target != null)
                    {
                        var predictionInput = new PredictionInput();
                        predictionInput.Range = q.Range;
                        predictionInput.RangeCheckFrom = objAiBase.Position;
                        predictionInput.From = objAiBase.Position;
                        predictionInput.Delay = q.Delay;
                        predictionInput.Speed = q.Speed;
                        predictionInput.Unit = target;
                        predictionInput.Type = SkillshotType.SkillshotLine;
                        predictionInput.Collision = false;

                        var predictionOutput = Prediction.GetPrediction(predictionInput);

                        if (predictionOutput.Hitchance >= HitChance.Medium)
                            q.Cast(predictionOutput.CastPosition);
                        //Console.WriteLine("combo shadow 2 q");
                    }
                }

                if (e.UseOnCombo && e.IsReady())
                {
                    var target = TargetSelector.GetTarget(e.Range, e.DamageType, true, null, objAiBase.Position);

                    if (target != null)
                        e.Cast();
                    //Console.WriteLine("combo shadow use e 3");
                }
            }
        }

        public void Harass()
        {
            var shadows = GetShadows();

            if (!shadows.Any()
                || !q.UseOnHarass && !e.UseOnHarass
                || !q.IsReady() && !e.IsReady())
                return;

            var blackList = zedMenu.GetBlockList(BlockListType.Harass);

            foreach (var objAiBase in shadows)
            {
                if ((q.UseOnHarass && !q.IsReady() || !q.UseOnHarass)
                    && (e.UseOnHarass && !e.IsReady() || !e.UseOnHarass))
                    break;

                if (q.UseOnHarass && q.IsReady())
                {
                    var target = TargetSelector.GetTarget(
                        q.Range,
                        q.DamageType,
                        true,
                        blackList,
                        objAiBase.Position);

                    if (target != null)
                    {
                        var predictionInput = new PredictionInput();
                        predictionInput.Range = q.Range;
                        predictionInput.RangeCheckFrom = objAiBase.Position;
                        predictionInput.From = objAiBase.Position;
                        predictionInput.Delay = q.Delay;
                        predictionInput.Speed = q.Speed;
                        predictionInput.Unit = target;
                        predictionInput.Type = SkillshotType.SkillshotLine;
                        predictionInput.Collision = false;

                        var predictionOutput = Prediction.GetPrediction(predictionInput);

                        if (predictionOutput.Hitchance >= HitChance.Medium)
                            q.Cast(predictionOutput.CastPosition);
                        //Console.WriteLine("shadow haras q 1");
                    }
                }

                if (e.UseOnHarass && e.IsReady())
                {
                    var target = TargetSelector.GetTarget(e.Range, e.DamageType, true, blackList, objAiBase.Position);

                    if (target != null)
                        e.Cast();
                    //Console.WriteLine("shadow haras e 2");
                }
            }
        }

        public void LaneClear(ActionQueue actionQueue, ActionQueueList laneClearQueue)
        {
            var shadow = GetShadows().FirstOrDefault();

            if (!energy.ReadyToLaneClear || shadow == null) return;

            if (e.UseOnLaneClear && e.IsReady())
            {
                var extendedWillHit = MinionManager.GetMinions(shadow.Position, e.Range).Count();
                var shortenWillHit = MinionManager.GetMinions(e.Range).Count;
                var param = zedMenu.GetParamSlider("koreanzed.laneclearmenu.useeif");

                if (extendedWillHit >= param || shortenWillHit >= param)
                {
                    actionQueue.EnqueueAction(
                        laneClearQueue,
                        () => true,
                        () => e.Cast(),
                        () => !e.IsReady());
                    //Console.WriteLine("shadow lc e 1");
                    return;
                }
            }

            if (q.UseOnLaneClear && q.IsReady())
            {
                var extendedWillHit = 0;
                var extendedFarmLocation = Vector3.Zero;
                foreach (var objAiBase in MinionManager.GetMinions(shadow.Position, q.Range))
                {
                    var colisionList = q.GetCollision(
                        shadow.Position.To2D(),
                        new List<Vector2> { objAiBase.Position.To2D() },
                        w.Delay);

                    if (colisionList.Count > extendedWillHit)
                    {
                        extendedFarmLocation =
                            colisionList.OrderByDescending(c => c.Distance(shadow.Position)).FirstOrDefault().Position;
                        extendedWillHit = colisionList.Count;
                    }
                }

                var shortenFarmLocation = q.GetLineFarmLocation(MinionManager.GetMinions(q.Range));

                var shortenWillHit = shortenFarmLocation.MinionsHit;
                var param = zedMenu.GetParamSlider("koreanzed.laneclearmenu.useqif");

                if (CanCast && shadow.Position != Vector3.Zero && extendedWillHit >= param)
                {
                    actionQueue.EnqueueAction(
                        laneClearQueue,
                        () => CanCast,
                        () => Cast(shadow.Position),
                        () => !CanCast);
                    //Console.WriteLine("shadow lc w 2");
                    actionQueue.EnqueueAction(
                        laneClearQueue,
                        () => w.Instance.ToggleState != 0,
                        () => q.Cast(extendedFarmLocation),
                        () => !q.IsReady());
                    //Console.WriteLine("shadow lc q 3");
                }
                else if (shortenWillHit >= param)
                {
                    actionQueue.EnqueueAction(
                        laneClearQueue,
                        () => q.IsReady(),
                        () => q.Cast(shortenFarmLocation.Position),
                        () => !q.IsReady());
                    //Console.WriteLine("shadow lc q 4");
                }
            }
        }
    }
}