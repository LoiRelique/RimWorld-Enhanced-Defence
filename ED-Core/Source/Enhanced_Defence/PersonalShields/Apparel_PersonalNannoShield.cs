﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

namespace Enhanced_Defence.PersonalShields
{
    public class Apparel_PersonalNannoShield : Apparel
    {
        private float MinDrawSize;
        private float MaxDrawSize;
        private const float MaxDamagedJitterDist = 0.05f;
        private const int JitterDurationTicks = 8;
        private bool isRotating;
        private float energy;
        private int ticksToReset = -1;
        private int lastAbsorbDamageTick = -9999;
        private Vector3 impactAngleVect;
        private int lastKeepDisplayTick = -9999;
        private Material BubbleMat = MaterialPool.MatFrom("Other/ShieldBubble", ShaderDatabase.Transparent);
        private int StartingTicksToReset = 3200;
        private float EnergyOnReset = 0.2f;
        private float EnergyLossPerDamage;
        private int KeepDisplayingTicks = 1000;
        private SoundDef SoundAbsorbDamage = SoundDef.Named("PersonalShieldAbsorbDamage");
        private SoundDef SoundBreak = SoundDef.Named("PersonalShieldBroken");
        private SoundDef SoundReset = SoundDef.Named("PersonalShieldReset");

        private int tickFlag = 0;
        public void GetParametersFromXml()
        {
            Enhanced_Defence.PersonalShields.ThingDef_PersonalNannoShield param = this.def as Enhanced_Defence.PersonalShields.ThingDef_PersonalNannoShield;

            MinDrawSize = param.minDrawSize;
            MaxDrawSize = param.maxDrawSize;
            isRotating = param.isRotating;
            EnergyLossPerDamage = param.energyLossPerDamage;
            if (param.bubbleGraphicPath != null)
            {
                BubbleMat = MaterialPool.MatFrom(param.bubbleGraphicPath, ShaderDatabase.Transparent);
            }
            if (param.soundAbsorb != null)
            {
                SoundAbsorbDamage = SoundDef.Named(param.soundAbsorb);
            }
            if (param.soundBreak != null)
            {
                SoundBreak = SoundDef.Named(param.soundBreak);
            }
            if (param.soundReset != null)
            {
                SoundReset = SoundDef.Named(param.soundReset);
            }
        }
        private float EnergyMax
        {
            get
            {
                return this.GetStatValue(StatDefOf.PersonalShieldEnergyMax, true);
            }
        }
        private float EnergyGainPerTick
        {
            get
            {
                return this.GetStatValue(StatDefOf.PersonalShieldRechargeRate, true) / 60f;
            }
        }
        public float Energy
        {
            get
            {
                return this.energy;
            }
        }
        public ShieldState ShieldState
        {
            get
            {
                if (this.ticksToReset > 0)
                {
                    return ShieldState.Resetting;
                }
                return ShieldState.Active;
            }
        }
        private bool ShouldDisplay
        {
            get
            {
                return !this.wearer.Dead && !this.wearer.Downed && (!this.wearer.IsPrisonerOfColony || (this.wearer.BrokenStateDef != null && this.wearer.BrokenStateDef == BrokenStateDefOf.Psychotic)) && ((this.wearer.playerController != null && this.wearer.playerController.Drafted) || this.wearer.Faction.HostileTo(Faction.OfColony) || Find.TickManager.TicksGame < this.lastKeepDisplayTick + this.KeepDisplayingTicks);
            }
        }
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.LookValue<float>(ref this.energy, "energy", 0f, false);
            Scribe_Values.LookValue<int>(ref this.ticksToReset, "ticksToReset", -1, false);
            Scribe_Values.LookValue<int>(ref this.lastKeepDisplayTick, "lastKeepDisplayTick", 0, false);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                GetParametersFromXml();
            }
        }
        public override void Tick()
        {
            if (tickFlag == 0)
            {
                GetParametersFromXml();
                tickFlag = 1;
            }
            base.Tick();
            if (this.wearer == null)
            {
                this.energy = 0f;
                return;
            }
            if (this.ShieldState == ShieldState.Resetting)
            {
                this.ticksToReset--;
                if (this.ticksToReset <= 0)
                {
                    this.Reset();
                }
            }
            else
            {
                if (this.ShieldState == ShieldState.Active)
                {
                    this.energy += this.EnergyGainPerTick;
                    if (this.energy > this.EnergyMax)
                    {
                        this.energy = this.EnergyMax;
                    }
                }
            }
        }
        public override bool CheckPreAbsorbDamage(DamageInfo dinfo)
        {
            if (this.ShieldState == ShieldState.Active && ((dinfo.Instigator != null && !dinfo.Instigator.Position.AdjacentTo8Way(this.wearer.Position)) || dinfo.Def.isExplosive))
            {
                this.energy -= (float)dinfo.Amount * this.EnergyLossPerDamage;
                if (dinfo.Def == DamageDefOf.EMP)
                {
                    this.energy = -1f;
                }
                if (this.energy < 0f)
                {
                    this.Break();
                }
                else
                {
                    this.AbsorbedDamage(dinfo);
                }
                return true;
            }
            return false;
        }
        public void KeepDisplaying()
        {
            this.lastKeepDisplayTick = Find.TickManager.TicksGame;
        }
        private void AbsorbedDamage(DamageInfo dinfo)
        {
            SoundAbsorbDamage.PlayOneShot(this.wearer.Position);
            this.impactAngleVect = Vector3Utility.HorizontalVectorFromAngle(dinfo.Angle);
            Vector3 loc = this.wearer.TrueCenter() + this.impactAngleVect.RotatedBy(180f) * 0.5f;
            float num = Mathf.Min(10f, 2f + (float)dinfo.Amount / 10f);
            MoteThrower.ThrowStatic(loc, ThingDefOf.Mote_ExplosionFlash, num);
            int num2 = (int)num;
            for (int i = 0; i < num2; i++)
            {
                MoteThrower.ThrowDustPuff(loc, Rand.Range(0.8f, 1.2f));
            }
            this.lastAbsorbDamageTick = Find.TickManager.TicksGame;
            this.KeepDisplaying();
        }
        private void Break()
        {
            SoundBreak.PlayOneShot(this.wearer.Position);
            MoteThrower.ThrowStatic(this.wearer.TrueCenter(), ThingDefOf.Mote_ExplosionFlash, 12f);
            for (int i = 0; i < 6; i++)
            {
                Vector3 loc = this.wearer.TrueCenter() + Vector3Utility.HorizontalVectorFromAngle((float)Rand.Range(0, 360)) * Rand.Range(0.3f, 0.6f);
                MoteThrower.ThrowDustPuff(loc, Rand.Range(0.8f, 1.2f));
            }
            this.energy = 0f;
            this.ticksToReset = this.StartingTicksToReset;
        }
        private void Reset()
        {
            SoundReset.PlayOneShot(this.wearer.Position);
            MoteThrower.ThrowLightningGlow(this.wearer.TrueCenter(), 3f);
            this.ticksToReset = -1;
            this.energy = this.EnergyOnReset;
        }
        public override void DrawWornExtras()
        {
            if (this.ShieldState == ShieldState.Active && this.ShouldDisplay)
            {
                float num = Mathf.Lerp(MinDrawSize, MaxDrawSize, this.energy);
                Vector3 vector = this.wearer.drawer.DrawPos;
                vector.y = Altitudes.AltitudeFor(AltitudeLayer.MoteOverhead);
                int num2 = Find.TickManager.TicksGame - this.lastAbsorbDamageTick;
                if (num2 < 8)
                {
                    float num3 = (float)(8 - num2) / 8f * 0.05f;
                    vector += this.impactAngleVect * num3;
                    num -= num3;
                }
                float angle;
                if (isRotating)
                {
                    angle = (float)Rand.Range(0, 360);
                }
                else
                {
                    angle = 0;
                }
                Vector3 s = new Vector3(num, 1f, num);
                Matrix4x4 matrix = default(Matrix4x4);
                matrix.SetTRS(vector, Quaternion.AngleAxis(angle, Vector3.up), s);
                Graphics.DrawMesh(MeshPool.plane10, matrix, BubbleMat, 0);
            }
        }
        internal class Gizmo_PersonalShieldStatus : Gizmo
        {
            //Links
            public Enhanced_Defence.PersonalShields.Apparel_PersonalNannoShield shield;

            //Constants
            private static readonly Texture2D FullTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.2f, 0.2f, 0.24f));
            private static readonly Texture2D EmptyTex = SolidColorMaterials.NewSolidColorTexture(Color.clear);

            //Properties
            public override float Width { get { return 140; } }



            public override GizmoResult GizmoOnGUI(UnityEngine.Vector2 topLeft)
            {
                Rect overRect = new Rect(topLeft.x, topLeft.y, Width, Height);
                Widgets.DrawWindow(overRect);

                Rect inRect = overRect.ContractedBy(6);

                //Item label
                {
                    Rect textRect = inRect;
                    textRect.height = overRect.height / 2;
                    Text.Font = GameFont.Tiny;
                    Widgets.Label(textRect, shield.LabelCap);
                }

                //Bar
                {
                    Rect barRect = inRect;
                    barRect.yMin = overRect.y + overRect.height / 2f;
                    float ePct = shield.Energy / Mathf.Max(1f, shield.GetStatValue(StatDefOf.PersonalShieldEnergyMax));
                    Widgets.FillableBar(barRect, ePct, FullTex, EmptyTex, false);
                    Text.Font = GameFont.Small;
                    Text.Anchor = TextAnchor.MiddleCenter;
                    Widgets.Label(barRect, (shield.Energy * 100).ToString("F0") + " / " + (shield.GetStatValue(StatDefOf.PersonalShieldEnergyMax) * 100).ToString("F0"));
                    Text.Anchor = TextAnchor.UpperLeft;
                }

                return new GizmoResult(GizmoState.Clear);
            }
        }
        public override IEnumerable<Gizmo> GetWornGizmos()
        {
            Enhanced_Defence.PersonalShields.Apparel_PersonalNannoShield.Gizmo_PersonalShieldStatus opt1 = new Enhanced_Defence.PersonalShields.Apparel_PersonalNannoShield.Gizmo_PersonalShieldStatus();
            opt1.shield = this;
            yield return opt1;
        }
        public override bool AllowVerbCast(IntVec3 root, TargetInfo targ)
        {
            return true;
        }
    }
}