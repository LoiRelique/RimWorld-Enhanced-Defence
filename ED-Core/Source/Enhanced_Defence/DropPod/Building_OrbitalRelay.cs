﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using UnityEngine;
using RimWorld;

namespace Enhanced_Defence.DropPod
{
    class Building_OrbitalRelay : Building
    {
        CompPowerTrader power;

        private static Texture2D UI_ADD_RESOURCES;
        private static Texture2D UI_ADD_COLONIST;
        private static Texture2D UI_DROPPOD;

        public bool DropPodDeepStrike;
        public bool DropPodAddUnits;
        public bool DropPodAddResources;

        public int DropPodAddUnitRadius;

        //private static List<List<Thing>> listOfThingLists = new List<List<Thing>>();
        private static List<Thing> listOfThings = new List<Thing>();

        //Dummy override
        public override void PostMake()
        {

            base.PostMake();
        }

        public override void SpawnSetup()
        {
            base.SpawnSetup();

            UI_ADD_RESOURCES = ContentFinder<Texture2D>.Get("UI/ADD_RESOURCES", true);
            UI_ADD_COLONIST = ContentFinder<Texture2D>.Get("UI/ADD_COLONIST", true);
            UI_DROPPOD = ContentFinder<Texture2D>.Get("UI/DEEP_STRIKE", true);

            this.power = base.GetComp<CompPowerTrader>();
            if (def is OrbitalRelayThingDef)
            {
                //Read in variables from the custom MyThingDef
                DropPodDeepStrike = ((Enhanced_Defence.DropPod.OrbitalRelayThingDef)def).DropPodDeepStrike;
                DropPodAddUnits = ((Enhanced_Defence.DropPod.OrbitalRelayThingDef)def).DropPodAddUnits;
                DropPodAddResources = ((Enhanced_Defence.DropPod.OrbitalRelayThingDef)def).DropPodAddResources;
                DropPodAddUnitRadius = ((Enhanced_Defence.DropPod.OrbitalRelayThingDef)def).DropPodAddUnitRadius;
            }
            else
            {
                Log.Error("OrbitalRelay definition not of type \"OrbitalRelayThingDef\"");
            }

        }

        public override IEnumerable<Command> GetCommands()
        {
            IList<Command> CommandList = new List<Command>();
            IEnumerable<Command> baseCommands = base.GetCommands();

            if (baseCommands != null)
            {
                CommandList = baseCommands.ToList();
            }

            if (this.DropPodAddResources)
            {
                //Upgrading
                Command_Action command_Action_InstallShield = new Command_Action();

                command_Action_InstallShield.defaultLabel = "Add Resources";

                command_Action_InstallShield.icon = UI_ADD_RESOURCES;
                command_Action_InstallShield.defaultDesc = "Add Resources";

                command_Action_InstallShield.activateSound = SoundDef.Named("Click");
                command_Action_InstallShield.action = new Action(this.AddResources);

                CommandList.Add(command_Action_InstallShield);
            }

            if (this.DropPodAddUnits)
            {
                //Upgrading
                Command_Action command_Action_InstallShield = new Command_Action();

                command_Action_InstallShield.defaultLabel = "Add Colonist";

                command_Action_InstallShield.icon = UI_ADD_COLONIST;
                command_Action_InstallShield.defaultDesc = "Add Colonist";

                command_Action_InstallShield.activateSound = SoundDef.Named("Click");
                command_Action_InstallShield.action = new Action(this.AddColonist);

                CommandList.Add(command_Action_InstallShield);
            }

            if (this.DropPodDeepStrike)
            {
                //Upgrading
                Command_Action command_Action_InstallShield = new Command_Action();

                command_Action_InstallShield.defaultLabel = "DeepStrike";

                command_Action_InstallShield.icon = UI_DROPPOD;
                command_Action_InstallShield.defaultDesc = "DeepStrike";

                command_Action_InstallShield.activateSound = SoundDef.Named("Click");
                command_Action_InstallShield.action = new Action(this.DeepStrike);

                CommandList.Add(command_Action_InstallShield);
            }


            return CommandList.AsEnumerable<Command>();
        }

        public void DeepStrike()
        {
            //List<Thing> thingList = new List<Thing>();
            //thingList.Add( ThingMaker.MakeThing(ThingDef.Named("MealNutrientPaste"),(ThingDef) null));
            //Building_OrbitalRelay.listOfThingLists.Add(thingList);

            //DropPodUtility.DropThingGroupsNear(this.Position, Building_OrbitalRelay.listOfThingLists);
            //Building_OrbitalRelay.listOfThingLists.Clear();

            List<List<Thing>> listOfListOfThings = new List<List<Thing>>();

            foreach (Thing currentThing in Building_OrbitalRelay.listOfThings)
            {
                List<Thing> newList = new List<Thing>();
                newList.Add(currentThing);

                listOfListOfThings.Add(newList);
            }

            DropPodUtility.DropThingGroupsNear(this.Position, listOfListOfThings);

            Building_OrbitalRelay.listOfThings.Clear();
        }

        public void AddResources()
        {
            if (power.PowerOn)
            {
                Thing foundThing = Enhanced_Defence.Utilities.Utilities.FindThingsInAutoLoader(this);

                if (foundThing != null)
                {
                    if (foundThing.SpawnedInWorld)
                    {
                        List<Thing> thingList = new List<Thing>();
                        //thingList.Add(foundThing);
                        Building_OrbitalRelay.listOfThings.Add(foundThing);
                        foundThing.DeSpawn();

                        //Building_OrbitalRelay.listOfThingLists.Add(thingList);


                        //Recursively Call to get Everything
                        this.AddResources();
                    }
                }
            }
            else
            {
                Messages.Message("Insufficient Power to add Resources");
            }
        }

        public void AddColonist()
        {
            if (power.PowerOn)
            {
                //Log.Message("CLick AddColonist");
                IEnumerable<Pawn> closePawns = Enhanced_Defence.Utilities.Utilities.findPawns(this.Position, this.DropPodAddUnitRadius);

                if (closePawns != null)
                {
                    foreach (Pawn currentPawn in closePawns)
                    {
                        if (currentPawn.SpawnedInWorld)
                        {
                            Building_OrbitalRelay.listOfThings.Add(currentPawn);
                            currentPawn.DeSpawn();


                            /*
                            List<Thing> thingList = new List<Thing>();
                            thingList.Add(currentPawn);
                            Building_OrbitalRelay.listOfThingLists.Add(thingList);
                            currentPawn.DeSpawn();*/
                        }
                    }
                }
            }
            else
            {
                Messages.Message("Insufficient Power to add Colonist");
            }
        }

        //Saving game
        public override void ExposeData()
        {
            base.ExposeData();


            //Scribe_Deep.LookDeep(ref listOfThingLists, "listOfThingLists");

            Scribe_Values.LookValue<int>(ref DropPodAddUnitRadius, "DropPodAddUnitRadius");
            Scribe_Values.LookValue<bool>(ref DropPodDeepStrike, "DropPodDeepStrike");
            Scribe_Values.LookValue<bool>(ref DropPodAddUnits, "DropPodAddUnits");
            Scribe_Values.LookValue<bool>(ref DropPodAddResources, "DropPodAddResources");
                
            //Scribe_Collections.LookList<Thing>(ref listOfThingLists, "listOfThingLists", LookMode.Deep, (object)null);
            Scribe_Collections.LookList<Thing>(ref listOfThings, "listOfThings", LookMode.Deep, (object)null);

        }
    }
}
