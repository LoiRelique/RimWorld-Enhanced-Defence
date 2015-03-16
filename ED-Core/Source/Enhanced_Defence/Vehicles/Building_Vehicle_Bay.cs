﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using UnityEngine;

namespace Enhanced_Defence.Vehicles
{
    public class Building_Vehicle_Bay : Building
    {

        private static Texture2D UI_ACTIVATE_GATE;


        public override void SpawnSetup()
        {
            base.SpawnSetup();

            UI_ACTIVATE_GATE = ContentFinder<Texture2D>.Get("UI/nuke", true);
        }

        #region Commands

        public override IEnumerable<Gizmo> GetGizmos()
        {
            //Add the stock Gizmoes
            foreach (var g in base.GetGizmos())
            {
                yield return g;
            }

            if (true)
            {
                Command_Action act = new Command_Action();
                //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                act.action = () => this.SpawnVehicle();
                act.icon = UI_ACTIVATE_GATE;
                act.defaultLabel = "Spawn Vehicle";
                act.defaultDesc = "Spawn Vehicle";
                act.activateSound = SoundDef.Named("Click");
                //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                //act.groupKey = 689736;
                yield return act;
            }
        }

        public void SpawnVehicle()
        {

        }

        #endregion



    }
}
